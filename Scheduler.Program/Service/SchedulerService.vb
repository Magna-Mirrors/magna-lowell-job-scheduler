Imports Scheduler.core

Public Class SchedulerService
    Implements iSchedulerService


    Public Sub New()

    End Sub


    Private Function SchedulerService_GetLine(Lineid As Integer) As GetLineResponse Implements iSchedulerService.GetLine
        Return DataManager.Instance.GetLine(Lineid)
    End Function

    Private Function SchedulerService_GetLines() As GetLinesResponse Implements iSchedulerService.GetLines
        Return DataManager.Instance.GetLines()
    End Function

    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse Implements iSchedulerService.GetPlan
        Return DataManager.Instance.GetPlan(Sourcedata)
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult Implements iSchedulerService.SavePlan
        Return DataManager.Instance.SavePlan(SourceData)
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse Implements iSchedulerService.ValidatePlanItems
        Return DataManager.Instance.ValidatePlanItems(SourceData)
    End Function


    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse Implements iSchedulerService.GetpartsForLine
        Return DataManager.Instance.GetpartsForLine(SourceData)
    End Function
End Class
