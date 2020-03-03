Imports System.IO
Imports System.ServiceModel

Public Class MainView
    Dim Utility As core.AppTools
    Private Lines As List(Of Line) = Nothing
    Private CurrentLine As Line
    Private LoadingPlan As Boolean
	Private Property ClientAccess As SchedulerClient
	Private _Cfg As ClientCfg

	Public Sub New()
        ' This call is required by the designer.
        CurrentLine = New Line
        InitializeComponent()

        Utility = New core.AppTools
        Lines = New List(Of Line)

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles LineTree.AfterSelect
        LineTree.Enabled = False
        cmdReadPlan.Enabled = False
        UpdateLineSelection()
        LineTree.Enabled = True
        cmdReadPlan.Enabled = True
        setPlanView()
    End Sub
	Private Sub MainView_Load(sender As Object, e As EventArgs) Handles MyBase.Load



		ConnectToService()
		RetreshLineData()
#If DEBUG Then
		Button1.Visible = True
		Button2.Visible = True
		Button3.Visible = True
		Button4.Visible = True
#End If
	End Sub

#Region "Clicks"
	'Private EditList As List(Of PlanItem)
	Private Sub Button4_Click(sender As Object, e As EventArgs)
        PlandataSource.DataSource = New List(Of PlanItem)
    End Sub

    Private Sub cmdReadPlan_Click(sender As Object, e As EventArgs) Handles cmdReadPlan.Click
        'read plan through service
        getThePlan()
    End Sub

    Private Sub btn_Click(sender As Object, e As EventArgs)
        PasteDataToGrid()
    End Sub

    Private Sub cmdCopyRowToClipboard_Click()
        'ByVal sender As System.Object,
        'ByVal e As System.EventArgs) Handles cmdCopyRowToClipboard.Click

        If dgvEdit.DataSource IsNot Nothing Then
            ' Set data from current row of DataGridView
            Clipboard.SetText(
               String.Join(","c,
                  Array.ConvertAll(
                  (
                     From cell As DataGridViewCell In
                     dgvEdit.Rows(dgvEdit.CurrentRow.Index).Cells.Cast(Of DataGridViewCell)()
                     Select cell.Value).ToArray, Function(o) o.ToString)))

            ' Assuming the data just set is there we now split by comma as we just sent the 
            ' data comma delimited we now split by comma into a string array then for displaying
            ' the data back we place each element into a StringBuilder object.
            Dim ReturningData As String() = Clipboard.GetText.Split(",".ToCharArray)
            Dim sb As New System.Text.StringBuilder
            For Each item In ReturningData
                sb.AppendLine(item)
            Next

            MessageBox.Show(sb.ToString)
        End If
    End Sub

    Private Sub cmdRun_Click()
        'Dim pARTS As New List(Of PlanItem)

        'ByVal sender As System.Object,
        'ByVal e As System.EventArgs) Handles cmdRun.Click
        '  dgvEdit.DataSource = Nothing
        Try
            Dim ClipboardData As IDataObject = Clipboard.GetDataObject()
            Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
            If Not ClipboardData Is Nothing Then
                If (ClipboardData.GetDataPresent(DataFormats.CommaSeparatedValue)) Then

                    Dim ClipboardStream As New IO.StreamReader(
                       CType(ClipboardData.GetData(DataFormats.CommaSeparatedValue), IO.Stream))

                    Dim FormattedData As String = ""
                    Dim Table As New DataTable With {.TableName = "ExcelData"}

                    While (ClipboardStream.Peek() > 0)
                        Dim SingleRowData As Array
                        Dim LoopCounter As Integer = 0

                        FormattedData = ClipboardStream.ReadLine()

                        SingleRowData = FormattedData.Split(",".ToCharArray)

                        If Table.Columns.Count <= 0 Then
                            For LoopCounter = 0 To SingleRowData.GetUpperBound(0)
                                Table.Columns.Add()
                            Next
                            LoopCounter = 0
                        End If

                        Dim rowNew As DataRow
                        rowNew = Table.NewRow()

                        Dim Pn As String = ""
                        Dim Qty As String = "0"
                        Dim Desc As String = ""
                        Dim CustOrderId As String = ""

                        For LoopCounter = 0 To SingleRowData.GetUpperBound(0)
                            rowNew(LoopCounter) = SingleRowData.GetValue(LoopCounter)
                            Select Case LoopCounter
                                Case 0 : Pn = CType(SingleRowData.GetValue(LoopCounter), String)
                                Case 1 : Qty = CType(SingleRowData.GetValue(LoopCounter), String)
                                Case 2 : Desc = CType(SingleRowData.GetValue(LoopCounter), String)
                                Case 3 : CustOrderId = CType(SingleRowData.GetValue(LoopCounter), String)
                            End Select
                        Next

                        If IsNumeric(Qty) AndAlso Pn.Length > 4 Then
                            Dim P As New PlanItem()
                            P.PartNumber = Pn
                            P.QTY = CInt(Qty)
                            P.Desc = Desc
                            P.CreationDate = Now
                            P.DueDate = Now
                            P.Chk = "*"
                            P.CustOrderId = CustOrderId
                            Litms.Add(P)
                        End If

                        LoopCounter = 0

                        ' Table.Rows.Add(rowNew)

                        rowNew = Nothing
                    End While

                    ClipboardStream.Close()
                    ' dgvEdit.DataSource = Table
                Else
                    MessageBox.Show("Clipboard data does not seem to be copied from Excel!")
                End If
            Else
                MessageBox.Show("Clipboard is empty!")
            End If
        Catch exp As Exception
            MessageBox.Show(exp.Message)
        End Try

        'If pARTS.Count > 0 Then
        '    Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
        '    Litms.AddRange(pARTS)
        PlandataSource.ResetBindings(False)
        'End If

    End Sub

    Private Sub cmdSendPlan_Click(sender As Object, e As EventArgs) Handles cmdSendPlan.Click
        Try
            If ValidatePlan() Then
                SaveThePlan()
            Else
                Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
                For Each I In Litms
                    If I.Chk <> "OK" Then
                        '   I.Chk = "PN?"
                    End If
                Next
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RetreshLineData()
    End Sub


    Private Sub cmdUp_Click(sender As Object, e As EventArgs) Handles cmdUp.Click
        MovePlanItem(True)
    End Sub
    Private Sub cmdDown_Click(sender As Object, e As EventArgs) Handles cmdDown.Click
        MovePlanItem(False)
    End Sub
