Imports System.ServiceModel
Imports Ninject

Module Root
    Public Controller As Control
    Public nKernel As IKernel
    Public SvcHost As ServiceHost

    Public Sub OnStart()
        Root.nKernel = New StandardKernel(New nBinding())
        Controller = nKernel.Get(Of Control)
        Controller.StartController()
    End Sub

    Public Sub OnStop()
        If Controller IsNot Nothing Then
            Controller.StopController()
        End If
    End Sub
End Module
