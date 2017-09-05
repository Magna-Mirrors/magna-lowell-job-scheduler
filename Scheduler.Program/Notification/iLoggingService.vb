

Public Interface iLoggingService
    Sub SendAlert(Eventdata As Scheduler.core.LogEventArgs)
    Function GetActivityItems(CutOff As DateTime) As List(Of Scheduler.core.ActivityItem)
End Interface
