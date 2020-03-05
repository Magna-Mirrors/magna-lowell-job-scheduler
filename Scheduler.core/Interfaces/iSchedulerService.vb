Imports System.ServiceModel

<ServiceContract()>
Public Interface iSchedulerService
    <OperationContract()>
    Function GetLine(Lineid As Integer) As GetLineResponse
    <OperationContract()>
    Function GetLines() As GetLinesResponse
    <OperationContract()>
    Function GetPlan(Sourcedata As GetPlanRequest) As GetPlanResponse
    <OperationContract()>
    Function SavePlan(SourceData As SavePlanRequest) As TransactionResult
    <OperationContract()>
    Function ValidatePlanItems(SourceData As ValidatePartsRequest) As ValidatePartsResponse
    <OperationContract()>
    Function GetpartsForLine(SourceData As GetPartsForLineRequest) As getPartsforLineResponse
    <OperationContract()>
    Function GetNextOrder(SourceData As GetNextOrderRequest) As GetNextOrderResult
    <OperationContract()>
    Function SkipThisorder(SourceData As SkipOrderRequest) As SkipOrderResult
    <OperationContract()>
    Function RemoveThisorder(SourceData As RemoveOrderRequest) As RemoveOrderResult
	<OperationContract()>
	Function GetLineSchedule(SourceData As GetScheduleRequest) As GetScheduleResult
	<OperationContract()>
	Function SuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult
	<OperationContract()>
	Function UnSuspendOrder(SourceData As SuspendOrderRequest) As SuspendOrderResult
	<OperationContract()>
	Function UpdatePartsFromXmlSupportFilesNow(Source As UpdatePartInfoFromSupportFilesNowRequest) As TransactionResult
End Interface
