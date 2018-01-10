Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract()>
Public Class GetPlanResponse
    <DataMember()>
    Public Property PlanData As List(Of PlanItem)
    <DataMember()>
    Public Property Result As Integer
    <DataMember()>
    Public Property ResultString As String
    Public Sub New()
        PlanData = New List(Of PlanItem)
        ResultString = ""
    End Sub
End Class