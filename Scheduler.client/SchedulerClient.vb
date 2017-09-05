Imports System.ServiceModel
Imports Scheduler.core
Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels

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
        Return MyBase.Channel.GetPlan(Sourcedata)
    End Function

    Public Function SavePlan(SourceData As SavePlanRequest) As TransactionResult Implements iSchedulerService.SavePlan
        Return MyBase.Channel.SavePlan(SourceData)
    End Function

    Public Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse Implements iSchedulerService.ValidatePlanItems
        Return MyBase.Channel.ValidatePlanItems(SourceData)
    End Function
End Class
