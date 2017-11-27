Imports System.IO


Public Class frmSchedule
    Private Sub cmdUp_Click(sender As Object, e As EventArgs) Handles cmdUp.Click

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
    'Dim LastPlanLoadTime As Date
    'Dim Customers As New System.Data.DataSet
    ''   Dim CustomerSS As New clsSnapShot(My.Settings.RawDatabase, My.Settings.CustomerSQL)
    ''E:\_Projects\M\Magna\Lowell\DainaWare\SupportFiles
    'Dim CustomerSS As New clsSnapShot("E:\_Projects\M\Magna\Lowell\DainaWare\SupportFiles\raw.mdb", My.Settings.CustomerSQL)

    'Dim Lines As New System.Data.DataSet
    ''    Dim LinesSS As New clsSnapShot(My.Settings.RawDatabase, My.Settings.LineSQL)
    'Dim LinesSS As New clsSnapShot("E:\_Projects\M\Magna\Lowell\DainaWare\SupportFiles\raw.mdb", My.Settings.LineSQL)
    'Dim Edited As Boolean
    'Dim spot As Integer
    'Dim LockedRow As New DataGridViewCellStyle
    'Dim WarningRow As New DataGridViewCellStyle
    'Private Sub frmSchedule_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    If Edited = True Or Me.dgvEdit.Rows.Count > 2 Then
    '        If MsgBox("You have edits are you sure you wish to exit", vbYesNo + vbQuestion, "Lose On Going Edits") = MsgBoxResult.No Then
    '            e.Cancel = True
    '        End If
    '    End If
    'End Sub


    'Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    Me.cmdSend.Enabled = Not My.Settings.TC_Enabled
    '    Me.cmdSend.Visible = Me.cmdSend.Enabled
    '    With LockedRow
    '        .BackColor = Color.DarkGreen
    '        .ForeColor = Color.White
    '    End With
    '    With WarningRow
    '        .BackColor = Color.Red
    '        .ForeColor = Color.White
    '    End With
    '    LoadProd()
    'End Sub

    'Private Function GetDate(DashDelimitedText As String) As Date
    '    Dim temp As String()
    '    Dim Dash As Char = "-"
    '    temp = DashDelimitedText.Split(Dash)
    '    If temp.GetUpperBound(0) = 2 Then
    '        If temp(0).Length = 6 Then
    '            Dim MonthData As Integer = Integer.Parse(temp(0).Substring(0, 2))
    '            Dim DayData As Integer = Integer.Parse(temp(0).Substring(2, 2))
    '            Dim YearData As Integer = Integer.Parse(temp(0).Substring(4, 2)) + 2000
    '            Dim tempdate As New Date(YearData, MonthData, DayData)
    '            Return tempdate
    '        Else
    '            Return Date.Now.AddDays(1)
    '        End If
    '    Else
    '        Return Date.Now.AddDays(1)
    '    End If
    'End Function

    'Private Function GetSpot(DashDelimitedText As String) As Integer
    '    Dim temp As String()
    '    Dim Dash As Char = "-"
    '    temp = DashDelimitedText.Split(Dash)
    '    If temp.GetUpperBound(0) = 2 Then
    '        Dim thisspot As Integer = spot
    '        If Integer.TryParse(temp(2), thisspot) Then
    '            Return thisspot
    '        Else
    '            Return 0
    '        End If
    '    Else
    '        Return 0
    '    End If
    'End Function

    'Private Function LoadSchedule(ScheduleFolder As String) As Boolean
    '    Dim filename As String = Path.Combine(ScheduleFolder, My.Settings.ScheduleFile)
    '    If File.Exists(filename) Then
    '        Try
    '            Dim ScheduleReader As StreamReader = File.OpenText(filename)
    '            If ScheduleReader.EndOfStream Then
    '                MsgBox("There is no Schedule for this Line at this time", vbOKOnly + vbExclamation, "No Schedule Error")
    '            Else
    '                dgv.Rows.Clear()
    '                Do Until ScheduleReader.EndOfStream
    '                    Dim ScheduleLine As String = ScheduleReader.ReadLine
    '                    Dim Data2Add As String() = ScheduleLine.Split(",")
    '                    If Data2Add.GetUpperBound(0) = 6 Or Data2Add.GetUpperBound(0) = 7 Then
    '                        Dim ThisSpot As Integer = GetSpot(Data2Add(0))
    '                        If ThisSpot > spot Then spot = ThisSpot
    '                        If Val(Data2Add(3)) < Val(Data2Add(2)) Then
    '                            dgv.Rows.Add(Data2Add)
    '                            Dim ThisCell As DataGridViewCell
    '                            For Each ThisCell In dgv.Rows.Item(dgv.Rows.Count - 1).Cells
    '                                ThisCell.Style = LockedRow
    '                            Next
    '                        End If
    '                        If Data2Add(5).StartsWith("RMVD") Then
    '                            dgv.Rows.Add(Data2Add)
    '                            Dim ThisCell As DataGridViewCell
    '                            For Each ThisCell In dgv.Rows.Item(dgv.Rows.Count - 1).Cells
    '                                ThisCell.Style = WarningRow
    '                            Next
    '                        End If
    '                    End If
    '                Loop
    '            End If

    '            ScheduleReader.Close()
    '            Return True
    '        Catch ex As Exception
    '            MsgBox(String.Format("There was an error {0} while trying to load the Schedule file", ex.Message, vbOKOnly + vbExclamation, "Schedule Loading Error"))
    '            Return False
    '        End Try
    '    Else
    '        MsgBox("There is no Schedule for this Line at this time", vbOKOnly + vbExclamation, "No Schedule Error")
    '        Return True
    '    End If
    'End Function

    'Private Function SavePlan(Planpath As String) As Boolean
    '    'TODO: get PlanData-------------------------------------
    '    Dim filename As String = Path.Combine(Planpath, "")
    '    If File.Exists(filename) Then
    '        'check to make sure plan has not been changed while we were working on it
    '        If File.GetLastWriteTime(filename) > LastPlanLoadTime Then
    '            MsgBox("The Plan File has been changed by the tester since you have last read it. You will need to reload the Plan and redo your edits", vbOKOnly + MsgBoxStyle.Exclamation, "Plan has Changed")
    '            Return False
    '        End If
    '    End If
    '    Try
    '        Dim PathWriter As StreamWriter = File.CreateText(filename)
    '        Dim index As Integer
    '        For index = 0 To dgv.Rows.Count - 1
    '            With dgv.Rows.Item(index)
    '                If .Cells("Built").Value = "0" And .Cells("Ordered").Value = "0" Then
    '                    Dim data2Write As String = String.Format("{0},{1},{2},{3},{4},{5},{6}",
    '                                            .Cells("BuildID").Value.ToString,
    '                                            .Cells("PartNumber").Value.ToString,
    '                                            .Cells("QTY").Value.ToString,
    '                                            .Cells("Built").Value.ToString,
    '                                            .Cells("Ordered").Value.ToString,
    '                                            .Cells("Desc").Value.ToString,
    '                                            .Cells("TRK").Value.ToString)
    '                    PathWriter.WriteLine(data2Write)
    '                End If
    '            End With
    '        Next
    '        PathWriter.Close()
    '        LastPlanLoadTime = Now
    '        Edited = False
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(String.Format("There was an error {0} while trying to write the Plan file", ex.Message), vbOKOnly + vbExclamation, "No Build Plan Error")
    '        Return False
    '    End Try
    'End Function

    'Private Function LoadPlan(PlanPath As String) As Boolean
    '    Dim filename As String = Path.Combine(PlanPath, My.Settings.PlanningFile)
    '    If File.Exists(filename) Then
    '        Try
    '            Dim ScheduleReader As StreamReader = File.OpenText(filename)
    '            If ScheduleReader.EndOfStream Then
    '                MsgBox("There is no Plan for this Line at this time", vbOKOnly + vbExclamation, "No Build Plan Error")
    '            Else
    '                Do Until ScheduleReader.EndOfStream
    '                    Dim ScheduleLine As String = ScheduleReader.ReadLine
    '                    Dim Data2Add As String() = ScheduleLine.Split(",")
    '                    If Data2Add.GetUpperBound(0) = 5 Or Data2Add.GetUpperBound(0) = 6 Then
    '                        Dim ThisSpot As Integer = GetSpot(Data2Add(0))
    '                        If ThisSpot > spot Then spot = ThisSpot
    '                        dgv.Rows.Add(Data2Add)
    '                        dgv.Rows.Item(dgv.RowCount - 1).Cells("TRK").Value = dgv.Rows.Item(dgv.RowCount - 1).Cells("Date").Value
    '                        dgv.Rows.Item(dgv.RowCount - 1).Cells("Date").Value = vbNullString
    '                    End If
    '                Loop
    '            End If
    '            LastPlanLoadTime = Now
    '            ScheduleReader.Close()
    '            Return True
    '        Catch ex As Exception
    '            MsgBox(String.Format("There was an error {0} while trying to load the Plan file", ex.Message), vbOKOnly + vbExclamation, "No Build Plan Error")
    '            Return False
    '        End Try
    '    Else
    '        MsgBox("There is no Build Plan for this Line at this time", vbOKOnly + vbExclamation, "No Build Plan Error")
    '        LastPlanLoadTime = Now
    '        Return True
    '    End If
    'End Function


    'Private Sub LoadProd()
    '    Customers = CustomerSS.Fill
    '    If Not CustomerSS.DataPresent Then
    '        MsgBox(CustomerSS.dataError)
    '        End
    '    End If
    '    Lines = LinesSS.Fill
    '    If Not LinesSS.DataPresent Then
    '        MsgBox(LinesSS.dataError)
    '        End
    '    End If
    '    PopulateCustomers()
    '    Dim CheckBoxCol As New DataGridViewCheckBoxColumn
    '    CheckBoxCol.Name = "TRK"
    '    CheckBoxCol.HeaderText = "TRK"
    '    Dim JD As New DataGridViewComboBoxColumn
    '    JD.Name = "JD"
    '    JD.HeaderText = "SHP"

    '    With Me.dgvEdit
    '        .Columns.Add("PartNumber", "Part Number")
    '        .Columns.Add("QTY", "QTY")
    '        .Columns.Add("Desc", "Description")
    '        .Columns.Add("Ship", "Ship")
    '        .Columns.Add(CheckBoxCol)
    '        .Columns.Add("CHK", "CHK")
    '        .Columns.Item("PartNumber").Width = Me.dgvEdit.Width * 0.23
    '        .Columns.Item("QTY").Width = Me.dgvEdit.Width * 0.07
    '        .Columns.Item("Desc").Width = Me.dgvEdit.Width * 0.35
    '        .Columns.Item("Ship").Width = Me.dgvEdit.Width * 0.15
    '        .Columns.Item("CHK").Width = Me.dgvEdit.Width * 0.07
    '        .Columns.Item("CHK").ReadOnly = True
    '        .Columns.Item("TRK").Width = Me.dgvEdit.Width * 0.07

    '    End With
    '    With Me.dgv
    '        .Columns.Add("BuildID", "ID")
    '        .Columns.Add("PartNumber", "Part Number")
    '        .Columns.Add("QTY", "Need")
    '        .Columns.Add("Built", "Blt")
    '        .Columns.Add("Ordered", "Ord")
    '        .Columns.Add("Desc", "Description")
    '        .Columns.Add("Date", "Date")
    '        .Columns.Add("TRK", "TRK")
    '        .Columns.Item("PartNumber").Width = Me.dgv.Width * 0.18
    '        .Columns.Item("BuildID").Width = Me.dgv.Width * 0.16
    '        .Columns.Item("QTY").Width = Me.dgv.Width * 0.05
    '        .Columns.Item("Built").Width = Me.dgv.Width * 0.05
    '        .Columns.Item("Ordered").Width = Me.dgv.Width * 0.05
    '        .Columns.Item("Desc").Width = Me.dgv.Width * 0.45
    '        .Columns.Item("Date").Width = 0
    '        .Columns.Item("TRK").Width = Me.dgv.Width * 0.06
    '    End With
    'End Sub

    'Private Sub PopulateCustomers()
    '    Dim tblCustomers As System.Data.DataTable = Customers.Tables(0)
    '    Dim eachCustomer As System.Data.DataRow
    '    Dim tblLines As System.Data.DataTable = Lines.Tables(0)
    '    Dim eachLIne As System.Data.DataRow

    '    For Each eachCustomer In tblCustomers.Rows
    '        ' Dim CustomPic As Image = System.Drawing.Image.FromFile(eachCustomer.Item("PicPath").ToString)
    '        ' ilCustomers.Images.Add("C" + CStr(eachCustomer.Item("ID")), CustomPic)
    '    Next
    '    '  Me.TreeView1.ImageList = ilCustomers

    '    Dim FirstCustomer As Boolean = True
    '    For Each eachCustomer In tblCustomers.Rows
    '        Me.TreeView1.Nodes.Add("C" + CStr(eachCustomer.Item("ID").ToString), eachCustomer.Item("CustomerName").ToString, "C" + CStr(eachCustomer.Item("ID").ToString))
    '        For Each eachLIne In tblLines.Rows
    '            If eachLIne.Item("CustomerID") = eachCustomer.Item("ID") Then
    '                Me.TreeView1.Nodes.Item("C" + CStr(eachCustomer.Item("ID").ToString)).Nodes.Add("L" + CStr(eachLIne.Item("ID").ToString), eachLIne.Item("Name").ToString, "C" + CStr(eachCustomer.Item("ID").ToString))
    '            End If
    '        Next
    '    Next
    'End Sub

    'Private Sub TreeView1_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
    '    Select Case Me.TreeView1.SelectedNode.Level
    '        Case 0 ' customer
    '            Me.lblLineName.Text = "Select Line to Edit"
    '            dgv.Visible = False
    '            dgvEdit.Visible = False
    '        Case 1
    '            dgv.Visible = True
    '            dgvEdit.Visible = True
    '            Me.lblLineName.Text = Me.TreeView1.SelectedNode.Text
    '            Edited = False
    '            ReadSchedule()
    '    End Select

    'End Sub


    'Private Function ConditionDataRow(TabDelimitedString As String) As String()
    '    Dim rowData As String() = Split(TabDelimitedString + vbTab + "?" + vbTab + "?" + vbTab + "?" + vbTab + "?", vbTab)
    '    Dim column As Integer
    '    Dim returndata(0 To 3) As String
    '    For column = 0 To 3
    '        returndata(column) = rowData(column).ToUpper.Trim
    '        Dim TestDate As Date
    '        If Not Date.TryParse(returndata(3), TestDate) Then
    '            TestDate = Date.Now.AddDays(1)
    '        End If
    '        returndata(3) = String.Format("{0:M/d/yy}", TestDate)
    '    Next
    '    Return returndata
    'End Function


    'Private Sub Paste()
    '    Dim Data2Add As String() = GetClipBoardData()
    '    If IsArray(Data2Add) Then
    '        If Data2Add.GetUpperBound(0) >= 0 Then
    '            Dim index As Integer
    '            For index = 0 To Data2Add.GetUpperBound(0)
    '                dgvEdit.Rows.Add(ConditionDataRow(Data2Add(index)))
    '            Next
    '        End If
    '    End If
    'End Sub

    'Private Sub Insert()
    '    Dim Data2Add As String() = GetClipBoardData()
    '    If IsArray(Data2Add) Then
    '        If Data2Add.GetUpperBound(0) >= 0 Then
    '            Dim index As Integer
    '            For index = 0 To Data2Add.GetUpperBound(0)
    '                dgvEdit.Rows.Insert(0, ConditionDataRow(Data2Add(index)))
    '            Next
    '        End If
    '    End If
    'End Sub

    'Private Sub AppendPasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AppendPasteToolStripMenuItem.Click
    '    Paste()

    'End Sub

    'Private Function GetClipBoardData() As String()
    '    Dim ClipboardData As String = Clipboard.GetText.Trim
    '    Dim DataArray As String() = Split(ClipboardData, vbCrLf)
    '    Return DataArray
    'End Function

    'Private Sub TreeView1_BeforeSelect(sender As Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeSelect
    '    If Me.dgvEdit.Rows.Count > 2 Then
    '        MsgBox("You are partnumbers that are being edited.  Please delete or upload them before changing lines", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Unsaved Data")
    '        e.Cancel = True
    '    End If
    'End Sub

    'Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
    '    dgvEdit.Rows.Clear()

    'End Sub

    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
    '    Approve()
    'End Sub

    'Private Function CheckALLOK() As Boolean
    '    With dgvEdit
    '        If .Rows.Count >= 2 Then
    '            Dim rowindex As Integer
    '            For rowindex = 0 To .Rows.Count - 2
    '                If .Rows.Item(rowindex).Cells("CHK").Value <> "OK" Then
    '                    Return False
    '                End If
    '            Next
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End With
    'End Function

    'Public Sub Approve()
    '    'approve 
    '    If Me.TreeView1.SelectedNode.Name.StartsWith("L") Then
    '        Dim LineID As Integer = Val(Me.TreeView1.SelectedNode.Name.Substring(1, Me.TreeView1.SelectedNode.Name.Length - 1))
    '        If Lines.Tables(0).Rows.Contains(LineID) Then
    '            Dim thisLine As DataRow = Lines.Tables(0).Rows.Find(LineID)
    '            Dim WcfFileName As String = thisLine.Item(3).ToString
    '            Dim SQLCall As String = thisLine.Item(4).ToString
    '            Dim WCF As New clsSnapShot(WcfFileName, SQLCall)
    '            Dim Parts As DataSet = WCF.Fill
    '            If WCF.DataPresent Then
    '                Dim rowindex As Integer
    '                For rowindex = 0 To dgvEdit.Rows.Count - 2
    '                    If dgvEdit.Rows.Item(rowindex).Cells("QTY").Value <> Nothing Then
    '                        If Val(dgvEdit.Rows.Item(rowindex).Cells("QTY").Value.ToString) > 0 Then
    '                            Dim TestDate As Date
    '                            If Date.TryParse(dgvEdit.Rows.Item(rowindex).Cells("Ship").Value.ToString, TestDate) Then
    '                                dgvEdit.Rows.Item(rowindex).Cells("Ship").Value = String.Format("{0:M/d/yy}", TestDate)
    '                                Dim CandidatePartNumber As String = dgvEdit.Rows.Item(rowindex).Cells("PartNumber").Value
    '                                If Parts.Tables(0).Rows.Contains(CandidatePartNumber) Then
    '                                    If dgvEdit.Rows.Item(rowindex).Cells("Desc").Value.ToString.Length > 1 Then
    '                                        dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "OK"
    '                                    Else
    '                                        dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "DESC"
    '                                    End If
    '                                Else
    '                                    dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "PN#"
    '                                End If
    '                            Else
    '                                dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "SHP"
    '                            End If
    '                        Else
    '                            dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "QTY"
    '                        End If
    '                    Else
    '                        dgvEdit.Rows.Item(rowindex).Cells("CHK").Value = "QTY"
    '                    End If
    '                Next
    '            Else
    '                MsgBox("Could not find Part Data for this line")
    '            End If
    '        End If
    '    End If

    'End Sub



    'Private Sub cmdMove_Click(sender As System.Object, e As System.EventArgs) Handles cmdMove.Click
    '    Approve()
    '    If CheckALLOK() Then
    '        Dim rowindex As Integer
    '        For rowindex = 0 To dgvEdit.Rows.Count - 2
    '            spot = spot + 1
    '            If spot > 999 Then spot = 1
    '            Dim BuildID As String = String.Format("{0:MMddyy}-{1:hhmm}-{2:000}", Date.Parse(dgvEdit.Rows.Item(rowindex).Cells("Ship").Value), Now, spot)
    '            Do While rowindex < dgvEdit.Rows.Count - 2 And True 'TODO: Handle Combine 'My.Settings.Combine
    '                If dgvEdit.Rows.Item(rowindex).Cells("PartNumber").Value = dgvEdit.Rows.Item(rowindex + 1).Cells("PartNumber").Value Then
    '                    dgvEdit.Rows.Item(rowindex).Cells("QTY").Value = CStr(Val(dgvEdit.Rows.Item(rowindex).Cells("QTY").Value.ToString) + Val(dgvEdit.Rows.Item(rowindex + 1).Cells("QTY").Value.ToString))
    '                    dgvEdit.Rows.RemoveAt(rowindex + 1)
    '                Else
    '                    Exit Do
    '                End If
    '            Loop
    '            If rowindex < dgvEdit.Rows.Count - 1 Then
    '                If dgvEdit.Rows(rowindex).Cells("PartNumber").Value <> Nothing Then
    '                    Dim data2add As String() = {BuildID, dgvEdit.Rows.Item(rowindex).Cells("PartNumber").Value.ToString.ToUpper, dgvEdit.Rows.Item(rowindex).Cells("QTY").Value, 0, 0, dgvEdit.Rows.Item(rowindex).Cells("Desc").Value, vbNullString, IIf(dgvEdit.Rows.Item(rowindex).Cells("TRK").Value = True, "T", " ")}
    '                    dgv.Rows.Add(data2add)
    '                End If
    '            End If
    '        Next
    '        dgvEdit.Rows.Clear()
    '        Edited = True
    '    End If
    'End Sub

    'Private Sub cmdEdit_Click(sender As System.Object, e As System.EventArgs) Handles cmdEdit.Click
    '    Dim rowindex As Integer
    '    For rowindex = 0 To dgv.Rows.Count - 1
    '        If dgv.Rows.Item(rowindex).Cells("Built").Value.ToString = "0" And dgv.Rows.Item(rowindex).Cells("Ordered").Value.ToString = "0" Then
    '            Dim testDate As Date = GetDate(dgv.Rows.Item(rowindex).Cells("BuildID").Value)
    '            Dim data2add As String() = {dgv.Rows.Item(rowindex).Cells("PartNumber").Value, dgv.Rows.Item(rowindex).Cells("QTY").Value, dgv.Rows.Item(rowindex).Cells("Desc").Value, String.Format("{0:M/d/yy}", testDate), CBool(dgv.Rows.Item(rowindex).Cells("TRK").Value = "T")}
    '            dgvEdit.Rows.Add(data2add)
    '            Edited = True
    '        End If
    '    Next
    '    For rowindex = dgv.Rows.Count - 1 To 0 Step -1
    '        If dgv.Rows.Item(rowindex).Cells("Built").Value.ToString = "0" And dgv.Rows.Item(rowindex).Cells("Ordered").Value.ToString = "0" Then
    '            dgv.Rows.RemoveAt(rowindex)
    '            Edited = True
    '        End If
    '    Next
    'End Sub

    'Private Sub OverwritePasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OverwritePasteToolStripMenuItem.Click
    '    dgvEdit.Rows.Clear()
    '    Paste()
    'End Sub

    'Private Sub InsertPasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InsertPasteToolStripMenuItem.Click
    '    Insert()
    'End Sub

    'Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
    '    If Edited = True Then
    '        If MsgBox("You are edited this schedule and not sent it.  Do you wish to clear this schedule?", vbYesNo + vbQuestion + vbDefaultButton2, "Clear Changes?") = vbNo Then
    '            Exit Sub
    '        End If
    '    End If
    '    Dim rowindex As Integer
    '    For rowindex = dgv.Rows.Count - 1 To 0 Step -1
    '        If dgv.Rows.Item(rowindex).Cells("Built").Value.ToString = "0" And dgv.Rows.Item(rowindex).Cells("Ordered").Value.ToString = "0" Then
    '            dgv.Rows.RemoveAt(rowindex)
    '            Edited = True
    '        End If
    '    Next
    'End Sub

    'Private Function ReadSchedule() As Boolean
    '    If Me.TreeView1.SelectedNode.Name.StartsWith("L") Then
    '        Dim LineID As Integer = Val(Me.TreeView1.SelectedNode.Name.Substring(1, Me.TreeView1.SelectedNode.Name.Length - 1))
    '        If Lines.Tables(0).Rows.Contains(LineID) Then
    '            Dim thisLine As DataRow = Lines.Tables(0).Rows.Find(LineID)
    '            Dim PlanPath As String = thisLine.Item(6).ToString
    '            Edited = False
    '            dgv.Rows.Clear()
    '            spot = 0

    '            'This will be handled in the service

    '            Return LoadSchedule(PlanPath) And LoadPlan(PlanPath)
    '        Else
    '            Return False
    '        End If

    '    Else
    '        Return False
    '    End If
    'End Function

    'Private Sub cmdRead_Click(sender As System.Object, e As System.EventArgs) Handles cmdRead.Click
    '    If Edited = True Then
    '        If MsgBox("You are edited this schedule and not sent it.  Do you wish to clear this schedule?", vbYesNo + vbQuestion + vbDefaultButton2, "Clear Changes?") = vbNo Then
    '            Exit Sub
    '        End If
    '    End If
    '    ReadSchedule()
    'End Sub

    'Private Sub cmdSend_Click(sender As System.Object, e As System.EventArgs) Handles cmdSend.Click
    '    If Me.TreeView1.SelectedNode.Name.StartsWith("L") Then
    '        Dim LineID As Integer = Val(Me.TreeView1.SelectedNode.Name.Substring(1, Me.TreeView1.SelectedNode.Name.Length - 1))
    '        If Lines.Tables(0).Rows.Contains(LineID) Then
    '            Dim thisLine As DataRow = Lines.Tables(0).Rows.Find(LineID)
    '            Dim PlanPath As String = thisLine.Item(6).ToString
    '            SavePlan(PlanPath)
    '            Edited = False
    '            Me.TreeView1.SelectedNode = Me.TreeView1.SelectedNode.Parent
    '        End If
    '    End If
    'End Sub

    'Private Sub cmdUp_Click(sender As System.Object, e As System.EventArgs) Handles cmdUp.Click
    '    Try
    '        If dgvEdit.SelectedRows.Count = 0 Then
    '            dgvEdit.CurrentRow.Selected = True
    '        End If
    '        Dim index As Integer
    '        Dim FirstSelectedRow As Integer = dgvEdit.SelectedRows(0).Index
    '        Dim LastSelectedRow As Integer = dgvEdit.SelectedRows(0).Index
    '        For index = 0 To dgvEdit.SelectedRows.Count - 1
    '            If dgvEdit.SelectedRows.Item(index).Index < FirstSelectedRow Then FirstSelectedRow = dgvEdit.SelectedRows.Item(index).Index
    '            If dgvEdit.SelectedRows.Item(index).Index > LastSelectedRow Then LastSelectedRow = dgvEdit.SelectedRows.Item(index).Index
    '        Next
    '        If FirstSelectedRow > 0 Then
    '            With dgvEdit
    '                Dim MoveRow As DataGridViewRow = .Rows.Item(FirstSelectedRow - 1)
    '                .Rows.RemoveAt(FirstSelectedRow - 1)
    '                .Rows.Insert(LastSelectedRow, MoveRow)
    '            End With
    '            'dgvEdit.Rows.RemoveAt(FirstSelectedRow - 1)
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub cmdDown_Click(sender As System.Object, e As System.EventArgs) Handles cmdDown.Click
    '    Try
    '        If dgvEdit.SelectedRows.Count = 0 Then
    '            dgvEdit.CurrentRow.Selected = True
    '        End If
    '        Dim index As Integer
    '        Dim FirstSelectedRow As Integer = dgvEdit.SelectedRows(0).Index
    '        Dim LastSelectedRow As Integer = dgvEdit.SelectedRows(0).Index
    '        For index = 0 To dgvEdit.SelectedRows.Count - 1
    '            If dgvEdit.SelectedRows.Item(index).Index < FirstSelectedRow Then FirstSelectedRow = dgvEdit.SelectedRows.Item(index).Index
    '            If dgvEdit.SelectedRows.Item(index).Index > LastSelectedRow Then LastSelectedRow = dgvEdit.SelectedRows.Item(index).Index
    '        Next
    '        If LastSelectedRow < (dgvEdit.Rows.Count - 2) Then
    '            With dgvEdit
    '                Dim MoveRow As DataGridViewRow = .Rows.Item(LastSelectedRow + 1)
    '                .Rows.RemoveAt(LastSelectedRow + 1)
    '                .Rows.Insert(FirstSelectedRow, MoveRow)
    '            End With
    '            'dgvEdit.Rows.RemoveAt(FirstSelectedRow - 1)
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub frmSchedule_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
    '    'original screen size
    '    '1165 x 762
    'End Sub

    'Private Sub dgvEdit_CellMouseEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEdit.CellMouseEnter
    '    If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
    '        Try
    '            dgvEdit.Item(e.ColumnIndex, e.RowIndex).ToolTipText = dgvEdit.Item(e.ColumnIndex, e.RowIndex).Value.ToString
    '        Catch
    '        End Try
    '    End If
    'End Sub


    'Private Sub dgv_CellMouseEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellMouseEnter
    '    If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
    '        Try
    '            dgv.Item(e.ColumnIndex, e.RowIndex).ToolTipText = dgv.Item(e.ColumnIndex, e.RowIndex).Value.ToString
    '        Catch ex As Exception

    '        End Try

    '    End If
    'End Sub

End Class


