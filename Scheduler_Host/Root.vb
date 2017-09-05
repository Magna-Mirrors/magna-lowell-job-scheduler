Imports System.Threading
Imports Scheduler.Program

Module Root
    Dim jTask As Threading.Thread
    Dim Are As New AutoResetEvent(False)
    Dim Ccont As Control


    Sub Main()

        Dim S As String = ""
        System.Console.WriteLine("Starting Scheduler Service")
        Try
            OnStart()
        Catch ex As Exception

        End Try

        System.Console.WriteLine("Type EXIT to Stop Scheduler Service")
        While UCase(S) <> "EXIT"
            S = System.Console.ReadLine
        End While
        OnStop()

        Environment.Exit(-1)
    End Sub


    Private Sub OnStart()
        Try
            Ccont = New Control
            Ccont.StartController()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub OnStop()
        Try
            Ccont.StopController()
        Catch ex As Exception

        End Try

    End Sub
End Module