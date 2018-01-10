Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract()>
Public Class GetScheduleResult
    <DataMember()>
    Public Property Items As List(Of PlanItem)
    <DataMember()>
    Public Property Result As Integer
    <DataMember()>
    Public Property ResultString As String
    Public Sub New()
        Items = New List(Of PlanItem)
        Result = 0
        ResultString = ""
    End Sub
End Class