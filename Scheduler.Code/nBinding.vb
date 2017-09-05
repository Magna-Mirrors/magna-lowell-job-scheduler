

Imports Scheduler.core

Public Class nBinding
    Inherits Ninject.Modules.NinjectModule

    Public Overrides Sub Load()
        '  Bind(Of iPartData).To(Of PartData)()
        ' Bind(Of iPlanData).To(Of PlanData)()
        Bind(Of iLoggingService).To(GetType(LoggingService)).InSingletonScope()
        Bind(Of iSchedulerService).To(Of SchedulerService)()
        Bind(Of AppTools).To(Of AppTools)()
        Bind(Of SvcParams).To(Of SvcParams)()

        Bind(Of AppTools).To(Of AppTools).InSingletonScope()
    End Sub
End Class
