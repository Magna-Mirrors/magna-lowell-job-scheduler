<DataContract>
Public Class SavePlanResponse
    <DataMember>
    Public Property Result As Integer
    <DataMember>
    Public Property ResultString As String
    <DataMember>
    Public Property lastLoadDate As DateTime

    Public Sub New()
        Result = 0
        ResultString = "0"
        lastLoadDate = DateTime.MinValue
    End Sub

End Class
