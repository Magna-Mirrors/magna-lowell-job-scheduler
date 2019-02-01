Imports Scheduler.core

Public Class SchedulerService
    Implements iSchedulerService
    Private ReadOnly DataMgr As DataManager

    Public Sub New(Dm As DataManager)
        DataMgr = Dm
    End Sub


    Private Function SchedulerService_GetLine(Lineid As Integer) As GetLineResponse Implements iSchedulerService.GetLine
        Return DataMgr.GetLine(Lineid)
    End Function

    Private Function SchedulerService_GetLines() As GetLinesResponse Implements iSchedulerService.GetLines
        Return DataMgr.GetLines()
    End Function

    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse Implements iSchedulerService.GetPlan
        Return DataMgr.GetPlan(Sourcedata)
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult Implements iSchedulerService.SavePlan
        Return DataMgr.SavePlan(SourceData)
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse Implements iSchedulerService.ValidatePlanItems
        Return DataMgr.ValidatePlanItems(SourceData)
    End Function


    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse Implements iSchedulerService.GetpartsForLine
        Return DataMgr.GetpartsForLine(SourceData)
    End Function

    Public Function GetNextOrder(SourceData As GetNextOrderRequest) As GetNextOrderResult Implements iSchedulerService.GetNextOrder
        Return DataMgr.GetNextOrder(SourceData)
    End Function


    'skip this order forces ia scheduled item to the back of the scheduled Queue
    Public Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult Implements iSchedulerService.SkipThisorder
        Return DataMgr.SkipThisorder(SourceData)
    End Function

    Public Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult Implements iSchedulerService.RemoveThisorder
        Return DataMgr.RemoveThisorder(SourceData)
    End Function

    Public Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult Implements iSchedulerService.GetLineSchedule
        Return DataMgr.GetLineSchedule(SourceData)
    End Function

	Public Function SuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult Implements iSchedulerService.SuspendOrder
		Return DataMgr.SuspendThisorder(SourceData)
	End Function

	Public Function UnSuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult Implements iSchedulerService.UnSuspendOrder
		Return DataMgr.UnSuspendThisorder(SourceData)
	End Function
End Class
