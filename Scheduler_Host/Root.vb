Imports System.Threading
Imports Scheduler.Program
Imports Autofac
Imports Scheduler.core
Imports System

Module Root
    Dim jTask As Threading.Thread
    Dim Are As New AutoResetEvent(False)
    Dim Ccont As Control
    Dim Container As IContainer



    Sub Main()

        Dim S As String = ""
        Console.WriteLine("Starting Scheduler Service")

        OnStart()


        System.Console.WriteLine(vbNewLine + "Type EXIT to Stop Scheduler Service")
        While S?.ToUpper() <> "EXIT"
            S = System.Console.ReadLine
        End While
        OnStop()

        Environment.Exit(-1)
    End Sub


    Private Sub OnStart()
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
		Builder.RegisterType(Of PartInformationUpdateManager).SingleInstance()

		Container = Builder.Build()

        Ccont = Container.Resolve(Of Control)
        Ccont.StartController(Container)
    End Sub

    Private Sub OnStop()
        Ccont.StopController()
    End Sub


End Module