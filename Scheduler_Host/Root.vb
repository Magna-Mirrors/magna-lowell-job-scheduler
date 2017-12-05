Imports System.Threading
Imports Scheduler.Program
Imports Autofac
Imports Scheduler.core

Module Root
    Dim jTask As Threading.Thread
    Dim Are As New AutoResetEvent(False)
    Dim Ccont As Control
    Dim Container As IContainer
    Dim d


    Sub Main()

        Dim S As String = ""
        System.Console.WriteLine("Starting Scheduler Service")

        OnStart()


        System.Console.WriteLine("Type EXIT to Stop Scheduler Service")
        While UCase(S) <> "EXIT"
            S = System.Console.ReadLine
        End While
        OnStop()

        Environment.Exit(-1)
    End Sub


    Private Sub OnStart()
        Try

            Dim Builder As New Autofac.ContainerBuilder
            Builder.RegisterType(Of Control)
            Builder.RegisterType(Of LoggingService).As(Of iLoggingService)().SingleInstance()
            Builder.RegisterType(Of AppTools).SingleInstance()
            Builder.RegisterType(Of SqlData).SingleInstance()
            Builder.RegisterType(Of DataManager).SingleInstance()
            Builder.RegisterType(Of SchedulerService)().As(Of iSchedulerService)()
            Builder.RegisterType(Of SvcParams)().SingleInstance()
            Builder.RegisterType(Of MdbData).SingleInstance()
            Builder.RegisterType(Of BaanOrderHandling).SingleInstance()
            Builder.RegisterType(Of ErpSql).SingleInstance()

            Container = Builder.Build()


            Ccont = Container.Resolve(Of Control)
            Ccont.StartController(Container)
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