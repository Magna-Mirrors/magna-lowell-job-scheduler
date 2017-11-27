Imports System.ServiceModel
Imports Ninject

Module Root
    Public Controller As Control

    Public SvcHost As ServiceHost

    Public Sub OnStart()

        '   Controller.StartController()
    End Sub

    Public Sub OnStop()
        If Controller IsNot Nothing Then
            Controller.StopController()
        End If
    End Sub
End Module
