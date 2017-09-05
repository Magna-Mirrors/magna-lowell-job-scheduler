<DataContract()>
Public Class GetPlanResponse
    <DataMember()>
    Public Property PlanData As List(Of PlanItem)
    <DataMember()>
    Public Property ScheduleData As List(Of PlanItem)
    <DataMember()>
    Public Property Result As Integer
    <DataMember()>
    Public Property ResultString As String
    Public Sub New()
        PlanData = New List(Of PlanItem)
        ScheduleData = New List(Of PlanItem)
        ResultString = ""
    End Sub
End Class
