<DataContract>
Public Class getPartsforLineResponse
    <DataMember>
    Public Property parts As List(Of Part)
    <DataMember>
    Public Property Result As Integer
    <DataMember>
    Public Property ResultString As String
    Public Sub New()
        parts = New List(Of Part)()
        Result = 0
        ResultString = ""
    End Sub
End Class
