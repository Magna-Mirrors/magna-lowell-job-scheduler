Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Threading
Imports Autofac
Imports Autofac.Integration.Wcf

Public Class Control
    Private LogService As iLoggingService
    Private ct As New CancellationTokenSource()
    Private ReadOnly mErpHandler As BaanOrderHandling
    Private ErpTask As Task

    Public Sub New(LgSvc As iLoggingService, ErpHandling As BaanOrderHandling)
        LogService = LgSvc
        mErpHandler = ErpHandling
    End Sub

    Public Sub StartController(Con As IContainer)
        StartServiceHost(Con)
        StartOrderHandling()
    End Sub


    Public Sub StopController()
        StopOrderHandling()
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
            LogService.SendAlert(New Scheduler.core.LogEventArgs(Scheduler.core.LogType.info, "Scheduler service was started", "Service"))
        Catch ex As Exception
            LogService.SendAlert(New Scheduler.core.LogEventArgs("Service Host start", ex))
        End Try
    End Sub


    Public Sub StopServiceHost()
        Try
            If SvcHost IsNot Nothing Then
                SvcHost.Close()
                SvcHost = Nothing
                StopOrderHandling()
            End If
        Catch ex As Exception
            LogService.SendAlert(New Scheduler.core.LogEventArgs("Err @ StopServiceHost ", ex))
        End Try

    End Sub


    Public Sub StartOrderHandling()
		Try
			ErpTask = mErpHandler.RunAsync(ct.Token)
		Catch ex As Exception
			LogService.SendAlert(New Scheduler.core.LogEventArgs("StartOrderHandling Error ErpTask ", ex))
		Finally

		End Try

    End Sub

    Public Async Sub StopOrderHandling()
        ct.Cancel()
        If ErpTask IsNot Nothing Then
            Await ErpTask
        End If

    End Sub


End Class