#End Region

#Region "data grid"
    Private Sub dgvEdit_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEdit.CellValueChanged
        If e.ColumnIndex > 3 Then Exit Sub
        If LoadingPlan Then Exit Sub
        Try
            dgvEdit.Rows(e.RowIndex).Cells(4).Value = "*"
            dgvEdit.Rows(e.RowIndex).Cells(4).Style.BackColor = System.Drawing.Color.Yellow
        Catch ex As Exception

        End Try
    End Sub

	Private Sub DgvHistory_Resize(sender As Object, e As EventArgs) Handles DgvHistory.Resize
		DgvHistory.Columns(0).Width = CInt(DgvHistory.Width * 0.17)
		DgvHistory.Columns(1).Width = CInt(DgvHistory.Width * 0.17)
		DgvHistory.Columns(2).Width = CInt(DgvHistory.Width * 0.12)
		DgvHistory.Columns(3).Width = CInt(DgvHistory.Width * 0.13)
		DgvHistory.Columns(4).Width = CInt(DgvHistory.Width * 0.07)
		DgvHistory.Columns(5).Width = CInt(DgvHistory.Width * 0.07)
		DgvHistory.Columns(6).Width = CInt(DgvHistory.Width * 0.13)

	End Sub

	Private Sub dgv_Resize(sender As Object, e As EventArgs) Handles dgv.Resize
		dgv.Columns(0).Width = CInt(dgv.Width * 0.2)
		dgv.Columns(1).Width = CInt(dgv.Width * 0.2)
		dgv.Columns(2).Width = CInt(dgv.Width * 0.08)
		dgv.Columns(3).Width = CInt(dgv.Width * 0.08)
		dgv.Columns(4).Width = CInt(dgv.Width * 0.1)
		dgv.Columns(5).Width = CInt(dgv.Width * 0.1)
		dgv.Columns(6).Width = CInt(dgv.Width * 0.2)

	End Sub

    Private Sub dgvEdit_Resize(sender As Object, e As EventArgs) Handles dgvEdit.Resize

        Call SetPlanView

    End Sub

    Private Sub setPlanView()
        If CurrentLine Is Nothing Then Exit Sub
        If CurrentLine.CustomerOderIdRequired Then
            dgvEdit.Columns(0).Width = CInt(dgvEdit.Width * 0.3) 'part Number
            dgvEdit.Columns(1).Width = CInt(dgvEdit.Width * 0.07) 'Qty
            dgvEdit.Columns(2).Width = CInt(dgvEdit.Width * 0.2) 'ShipDate
            dgvEdit.Columns(3).Width = CInt(dgvEdit.Width * 0.07) 'Truck
            dgvEdit.Columns(4).Width = CInt(dgvEdit.Width * 0.09) 'Chk
            dgvEdit.Columns(5).Visible = True
            dgvEdit.Columns(5).Width = CInt(dgvEdit.Width * 0.2) 'CustorderId
        Else
            dgvEdit.Columns(0).Width = CInt(dgvEdit.Width * 0.3) 'part Number
            dgvEdit.Columns(1).Width = CInt(dgvEdit.Width * 0.07) 'Qty
            dgvEdit.Columns(2).Width = CInt(dgvEdit.Width * 0.3) 'ShipDate
            dgvEdit.Columns(3).Width = CInt(dgvEdit.Width * 0.1) 'Truck
            dgvEdit.Columns(4).Width = CInt(dgvEdit.Width * 0.1) 'Chk
            dgvEdit.Columns(5).Visible = False
            dgvEdit.Columns(5).Width = CInt(dgvEdit.Width * 0.0) 'CustorderId
        End If
    End Sub


    Private Sub dgvEdit_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEdit.SelectionChanged
        If Not LoadingPlan Then
            RefreshRowColors()
        End If
    End Sub

    Private Sub dgvEdit_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgvEdit.UserDeletingRow
        If dgvEdit.SelectedRows IsNot Nothing Then
            Dim Itm = DirectCast(dgvEdit.SelectedRows(0).DataBoundItem, PlanItem)
            If Itm.OrderId > 0 Then
                Itm.Status = PlanStatus.Removed
                e.Row.Cells(4).Value = "X"
                e.Row.Cells(4).Style.BackColor = System.Drawing.Color.Yellow
                e.Cancel = True
            ElseIf Itm.OrderId = 0 Then
                ' Dim Lst As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
                'dgvEdit.SelectedRows(0).de


                'PlandataSource.Remove(Itm)
                '       PlandataSource.ResetBindings(False)
            End If
            RefreshRowColors()
        End If
    End Sub
