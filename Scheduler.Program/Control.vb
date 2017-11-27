Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports Autofac
Imports Autofac.Integration.Wcf

Public Class Control
    Private LogService As iLoggingService

    Public Sub New(LgSvc As iLoggingService)
        LogService = LgSvc
    End Sub

    Public Sub StartController(Con As IContainer)

        StartServiceHost(Con)
    End Sub


    Public Sub StopController()
        StopServiceHost()
    End Sub

    Private Sub StartServiceHost(Cont As IContainer)
        Try
            ' Dim svc = Cont.Resolve(Of SchedulerService)
            Dim baseAddress As New Uri("http://localhost:8045/SchedulerService")
            SvcHost = New ServiceHost(GetType(SchedulerService), baseAddress)
            Dim smb As New ServiceMetadataBehavior()
            smb.HttpGetEnabled = True
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15
            SvcHost.AddDependencyInjectionBehavior(Of core.iSchedulerService)(Cont)
            SvcHost.Description.Behaviors.Add(smb)
            SvcHost.Open()
            LogService.SendAlert(New Scheduler.core.LogEventArgs(Scheduler.core.LogType.info, "Scheduler service was started"))
        Catch ex As Exception
            LogService.SendAlert(New Scheduler.core.LogEventArgs("Service Host start", ex))
        End Try

    End Sub


    Public Sub StopServiceHost()
        Try
            If SvcHost IsNot Nothing Then
                SvcHost.Close()
                SvcHost = Nothing
            End If
        Catch ex As Exception
            LogService.SendAlert(New Scheduler.core.LogEventArgs("Err @ StopServiceHost ", ex))
        End Try

    End Sub
End Class
