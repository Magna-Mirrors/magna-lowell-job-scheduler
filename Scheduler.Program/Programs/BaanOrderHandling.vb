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
    Private ReadOnly _Log As iLoggingService
    Private _IsRunning As Boolean
    Private _LastRan As Date
    Private _Tools As AppTools
    Public Property RunNow As Boolean
    Dim Busy As Boolean
    Private Sub Enque()

    End Sub

    Public ReadOnly Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
    End Property

    Public Async Function RunAsync(CT As CancellationToken) As Task

        _IsRunning = True
                            While (Not CT.IsCancellationRequested)
                                Await Task.Delay(10000)

            If Now.Subtract(_LastRan).TotalMinutes >= _Cfg.UpdateOrdersIntervalMinutes And Not Busy Then
                Try
                    Busy = True
                    _DataMgr.ProcessLineOrders()
                    _DataMgr.CommitBuildToProdOrders()
                    Dim Cfg = _Tools.GetProgramParams
                    _Cfg.UpdateOrdersIntervalMinutes = Cfg.UpdateOrdersIntervalMinutes
                Catch ex As Exception
                    _Log.SendAlert(New LogEventArgs("OrderHandling", ex))
                Finally
                    Busy = False
                End Try

                _LastRan = Now
            End If

            If RunNow Then
                                    RunNow = False
                                End If

                            End While

                            _IsRunning = False

    End Function


    Public Sub New(Dm As DataManager, Atools As AppTools, MsgSvc As iLoggingService)
        _Tools = Atools
        _Cfg = _Tools.GetProgramParams
        _DataMgr = Dm
        _LastRan = DateTime.MinValue
    End Sub

End Class
