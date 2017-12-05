Imports System.Threading
Imports Scheduler.core
Public Class BaanOrderHandling
    'this class will be used to monitor activity from the lines based on the following items
    'every 5 minutes get a view of the order status for lines that are subscribing to this service.
    'have trigger here is a line removes and order that the this will update that line right away
    'or have an interval us it as A timed task for each line

    '1: sum parts ordered and total minutes for open orders
    '2: sum parts built for open orders and total time 
    '3; get number of users on the line
    '4: calculate number of parts needed to meet the work buffer
    '5: order and note parts from BAAN mark them down in order history ordered 
    '6: get remaining parts due in scheduled items in the build order
    '7: work into and convert the planed items into scheduled items
    Private ReadOnly _DataMgr As DataManager
    Private ReadOnly _Cfg As SvcParams
    Private _IsRunning As Boolean
    Private _LastRan As Date
    Public Property RunNow As Boolean




    Private Sub Enque()

    End Sub

    Public ReadOnly Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
    End Property



    Public Function RunAsync(CT As CancellationToken) As Task
        Return Task.Run(Async Function()
                            _IsRunning = True
                            While (Not CT.IsCancellationRequested)
                                Await Task.Delay(100)

                                If Now.Subtract(_LastRan).TotalMinutes >= _Cfg.UpdateOrdersIntervalMinutes Then
                                    _DataMgr.ProcessOrders()
                                End If

                                If RunNow Then
                                    RunNow = False
                                End If

                            End While
                            'Timer Or task
                            'tool for task
                            _IsRunning = False
                        End Function)
    End Function






    Public Sub New(Dm As DataManager, Atools As AppTools)
        _Cfg = Atools.GetProgramParams
        _DataMgr = Dm
        _LastRan = DateTime.MinValue
    End Sub



    Public Sub ProcessLineOrders()


    End Sub





End Class
