Imports Scheduler.core
Public Class DataManager

    Protected ReadOnly _LoggingService As iLoggingService
    Protected ReadOnly _SqlAccess As SqlData
    Protected ReadOnly _MdbAccess As MdbData
    Protected ReadOnly _Tools As AppTools
	Protected ReadOnly _ErpAccess As ErpSql

	Private _Cfg As SvcParams


	Public Sub New(LgSvc As iLoggingService, Atool As AppTools, SqlSvc As SqlData, MdbAccess As MdbData, ErpAccess As ErpSql)
		Dim Prm = Atool.GetProgramParams
		_Tools = Atool
		_Cfg = Prm
		_MdbAccess = MdbAccess
		_LoggingService = LgSvc
		_SqlAccess = SqlSvc
		_ErpAccess = ErpAccess

	End Sub

	Public Property RequestingUpdateNow As Boolean

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

	Public Sub ResetRequestingUpdateNow()
		RequestingUpdateNow = False
	End Sub

	Public Function GetSqlLines() As List(Of Line)
        Dim Rslt As New GetLinesResponse
        Try
            Rslt = _SqlAccess.GetLinesData(True)
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = "GetLines Error " & ex.Message
        End Try

        Return Rslt.Lines
    End Function



    Public Function getLineUserCount(LineId As Integer) As Integer
        Return _SqlAccess.GetUsersOnLine(LineId)
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
        Dim nPr As New GetPlanResponse With {.Result = -1}
        If Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Try
                nPr = _SqlAccess.GetActiveOrders(Sourcedata)
                nPr.Result = 1
            Catch ex As Exception
                nPr.ResultString = "GetPlan -> MsSql Exception :::: " + ex.Message
            End Try
        ElseIf Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MdbAndPlanFiles Then
            Dim PlanTxt As New TextData(_Tools, _Cfg)
            Try
                nPr = PlanTxt.getPlanData(Sourcedata.LineData)
                nPr.Result = 1
            Catch ex As Exception
                nPr.ResultString = "GetPlan -> MdbAndPlanFiles Exception :::: " + ex.Message
            End Try
        Else
            nPr.ResultString = "SchedulerMethod must be MsSql OR MdbAndPlanFiles"
        End If
        Return nPr
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult
        Dim nPr As New TransactionResult
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
			nPr = _SqlAccess.SavePlan(SourceData.PlanData)
			RequestingUpdateNow = True
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
        If SourceData.LineId > 0 Then
            Dim Sd = GetScheduledOrders(SourceData.LineId)
            If Sd IsNot Nothing AndAlso Sd.Any() Then
                Rslt.Item = Sd(0)
                Rslt.Result = 1
            Else
                Rslt.Result = 0
                Rslt.ResultString = "No Scheduled Orders"
            End If
        End If
        Return Rslt
    End Function

    Public Function CommitBuildToProdOrders() As Integer
        Dim BuiltItems = _SqlAccess.GetActiveOrders
        Dim Rslt = _ErpAccess.Commit_Built_Items_To_ProdCounts(BuiltItems)
        If Rslt.Result > 0 Then
            Return _SqlAccess.Update_PartBuilt_Postings(BuiltItems.Min(Function(x) x.BuiltId), BuiltItems.Max(Function(x) x.BuiltId))
        End If
        Return 0
    End Function


	Public Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult
		Dim Rslt As New SkipOrderResult
		'GetOrderId
		If SourceData.Lineid > 0 Then
			Dim Sd = GetScheduledOrders(SourceData.Lineid)       'get all orders that are scheduled
			If Sd.Count > 0 Then
				Dim Mx = (From x In Sd.Select(Function(x) x.Position)).Max

				If Sd IsNot Nothing AndAlso Sd.Any() Then
					Sd(0).Position = Mx + 1                                                          'set position to found plan list count
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
				End If

		Else
				Rslt.ResultString = "No Scheduled Orders"
			End If
		End If
		Return Rslt
	End Function

	Public Function SuspendThisorder(SourceData As SuspendOrderRequest) As SuspendOrderResult
		Dim Rslt As New SuspendOrderResult
		If SourceData.OrderId > 0 Then
			Dim Ts As Long = _Tools.GetUnitxTimeStamp + 1000
			If _SqlAccess.UpdateOrderstatus(SourceData.OrderId, PlanStatus.Suspended, Ts) > 0 Then
				RequestingUpdateNow = True
			End If
		End If
		Return Rslt
	End Function

	Public Function UnSuspendThisorder(SourceData As SuspendOrderRequest) As SuspendOrderResult
		Dim Rslt As New SuspendOrderResult
		If SourceData.OrderId > 0 Then
			Dim Ts As Long = _Tools.GetUnitxTimeStamp + 1000
			If _SqlAccess.UpdateOrderstatus(SourceData.OrderId, PlanStatus.Scheduled, Ts) > 0 Then
				RequestingUpdateNow = True
			End If
		End If
		Return Rslt
	End Function

	Public Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult
        Dim Rslt = New RemoveOrderResult

        Dim WipItem = _SqlAccess.GetWipOrders(SourceData.OrderId).FirstOrDefault
        Try
            If WipItem IsNot Nothing Then
                Dim PrtOrder As New core.PartOrder
                With PrtOrder
                    .partnumber = WipItem.PartNumber
                    .Qty = WipItem.Ordered * -1
                    .WC = WipItem.WorkCell
                End With

                If PrtOrder.Qty <> 0 Then
                    Dim ErpRslt As Integer = _ErpAccess.Commit_To_PartCounts_Table(PrtOrder).Result
                    If ErpRslt > 0 Then
                        WipItem.RequestOrderQty = (WipItem.Ordered * -1)
                        If _SqlAccess.LogPartOrder(WipItem) > 0 Then

                        End If
                    End If
                End If

				Dim F_Rslt = _SqlAccess.updateJobStatus(SourceData.OrderId, PlanStatus.Removed)
                Rslt.Result = F_Rslt.Result
				Rslt.ResultString = F_Rslt.ResultString

				RequestingUpdateNow = True
			End If

        Catch ex As Exception
            _LoggingService.SendAlert(New LogEventArgs("Ordering Part", ex))
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try

        Return Rslt
    End Function


    Public Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult
        Dim Rslt As New GetScheduleResult
        If SourceData.LineId > 0 Then
            Dim Sd = GetScheduledOrders(SourceData.LineId)
            Rslt.Items = Sd.ToList()
            Rslt.Result = 1
        Else
            Rslt.Result = 0
            Rslt.ResultString = "No Scheduled Items for this line"
        End If
        Return Rslt
    End Function



    Public Sub ProcessLineOrders()
        Dim CurrentWip As List(Of WipOrder) = _SqlAccess.GetWipOrders(0)
        If CurrentWip IsNot Nothing AndAlso CurrentWip.Count > 0 Then
            Dim Lines = CurrentWip.Select(Function(x) x.LineId).Distinct.ToList
            For Each L In Lines
                Dim UserCnt As Integer = getLineUserCount(L)
				Dim Witms = From x In CurrentWip Where x.LineId = L
				Dim OrdersWithUnOrderedparts = From x In CurrentWip Where x.LineId = L AndAlso (x.Ordered < x.TargetQty)
				If UserCnt <= 0 Then UserCnt = 1 'minimum user count needs to be 1 for patislpating line
                If Witms IsNot Nothing AndAlso Witms.Count > 0 Then
					Dim PPPP As Double = Witms.Select(Function(x) x.PartsPerHourPerPerson).Average
					Dim SchedOrd = From X In Witms Where X.Status = PlanStatus.Scheduled
					Dim PartHoursOnHand As Double = CType((((SchedOrd.Select(Function(x) x.Ordered).Sum) - SchedOrd.Select(Function(x) x.Built).Sum) / PPPP) / UserCnt, Double)
					If PartHoursOnHand < 0 Then
                        PartHoursOnHand = 0
                    End If
					Dim WipHours = Witms.Select(Function(x) x.WipHours).FirstOrDefault
					Dim Delta As Double = WipHours - PartHoursOnHand
                    Dim PartsNeeded As Integer = CInt(Delta * (PPPP * UserCnt))
					Dim TriggerLevel = WipHours * SchedOrd.Select(Function(x) x.ReOrderAtPercent).FirstOrDefault
					If (PartsNeeded > 0) AndAlso (PartHoursOnHand <= TriggerLevel) Then 'andalso (PartHoursOnHand >= 0) 
                        For Each W In OrdersWithUnOrderedparts
                            Try
                                Dim MaxPosition As Long = 0
                                Dim SItms = (From V In CurrentWip Where V.LineId = L AndAlso V.Status = PlanStatus.Scheduled)
                                If SItms IsNot Nothing AndAlso SItms.Any Then
                                    MaxPosition = SItms.Select(Function(x) x.Position).Max
                                End If
                                PartsNeeded -= OrderThisPart(W, PartsNeeded, MaxPosition)
                                If PartsNeeded <= 0 Then Exit For
                            Catch ex As Exception
                                _LoggingService.SendAlert(New LogEventArgs("Processing Orders", ex))
                            End Try
                        Next
                    End If

                End If
                'move on to the next line
            Next
        End If
        'get all orders that are open and ordered Counts less than target Counts

    End Sub


    Public Function OrderThisPart(Itm As WipOrder, Required As Integer, MaxPos As Long) As Integer
        Try
            Dim Porder As New PartOrder()
            Dim Qty As Integer = 0
            Dim D = Itm.TargetQty - If(Itm.Ordered >= Itm.Built, Itm.Ordered, Itm.Built)
            If D >= Required Then
                Qty = Required
            ElseIf D > 0 Then
                Qty = D
            End If



            If Qty > 0 Then
                With Porder
                    .Id = Itm.OrderId
                    .partnumber = Itm.PartNumber
                    .Qty = Qty
                    .WC = Itm.WorkCell
                    .PartRequestType = PartOrderType.Order
                End With
                Dim Result As TransactionResult = _ErpAccess.Commit_To_PartCounts_Table(Porder)

                If Result.Result = 1 Then
                    Itm.RequestOrderQty = Qty
                    If _SqlAccess.LogPartOrder(Itm) > 0 Then
                        Itm.Ordered = Itm.Ordered + Itm.RequestOrderQty
                        Itm.RequestOrderQty = 0
                        If Itm.Status = PlanStatus.Planed Then
                            ' GetAllSettings max position from orderes that are scheduled add 1 for this one if there isnt on then leave the position

                            If MaxPos = 0 Then
                                MaxPos = Itm.Position
                            Else
                                MaxPos += 1
                            End If

                            If _SqlAccess.UpdateOrderstatus(Itm.OrderId, PlanStatus.Scheduled, MaxPos) > 0 Then
                                Itm.Status = PlanStatus.Scheduled
                            End If
                        End If
                        Return Qty
                    End If
                End If
            End If
            Return 0
        Catch ex As Exception
            _LoggingService.SendAlert(New LogEventArgs("Ordering Part", ex))
            Return 0
        End Try

    End Function

    'TODO: test this
    Private Function GetScheduledOrders(lineid As Integer) As List(Of PlanItem)
        If lineid <= 0 Then
            Throw New ArgumentException("Line Id must be greater than zero.", NameOf(lineid))
        End If

        Dim Ln As New Line With {.Id = lineid}
        Dim L As New GetPlanRequest(Ln)

        Dim Resp = _SqlAccess.GetActiveOrders(L)

        If Resp.Result <> 1 Then
            Dim plans = (From x In Resp.PlanData Where x.Status = PlanStatus.Scheduled).ToList()
            Return plans
        End If
        Return Nothing
    End Function

End Class
