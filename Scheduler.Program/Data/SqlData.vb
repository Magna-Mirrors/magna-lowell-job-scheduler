Imports System.Data.SqlClient
Imports Scheduler.core

Public Class SqlData
    Protected ReadOnly _Cfg As SvcParams
    Private ReadOnly LgSvr As iLoggingService

    'TargetLineId,	priority,	PN,	OrderId,	Ordered,	Built,	Qty,	PartId,	Flags,	Desc,	Name,	Status,	PPHPP, DueDate
    Private Const ActiveOrderQuery As String = "SELECT TOP (100) PERCENT Schedule_Order_History.TargetLineId, Schedule_Order_History.Position AS priority, Part_Info.PN, Part_Info.PartId, Schedule_Order_History.ID AS OrderId, 
                      SUM(Schedule_Part_Order_History.Qty) AS Ordered, SUM(Schedule_Part_Production_History.Qty) AS Built, MAX(Schedule_Order_History.Quantity) AS Qty, 
                      Schedule_Order_History.Flags, Part_Info.Desc1 AS [Desc], Part_Info.Name, Schedule_Order_History.Status, Part_Options.PartsPerHourPerPerson AS PPHPP, 
                      Schedule_Order_History.ScheduleDate AS DueDate
                      FROM Schedule_Order_History INNER JOIN
                      Part_Info ON Schedule_Order_History.PartId = Part_Info.PartId INNER JOIN
                      Part_Options ON Part_Info.OptionId = Part_Options.OptionId LEFT OUTER JOIN
                      Schedule_Part_Production_History ON Schedule_Order_History.ID = Schedule_Part_Production_History.OrderId LEFT OUTER JOIN
                      Schedule_Part_Order_History ON Schedule_Order_History.ID = Schedule_Part_Order_History.OrderId
                      WHERE (Schedule_Order_History.Status < 4) {0}
                      GROUP BY Schedule_Order_History.Position, Schedule_Order_History.ID, Schedule_Order_History.TargetLineId, Part_Info.PN, Schedule_Order_History.Status, Part_Info.Desc1, 
                      Part_Info.Name, Schedule_Order_History.Flags, Part_Options.PartsPerHourPerPerson, Schedule_Order_History.ScheduleDate, Part_Info.PartId
                      ORDER BY Schedule_Order_History.TargetLineId, priority"

    'LineId,	CustomerId,	LineName,	LineDefinition,	MaxConcurrentLogins,	WcfFileName,	SelectCmd,	ScheduleFolder,	SchedulerMethod,	WorkBufferMinutes,	CustomerName,	ProgramId,	LH,	RH, ReOrderPercentThreshold, User_Count,Wc
    Private Const LineQuery As String = "SELECT  eqp_Lines.Id AS LineId, eqp_Lines.CustomerId, eqp_Lines.LineName, eqp_Lines.LineDefinition, eqp_Lines.MaxConcurrentLogins, eqp_Lines.WcfFileName, 
                      eqp_Lines.SelectCmd, eqp_Lines.ScheduleFolder, eqp_Lines.SchedulerMethod, eqp_Lines.WorkBufferMinutes, Part_Customers.Name AS CustomerName, 
                      Part_Program_Line_Map.ProgramId, Part_Program_Line_Map.LH, Part_Program_Line_Map.RH, eqp_Lines.ReOrderPercentThreshold, 
                      LineUserCount.user_count, eqp_Lines.Workcell as WC
                      FROM eqp_Lines INNER JOIN
                      Part_Customers ON eqp_Lines.CustomerId = Part_Customers.Id LEFT OUTER JOIN
                      LineUserCount ON eqp_Lines.Id = LineUserCount.LineId LEFT OUTER JOIN
                      Part_Program_Line_Map ON eqp_Lines.Id = Part_Program_Line_Map.LineId
                      {0}
                      ORDER BY CustomerId,LineId"

    'PN,	Desc,	LineId,	PartId
    Private Const GetpartsQuery = "SELECT dbo.Part_Info.PN, dbo.Part_Info.Desc1 AS [Desc], dbo.Part_Program_Line_Map.LineId, dbo.Part_Info.PartId
                      FROM dbo.Part_Info INNER JOIN
                      dbo.Part_Programs ON dbo.Part_Info.ProgId = dbo.Part_Programs.ProgId INNER JOIN
                      dbo.Part_Program_Line_Map ON dbo.Part_Programs.ProgId = dbo.Part_Program_Line_Map.ProgramId
                      Where dbo.Part_Program_Line_Map.LineId = {0} and dbo.Part_Info.PN in({1}) "


    Public Sub New(LgSvr As iLoggingService, Atools As AppTools)
        _Cfg = Atools.GetProgramParams
        Me.LgSvr = LgSvr
    End Sub

    Public Function ValidateParts(PartReq As ValidatePartsRequest) As ValidatePartsResponse
        Dim Rslt As New ValidatePartsResponse
        Dim PartList As New List(Of Part)

        Dim Pry = PartReq.Parts.Select(Function(x) x.PN).ToArray
        PartReq.Parts.Select(Function(x) x.Valid = False)
        Dim Jn = "'" & Join(Pry, "','") & "'"

        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format(GetpartsQuery, PartReq.LineData.Id, Jn), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Pn = CStr(dRead("PN"))
                    Dim Pr = (From x In PartReq.Parts Where x.PN = Pn)
                    If Pr IsNot Nothing Then
                        'update all like parts in PartReq
                        For Each Itm In Pr
                            Itm.Id = CInt(dRead("PartId"))
                            Itm.Valid = True
                            Itm.Desc = CStr(dRead("Desc"))
                        Next
                    End If
                End While
            End Using
        End Using
        Rslt.parts = PartReq.Parts
        Return Rslt
    End Function

    Public Function GetParts(PartReq As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Rslt As New getPartsforLineResponse
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} Where Lineid = {1}", GetpartsQuery, PartReq.LineData.Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim P As New Part
                    P.PN = CStr(dRead("PN"))
                    P.Desc = CStr(dRead("Desc"))
                    P.Id = dRead("PartId")
                    Rslt.parts.Add(P)
                End While
            End Using
        End Using
        Return Rslt
    End Function



    Public Function GetLinesData(SqlLinesOnly As Boolean) As GetLinesResponse
        Dim Rslt As New GetLinesResponse

        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Wc = If(SqlLinesOnly, "(eqp_Lines.SchedulerMethod =2)", "(eqp_Lines.SchedulerMethod > 0)")
            Dim Cmd As New SqlClient.SqlCommand(String.Format(LineQuery, ""), Cn)
            Rslt.Lines.AddRange(ParseOutLines(Cmd))
            'Using dRead As IDataReader = Cmd.ExecuteReader()
            '    While dRead.Read
            '        Dim Itm As New Line
            '        With Itm
            '            'LineId,	CustomerId,	LineName,	LineDefinition,	MaxConcurrentLogins,	WcfFileName,	SelectCmd,	ScheduleFolder,	SchedulerMethod,	WorkBufferMinutes,	CustomerName,	ProgramId,	LH,	RH, ReOrderPercentThreshold, User_count
            '            .Description = dRead("LineDefinition")
            '            .Id = dRead("LineId")
            '            .Name = dRead("LineName")
            '            .SchedulerMethod = If(DBNull.Value.Equals(dRead("SchedulerMethod")), "", dRead("SchedulerMethod")) 'dRead("SchedulerMethod")
            '            .SelectCmd = dRead("SelectCmd")
            '            .WcfFileName = dRead("WcfFileName")
            '            .ScheduleFolder = If(DBNull.Value.Equals(dRead("ScheduleFolder")), "", dRead("ScheduleFolder"))
            '            .CustomerName = If(DBNull.Value.Equals(dRead("CustomerName")), "", dRead("CustomerName"))
            '            .CustomerId = If(DBNull.Value.Equals(dRead("CustomerId")), 0, dRead("CustomerId"))
            '            .ProgramId = If(DBNull.Value.Equals(dRead("ProgramId")), 0, dRead("ProgramId"))
            '            .WorkBufferMinutes = If(DBNull.Value.Equals(dRead("WorkBufferMinutes")), 0, dRead("WorkBufferMinutes"))
            '            .ReOrderPercentThreshold = If(DBNull.Value.Equals(dRead("ReOrderPercentThreshold")), 0.8, dRead("ReOrderPercentThreshold"))
            '            .UserCount = If(DBNull.Value.Equals(dRead("user_count")), 1, dRead("user_count"))
            '            .WC = If(DBNull.Value.Equals(dRead("WC")), "", dRead("WC"))
            '            .QueuedMinutes = 0
            '        End With
            '        Rslt.Lines.Add(Itm)
            '    End While
            'End Using
        End Using
        Return Rslt
    End Function




    Public Function GetLineData(LineId As Integer) As Line
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()

            Dim Cmd As New SqlClient.SqlCommand(String.Format(LineQuery, "Where Id = " & LineId), Cn)
            Return ParseOutLines(Cmd, True).FirstOrDefault()
            '    Using dRead As IDataReader = Cmd.ExecuteReader()
            '        While dRead.Read
            '            Dim Itm As New Line
            '            With Itm
            '                .Description = dRead("LineDefinition")
            '                .Id = dRead("LineId")
            '                .Name = dRead("LineName")
            '                .SchedulerMethod = dRead("SchedulerMethod")
            '                .SelectCmd = dRead("SelectCmd")
            '                .WcfFileName = dRead("WcfFileName")
            '                .ScheduleFolder = dRead("ScheduleFolder")
            '                .CustomerName = If(DBNull.Value.Equals(dRead("CustomerName")), "", dRead("CustomerName"))
            '                .CustomerId = If(DBNull.Value.Equals(dRead("CustomerId")), 0, dRead("CustomerId"))
            '                .ProgramId = If(DBNull.Value.Equals(dRead("ProgramId")), 0, dRead("ProgramId"))
            '                .WorkBufferMinutes = If(DBNull.Value.Equals(dRead("WorkBufferMinutes")), 0, dRead("WorkBufferMinutes"))
            '                .ReOrderPercentThreshold = If(DBNull.Value.Equals(dRead("ReOrderPercentThreshold")), 0.8, dRead("ReOrderPercentThreshold"))
            '                .UserCount = If(DBNull.Value.Equals(dRead("user_count")), 1, dRead("user_count"))
            '                .WC = If(DBNull.Value.Equals(dRead("WC")), "", dRead("WC"))
            '                .QueuedMinutes = 0
            '            End With
            '            Return Itm
            '        End While
            '    End Using
        End Using
        Return Nothing
    End Function

    Private Shared Function ParseOutLines(cmd As SqlCommand, Optional firstOnly As Boolean = False) As IEnumerable(Of Line)
        Dim lines As New List(Of Line)()
        Using dRead As IDataReader = cmd.ExecuteReader()
            While dRead.Read
                Dim Itm As New Line
                With Itm
                    'LineId,	CustomerId,	LineName,	LineDefinition,	MaxConcurrentLogins,	WcfFileName,	SelectCmd,	ScheduleFolder,	SchedulerMethod,	WorkBufferMinutes,	CustomerName,	ProgramId,	LH,	RH, ReOrderPercentThreshold, User_count
                    .Description = CStr(dRead("LineDefinition"))
                    .Id = CInt(dRead("LineId"))
                    .Name = CStr(dRead("LineName"))
                    .SchedulerMethod = If(DBNull.Value.Equals(dRead("SchedulerMethod")), "", dRead("SchedulerMethod")) 'dRead("SchedulerMethod")
                    .SelectCmd = CStr(dRead("SelectCmd"))
                    .WcfFileName = CStr(dRead("WcfFileName"))
                    .ScheduleFolder = If(DBNull.Value.Equals(dRead("ScheduleFolder")), "", dRead("ScheduleFolder"))
                    .CustomerName = If(DBNull.Value.Equals(dRead("CustomerName")), "", dRead("CustomerName"))
                    .CustomerId = If(DBNull.Value.Equals(dRead("CustomerId")), 0, dRead("CustomerId"))
                    .ProgramId = If(DBNull.Value.Equals(dRead("ProgramId")), 0, dRead("ProgramId"))
                    .WorkBufferMinutes = If(DBNull.Value.Equals(dRead("WorkBufferMinutes")), 0, dRead("WorkBufferMinutes"))
                    .ReOrderPercentThreshold = If(DBNull.Value.Equals(dRead("ReOrderPercentThreshold")), 0.8, dRead("ReOrderPercentThreshold"))
                    .UserCount = If(DBNull.Value.Equals(dRead("user_count")), 1, dRead("user_count"))
                    .WC = If(DBNull.Value.Equals(dRead("WC")), "", dRead("WC"))
                    .QueuedMinutes = 0
                End With
                lines.Add(Itm)
                If firstOnly Then
                    Return lines
                End If
            End While
        End Using
        Return lines
    End Function

    Public Function updateOrderPosition(OrderId_Id As Integer, Position As Long) As TransactionResult
        Dim Rslt As New TransactionResult
        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim UpStr As String = "Update Schedule_Order_History Set Position = {0} where Id = {1}"
                Dim Cmd As New SqlClient.SqlCommand(String.Format(UpStr, Position, OrderId_Id), Cn)
                Cn.Close()
                Rslt.Result = Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Update Job Position", ex))
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try
        Return Rslt
    End Function

    Public Function updateJobStatus(Order_Id As Integer, Status As PlanStatus) As TransactionResult
        Dim Rslt As New TransactionResult
        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim UpStr As String = "Update Schedule_Order_History Set Status = {0} where Id = {1}"
                Dim Cmd As New SqlClient.SqlCommand(String.Format(UpStr, Status, Order_Id), Cn)
                Cn.Close()
                Rslt.Result = Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Update Job Status", ex))
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try
        Return Rslt
    End Function


    Public Function UpdatePlanStatus(OrderId As Integer, Status As PlanStatus) As Integer
        Dim RetV As Integer = 0
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Try
                Dim Cmd As New SqlClient.SqlCommand(String.Format("Update Schedule_Order_History set Status = {0} where Id = {1}", Status, OrderId), Cn)
                RetV = Cmd.ExecuteNonQuery()
                Cmd.Dispose()
            Catch ex As Exception

            End Try

        End Using
        Return RetV
    End Function

    Public Function GetActiveOrders(Lineid As Integer) As GetPlanResponse
        Dim Pr As New GetPlanResponse
        Dim Items As New List(Of PlanItem)
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim WC = If(Lineid > 0, String.Format("AND (Schedule_Order_History.TargetLineId = {0})", Lineid), "")
            Dim Cmd As New SqlClient.SqlCommand(String.Format(ActiveOrderQuery, WC), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New PlanItem
                    With Itm
                        'TargetLineId, priority, PN, OrderID, Ordered, Built, Qty, PartId, Flags, Desc,	Name, Status, PPHPP, DueDate
                        .OrderId = dRead("OrderID") 'Orderid
                        .TargetLineId = dRead("TargetLineId")
                        .Built = If(DBNull.Value.Equals(dRead("Built")), 0, dRead("Built"))
                        .Desc = If(DBNull.Value.Equals(dRead("Desc")), "", dRead("Desc"))
                        .DueDate = If(DBNull.Value.Equals(dRead("DueDate")), Now, dRead("DueDate"))
                        .Ordered = If(DBNull.Value.Equals(dRead("Ordered")), 0, dRead("Ordered"))
                        .OrderId = dRead("OrderId")
                        .PartNumber = dRead("PN")
                        .QTY = dRead("Qty")
                        .PartId = If(DBNull.Value.Equals(dRead("PartId")), 0, dRead("PartId"))
                        .Status = dRead("Status")
                        .Flags = dRead("Flags")
                        .Position = dRead("Priority")
                        .PPHPP = .PPHPP = dRead("PPHPP")
                        .Chk = If(DBNull.Value.Equals(dRead("PartId")) OrElse dRead("PartId") = 0, "ID?", "OK")
                    End With
                    Items.Add(Itm)
                End While
            End Using
        End Using
        Pr.PlanData.AddRange(From x In Items Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended OrElse x.Status = PlanStatus.Planed)
        Return Pr
    End Function


    Public Function SavePlan(PlanItems As List(Of PlanItem)) As TransactionResult
        Dim Tr As New TransactionResult
        Dim Cnt As Integer = PlanItems.Count

        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim Cmd As SqlClient.SqlCommand
                For Each R In PlanItems
                    If R.OrderId > 0 Then
                        Cmd = GetPlanUpdate(R)
                    Else
                        Cmd = GetPlanInsert(R)
                    End If
                    Cmd.Connection = Cn
                    Tr.Result += Cmd.ExecuteNonQuery()
                    Cmd.Dispose()
                Next
            End Using
        Catch ex As Exception
            Tr.Result = -1
            Tr.ResultString = "Save plan Error " & ex.Message
        End Try
        Return Tr
    End Function


    Private Function GetPlanInsert(Pi As PlanItem) As SqlClient.SqlCommand
        Dim dCmd As New SqlClient.SqlCommand
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ID", Pi.OrderId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CreationDate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ShipDate", Pi.Shipdate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ScheduleDate", Pi.Shipdate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartId", Pi.PartId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Quantity", Pi.QTY))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@TargetLineId", Pi.TargetLineId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Position", Pi.Position))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Status", Pi.Status))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@LastUpdate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Flags", Pi.Flags))

        dCmd.CommandText = String.Format("Insert into Schedule_Order_History (CreationDate,ShipDate,ScheduleDate,PartId,Quantity,TargetLineId,Position,LastUpdate,Flags,Status) 
         Values(@CreationDate,@ShipDate,@ScheduleDate,@PartId,@Quantity,@TargetLineId,@Position,@LastUpdate,@Flags,@Status)")
        Return dCmd
    End Function

    Private Function GetPlanUpdate(Pi As PlanItem) As SqlClient.SqlCommand
        Dim dCmd As New SqlClient.SqlCommand
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ID", Pi.OrderId))
        '   dCmd.Parameters.Add(New SqlClient.SqlParameter("@CreationDate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ShipDate", Pi.Shipdate))
        '  dCmd.Parameters.Add(New SqlClient.SqlParameter("@ScheduleDate", Pi.ScheduleDate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartId", Pi.PartId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Quantity", Pi.QTY))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@TargetLineId", Pi.TargetLineId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Position", Pi.Position))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Status", Pi.Status))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@LastUpdate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Flags", Pi.Flags))

        dCmd.CommandText = String.Format("Update Schedule_Order_History 
            set ShipDate = @ShipDate,
            Quantity = @Quantity,
            Position = @Position,
            Partid = @PartId,
            LastUpdate = @LastUpdate,
            Flags = @Flags
           where Id = @ID and Status = 2")
        Return dCmd
    End Function

    Private Function GetConnectionString() As String
        Dim CnS As New Data.SqlClient.SqlConnectionStringBuilder
        With CnS
            .DataSource = _Cfg.SqlSeverName
            .UserID = _Cfg.SqlUserName
            .Password = _Cfg.SqlPw
            .InitialCatalog = _Cfg.SqlDbName
        End With

        Return CnS.ConnectionString
    End Function

End Class
