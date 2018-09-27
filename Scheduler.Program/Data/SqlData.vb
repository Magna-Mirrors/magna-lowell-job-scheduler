Imports System.Data.SqlClient
Imports Scheduler.core

Public Class SqlData
    Protected ReadOnly _Cfg As SvcParams
    Private ReadOnly LgSvr As iLoggingService

    'TargetLineId,	priority,	PN,	OrderId,	Ordered,	Built,	Qty,	PartId,	Flags,	Desc,	Name,	Status,	PPHPP, DueDate
    Private Const ActiveOrderQuery As String = "SELECT [TargetLineId]
                     ,[Priority],[PN],[PartId],[OrderId],[Ordered],[Built],[Qty],[Flags],[Desc],[Name],[Status],[PPHPP],[DueDate],[CustOrderId],
                     [ReOrderPercentThreshold], [WorkBufferMinutes],[Workcell]
                     FROM [View_ActiveOrders]
                     {0}
                     order by status desc, Priority"

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

    Private Const GetpartsForLineQuery = "SELECT Part_Info.PN, Part_Info.Desc1 AS [Desc], Part_Program_Line_Map.LineId, Part_Info.PartID AS partId, Part_Info.RH, Part_Info.LH, Part_Colors.Name AS colorName, 
                      Part_Colors.ProgId
                      FROM  Part_Program_Line_Map INNER JOIN
                      Part_Programs ON Part_Program_Line_Map.ProgramId = Part_Programs.ProgId INNER JOIN
                      Part_Info ON Part_Program_Line_Map.LH = Part_Info.LH or Part_Program_Line_Map.RH = Part_Info.RH INNER JOIN
                      Part_Colors ON Part_Info.ColorId = Part_Colors.ColorId AND Part_Programs.ProgId = Part_Colors.ProgId
                      Where (dbo.Part_Program_Line_Map.LineId = {0})"

    'PN,	Desc,	LineId,	PartId
    Private Const GetFiletedpartsQuery = "SELECT Part_Info.PN, Part_Info.Desc1 AS [Desc], Part_Program_Line_Map.LineId, Part_Info.PartId AS partId, Part_Info.RH, Part_Info.LH, Part_Colors.Name AS colorName, 
                      Part_Colors.ProgId
					  FROM Part_Program_Line_Map INNER JOIN
					   Part_Programs ON Part_Program_Line_Map.ProgramId = Part_Programs.ProgId INNER JOIN
					  Part_Info ON Part_Program_Line_Map.LH = Part_Info.LH or Part_Program_Line_Map.RH = Part_Info.RH INNER JOIN
					   Part_Colors ON Part_Info.ColorId = Part_Colors.ColorId AND Part_Programs.ProgId = Part_Colors.ProgId
                       Where (dbo.Part_Program_Line_Map.LineId = {0}) and (dbo.Part_Info.PN in({1}))"


    Private Const GetUnPostedProduction = "SELECT TOP 1000 dbo.Schedule_Part_Production_History.LogDateTime AS ProdDate, 
                                          dbo.Schedule_Part_Production_History.OrderId AS ProdOrder, dbo.eqp_Lines.Workcell, dbo.Part_Info.PN AS ProdItem, 
                                          dbo.Schedule_Part_Production_History.Userid AS Operator,dbo.Schedule_Part_Production_History.Qty AS Qty_Passed, 
                                          dbo.Schedule_Part_Production_History.Id AS BuiltId
                                          FROM dbo.Schedule_Part_Production_History INNER JOIN
                                          dbo.Schedule_Order_History ON dbo.Schedule_Part_Production_History.OrderId = dbo.Schedule_Order_History.ID INNER JOIN
                                          dbo.Part_Info ON dbo.Schedule_Order_History.PartId = dbo.Part_Info.PartId INNER JOIN
                                          dbo.eqp_Lines ON dbo.Schedule_Part_Production_History.LineId = dbo.eqp_Lines.Id
                                          WHERE (dbo.Schedule_Part_Production_History.Posted = 0)
                                          order by dbo.Schedule_Part_Production_History.LogDateTime"




    'SELECT TOP 500 DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.Schedule_Part_Production_History.LogDateTime)) AS ProdDate, 
    '  dbo.Schedule_Part_Production_History.OrderId AS ProdOrder, dbo.eqp_Lines.Workcell, dbo.Part_Info.PN AS ProdItem, 
    '  dbo.Schedule_Part_Production_History.Userid AS Operator, SUM(dbo.Schedule_Part_Production_History.Qty) AS Qty_Passed, 
    '  MAX(dbo.Schedule_Part_Production_History.Id) AS MaxBuiltId,Min(dbo.Schedule_Part_Production_History.Id) AS MinBuiltId
    '  FROM dbo.Schedule_Part_Production_History INNER JOIN
    '  dbo.Schedule_Order_History ON dbo.Schedule_Part_Production_History.OrderId = dbo.Schedule_Order_History.ID INNER JOIN
    '  dbo.Part_Info ON dbo.Schedule_Order_History.PartId = dbo.Part_Info.PartId INNER JOIN
    '  dbo.eqp_Lines ON dbo.Schedule_Part_Production_History.LineId = dbo.eqp_Lines.Id
    '  WHERE (dbo.Schedule_Part_Production_History.Posted = 0)
    '  GROUP BY dbo.Schedule_Part_Production_History.Userid, dbo.Part_Info.PN, dbo.Schedule_Part_Production_History.OrderId, dbo.eqp_Lines.Workcell, DATEADD(dd, 0, 
    '  DATEDIFF(dd, 0, dbo.Schedule_Part_Production_History.LogDateTime))
    '  ORDER BY ProdDate, ProdOrder, dbo.eqp_Lines.Workcell, ProdItem, Operator"

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
            Dim Cmd As New SqlClient.SqlCommand(String.Format(GetFiletedpartsQuery, PartReq.LineData.Id, Jn), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                Try
                    While dRead.Read
                        Dim Pn = CStr(dRead("PN"))
                        Dim Pr = (From x In PartReq.Parts Where x.PN = Pn)
                        If Pr IsNot Nothing Then
                            'update all like parts in PartReq
                            For Each Itm In Pr
                                Itm.Id = CInt(dRead("PartId"))
                                Itm.Valid = True
                                ' Itm.Desc = CStr(dRead("Desc"))
                                Rslt.Result += 1
                            Next

                        End If
                    End While
                Catch ex As Exception
                    Rslt.ResultString = ex.Message
                End Try

            End Using
        End Using
        If Rslt.Result = 0 Then Rslt.ResultString = "these parts did not validate for this Line"
        Rslt.parts = PartReq.Parts
        Return Rslt
    End Function

    Public Function GetParts(PartReq As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Rslt As New getPartsforLineResponse
        Using Cn = GetConnection()
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} Where Lineid = {1}", GetpartsForLineQuery, PartReq.LineData.Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim P As New Part
                    P.PN = CStr(dRead("PN"))
                    ' P.Desc = CStr(dRead("Desc"))
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
            Dim Wc = "Where (eqp_Lines.SchedulerMethod > 0)" 'If(SqlLinesOnly, "Where (eqp_Lines.SchedulerMethod =2)", "Where (eqp_Lines.SchedulerMethod > 0)")
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
                    .WC = CType(If(DBNull.Value.Equals(dRead("Wc")), "", dRead("Wc")), String)
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
                Rslt.Result = Cmd.ExecuteNonQuery
                Cn.Close()

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
                Dim Cmd As New SqlClient.SqlCommand(String.Format(UpStr, CInt(Status), Order_Id), Cn)
                Rslt.Result = Cmd.ExecuteNonQuery
                Cn.Close()

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


    'TODO: Update this FUNCTON
    Public Function GetActiveOrders(Req As GetPlanRequest) As GetPlanResponse
        Dim Pr As New GetPlanResponse
        Dim Items As New List(Of PlanItem)
        Dim Lineid As Integer = Req.LineData.Id
        Dim Last24 As Boolean = Req.IncludeHistory
        Using Cn = GetConnection()
            Cn.Open()
            Dim WC As String = ""

            If Last24 Then
                WC = String.Format("WHERE (TargetLineId = {0}) and ((Status BETWEEN 2 AND 3) or (LastUpdate >= (GetDate()-1)))", Req.LineData.Id)
            Else
                WC = String.Format("WHERE (Status BETWEEN 2 AND 3) and (TargetLineId = {0})", Req.LineData.Id)
            End If

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
                        .ScheduleDate = CDate(If(DBNull.Value.Equals(dRead("DueDate")), Now, dRead("DueDate")))
                        .WorkCell = If(DBNull.Value.Equals(dRead("WorkCell")), "", dRead("WorkCell").ToString)
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
        Pr.PlanData.AddRange(Items.ToList) 'From x In Items Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended OrElse x.Status = PlanStatus.Planed
        Return Pr
    End Function
    Public Function GetActiveOrders() As List(Of BuildItem)
        Dim Items As New List(Of BuildItem)
        Using Cn = GetConnection()
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(GetUnPostedProduction, Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New BuildItem
                    With Itm
                        Itm.Workcell = dRead("Workcell")
                        Itm.ProdDate = dRead("ProdDate")
                        Itm.ProdItem = dRead("ProdItem")
                        Itm.Qty_Passed = dRead("Qty_Passed")
                        Itm.ProdOrder = dRead("ProdOrder")
                        Itm.Operator = dRead("Operator")
                        Itm.BuiltId = dRead("BuiltId")
                    End With
                    Items.Add(Itm)
                End While
            End Using
        End Using
        Return Items
    End Function

    Public Function GetWipOrders(OrderId As Integer) As List(Of WipOrder)
        Dim Wip As New List(Of WipOrder)
        Using Cn = GetConnection()
            Cn.Open()
            'Dim Cmd As New SqlClient.SqlCommand(String.Format(ActiveOrderQuery, If(OrderId > 0, String.Format("Where OrderId = {0}", OrderId), "")), Cn)
            Dim WC As String = ""
            If OrderId > 0 Then WC = String.Format("Where OrderId = {0}", OrderId)
            Dim Cmd As New SqlClient.SqlCommand(String.Format(ActiveOrderQuery, WC), Cn)

            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    'TargetLineId,	priority,	PN,	OrderId,	Ordered,	Built,	Qty,	PartId,	Flags,	Desc,	Name,	Status,	PPHPP, DueDate,ReOrderPercentThreshold,WorkBufferMinutes,Workcell
                    Dim W As New WipOrder
                    W.LineId = CInt(dRead("TargetLineId"))
                    W.Status = CType(dRead("Status"), PlanStatus)
                    W.Position = CType(dRead("priority"), Long)
                    W.OrderId = CType(dRead("OrderID"), Integer)
                    W.PartNumber = CStr(dRead("PN"))
                    W.TargetQty = CType(dRead("Qty"), Integer)
                    W.WipHours = CType(dRead("WorkBufferMinutes"), Double) / 60
                    W.ReOrderAtPercent = CType(dRead("ReOrderPercentThreshold"), Double)
                    If Not DBNull.Value.Equals(dRead("WorkCell")) Then W.WorkCell = dRead("WorkCell").ToString
                    If Not DBNull.Value.Equals(dRead("PPHPP")) Then W.PartsPerHourPerPerson = CInt(dRead("PPHPP"))
                    If Not DBNull.Value.Equals(dRead("Built")) Then W.Built = CInt(dRead("Built"))
                    If Not DBNull.Value.Equals(dRead("Ordered")) Then W.Ordered = CInt(dRead("Ordered"))
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

    Public Function Update_PartBuilt_Postings(MinId As Integer, MxId As Integer) As Integer
        Try
            Using Cn = GetConnection()
                Cn.Open()
                Dim Cmd As New SqlClient.SqlCommand(String.Format("Update Schedule_Part_Production_History Set Posted = 1 where (posted = 0) and (id >= {0}) and (id <= {1})", MinId, MxId), Cn)
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception

            LgSvr.SendAlert(New LogEventArgs("Update_PartBuilt_Postings", ex))
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
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartDesc", Pi.Desc))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CustOrderId", Pi.CustOrderId))

        dCmd.CommandText = String.Format("Insert into Schedule_Order_History (CreationDate,ShipDate,ScheduleDate,PartId,PartDesc,Quantity,TargetLineId,Position,LastUpdate,Flags,Status,CustOrderId) 
         Values(@CreationDate,@ShipDate,@ScheduleDate,@PartId,@PartDesc,@Quantity,@TargetLineId,@Position,@LastUpdate,@Flags,@Status,@CustOrderId)")
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
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartDesc", Pi.Desc))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CustOrderId", Pi.CustOrderId))

        dCmd.CommandText = String.Format("Update Schedule_Order_History 
            set ShipDate = @ShipDate,
            Quantity = @Quantity,
            Position = @Position,
            Partid = @PartId,
            PartDesc = @PartDesc,
            {0} 
            LastUpdate = @LastUpdate,
            Flags = @Flags,
            CustOrderId = @CustOrderId
            where Id = @ID and Status = 2", If(Pi.Status = PlanStatus.Removed, "Status = @Status,", ""))
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
