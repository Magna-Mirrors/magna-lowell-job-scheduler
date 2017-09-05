Imports System.Threading
Imports Scheduler.core
Imports Scheduler.core.Classes
Imports System.Drawing

Public NotInheritable Class DataManager
    Implements iSchedulerService

    Private Shared ReadOnly m_Instance As New Lazy(Of DataManager)(Function() New DataManager(), LazyThreadSafetyMode.ExecutionAndPublication)
    Protected ReadOnly _LoggingService As iLoggingService
    Protected ReadOnly _SqlAccess As SqlData
    Protected ReadOnly _Tools As AppTools
    Private _Cfg As SvcParams
    'this is the goto spot got getting part information

    Public Shared ReadOnly Property Instance() As DataManager
        Get
            Return m_Instance.Value
        End Get
    End Property

    Public Sub New()


    End Sub

    Public Sub New(LgSvc As iLoggingService, Sql As SqlData, Atool As AppTools)
        _LoggingService = LgSvc
        _Tools = Atool
        _SqlAccess = Sql
        _Cfg = _Tools.GetProgramParams()
    End Sub

    Public Function GetLine(Lineid As Integer) As GetLineResponse Implements iSchedulerService.GetLine
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

    Public Function GetLines() As GetLinesResponse Implements iSchedulerService.GetLines
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

    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse Implements iSchedulerService.GetPlan
        Dim nPr As New GetPlanResponse
        If Sourcedata.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Return _SqlAccess.GetPlanData(Sourcedata.LineData)
        Else
            Dim PlanTxt As New TextData(_Tools, _Cfg)
            Return PlanTxt.getPlanData(Sourcedata.LineData)
        End If
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult Implements iSchedulerService.SavePlan
        Dim nPr As New TransactionResult
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            nPr = _SqlAccess.SavePlan(SourceData.PlanData)
        Else
            Dim txTools As New TextData
            nPr = txTools.SavePlan(SourceData)
        End If
        Return nPr
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse Implements iSchedulerService.ValidatePlanItems
        Dim Rslt As New ValidatePartsResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.ValidateParts(SourceData)
        Else
            Dim mData As New MdbData
            Rslt = mData.ValidateParts(SourceData)
        End If
        Return Rslt
    End Function

    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse Implements iSchedulerService.GetpartsForLine
        Dim Rslt As New getPartsforLineResponse
        If SourceData.LineData.SchedulerMethod = SchedulerMethods.MsSql Then
            Rslt = _SqlAccess.GetParts(SourceData)
        Else
            Dim mData As New MdbData
            Rslt = mData.GetParts(SourceData)
        End If
        Return Rslt
    End Function
End Class
