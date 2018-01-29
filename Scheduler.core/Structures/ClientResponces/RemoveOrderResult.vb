<DataContract()>
Public Class RemoveOrderResult
    <DataMember()>
    Public Property Result As Integer
    <DataMember()>
    Public Property ResultString As String
    Public Sub New()
        Result = 0
        ResultString = ""
    End Sub
End Class
