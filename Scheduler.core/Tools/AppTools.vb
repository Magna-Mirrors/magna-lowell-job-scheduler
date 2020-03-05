Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Scheduler.core.Classes

Public Class AppTools
	Public Sub DetectPath(path As String)
		If Not Directory.Exists(path) Then
			Directory.CreateDirectory(path)
		End If
	End Sub

	Public Function PathValid(Path As String) As Boolean
		Try
			Return Directory.Exists(Path)
		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Function FilePresent(Filename As String) As Boolean
		Try
			Return File.Exists(Filename)
		Catch ex As Exception
			Return False
		End Try
	End Function

	''' <summary>
	''' Returns a time stamp with the 1s and 10s place set to zero for indexing
	''' </summary>
	Public Function GetUnitxTimeStamp() As Long
		'using integer division "\" to knock off the 1s and 10s place
		Return (DateTimeOffset.Now.ToUnixTimeMilliseconds() \ 100) * 100
		'Return CLng((DateTime.Now - New DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds * 10000) * 100
	End Function

	Public Function GetOrderPosition(BuildId As String, ByRef mmddyy As String, ByRef hhmm As String) As Integer
		Dim Pos As Integer = 0
		If BuildIdIsValid(BuildId) Then
			Pos = CInt(BuildId.Substring(12, 3))
			mmddyy = BuildId.Substring(0, 6)
			hhmm = BuildId.Substring(7, 4)
		End If
		Return Pos
	End Function

	Public Function BuildIdIsValid(BuildId As String) As Boolean
		Return Regex.IsMatch(BuildId, "[0-9]{6}-[0-9]{4}-[0-9]{3}")
	End Function

	Public Function GetProgramParams() As SvcParams
		Dim Cpath As String = Path.GetDirectoryName(SvcParams.ParamPathAndFile)
		DetectPath(Cpath)
		If Not File.Exists(SvcParams.ParamPathAndFile) Then
			ConfigurationManager(Of SvcParams).Save(SvcParams.getDefaults)
		End If
		Dim Resave As Boolean
		Dim cfg = ConfigurationManager(Of SvcParams).Load()
		If Not IsDate(cfg.UpdatePartInfoTime) Then
			cfg.UpdatePartInfoTime = New Date(2020, 1, 1, 19, 0, 0)
			Resave = True
		End If
		If cfg.TcpPortNumberForWcf = 0 Then
			cfg.TcpPortNumberForWcf = 8045
			Resave = True
		End If
		If cfg.PasswordUpdate Then
			cfg.ApplyPwChange()
			Resave = True
		End If
		If Resave Then
			ConfigurationManager(Of SvcParams).Save(cfg)
			cfg = ConfigurationManager(Of SvcParams).Load()
		End If
		Return cfg
	End Function

	Public Function IsThereANewerFile(Info As AttributeFileLog, ByRef Sz As Integer) As Boolean?
		Dim Fi As New FileInfo(Info.PathAndFile)
		If Fi.Exists Then
			Sz = Fi.Length
			If Fi.LastWriteTime >= Info.Updated OrElse Fi.Length <> Info.Size Then
				Return True
			End If
			Return False
		End If
		Return False
	End Function


	Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
		Return value.Count(Function(c As Char) c = ch)
	End Function


End Class
