Imports Scheduler.core

Public Interface iLoggingService
    Sub SendAlert(Eventdata As LogEventArgs)
    Function GetActivityItems(CutOff As DateTime) As List(Of ActivityItem)
End Interface
