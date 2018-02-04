Imports System.Threading
Imports Scheduler.Program
Imports Autofac
Imports Scheduler.core
Imports System

Public Class Service1

    Dim jTask As Threading.Thread
    Dim Are As New AutoResetEvent(False)


    Dim Ccont As Control
    Private Con_tainer As IContainer

    Protected Overrides Sub OnStart(ByVal args() As String)

        jTask = New Thread(Sub()
                               Are.Reset()
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

                               Con_tainer = Builder.Build()

                               Ccont = Con_tainer.Resolve(Of Control)
                               Ccont.StartController(Con_tainer)
                               Are.WaitOne()
                           End Sub)

        If Ccont Is Nothing Then
            jTask.Start()
        End If

    End Sub

    Protected Overrides Sub OnStop()
        Ccont.StopController()
        Are.Set()
    End Sub





End Class
