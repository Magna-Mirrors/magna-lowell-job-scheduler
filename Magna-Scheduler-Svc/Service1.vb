Imports System.Threading
Imports Scheduler.Program
Public Class Service1

    Dim processController As Control
    Dim jTask As Threading.Thread
    Dim Are As New AutoResetEvent(False)
    Protected Overrides Sub OnStart(ByVal args() As String)

        jTask = New Thread(Sub()
                               Are.Reset()
                               processController = New Control()
                               Are.WaitOne()
                           End Sub)

        If processController Is Nothing Then
            jTask.Start()
        End If

    End Sub

    Protected Overrides Sub OnStop()

        processController.StopController()
        Are.Set()

    End Sub

End Class
