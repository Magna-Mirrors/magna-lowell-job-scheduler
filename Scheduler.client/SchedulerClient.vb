﻿Imports System.ServiceModel
Imports Scheduler.core
Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels
Imports System

Public Class SchedulerClient
    Inherits System.ServiceModel.ClientBase(Of iSchedulerService)
    Implements core.iSchedulerService

    Protected Sub New()

    End Sub
    Protected Sub New(ByVal endpointConfigurationName As String)
        MyBase.New(endpointConfigurationName)

    End Sub
    Protected Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)

    End Sub
    Protected Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As EndpointAddress)
        MyBase.New(endpointConfigurationName, remoteAddress)

    End Sub
    Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As EndpointAddress)
        MyBase.New(binding, remoteAddress)

    End Sub
    Protected Sub New(ByVal endpoint As System.ServiceModel.Description.ServiceEndpoint)
        MyBase.New(endpoint)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext)
        MyBase.New(callbackInstance)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext, ByVal endpointConfigurationName As String)
        MyBase.New(callbackInstance, endpointConfigurationName)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext, ByVal endpointConfigurationName As String, ByVal remoteAddress As EndpointAddress)
        MyBase.New(callbackInstance, endpointConfigurationName, remoteAddress)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext, ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As EndpointAddress)
        MyBase.New(callbackInstance, binding, remoteAddress)

    End Sub
    Protected Sub New(ByVal callbackInstance As InstanceContext, ByVal endpoint As System.ServiceModel.Description.ServiceEndpoint)
        MyBase.New(callbackInstance, endpoint)

    End Sub

    Public Function GetLine(Lineid As Integer) As GetLineResponse Implements iSchedulerService.GetLine
        Return MyBase.Channel.GetLine(Lineid)
    End Function

    Public Function GetLines() As GetLinesResponse Implements iSchedulerService.GetLines
        Return MyBase.Channel.GetLines()
    End Function

    Public Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse Implements iSchedulerService.GetpartsForLine
        Return MyBase.Channel.GetpartsForLine(SourceData)
    End Function

    Public Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse Implements iSchedulerService.GetPlan
        Try
            Return MyBase.Channel.GetPlan(Sourcedata)
        Catch ex As Exception
            Dim R As New GetPlanResponse
            R.Result = -1
            R.ResultString = ex.Message
            Return R
        End Try

    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult Implements iSchedulerService.SavePlan
        Return MyBase.Channel.SavePlan(SourceData)
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse Implements iSchedulerService.ValidatePlanItems
        Return MyBase.Channel.ValidatePlanItems(SourceData)
    End Function

    Public Function GetNextOrder(SourceData As GetNextOrderRequest) As GetNextOrderResult Implements iSchedulerService.GetNextOrder
        Return MyBase.Channel.GetNextOrder(SourceData)
    End Function

    Public Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult Implements iSchedulerService.SkipThisorder
        Return MyBase.Channel.SkipThisorder(SourceData)
    End Function

    Public Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult Implements iSchedulerService.RemoveThisorder
        Return MyBase.Channel.RemoveThisorder(SourceData)
    End Function

    Public Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult Implements iSchedulerService.GetLineSchedule
        Return MyBase.Channel.GetLineSchedule(SourceData)
    End Function

	Public Function SuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult Implements iSchedulerService.SuspendOrder
		Return MyBase.Channel.SuspendOrder(SourceData)
	End Function

	Public Function UnSuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult Implements iSchedulerService.UnSuspendOrder
		Return MyBase.Channel.UnSuspendOrder(SourceData)
	End Function

	Public Function UpdatePartsFromXmlSupportFilesNow(Source As UpdatePartInfoFromSupportFilesNowRequest) As TransactionResult Implements iSchedulerService.UpdatePartsFromXmlSupportFilesNow
		Return MyBase.Channel.UpdatePartsFromXmlSupportFilesNow(Source)
	End Function
End Class
