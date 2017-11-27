Imports System.IO


Public Class MainView
    Dim Utility As core.AppTools
    Private Lines As List(Of Line) = Nothing
    Private CurrentLine As Line
    Private LoadingPlan As Boolean

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Utility = New core.AppTools
        Lines = New List(Of Line)
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private EditList As List(Of PlanItem)
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        PlandataSource.DataSource = New List(Of PlanItem)
    End Sub

    Private Sub cmdReadPlan_Click(sender As Object, e As EventArgs) Handles cmdReadPlan.Click
        'read plan through service
        getThePlan()
    End Sub


    Private Sub getThePlan()
        LineTree.Enabled = False
        cmdReadPlan.Enabled = False
        LoadPlanData()
        LineTree.Enabled = True
        cmdReadPlan.Enabled = True
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles LineTree.AfterSelect
        LineTree.Enabled = False
        cmdReadPlan.Enabled = False
        UpdateLineSelection()
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
                CurrentLine = (From x In Lines Where x.Id = S.Tag).FirstOrDefault
            Else
                lblLineName.Text = "Select Line For Schedule Edit"
                CurrentLine = New Line()
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
        If CurrentLine IsNot Nothing Then
            Dim GetPlanReq As New GetPlanRequest(CurrentLine)
            Dim R = Root.ClientAccess.GetPlan(GetPlanReq)
            If R.Result > 0 Then
                Dim PlanList = R.PlanData.OrderBy(Function(x) x.Position).ToList()
                PlandataSource.DataSource = (From x In PlanList Where x.Status = PlanStatus.Planed).ToList
                ScheduleDataSource.DataSource = (From x In PlanList Where x.Status = PlanStatus.Scheduled).ToList
                RefreshRowColors()
            Else

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
            Dim Line_Data As GetLinesResponse = Root.ClientAccess.GetLines()
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
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MainView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConnectToService()
        RetreshLineData()
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

    Private Sub MovePlanItem(Up As Boolean)
        Dim Itm As PlanItem = GetSelectedRowItem()

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
                Next
            End If

        End If
        RefreshRowColors()
    End Sub


    Private Function GetSelectedRowItem() As PlanItem
        If dgvEdit.SelectedRows IsNot Nothing AndAlso dgvEdit.SelectedRows.Count = 1 Then
            If dgvEdit.SelectedRows(0).DataBoundItem IsNot Nothing AndAlso dgvEdit.SelectedRows(0).DataBoundItem.GetType.Name = "PlanItem" Then
                Return DirectCast(dgvEdit.SelectedRows(0).DataBoundItem, PlanItem)
            End If
        End If
        Return Nothing
    End Function

    Private Function GetPlanAtRow(R As Integer) As PlanItem
        If dgvEdit.Rows(R).DataBoundItem IsNot Nothing AndAlso dgvEdit.Rows(R).DataBoundItem.GetType.Name = "PlanItem" Then
            Return DirectCast(dgvEdit.SelectedRows(0).DataBoundItem, PlanItem)
        End If
        Return Nothing
    End Function


    Private Sub cmdSendPlan_Click(sender As Object, e As EventArgs) Handles cmdSendPlan.Click
        SaveThePlan()
    End Sub


    Private Sub SaveThePlan()
        Dim Ts As Long = Utility.GetUnitxTimeStamp
        Dim ListToSave As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))

        For i = 0 To ListToSave.Count - 1
            If Not ListToSave(i).Chk Then
                'suppli alarm not all items a validated
                Exit Sub
            End If
            ListToSave(i).Position = Ts + i
        Next

        Dim PR As New SavePlanRequest()
        With PR
            .LineData = CurrentLine
            .PlanData = ListToSave
            .UserId = 0
            .LastLoadTime = Now
        End With
        Dim SaveResult = Root.ClientAccess.SavePlan(PR)
        If SaveResult.Result > 0 Then
            getThePlan()
        Else

        End If
    End Sub


    Private Sub dgvEdit_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles dgvEdit.UserDeletingRow
        If dgvEdit.SelectedRows IsNot Nothing Then
            Dim Itm = DirectCast(dgvEdit.SelectedRows(0).DataBoundItem, PlanItem)
            If Itm.OrderId > 0 Then
                Itm.Status = PlanStatus.Removed
                RefreshRowColors()
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub dgvEdit_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEdit.CellContentClick

    End Sub



    Private Sub RefreshRowColors()
        cmdSendPlan.Enabled = False
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
                        R.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                        dgvEdit.Item(0, R.Index).ToolTipText = "New Order"
                    Case PlanStatus.Planed
                        R.DefaultCellStyle.BackColor = System.Drawing.Color.White
                        dgvEdit.Item(0, R.Index).ToolTipText = "Planned"
                    Case PlanStatus.Removed
                        R.DefaultCellStyle.BackColor = System.Drawing.Color.Salmon
                        dgvEdit.Item(0, R.Index).ToolTipText = "This Item Is set to be Deleted"
                    Case Else
                        R.DefaultCellStyle.BackColor = System.Drawing.Color.White
                        dgvEdit.Item(0, R.Index).ToolTipText = Itm.Status.ToString
                End Select
                If Not Itm.Chk Then
                    ValidPns = False
                End If
            Next

        Catch ex As Exception

        End Try
        cmdSendPlan.Enabled = ValidPns
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




    Private Sub dgvEdit_SelectionChanged(sender As Object, e As EventArgs) Handles dgvEdit.SelectionChanged
        If Not LoadingPlan Then
            RefreshRowColors()
        End If
    End Sub

    Private Sub BtnApproveEdits_Click(sender As Object, e As EventArgs) Handles BtnApproveEdits.Click
        Dim Litms As List(Of PlanItem) = DirectCast(PlandataSource.DataSource, List(Of PlanItem))
        Dim Vpr As New ValidatePartsRequest(CurrentLine)

        For Each P In Litms
            Vpr.Parts.Add(New Part() With {.PN = P.PartNumber, .Valid = P.Chk, .Desc = P.Desc})
        Next

        Vpr.LineData = CurrentLine
        Dim ValidateResult = Root.ClientAccess.ValidatePlanItems(Vpr)

        For Each p In ValidateResult.parts
            If p.Valid Then
                Dim Prts = From x In Litms Where x.PartNumber = p.PN
                If Prts IsNot Nothing AndAlso Prts.Count > 0 Then
                    For Each i In Prts
                        i.Chk = True
                        i.Desc = p.Desc
                    Next
                End If
            End If
        Next

        For Each j In Litms
            If Not j.Chk Then
                Exit Sub
            End If
        Next
        cmdSendPlan.Enabled = True
    End Sub

    Private Sub lblLineName_Click(sender As Object, e As EventArgs) Handles lblLineName.Click

    End Sub


    Private Sub PasetDataToGrid()
        ' dgvEdit.AllowUserToAddRows = False
        Dim ClipboardData As IDataObject = Clipboard.GetDataObject()
        ' cmdRun.Enabled = False
        If Not ClipboardData Is Nothing Then
            If (ClipboardData.GetDataPresent(DataFormats.CommaSeparatedValue)) Then
                cmdRun_Click()
            End If
        End If
    End Sub
    Private Sub cmdRun_Click()
        'ByVal sender As System.Object,
        'ByVal e As System.EventArgs) Handles cmdRun.Click
        '  dgvEdit.DataSource = Nothing
        Try
            Dim ClipboardData As IDataObject = Clipboard.GetDataObject()

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

                        For LoopCounter = 0 To SingleRowData.GetUpperBound(0)
                            rowNew(LoopCounter) = SingleRowData.GetValue(LoopCounter)
                        Next

                        LoopCounter = 0

                        Table.Rows.Add(rowNew)

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

    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn.Click
        PasetDataToGrid()
    End Sub
End Class


