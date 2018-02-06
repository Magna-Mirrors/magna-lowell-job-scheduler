
Imports System.Threading
Imports NLog
Imports Scheduler.core
Imports Scheduler.Program

Public NotInheritable Class LoggingService
    Implements iLoggingService

    ' Private Shared ReadOnly m_Instance As New Lazy(Of LoggingService)(Function() New LoggingService(), LazyThreadSafetyMode.ExecutionAndPublication)
    Private ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()
    Public Property ActivityId As Integer

    'Public Shared ReadOnly Property Instance() As LoggingService
    '    Get
    ' Return m_Instance.Value
    ' End Get
    ' End Property


    Public Sub New()

        ActivityBuffer() = New List(Of ActivityItem)

    End Sub
    Private Sub AddToActivityBuffer(Itm As ActivityItem)
        If ActivityBuffer.Count >= 300 Then
            ActivityBuffer.RemoveAt(299)
        End If
        ActivityBuffer.Add(Itm)
    End Sub



    Public Property ActivityBuffer() As List(Of ActivityItem)


    Public Function GetActivityItems(CutOff As DateTime) As List(Of ActivityItem) Implements iLoggingService.GetActivityItems
        If ActivityBuffer IsNot Nothing Then
            Return (From x In ActivityBuffer Where x.ActivityDate > CutOff Order By x.ActivityDate Descending).ToList
        Else
            Return Nothing
        End If
    End Function



    Private mEventArgs As LogEventArgs
    Public Sub SendAlert(Eventdata As LogEventArgs) Implements iLoggingService.SendAlert
        'Dim Logged As Boolean = False

        mEventArgs = Eventdata

        Console.Write(String.Format("{0}, {1}, Facet = {2}, msg = {3}", Now, Eventdata.LogType, Eventdata.Facet, Eventdata.Message))

        'If Not Eventdata.LogType = LogType.plcevent Then
        '    ActivityId += 1

        '    'If Eventdata.Ex IsNot Nothing Then
        '    '    AddToActivityBuffer(New ActivityItem(ActivityId, Eventdata.Facet, Eventdata.Message, Eventdata.Ex))
        '    'Else
        '    '    AddToActivityBuffer(New ActivityItem(ActivityId, Eventdata.Facet, Eventdata.Message, Eventdata.LogType))
        '    'End If

        'End If



        If Eventdata.Ex IsNot Nothing Then
            Logger.Log(New LogEventInfo(LogLevel.Error, Eventdata.Facet, Nothing, Eventdata.Message, Nothing, Eventdata.Ex))


        Else
            Dim Lv As LogLevel
            Select Case Eventdata.LogType
                Case LogType.Error
                    Lv = LogLevel.Error
                Case LogType.info
                    Lv = LogLevel.Info
                Case LogType.Success
                    Lv = LogLevel.Info
                Case LogType.Warn
                    Lv = LogLevel.Warn
                Case Else
                    Lv = LogLevel.Warn
            End Select
            Logger.Log(New LogEventInfo(Lv, Eventdata.Facet, Nothing, Eventdata.Message, Nothing))
        End If




        'If Eventdata.Options.HasFlag(NotifyOptions.Email) Then



        '    'If SendEmail Then
        '    '    If (mEventArgs.Facet.ToLower <> "email send error") Or (mEventArgs.Facet.ToLower <> "reportemailerror") Then
        '    '        If Eventdata.Body.Length = 0 Then
        '    '            Eventdata.Body = BuildBody("", Eventdata)
        '    '        End If
        '    '    End If
        '    'End If
        'End If

    End Sub

    Private Function BuildBody(IpAddress As String, Arg As LogEventArgs) As String
        Dim IpInfo As String = ""
        If IpAddress.Length > 0 Then
            IpInfo = "PLC Device Connection Data-----------------------------<Br>" &
                String.Format("Plc item Path = {0}<br>", Arg.SystemPath) &
                     String.Format("PcIpAddress = {0}<br>", IpAddress)
        End If

        Dim Bdy As String = String.Format("EventDate = {0}<br>", Arg.TimeStamp) &
                     IpInfo &
                     "Content-----------------------------------------<Br>" &
                     String.Format("MessageType = {0}<br>", Arg.LogType) &
                     String.Format("Facet = {0}<br>", Arg.Facet) &
                     "Message = " & Arg.Message
        Return Bdy.ToString
    End Function

End Class
