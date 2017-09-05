Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports Scheduler.core

Public Class Control

    Public Sub New()

    End Sub

    Public Sub StartController()
        StartServiceHost()
    End Sub


    Public Sub StopController()
        StopServiceHost()
    End Sub

    Private Sub StartServiceHost()
        Try
            Dim baseAddress As New Uri("http://localhost:8045/SchedulerService")
            SvcHost = New ServiceHost(GetType(SchedulerService), baseAddress)
            Dim smb As New ServiceMetadataBehavior()
            smb.HttpGetEnabled = True
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15
            SvcHost.Description.Behaviors.Add(smb)

            SvcHost.Open()
            LoggingService.Instance.SendAlert(New LogEventArgs(LogType.info, "Scheduler service was started"))
        Catch ex As Exception
            LoggingService.Instance.SendAlert(New LogEventArgs("Service Host start", ex))
        End Try

    End Sub


    Public Sub StopServiceHost()
        Try
            If SvcHost IsNot Nothing Then
                SvcHost.Close()
                SvcHost = Nothing
            End If
        Catch ex As Exception
            LoggingService.Instance.SendAlert(New LogEventArgs("Err @ StopServiceHost ", ex))
        End Try

    End Sub
End Class
