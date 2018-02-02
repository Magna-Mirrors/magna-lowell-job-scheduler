Imports System.Data.SqlClient
Imports Scheduler.core

Public Class SqlData
    Protected ReadOnly _Cfg As SvcParams
    Private ReadOnly LgSvr As iLoggingService

    'TargetLineId,	priority,	PN,	OrderId,	Ordered,	Built,	Qty,	PartId,	Flags,	Desc,	Name,	Status,	PPHPP, DueDate
    Private Const ActiveOrderQuery As String = "SELECT TOP (100) PERCENT Schedule_Order_History.TargetLineId, Schedule_Order_History.Position AS priority, Part_Info.PN, Part_Info.PartId, Schedule_Order_History.ID AS OrderId, 
                      SUM(Schedule_Part_Order_History.Qty) AS Ordered, SUM(Schedule_Part_Production_History.Qty) AS Built, MAX(Schedule_Order_History.Quantity) AS Qty, 
                      Schedule_Order_History.Flags, Part_Info.Desc1 AS [Desc], Part_Info.Name, Schedule_Order_History.Status, Part_Options.PartsPerHourPerPerson AS PPHPP, 
                      Schedule_Order_History.ScheduleDate AS DueDate, Schedule_Order_History.CustOrderId
                      FROM Schedule_Order_History INNER JOIN
                      Part_Info ON Schedule_Order_History.PartId = Part_Info.PartId INNER JOIN
                      Part_Options ON Part_Info.OptionId = Part_Options.OptionId LEFT OUTER JOIN
                      Schedule_Part_Production_History ON Schedule_Order_History.ID = Schedule_Part_Production_History.OrderId LEFT OUTER JOIN
                      Schedule_Part_Order_History ON Schedule_Order_History.ID = Schedule_Part_Order_History.OrderId
                      WHERE (Schedule_Order_History.Status < 4) {0}
                      GROUP BY Schedule_Order_History.Position, Schedule_Order_History.ID, Schedule_Order_History.TargetLineId, Part_Info.PN, Schedule_Order_History.Status, Part_Info.Desc1, 
                      Part_Info.Name, Schedule_Order_History.Flags, Part_Options.PartsPerHourPerPerson, Schedule_Order_History.ScheduleDate, Part_Info.PartId, Schedule_Order_History.CustOrderId
                      ORDER BY Schedule_Order_History.TargetLineId, priority"

    'LineId,	CustomerId,	LineName,	LineDefinition,	MaxConcurrentLogins,	WcfFileName,	SelectCmd,	ScheduleFolder,	SchedulerMethod,	WorkBufferMinutes,	CustomerName,	ProgramId,	LH,	RH, ReOrderPercentThreshold, User_Count,Wc
    Private Const LineQuery As String = "SELECT eqp_Lines.Id AS LineId, eqp_Lines.CustomerId, Part_Programs.Customer_OrderId_Required, eqp_Lines.LineName, eqp_Lines.LineDefinition, 
                      eqp_Lines.MaxConcurrentLogins, eqp_Lines.WcfFileName, eqp_Lines.SelectCmd, eqp_Lines.ScheduleFolder, eqp_Lines.SchedulerMethod, 
                      eqp_Lines.WorkBufferMinutes, Part_Customers.Name AS CustomerName, Part_Program_Line_Map.ProgramId, Part_Program_Line_Map.LH, 
                      Part_Program_Line_Map.RH, eqp_Lines.ReOrderPercentThreshold, LineUserCount.user_count, eqp_Lines.Workcell AS WC
                      FROM Part_Programs INNER JOIN
                      Part_Program_Line_Map ON Part_Programs.ProgId = Part_Program_Line_Map.ProgramId RIGHT OUTER JOIN
                      eqp_Lines INNER JOIN
                      Part_Customers ON eqp_Lines.CustomerId = Part_Customers.Id LEFT OUTER JOIN
                      LineUserCount ON eqp_Lines.Id = LineUserCount.LineId ON Part_Program_Line_Map.LineId = eqp_Lines.Id
                      {0}
                      ORDER BY eqp_Lines.CustomerId, LineId"

    'PN,	Desc,	LineId,	PartId
    Private Const GetpartsQuery = "SELECT dbo.Part_Info.PN, dbo.Part_Info.Desc1 AS [Desc], dbo.Part_Program_Line_Map.LineId, dbo.Part_Info.PartId
                      FROM dbo.Part_Info INNER JOIN
                      dbo.Part_Programs ON dbo.Part_Info.ProgId = dbo.Part_Programs.ProgId INNER JOIN
                      dbo.Part_Program_Line_Map ON dbo.Part_Programs.ProgId = dbo.Part_Program_Line_Map.ProgramId
                      Where dbo.Part_Program_Line_Map.LineId = {0} and dbo.Part_Info.PN in({1}) "


    Private LineOrderstatusQuery As String = "SELECT TOP (100) PERCENT dbo.Schedule_Order_History.TargetLineId AS LineId, dbo.Schedule_Order_History.Status, dbo.Schedule_Order_History.Position, 
                      dbo.Schedule_Order_History.ID AS OrderId, dbo.Part_Info.PN AS PartNumber, MAX(dbo.Schedule_Order_History.Quantity) AS TargetQty, 
                      SUM(dbo.Schedule_Part_Production_History.Qty) AS Built, SUM(dbo.Schedule_Part_Order_History.Qty) AS Ordered, AVG(dbo.Part_Options.PartsPerHourPerPerson) 
                      AS PPPP, dbo.eqp_Lines.Workcell, MAX(dbo.eqp_Lines.WorkBufferMinutes) / 60 AS WipHours, MAX(dbo.eqp_Lines.ReOrderPercentThreshold) 
                      AS ReOrderAtPercent
                      FROM dbo.Schedule_Order_History INNER JOIN
                      dbo.Part_Info ON dbo.Schedule_Order_History.PartId = dbo.Part_Info.PartId INNER JOIN
                      dbo.Part_Options ON dbo.Part_Info.OptionId = dbo.Part_Options.OptionId INNER JOIN
                      dbo.eqp_Lines ON dbo.Schedule_Order_History.TargetLineId = dbo.eqp_Lines.Id LEFT OUTER JOIN
                      dbo.Schedule_Part_Order_History ON dbo.Schedule_Order_History.ID = dbo.Schedule_Part_Order_History.OrderId LEFT OUTER JOIN
                      dbo.Schedule_Part_Production_History ON dbo.Schedule_Order_History.ID = dbo.Schedule_Part_Production_History.OrderId
                      WHERE (dbo.Schedule_Order_History.Status BETWEEN 2 AND 3)
                      GROUP BY dbo.Schedule_Order_History.TargetLineId, dbo.Schedule_Order_History.Status, dbo.Schedule_Order_History.Position, dbo.Schedule_Order_History.ID, 
                      dbo.Part_Info.PN, dbo.eqp_Lines.Workcell
                      ORDER BY dbo.Schedule_Order_History.Status DESC, dbo.Schedule_Order_History.Position"


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
        Rslt.ResultString = ""
        Using Cn = GetConnection()
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
                            Rslt.Result += 1
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
        Using Cn = GetConnection()
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} Where Lineid = {1}", GetpartsQuery, PartReq.LineData.Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim P As New Part
                    P.PN = CStr(dRead("PN"))
                    P.Desc = CStr(dRead("Desc"))
                    P.Id = CType(dRead("PartId"), Integer?)
                    Rslt.parts.Add(P)
                End While
            End Using
        End Using
        Return Rslt
    End Function



    Public Function GetLinesData(SqlLinesOnly As Boolean) As GetLinesResponse
        Dim Rslt As New GetLinesResponse

        Using Cn = GetConnection()
            Cn.Open()
            Dim Wc = If(SqlLinesOnly, "Where (eqp_Lines.SchedulerMethod =2)", "Where (eqp_Lines.SchedulerMethod > 0)")
            Dim Cmd As New SqlClient.SqlCommand(String.Format(LineQuery, Wc), Cn)
            Rslt.Lines.AddRange(ParseOutLines(Cmd))

        End Using
        Return Rslt
    End Function




    Public Function GetLineData(LineId As Integer) As Line
        Using Cn = GetConnection()
            Cn.Open()

            Dim Cmd As New SqlClient.SqlCommand(String.Format(LineQuery, "Where Id = " & LineId), Cn)
            Return ParseOutLines(Cmd, True).FirstOrDefault()
           
        End Using
        Return Nothing
    End Function

    Private Shared Function ParseOutLines(cmd As SqlCommand, Optional firstonly As Boolean = False) As IEnumerable(Of Line)
        Dim lines As New List(Of Line)()
        Using dRead As IDataReader = cmd.ExecuteReader()
            While dRead.Read
                Dim Itm As New Line
                With Itm
                    'LineId,	CustomerId,	LineName,	LineDefinition,	MaxConcurrentLogins,	WcfFileName,	SelectCmd,	ScheduleFolder,	SchedulerMethod,	WorkBufferMinutes,	CustomerName,	ProgramId,	LH,	RH, ReOrderPercentThreshold, User_count
                    .Description = CStr(dRead("LineDefinition"))
                    .Id = CInt(dRead("LineId"))
                    .Name = CStr(dRead("LineName"))
                    .SchedulerMethod = CType(If(DBNull.Value.Equals(dRead("SchedulerMethod")), SchedulerMethods.None, dRead("SchedulerMethod")), SchedulerMethods) 'dRead("SchedulerMethod")

                    'SchedulerMethods

                    .SelectCmd = CStr(dRead("SelectCmd"))
                    .WcfFileName = CStr(dRead("WcfFileName"))
                    .ScheduleFolder = CType(If(DBNull.Value.Equals(dRead("ScheduleFolder")), "", dRead("ScheduleFolder")), String)
                    .CustomerName = CType(If(DBNull.Value.Equals(dRead("CustomerName")), "", dRead("CustomerName")), String)
                    .CustomerId = CInt(If(DBNull.Value.Equals(dRead("CustomerId")), 0, dRead("CustomerId")))
                    .ProgramId = CInt(If(DBNull.Value.Equals(dRead("ProgramId")), 0, dRead("ProgramId")))
                    .WorkBufferMinutes = CInt(If(DBNull.Value.Equals(dRead("WorkBufferMinutes")), 0, dRead("WorkBufferMinutes")))
                    .ReOrderPercentThreshold = CSng(If(DBNull.Value.Equals(dRead("ReOrderPercentThreshold")), 0.8, dRead("ReOrderPercentThreshold")))
                    .UserCount = CSng(If(DBNull.Value.Equals(dRead("user_count")), 1, dRead("user_count")))
                    .WC = CType(If(DBNull.Value.Equals(dRead("WC")), "", dRead("WC")), String)
                    .CustomerOderIdRequired = CBool(If(DBNull.Value.Equals(dRead("Customer_OrderId_Required")), False, dRead("Customer_OrderId_Required")))
                    .QueuedMinutes = 0
                End With
                If Itm.SchedulerMethod <> SchedulerMethods.None Then
                    lines.Add(Itm)
                Else
                    '   Stop
                End If


                If firstonly Then
                    Return lines
                End If
            End While
        End Using
        Return lines
    End Function

    Public Function updateOrderPosition(OrderId_Id As Integer, Position As Long) As TransactionResult
        Dim Rslt As New TransactionResult
        Try
            Using Cn = GetConnection()
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
            Using Cn = GetConnection()
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
        Using Cn = GetConnection()
            Try
                Dim Cmd As New SqlClient.SqlCommand(String.Format("Update Schedule_Order_History set Status = {0} where Id = {1}", Status, OrderId), Cn)
                RetV = Cmd.ExecuteNonQuery()
                Cmd.Dispose()
            Catch ex As Exception
                LgSvr.SendAlert(New LogEventArgs("UpdatePlanStatus", ex))
            End Try
        End Using
        Return RetV
    End Function

    Public Function GetActiveOrders(Lineid As Integer) As GetPlanResponse
        Dim Pr As New GetPlanResponse
        Dim Items As New List(Of PlanItem)
        Using Cn = GetConnection()
            Cn.Open()
            Dim WC = If(Lineid > 0, String.Format("AND (Schedule_Order_History.TargetLineId = {0})", Lineid), "")
            Dim Cmd As New SqlClient.SqlCommand(String.Format(ActiveOrderQuery, WC), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New PlanItem
                    With Itm
                        'TargetLineId, priority, PN, OrderID, Ordered, Built, Qty, PartId, Flags, Desc,	Name, Status, PPHPP, DueDate
                        .OrderId = CInt(dRead("OrderID")) 'Orderid
                        .TargetLineId = CInt(dRead("TargetLineId"))
                        .Built = CInt(If(DBNull.Value.Equals(dRead("Built")), 0, dRead("Built")))
                        .Desc = CType(If(DBNull.Value.Equals(dRead("Desc")), "", dRead("Desc")), String)
                        .DueDate = CDate(If(DBNull.Value.Equals(dRead("DueDate")), Now, dRead("DueDate")))
                        .CustOrderId = dRead("CustOrderId").ToString
                        .Ordered = CInt(If(DBNull.Value.Equals(dRead("Ordered")), 0, dRead("Ordered")))
                        .OrderId = CInt(dRead("OrderId"))
                        .PartNumber = CType(dRead("PN"), String)
                        .QTY = CInt(dRead("Qty"))
                        .PartId = CInt(If(DBNull.Value.Equals(dRead("PartId")), 0, dRead("PartId")))
                        .Status = CType(dRead("Status"), PlanStatus)
                        .Flags = CType(dRead("Flags"), OrderFlags)
                        .Position = CLng(dRead("Priority"))
                        .PPHPP = CSng(dRead("PPHPP"))
                        .Chk = If(DBNull.Value.Equals(dRead("PartId")) OrElse CInt(dRead("PartId")) = 0, "ID?", "OK")
                    End With
                    Items.Add(Itm)
                End While
            End Using
        End Using
        Pr.PlanData.AddRange(From x In Items Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended OrElse x.Status = PlanStatus.Planed)
        Return Pr
    End Function

    Public Function GetWipOrders() As List(Of WipOrder)
        Dim Wip As New List(Of WipOrder)
        Using Cn = GetConnection()
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(LineOrderstatusQuery, Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim W As New WipOrder
                    W.LineId = CInt(dRead("LineId"))
                    W.Status = CType(dRead("Status"), PlanStatus)
                    W.Position = CType(dRead("Position"), Long)
                    W.OrderId = CType(dRead("OrderId"), Integer)
                    W.PartNumber = CStr(dRead("PartNumber"))
                    W.TargetQty = CType(dRead("TargetQty"), Integer)
                    W.WipHours = CType(dRead("WipHours"), Double)
                    W.ReOrderAtPercent = CType(dRead("ReOrderAtPercent"), Double)
                    If Not DBNull.Value.Equals(dRead("WorkCell")) Then W.WorkCell = dRead("WorkCell").ToString
                    If Not DBNull.Value.Equals(dRead("PPPP")) Then W.PartsPerHourPerPerson = CInt(dRead("PPPP"))
                    If Not DBNull.Value.Equals(dRead("Built")) Then W.Built = CType(dRead("Built"), Integer)
                    If Not DBNull.Value.Equals(dRead("Ordered")) Then W.Ordered = CType(dRead("Ordered"), Integer)

                    Wip.Add(W)
                End While
            End Using
        End Using
        Return Wip
    End Function

    Public Function UpdateOrderstatus(OrderId As Integer, status As PlanStatus, Position As Long) As Integer
        Try
            Using Cn = GetConnection()
                Cn.Open()
                Dim Cmd As New SqlClient.SqlCommand(String.Format("Update dbo.Schedule_Order_History set [Status] = {0}, Position = {1} Where Id = {2}", CInt(status), Position, OrderId), Cn)
                Return CInt(Cmd.ExecuteNonQuery)
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Updatin Order status", ex))
            Return 0
        End Try

        Return 0
    End Function




    Public Function LogPartOrder(W As WipOrder) As Integer
        Try
            Using Cn As SqlConnection = GetConnection()
                Cn.Open()
                Dim Cmd As New SqlClient.SqlCommand("INSERT INTO [dbo].[Schedule_Part_Order_History] ([LineId],[OrderId],[Qty],[Posted]) Values(@LineId,@OrderId,@Qty,@Posted)", Cn)
                Cmd.Parameters.Add(New SqlParameter("@LineId", SqlDbType.Int) With {.SqlValue = W.LineId})
                Cmd.Parameters.Add(New SqlParameter("@OrderId", SqlDbType.Int) With {.SqlValue = W.OrderId})
                Cmd.Parameters.Add(New SqlParameter("@Qty", SqlDbType.Int) With {.SqlValue = W.RequestOrderQty})
                Cmd.Parameters.Add(New SqlParameter("@Posted", SqlDbType.Int) With {.SqlValue = 1})
                Return CInt(Cmd.ExecuteNonQuery)
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Logging PartOrder", ex))
            Return 0
        End Try

        Return 0
    End Function



    Public Function GetUsersOnLine(LineId As Integer) As Integer
        Try
            Using Cn = GetConnection()
                Cn.Open()
                Dim Cmd As New SqlClient.SqlCommand("GetUserCountForTheLine", Cn)
                Cmd.CommandType = CommandType.StoredProcedure
                Dim Prm As New SqlParameter("@LineId", SqlDbType.Int) With {.SqlValue = LineId}
                Cmd.Parameters.Add(Prm)
                Return CInt(Cmd.ExecuteScalar)
            End Using
        Catch ex As Exception
            Return 0
        End Try

        Return 0
    End Function



    'only used by client scheduler-------------------------------------------
    Public Function SavePlan(PlanItems As List(Of PlanItem)) As TransactionResult
        Dim Tr As New TransactionResult
        Dim Cnt As Integer = PlanItems.Count

        Try
            Using Cn = GetConnection()
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
            LgSvr.SendAlert(New LogEventArgs("Saving Plan", ex))
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
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CustOrderId", Pi.CustOrderId))

        dCmd.CommandText = String.Format("Insert into Schedule_Order_History (CreationDate,ShipDate,ScheduleDate,PartId,Quantity,TargetLineId,Position,LastUpdate,Flags,Status,CustOrderId) 
         Values(@CreationDate,@ShipDate,@ScheduleDate,@PartId,@Quantity,@TargetLineId,@Position,@LastUpdate,@Flags,@Status,@CustOrderId)")
        Return dCmd
    End Function

    'used by save plan
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
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CustOrderId", Pi.CustOrderId))

        dCmd.CommandText = String.Format("Update Schedule_Order_History 
            set ShipDate = @ShipDate,
            Quantity = @Quantity,
            Position = @Position,
            Partid = @PartId,
            LastUpdate = @LastUpdate,
            Flags = @Flags,
            CustOrderId = @CustOrderId
             where Id = @ID and Status = 2")
        Return dCmd
    End Function
    Private Function GetConnection() As SqlConnection
        Dim connstr = GetConnectionString() + "; Persist Security Info=True;"
        Return New SqlClient.SqlConnection(connstr)
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
