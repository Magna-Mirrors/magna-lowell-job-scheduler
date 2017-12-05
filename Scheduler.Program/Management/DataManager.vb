Imports System.Threading
Imports Scheduler.core
Imports Scheduler.core.Classes
Imports System.Drawing

Public Class DataManager

    Protected ReadOnly _LoggingService As iLoggingService
    Protected ReadOnly _SqlAccess As SqlData
    Protected ReadOnly _MdbAccess As MdbData
    Protected ReadOnly _Tools As AppTools
    Protected ReadOnly _ErpAccess As ErpSql
    Private _Cfg As SvcParams
    'this is the goto spot got getting part information

    '  Public Shared ReadOnly Property Instance() As DataManager
    ' Get
    '   Return m_Instance.Value
    '  End Get
    '   End Property



    Public Sub New(LgSvc As iLoggingService, Atool As AppTools, SqlSvc As SqlData, MdbAccess As MdbData, ErpAccess As ErpSql)
        Dim Prm = Atool.GetProgramParams
        _Tools = Atool
        _Cfg = Prm
        _MdbAccess = MdbAccess
        _LoggingService = LgSvc
        _SqlAccess = SqlSvc

    End Sub

    Public Function GetLine(Lineid As Integer) As GetLineResponse
        Dim Rslt As New GetLineResponse
        Try
            Rslt.LineInfo = _SqlAccess.GetLineData(Lineid)
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try

        Return Rslt
    End Function

    Public Function GetLines() As GetLinesResponse
        Dim Rslt As New GetLinesResponse
        Try
            Rslt = _SqlAccess.GetLinesData(False)
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = "GetLines Error " & ex.Message
        End Try

        Return Rslt
    End Function


    Public Function GetSubscribingLines() As GetLinesResponse
        Dim Rslt As New GetLinesResponse
        Try
            Rslt = _SqlAccess.GetLinesData(False)
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = "GetLines Error " & ex.Message
        End Try

        Return Rslt
    End Function


    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse
        Dim nPr As New GetPlanResponse
        Try
            If Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
                nPr = _SqlAccess.GetActiveOrders(Sourcedata.LineData.Id)
                nPr.Result = 1
            ElseIf Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MdbAndPlanFiles Then
                Dim PlanTxt As New TextData(_Tools, _Cfg)
                nPr = PlanTxt.getPlanData(Sourcedata.LineData)
                nPr.Result = 1
            Else

            End If
        Catch ex As Exception

        End Try
        Return nPr
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult
        Dim nPr As New TransactionResult
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            nPr = _SqlAccess.SavePlan(SourceData.PlanData)
        Else
            Dim txTools As New TextData
            nPr = txTools.SavePlan(SourceData)
        End If
        Return nPr
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse
        Dim Rslt As New ValidatePartsResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.ValidateParts(SourceData)
        Else

            Rslt = _MdbAccess.ValidateParts(SourceData)
        End If
        Return Rslt
    End Function

    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Rslt As New getPartsforLineResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.GetParts(SourceData)
        Else

            Rslt = _MdbAccess.GetParts(SourceData)
        End If
        Return Rslt
    End Function

    Public Function GetNextOrder(SourceData As GetNextOrderRequest) As GetNextOrderResult
        Dim Rslt As New GetNextOrderResult
        If SourceData.LineId Then
            Dim Resp = _SqlAccess.GetActiveOrders(SourceData.LineId)
            Dim Sd = From x In Resp.PlanData Where x.Status = PlanStatus.Scheduled
            If Sd IsNot Nothing AndAlso Sd.Count > 0 Then
                Rslt.Item = Sd(0)
                Rslt.Result = 1
            Else
                Rslt.Result = 0
                Rslt.ResultString = "No Scheduled Orders"
            End If
        End If
        Return Rslt
    End Function

    Public Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult
        Dim Rslt As New SkipOrderResult
        'GetOrderId
        If SourceData.Lineid Then
            Dim Resp = _SqlAccess.GetActiveOrders(SourceData.Lineid)
            Dim Sd = (From x In Resp.PlanData Where x.Status = PlanStatus.Scheduled).ToList()       'get all orders that are scheduled
            If Sd IsNot Nothing AndAlso Sd.Count > 0 Then
                Sd(0).Position = Sd.Count                                                           'set position to found plan list count
                _SqlAccess.updateOrderPosition(Sd(0).OrderId, Sd(0).Position)
                If Sd.Count = 1 Then
                    Rslt.Item = Sd(0)
                    Rslt.Result = 1
                Else
                    Rslt.Item = Sd(1)
                    Rslt.Result = 1
                    For i = 1 To Sd.Count - 1
                        _SqlAccess.updateOrderPosition(Sd(i).OrderId, Sd(i).Position)
                    Next
                End If
            Else
                Rslt.ResultString = "No Scheduled Orders"
            End If
        End If
        Return Rslt
    End Function

    Public Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult
        Dim Rslt = New RemoveOrderResult
        Dim F_Rslt = _SqlAccess.updateJobStatus(SourceData.OrderId, PlanStatus.Removed)
        Rslt.Result = F_Rslt.Result
        Rslt.ResultString = F_Rslt.ResultString
        Return Rslt
    End Function

    Public Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult
        Dim Rslt As New GetScheduleResult
        If SourceData.LineId > 0 Then
            Dim Sd = From x In _SqlAccess.GetActiveOrders(SourceData.LineId).PlanData Where x.Status = PlanStatus.Scheduled
            Rslt.Items = Sd
            Rslt.Result = 1
        Else
            Rslt.Result = 0
            Rslt.ResultString = "No Scheduled Items for this line"
        End If
        Return Rslt
    End Function




    'Order Management
    Public Sub ProcessOrders()
        Dim LinesRequest = _SqlAccess.GetLinesData(True) 'get line information for lines with scheduled controlled in Sql
        If LinesRequest.Result > 0 Then
            'get listoflineid
            Dim rOrder = _SqlAccess.GetActiveOrders(0)
            If rOrder.Result > 0 Then
                For Each L In LinesRequest.Lines
                    'what is the sum of orderes you have now in minutes
                    Dim LineOrders = From x In rOrder.PlanData Where x.TargetLineId = L.Id
                    If LineOrders IsNot Nothing AndAlso LineOrders.Count > 0 Then
                        Dim Ordered = LineOrders.Select(Function(x) x.Built).Sum
                        Dim Built = LineOrders.Select(Function(x) x.Built).Sum
                        Dim PPPHAvg = LineOrders.Select(Function(x) x.PPHPP).Average
                        Dim Resources = ((L.UserCount * PPPHAvg) / 60)
                        L.QueuedMinutes = ((Ordered - Built) * Resources) 'minutes worth of parts that have been ordered
                        If (L.QueuedMinutes / L.WorkBufferMinutes) < L.ReOrderPercentThreshold Then
                            Dim DeltaMin = (L.WorkBufferMinutes - L.QueuedMinutes) 'get hou many minutes that will be needed to fill the requirement
                            Dim PartsToOrder As Integer = DeltaMin / Resources 'translate this to the number of parts that will be needed
                            If PartsToOrder > 0 Then
                                Orderparts(PartsToOrder, LineOrders, L.WC)
                            End If
                        End If

                    End If
                Next
            End If


        End If
    End Sub


    Public Function Orderparts(QtyToOrder As Integer, ActiveOrders As List(Of PlanItem), Wc As String) As Integer
        Dim Qh As Integer
        Dim Exess = (From x In ActiveOrders Where x.Built < x.Ordered).ToArray
        If Exess IsNot Nothing AndAlso Exess.Count > 0 Then
            For Each E In Exess
                If QtyToOrder <= 0 Then Exit For
                Dim Remaining = E.Ordered - E.Built
                If QtyToOrder >= Remaining Then
                    'order remaining
                    OrderThisPart(Remaining, E, Wc)
                    If E.Status = PlanStatus.Planed Then
                        _SqlAccess.UpdatePlanStatus(E.OrderId, PlanStatus.Scheduled)
                    End If
                    E.Ordered += Remaining
                    QtyToOrder -= Remaining
                    Qh += Remaining
                Else
                    ' Order this
                    OrderThisPart(Remaining, E, Wc)
                    If E.Status = PlanStatus.Planed Then
                        _SqlAccess.UpdatePlanStatus(E.OrderId, PlanStatus.Scheduled)
                    End If
                    QtyToOrder -= Remaining
                    E.Ordered += Remaining
                    Qh += Remaining
                End If
            Next
        End If

        Return Qh
    End Function

    Public Function OrderThisPart(Qty As Integer, OrderItem As PlanItem, Wc As String) As Integer
        Dim Porder As New PartOrder()
        With Porder
            .Id = OrderItem.OrderId
            .partnumber = OrderItem.PartNumber
            .Qty = Qty
            .WC = Wc
            .PartRequestType = PartOrderType.Order
        End With

        Dim Result = _ErpAccess.CommitpartOrder(Porder)

        Return 1
    End Function


End Class
