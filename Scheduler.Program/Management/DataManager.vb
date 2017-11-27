Imports System.Threading
Imports Scheduler.core
Imports Scheduler.core.Classes
Imports System.Drawing

Public NotInheritable Class DataManager
    ' Implements iSchedulerService

    '   Private Shared ReadOnly m_Instance As New Lazy(Of DataManager)(Function() New DataManager(), LazyThreadSafetyMode.ExecutionAndPublication)
    Protected ReadOnly _LoggingService As iLoggingService
    Protected ReadOnly _SqlAccess As SqlData
    Protected ReadOnly _MdbAccess As MdbData
    Protected ReadOnly _Tools As AppTools

    Private _Cfg As SvcParams
    'this is the goto spot got getting part information

    '  Public Shared ReadOnly Property Instance() As DataManager
    ' Get
    '   Return m_Instance.Value
    '  End Get
    '   End Property



    Public Sub New(LgSvc As iLoggingService, Atool As AppTools, SqlSvc As SqlData, MdbAccess As MdbData)
        Dim Prm = Atool.GetProgramParams
        _Tools = Atool
        _Cfg = Prm
        _MdbAccess = MdbAccess
        _LoggingService = LgSvc
        _SqlAccess = SqlSvc

    End Sub

    Public Function GetLine(Lineid As Integer) As GetLineResponse
        Dim Rslt As New GetLineResponse
        Try
            Rslt.LineInfo = _SqlAccess.GetLineData(Lineid)
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try

        Return Rslt
    End Function

    Public Function GetLines() As GetLinesResponse
        Dim Rslt As New GetLinesResponse
        Try
            Rslt = _SqlAccess.GetLinesData()
            Rslt.Result = 1
        Catch ex As Exception
            Rslt.Result = -1
            Rslt.ResultString = "GetLines Error " & ex.Message
        End Try

        Return Rslt
    End Function

    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse
        Dim nPr As New GetPlanResponse
        Try
            If Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
                nPr = _SqlAccess.GetPlanData(Sourcedata.LineData.Id)
            Else
                Dim PlanTxt As New TextData(_Tools, _Cfg)
                nPr = PlanTxt.getPlanData(Sourcedata.LineData)
            End If
        Catch ex As Exception

        End Try
        Return nPr
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult
        Dim nPr As New TransactionResult
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            nPr = _SqlAccess.SavePlan(SourceData.PlanData)
        Else
            Dim txTools As New TextData
            nPr = txTools.SavePlan(SourceData)
        End If
        Return nPr
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse
        Dim Rslt As New ValidatePartsResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.ValidateParts(SourceData)
        Else

            Rslt = _MdbAccess.ValidateParts(SourceData)
        End If
        Return Rslt
    End Function

    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Rslt As New getPartsforLineResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.GetParts(SourceData)
        Else

            Rslt = _MdbAccess.GetParts(SourceData)
        End If
        Return Rslt
    End Function

    Public Function GetNextOrder(SourceData As GetNextOrderRequest) As GetNextOrderResult
        Dim Rslt As New GetNextOrderResult
        If SourceData.LineId Then
            Dim Resp = _SqlAccess.GetPlanData(SourceData.LineId)
            Dim Sd = From x In Resp.PlanData Where x.Status = PlanStatus.Scheduled
            If Sd IsNot Nothing AndAlso Sd.Count > 0 Then
                Rslt.Item = Sd(0)
                Rslt.Result = 1
            Else
                Rslt.Result = 0
                Rslt.ResultString = "No Scheduled Orders"
            End If
        End If
        Return Rslt
    End Function

    Public Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult
        Dim Rslt As New SkipOrderResult
        'GetOrderId
        If SourceData.Lineid Then
            Dim Resp = _SqlAccess.GetPlanData(SourceData.Lineid)
            Dim Sd = From x In Resp.PlanData Where x.Status = PlanStatus.Scheduled
            If Sd IsNot Nothing AndAlso Sd.Count > 0 Then
                Sd(0).Position = Sd.Count
                _SqlAccess.updateJobPosition(Sd(0).OrderId, Sd(0).Position)
                If Sd.Count = 1 Then
                    Rslt.Item = Sd(0)
                    Rslt.Result = 1
                Else
                    For i = 1 To Sd.Count - 1
                        _SqlAccess.updateJobPosition(Sd(i).OrderId, Sd(i).Position)
                        If i = 1 Then
                            Rslt.Item = Sd(i)
                            Rslt.Result = 1
                        End If
                    Next
                End If
            Else
                Rslt.ResultString = "No Scheduled Orders"
            End If
        End If
        Return Rslt
    End Function

    Public Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult
        Dim Rslt = New RemoveOrderResult
        Dim F_Rslt = _SqlAccess.updateJobStatus(SourceData.OrderId, PlanStatus.Removed)
        Rslt.Result = F_Rslt.Result
        Rslt.ResultString = F_Rslt.ResultString
        Return Rslt
    End Function

    Public Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult
        Dim Rslt As New GetScheduleResult
        If SourceData.LineId > 0 Then
            Dim Sd = From x In _SqlAccess.GetPlanData(SourceData.LineId).PlanData Where x.Status = PlanStatus.Scheduled
            Rslt.Items = Sd
            Rslt.Result = 1
        Else
            Rslt.Result = 0
            Rslt.ResultString = "No Scheduled Items for this line"
        End If
        Return Rslt
    End Function
End Class
