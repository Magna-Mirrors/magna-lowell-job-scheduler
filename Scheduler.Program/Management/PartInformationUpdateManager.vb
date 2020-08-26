Imports System.IO
Imports System.Threading
Imports Scheduler.core
Imports Scheduler.core.XmlHandling


Public Class PartInformationUpdateManager
	Private _IsRunning As Boolean
	Private _LastRan As DateTime
	Private ReadOnly _tools As AppTools
	Private ReadOnly _Cfg As SvcParams
	Private Busy As Boolean
	Private ReadOnly _Db As SqlData
	Private ReadOnly _Log As iLoggingService
	Private _ExecuteNowRequest As Boolean
	Private _ItRan As Boolean


	Public Sub ExecuteNowRequest()
		If Not Busy Then
			_ExecuteNowRequest = True
		End If
	End Sub

	Public Async Function RunAsync(CT As CancellationToken) As Task
		_IsRunning = True
		Dim Sdate As DateTime
		While (Not CT.IsCancellationRequested)
			Await Task.Delay(1000)
			Sdate = New DateTime(Now.Year, Now.Month, Now.Day, _Cfg.UpdatePartInfoTime.Hour, _Cfg.UpdatePartInfoTime.Minute, _Cfg.UpdatePartInfoTime.Second)
			If (Now >= Sdate AndAlso Not _ItRan) OrElse _ExecuteNowRequest AndAlso Not Busy Then
				Try
					Busy = True
					_ItRan = True
					_ExecuteNowRequest = False
					Execute()
				Catch ex As Exception
					_Log.SendAlert(New LogEventArgs("OrderHandling", ex))
				Finally
					Busy = False
				End Try
				_LastRan = Now
			Else
				If _ItRan AndAlso Now < Sdate Then
					_ItRan = False
				End If
			End If
		End While

		_IsRunning = False

	End Function

	Public Sub New(Db As SqlData, Atools As AppTools, MsgSvc As iLoggingService)
		_tools = Atools
		_Cfg = _tools.GetProgramParams
		_Db = Db
		_Log = MsgSvc
		_LastRan = DateTime.MinValue

	End Sub

	Private Sub Execute()
		Dim Prgs As List(Of Part_Program) = _Db.getProgramData()
		If Prgs Is Nothing Then
			_Log.SendAlert(New Scheduler.core.LogEventArgs("no part programs were loaded", "Load part Programs"))
			Exit Sub
		End If
		Dim ProcessPartUpdates As Boolean
		Dim UpToDate As Boolean
		Dim FileList As List(Of AttributeFileLog) = Nothing
		For Each p In Prgs
			UpToDate = True 'set this optimistic
			ProcessPartUpdates = True 'set this optimistic
			If _tools.PathValid(p.AttributeFilePath) Then
				FileList = GetAttributeFileList(p.ProgId, p.AttributeFilePath)
				If FileList IsNot Nothing AndAlso FileList.Count = 4 Then
					For Each f In FileList
						If Not _tools.FilePresent(f.PathAndFile) Then
							_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("Could not Find this support File ({0}) for {1}", f.PathAndFile, p.Name), "Part Attribute Files"))
							ProcessPartUpdates = False
							Exit For
						End If
						Dim sZ As Integer = 0
						If _tools.IsThereANewerFile(f, sZ) Then
							_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("Found newer File ({0}) for {1}", f.PathAndFile, p.Name), "Part Attribute Files"))
							f.Size = sZ
							UpToDate = False
						End If
					Next
				End If
			Else
				ProcessPartUpdates = False
				_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("Invalid file path of ({0}) for {1} detected", p.AttributeFilePath, p.Name), "Part Attribute Files"))
			End If

			If ProcessPartUpdates AndAlso Not UpToDate Then
				Dim XmdFileData As XmlAttributeData = GetAttributeDataFiles(p.AttributeFilePath)
				ProcessNestIdAndColorIdUpdates(XmdFileData, p.ProgId)
				ProcesspartDefinitions(XmdFileData, p.ProgId)

				For Each f In FileList
					_Db.SaveXmlAttributeFileName(f)
				Next

			End If
		Next
	End Sub

	Private Function ProcesspartDefinitions(XmlFileData As XmlAttributeData, Progid As Integer) As Integer
		Dim PartDef As List(Of Part_Definition) = _Db.GetPartDefinitions(Progid)
		Dim Items = (From x In XmlFileData.ItemAttributes.ROW Select x.Item).Distinct
		For Each i In Items
			Dim DB_Part As Part_Definition = (From x In PartDef Where x.PartNumber.ToUpper = i.ToUpper).FirstOrDefault
			If DB_Part Is Nothing Then
				DB_Part = New Part_Definition With {.PartNumber = i.ToUpper, .AddIt = True, .ProgramId = Progid}
				PartDef.Add(DB_Part)
			End If
			Dim xItmData = (From x In XmlFileData.Itemdata.ROW Where x.Item = DB_Part.PartNumber).FirstOrDefault
			Dim xService = xItmData.ExpCartonQty > 0 AndAlso xItmData.RtnCartonQty = 0
			Dim xPPT As Integer = {xItmData.RtnCartonQty, xItmData.ExpCartonQty}.Max
			Dim xNestidx = (From x In XmlFileData.ItemAttributes.ROW Where x.Item = DB_Part.PartNumber AndAlso x.AttributeName.StartsWith("NESTID") Select x.AttributeValue).FirstOrDefault
			Dim xColoridx = (From x In XmlFileData.ItemAttributes.ROW Where x.Item = DB_Part.PartNumber AndAlso x.AttributeName.ToLower.StartsWith("colorid") Select x.AttributeValue).FirstOrDefault
			Dim xCpn = (From x In XmlFileData.ItemExtDesc.ROW Where x.Item = DB_Part.PartNumber AndAlso x.FieldName.ToLower = "cuspar" Select x.FieldValue).FirstOrDefault
			Dim xNote = (From x In XmlFileData.ItemExtDesc.ROW Where x.Item.ToLower = DB_Part.PartNumber.ToLower AndAlso x.FieldName.ToLower = "descr1" Select x.FieldValue).FirstOrDefault

			'Nest Id Change check
			If xNestidx IsNot Nothing AndAlso IsNumeric(xNestidx) Then
				If DB_Part.NestIdx <> CInt(xNestidx) Then
					DB_Part.NestIdx = CInt(xNestidx)
					If Not DB_Part.AddIt Then
						DB_Part.UpdateIt = True
					End If
				End If
			End If

			'Color Id Change check
			If xColoridx IsNot Nothing AndAlso IsNumeric(xColoridx) Then
				If DB_Part.ColIdx <> CInt(xColoridx) Then
					DB_Part.ColIdx = CInt(xColoridx)
					If Not DB_Part.AddIt Then
						DB_Part.UpdateIt = True
					End If
				End If
			End If

			'Cust Pn
			If xCpn IsNot Nothing AndAlso xCpn.Length > 0 Then
				If DB_Part.CPN <> xCpn Then
					DB_Part.CPN = xCpn
					If Not DB_Part.AddIt Then
						DB_Part.UpdateIt = True
					End If
				End If
			End If

			'Service
			If (DB_Part.Service <> xService) Then
				DB_Part.Service = xService
				If Not DB_Part.AddIt Then
					DB_Part.UpdateIt = True
				End If
			End If

			'Note
			If xNote IsNot Nothing AndAlso xNote.Length > 0 Then
				If DB_Part.Note <> xNote Then
					DB_Part.Note = xNote
					If Not DB_Part.AddIt Then
						DB_Part.UpdateIt = True
					End If
				End If
			End If

			'ppt

			If DB_Part.PartsPerTote <> xPPT Then
				DB_Part.PartsPerTote = xPPT
				If Not DB_Part.AddIt Then
					DB_Part.UpdateIt = True
				End If
			End If
		Next

		Dim Updates = (From x In PartDef Where x.UpdateIt = True)
		If Updates IsNot Nothing AndAlso Updates.Count > 0 Then
			_Db.UpdatePartDefinitionItem(Updates.ToArray)
		End If

		Dim Additions = (From x In PartDef Where x.AddIt = True)
		If Additions IsNot Nothing AndAlso Additions.Count > 0 Then
			_Db.AddPartDefinitionItem(Additions.ToArray)
		End If

		Return 1
	End Function

	Private Function ProcessNestIdAndColorIdUpdates(XmlFileData As XmlAttributeData, Progid As Integer) As Integer
		Dim Colors As List(Of Part_Color) = _Db.GetPartColors(Progid)
		Dim NestInfo As List(Of part_Options) = _Db.GetPartOptions(Progid)
		Dim cls = From X In XmlFileData.Attributes.ROW Where LCase(X.AttributeName).StartsWith("colorid")
		Dim Opt = From X In XmlFileData.Attributes.ROW Where LCase(X.AttributeName).StartsWith("nestid")
		Dim rslt As Integer
		Try
			If Colors IsNot Nothing Then
				For Each c In cls
					Dim ColItm = (From y In Colors Where y.ColIdx = c.AttributeValue).FirstOrDefault
					If ColItm IsNot Nothing Then
						If ColItm.Name <> c.AttributeValueDesc Then
							_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("ColorId Name CHanges From {0} to {1} for ColorIdx {2} ProgId {3}) ", ColItm.Name, c.AttributeValueDesc, c.AttributeValue, Progid), "Color Name"))
							ColItm.Name = c.AttributeValueDesc
							ColItm.Changed = True
						End If
					End If
				Next

				For Each O In Opt
					Dim OptItm = (From y In NestInfo Where y.NestIdx = O.AttributeValue).FirstOrDefault
					If OptItm IsNot Nothing Then
						If OptItm.Description <> O.AttributeValueDesc Then
							_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("NestId Name CHanges From {0} to {1} for NestIdx {2} ProgId {3}) ", OptItm.Description, O.AttributeValueDesc, O.AttributeValue, Progid), "Color Name"))
							OptItm.Description = O.AttributeValueDesc
							OptItm.Changed = True
						End If
					End If
				Next
			End If

			If Colors.Any(Function(x) x.Changed = True) Then
				rslt += _Db.UpdateColorItems((From x In Colors Where x.Changed).ToArray)
			End If

			If NestInfo.Any(Function(x) x.Changed = True) Then
				rslt += _Db.UpdatePartOptionItem((From x In NestInfo Where x.Changed).ToArray)
			End If




		Catch ex As Exception
			rslt = 0
		End Try

		Return rslt
	End Function



	''' <summary>
	''' uset to do a pre check on if present
	''' if date newer and is size is deferent 
	''' </summary>
	''' <param name="ID"></param>
	''' <param name="Path"></param>
	''' <returns></returns>
	Private Function GetAttributeFileList(ID As Integer, Path As String) As List(Of AttributeFileLog)
		Dim Lst As New List(Of AttributeFileLog)
		Lst.Add(_Db.GetLastXmlFileInfo(ID, "attributes.xml", Path))
		Lst.Add(_Db.GetLastXmlFileInfo(ID, "itemAttributes.xml", Path))
		Lst.Add(_Db.GetLastXmlFileInfo(ID, "itemData.xml", Path))
		Lst.Add(_Db.GetLastXmlFileInfo(ID, "itemExtDesc.xml", Path))
		Return Lst
	End Function


	''' <summary>
	''' gets all the zml file data into 1 object
	''' </summary>
	''' <param name="PathInfo"></param>
	''' <returns></returns>
	Private Function GetAttributeDataFiles(PathInfo As String) As XmlAttributeData
		Try
			Return New XmlAttributeData With {
		.Attributes = getattributesXML(PathInfo),
		.ItemAttributes = getItemattributesXML(PathInfo),
		.Itemdata = GetItemDataXML(PathInfo),
		.ItemExtDesc = getitemExtDescXML(PathInfo)}
		Catch ex As Exception
			_Log.SendAlert(New Scheduler.core.LogEventArgs(String.Format("Error Getting Attribute Data From ({0}) ", PathInfo), ex))
			Return Nothing

		End Try


	End Function



	Public Function getattributesXML(sPath As String) As attributesROWSET
		Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(attributesROWSET))
		Dim file As New System.IO.StreamReader(Path.Combine(sPath, "attributes.xml"))
		Return CType(reader.Deserialize(file), attributesROWSET)
	End Function

	Public Function getItemattributesXML(spath As String) As ItemattributesROWSET
		Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(ItemattributesROWSET))
		Dim file As New System.IO.StreamReader(Path.Combine(spath, "itemAttributes.xml"))
		Return CType(reader.Deserialize(file), ItemattributesROWSET)
	End Function

	Public Function GetItemDataXML(spath As String) As ItemDataROWSET
		Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(ItemDataROWSET))
		Dim file As New System.IO.StreamReader(Path.Combine(spath, "itemData.xml"))
		Return CType(reader.Deserialize(file), ItemDataROWSET)
	End Function

	Public Function getitemExtDescXML(sPAth As String) As ItemExtDescROWSET
		Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(ItemExtDescROWSET))
		Dim file As New System.IO.StreamReader(Path.Combine(sPAth, "itemExtDesc.xml"))
		Return CType(reader.Deserialize(file), ItemExtDescROWSET)
	End Function


End Class