#End Region

    Private Sub getThePlan()
        LineTree.Enabled = False
        cmdReadPlan.Enabled = False
        LoadPlanData()
        LineTree.Enabled = True
        cmdReadPlan.Enabled = True
    End Sub

    Private Sub UpdateLineSelection()
        LoadingPlan = True
        Dim Enabelnavigation As Boolean = False
        If LineTree.Nodes.Count > 0 AndAlso LineTree.SelectedNode IsNot Nothing Then
            Dim S As TreeNode = LineTree.SelectedNode
            If S.Parent IsNot Nothing Then
                lblLineName.Text = String.Format("({0} {1}) Selected For Edit", S.Parent.Text, S.Text)
                Enabelnavigation = True
                CurrentLine = (From x In Lines Where x.Id = CInt(S.Tag)).FirstOrDefault
            Else
                lblLineName.Text = "Select Line For Schedule Edit"
                CurrentLine = Nothing ' New Line()
            End If
        End If
        Dim PlanList = New List(Of PlanItem)
        PlandataSource.DataSource = New List(Of PlanItem)
        ScheduleDataSource.DataSource = New List(Of PlanItem)
        cmdUp.Enabled = Enabelnavigation
        cmdDown.Enabled = Enabelnavigation
        BtnApproveEdits.Enabled = Enabelnavigation
        cmdReadPlan.Enabled = Enabelnavigation
        LoadPlanData()
        LoadingPlan = False
    End Sub

    Private Sub LoadPlanData()
        LoadingPlan = True
        If CurrentLine IsNot Nothing AndAlso CurrentLine.Id > 0 Then
			Dim GetPlanReq As New GetPlanRequest(CurrentLine)
			GetPlanReq.IncludeHistory = True
			Dim R = ClientAccess.GetPlan(GetPlanReq)
            If R.Result > 0 Then
                Dim PlanList = R.PlanData.OrderBy(Function(x) x.Position).ToList()
                PlandataSource.DataSource = (From x In PlanList Where x.Status = PlanStatus.Planed).ToList
				ScheduleDataSource.DataSource = (From x In PlanList Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended).ToList
				CompleteAndRemovedDataSource.DataSource = (From x In PlanList Where x.Status = PlanStatus.Removed OrElse x.Status = PlanStatus.Complete).ToList
				RefreshRowColors()
			Else
                MessageBox.Show(Me, R.ResultString, "Service ERROR", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
            End If

        End If
        LoadingPlan = False
    End Sub

    Private Sub RetreshLineData()
        Dim LastCustId As Integer = 0
        Dim CustomerNode As TreeNode = Nothing

        Try
            LineTree.Nodes.Clear()
            UpdateLineSelection()
            Dim Line_Data As GetLinesResponse = ClientAccess.GetLines()
            If Line_Data.Result > 0 Then

                Lines = Line_Data.Lines
                If Lines IsNot Nothing Then
                    'Customer level
                    'Line Level
                    For Each line In Lines
                        If line.CustomerId <> LastCustId Then
                            CustomerNode = New TreeNode(line.CustomerName)
                            CustomerNode.ImageIndex = line.CustomerId
                            LineTree.Nodes.Add(CustomerNode)
                            CustomerNode.Tag = line.CustomerId
                            LastCustId = line.CustomerId
                        End If
                        Dim LineNode As New TreeNode(line.Name)
                        LineNode.Tag = line.Id
                        CustomerNode.Nodes.Add(LineNode)
                    Next
                End If
            Else
                MessageBox.Show(Me, Line_Data.ResultString, "Service ERROR", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub ConnectToService()
		Dim cfgt = New CfgTool()
		_Cfg = cfgt.Read
		Dim arguments As String() = Environment.GetCommandLineArgs()
		If arguments IsNot Nothing AndAlso arguments.Length > 0 Then
			If arguments.Length > 1 Then
				Try
					Dim Parts() = arguments(1).Split(",")
					If Parts.Length = 2 AndAlso Utility.CountCharacter(Parts(0), ".") = 3 AndAlso IsNumeric(Parts(1)) Then
						_Cfg.ServiceAddress = Trim(Parts(0))
						_Cfg.ServicePort = CInt(Parts(1))
					End If
				Catch ex As Exception

				End Try
			End If
		End If

		Dim binding As New BasicHttpBinding()
		Dim Address As New EndpointAddress(_Cfg.ServiceUrl)
		ClientAccess = New SchedulerClient(binding, Address)
		binding.MaxReceivedMessageSize = Int32.MaxValue
		ClientAccess.Open()
    End Sub

    Private Sub MovePlanItem(Up As Boolean)
		Dim Itm As PlanItem = GetSelectedPlannedRowItem()

		If Itm IsNot Nothing Then

			Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
			Dim ReIndex As Boolean = False
			If Up Then
				Dim Idx = dgvEdit.SelectedRows(0).Index

				If Idx > 0 Then
					Litms.Insert(Idx - 1, Itm)
					Litms.RemoveAt(Idx + 1)
					dgvEdit.Rows(Idx - 1).Selected = True
					ReIndex = True
				End If
			Else
				Dim Idx = dgvEdit.SelectedRows(0).Index
				If Idx < Litms.Count - 1 Then
					If Idx + 1 = Litms.Count - 1 Then
						Litms.Add(Itm)
						Litms.RemoveAt(Idx)
						dgvEdit.Rows(Idx + 1).Selected = True
					Else
						Litms.Insert(Idx + 1, Itm)
						dgvEdit.Rows(Idx + 1).Selected = True
						Litms.RemoveAt(Idx)
					End If
					ReIndex = True
				End If
			End If

			If ReIndex Then
				Dim Ts As Long = Utility.GetUnitxTimeStamp
				For I = 0 To Litms.Count - 1
					Litms(I).Position = Ts + I
					Litms(I).Chk = "*"
				Next
			End If

		End If
		RefreshRowColors()
	End Sub


	Private Function GetSelectedPlannedRowItem() As PlanItem
		If dgvEdit.SelectedRows IsNot Nothing AndAlso dgvEdit.SelectedRows.Count = 1 Then
			If dgvEdit.SelectedRows(0).DataBoundItem IsNot Nothing AndAlso dgvEdit.SelectedRows(0).DataBoundItem.GetType.Name = "PlanItem" Then
				Return DirectCast(dgvEdit.SelectedRows(0).DataBoundItem, PlanItem)
			End If
		End If
		Return Nothing
	End Function

	Private Function GetSelectedScheduledRowItem() As PlanItem
		If dgv.SelectedRows IsNot Nothing AndAlso dgv.SelectedRows.Count = 1 Then
			If dgv.SelectedRows(0).DataBoundItem IsNot Nothing AndAlso dgv.SelectedRows(0).DataBoundItem.GetType.Name = "PlanItem" Then
				Return DirectCast(dgv.SelectedRows(0).DataBoundItem, PlanItem)
			End If
		End If
		Return Nothing
	End Function

	Private Function GetPlanAtRow(R As Integer) As PlanItem
		If dgvEdit.Rows(R).DataBoundItem IsNot Nothing AndAlso dgvEdit.Rows(R).DataBoundItem.GetType.Name = "PlanItem" Then
			Return DirectCast(dgvEdit.Rows(R).DataBoundItem, PlanItem)
		End If
		Return Nothing
	End Function

	Private Function GetScheduledAtRow(R As Integer) As PlanItem
		If dgv.Rows(R).DataBoundItem IsNot Nothing AndAlso dgv.Rows(R).DataBoundItem.GetType.Name = "PlanItem" Then
			Return DirectCast(dgv.Rows(R).DataBoundItem, PlanItem)
		End If
		Return Nothing
	End Function

	Private Sub SaveThePlan()
        If CurrentLine Is Nothing Then
            MessageBox.Show(Me, "No line selected cannot save plan.", "User ERROR", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
            Return
        End If
        Dim Ts As Long = Utility.GetUnitxTimeStamp
        Dim ListToSave As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))

        For i = 0 To ListToSave.Count - 1
            ListToSave(i).Position = Ts + i
        Next

        Dim PR As New SavePlanRequest()
        With PR
            .LineData = CurrentLine
            .PlanData = ListToSave
            .UserId = 0
            .LastLoadTime = Now
        End With
        Dim SaveResult = ClientAccess.SavePlan(PR)
        If SaveResult.Result > 0 Then
            getThePlan()
            Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
            For Each I In Litms
                I.Chk = "Done"
            Next
        Else
            MessageBox.Show(Me, SaveResult.ResultString, "Service ERROR", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub RefreshRowColors()
        '  cmdSendPlan.Enabled = False
        Dim ValidPns As Boolean = True
        Try
            For Each R As DataGridViewRow In dgvEdit.Rows
                Dim Itm = GetPlanAtRow(R.Index)
                If dgvEdit.Rows.Count = 0 Then
                    ValidPns = False
                    Exit For
                End If

                If Itm Is Nothing Then
                    Exit For
                End If

                Select Case Itm.Status
                    Case PlanStatus.Unknown
                        ' R.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                        dgvEdit.Item(0, R.Index).ToolTipText = "New Order " & Itm.Desc
                    Case PlanStatus.Planed
                        R.DefaultCellStyle.BackColor = System.Drawing.Color.White
                        dgvEdit.Item(0, R.Index).ToolTipText = "DueDate " & Itm.DueDate
                    Case PlanStatus.Removed
                        '  R.DefaultCellStyle.BackColor = System.Drawing.Color.Salmon
                        dgvEdit.Item(0, R.Index).ToolTipText = "This Item Is set to be Deleted"
                    Case Else
                        ' R.DefaultCellStyle.BackColor = System.Drawing.Color.White
                        dgvEdit.Item(0, R.Index).ToolTipText = "DueDate " & Itm.DueDate
                End Select

                Select Case Itm.Chk
                    Case "OK" : R.Cells(4).Style.BackColor = System.Drawing.Color.Lime
                    Case "Done" : R.Cells(4).Style.BackColor = System.Drawing.Color.Lime
                    Case "PN?" : R.Cells(4).Style.BackColor = System.Drawing.Color.Salmon
                    Case "*" : R.Cells(4).Style.BackColor = System.Drawing.Color.Yellow
                    Case "CID?" : R.Cells(4).Style.BackColor = System.Drawing.Color.Salmon
                    Case "X" : R.Cells(4).Style.BackColor = System.Drawing.Color.Yellow
                    Case Else : R.Cells(4).Style.BackColor = System.Drawing.Color.Coral
                End Select

			Next


			For Each R As DataGridViewRow In dgv.Rows
				If R.Cells(6).Value = PlanStatus.Suspended Then
					R.Cells(6).Style.BackColor = System.Drawing.Color.Pink
				Else
					R.Cells(6).Style.BackColor = System.Drawing.Color.White
				End If

			Next


			For Each R As DataGridViewRow In DgvHistory.Rows
				If R.Cells(6).Value = PlanStatus.Removed Then
					R.Cells(6).Style.BackColor = System.Drawing.Color.Salmon

				ElseIf R.Cells(6).Value = PlanStatus.Complete Then
					R.Cells(6).Style.BackColor = System.Drawing.Color.Lime
				Else
					R.Cells(6).Style.BackColor = System.Drawing.Color.White
				End If
			Next

		Catch ex As Exception

        End Try
        ' cmdSendPlan.Enabled = True
    End Sub


    'Private Sub RefreshRowColorsAfterValidation()
    '    Try
    '        For Each R As DataGridViewRow In dgvEdit.Rows
    '            If R.DataBoundItem Is Nothing Then
    '                Exit For
    '            End If
    '            Dim Itm = DirectCast(R.DataBoundItem, PlanItem)
    '            Select Case Itm.Status
    '                Case PlanStatus.Unknown
    '                    R.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
    '                    dgvEdit.Item(0, R.Index).ToolTipText = "New Order"
    '                Case PlanStatus.Planed
    '                    R.DefaultCellStyle.BackColor = System.Drawing.Color.White
    '                    dgvEdit.Item(0, R.Index).ToolTipText = "Planned"
    '                Case PlanStatus.Removed
    '                    R.DefaultCellStyle.BackColor = System.Drawing.Color.Salmon
    '                    dgvEdit.Item(0, R.Index).ToolTipText = "This Item Is set to be Deleted"
    '                Case Else
    '                    R.DefaultCellStyle.BackColor = System.Drawing.Color.White
    '                    dgvEdit.Item(0, R.Index).ToolTipText = Itm.Status.ToString
    '            End Select
    '        Next
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Sub BtnApproveEdits_Click(sender As Object, e As EventArgs) Handles BtnApproveEdits.Click
    '    ValidatePlan()
    'End Sub


    Private Function ValidatePlan() As Boolean
        If CurrentLine Is Nothing Then
            Return False
        End If
        Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
        Dim Vpr As New ValidatePartsRequest(CurrentLine)
        Dim Failed As Boolean = False
        For Each P In Litms
            If P.Status <> PlanStatus.Removed Then
                If CurrentLine.CustomerOderIdRequired Then
                    If Not P.Flags.HasFlag(OrderFlags.RequiresCustomerOrderId) Then
                        P.Flags = P.Flags Or OrderFlags.RequiresCustomerOrderId
                    End If
                End If
                Vpr.Parts.Add(New Part() With {.PN = P.PartNumber, .Valid = False, .Desc = P.Desc})
            End If
        Next

        Vpr.LineData = CurrentLine
        Dim ValidateResult = ClientAccess.ValidatePlanItems(Vpr)
        If ValidateResult.Result > 0 Then
            For Each p In ValidateResult.parts
                If p.Valid Then
                    Dim Prts = From x In Litms Where x.PartNumber = p.PN
                    If Prts IsNot Nothing AndAlso Prts.Count > 0 Then
                        For Each i As PlanItem In Prts
                            If i.OrderId = 0 Then
                                i.Status = PlanStatus.Planed
                            End If
                            i.TargetLineId = CurrentLine.Id
                            i.Desc = p.Desc
                            If p.Id IsNot Nothing Then
                                i.PartId = CInt(p.Id)
                            Else
                                ' Stop
                            End If
                            If i.Flags.HasFlag(OrderFlags.RequiresCustomerOrderId) Then
                                If i.CustOrderId.Length > 2 Then
                                    i.Chk = "OK"
                                Else
                                    i.Chk = "CID?"
                                End If

                            Else
                                i.Chk = "OK"
                            End If

                        Next
                    End If
                Else
                    Dim Prts = From x In Litms Where x.PartNumber = p.PN
                    For Each i As PlanItem In Prts
                        i.Chk = "PN?"
                    Next
                End If
            Next
        Else
            MessageBox.Show(Me, ValidateResult.ResultString, "Service ERROR", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
        End If
        For Each j In Litms
            If Not j.Chk = "OK" Then
                If j.Status <> PlanStatus.Removed Then
                    ' j.Chk = "PN?"
                    Failed = True
                End If

            End If
        Next
        PlandataSource.ResetBindings(False)
        '  RefreshRowColors()

        If Failed Then
            Return False
        End If

        '  cmdSendPlan.Enabled = True
        Return True
    End Function

    Private Sub PasteDataToGrid()
        ' dgvEdit.AllowUserToAddRows = False
        Dim ClipboardData As IDataObject = Clipboard.GetDataObject()
        ' cmdRun.Enabled = False
        If Not ClipboardData Is Nothing Then
            If (ClipboardData.GetDataPresent(DataFormats.CommaSeparatedValue)) Then
                cmdRun_Click()
            End If
        End If
    End Sub

    Private Sub dgvEdit_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEdit.CellContentClick

    End Sub

    Private Sub dgvEdit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dgvEdit.KeyPress
        If e.KeyChar = "" Then 'ctrlV
            PasteDataToGrid()
        End If
    End Sub

    Private Sub dgvEdit_Click(sender As Object, e As EventArgs) Handles dgvEdit.Click

    End Sub

    Private Sub dgvEdit_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvEdit.MouseClick
        If e.Button = MouseButtons.Right Then

        End If
    End Sub



    Private Sub mnuEdit_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles mnuEdit.ItemClicked
        If e.ClickedItem.AccessibilityObject.Name.Contains("Paste") Then
            PasteDataToGrid()
        End If
        mnuEdit.Close()
    End Sub

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvHistory.CellContentClick

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		'Call Skip order

		Dim Sor As New SkipOrderRequest
		If CurrentLine IsNot Nothing Then
			Sor.Lineid = CurrentLine.Id
			ClientAccess.SkipThisorder(Sor)
			LoadPlanData()
		End If


	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		'call suspend order
		Dim Odr = GetSelectedScheduledRowItem()
		If Odr IsNot Nothing Then
			Dim Req = New SuspendOrderRequest
			Req.OrderId = Odr.OrderId
			ClientAccess.SuspendOrder(Req)
			LoadPlanData()
		End If
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		'Call unsuspend order
		Dim Odr = GetSelectedScheduledRowItem()
		If Odr IsNot Nothing Then
			Dim Req = New SuspendOrderRequest
			Req.OrderId = Odr.OrderId
			ClientAccess.UnSuspendOrder(Req)
			LoadPlanData()
		End If
	End Sub

	Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
		Dim Odr = GetSelectedScheduledRowItem()
		If Odr IsNot Nothing Then
			Dim Req = New RemoveOrderRequest
			Req.OrderId = Odr.OrderId
			ClientAccess.RemoveThisorder(Req)
			LoadPlanData()
		End If
	End Sub

	Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

	End Sub

	Private Sub GetPartListPb_Click(sender As Object, e As EventArgs) Handles GetPartListPb.Click
		GetPartListPb.Enabled = False
		Dim Retparts As List(Of Part) = Nothing
		If CurrentLine IsNot Nothing AndAlso CurrentLine.Id > 0 Then
			Dim Rslt As getPartsforLineResponse = ClientAccess.GetpartsForLine(New GetPartsForLineRequest With {.LineData = CurrentLine})
			Using PrtsSelect As New PartSelectDialog With {.PartData = Rslt, .LineData = CurrentLine}
				PrtsSelect.Owner = Me
				Dim dlgRslt As DialogResult = PrtsSelect.ShowDialog
				If dlgRslt = DialogResult.OK Then
					'push these items to the play view
					Retparts = PrtsSelect.SelectedParts
				End If
			End Using
			If Retparts IsNot Nothing AndAlso Retparts.Count > 0 Then
				Dim CurrentPlan As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
				For Each R In Retparts
					CurrentPlan.Add(New PlanItem With {.Desc = R.Desc, .CreationDate = Now, .PartNumber = R.PN, .QTY = R.Qty, .TargetLineId = CurrentLine.Id})
				Next
				PlandataSource.ResetBindings(False)
			End If
		End If
		GetPartListPb.Enabled = True
	End Sub



	Private Sub PullXmlPartAttributeDataNowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PullXmlPartAttributeDataNowToolStripMenuItem.Click
		PullXmlPartAttributeDataNowToolStripMenuItem.Enabled = False
		Try
			ClientAccess.UpdatePartsFromXmlSupportFilesNow(New UpdatePartInfoFromSupportFilesNowRequest)
		Catch ex As Exception

Finally
			PullXmlPartAttributeDataNowToolStripMenuItem.Enabled = True

		End Try



	End Sub
End Class


