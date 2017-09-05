<DataContract()>
Public Class GetLinesResponse
    <DataMember()>
    Public Property Lines As List(Of Line)
    <DataMember()>
    Public Property Result As Integer
    <DataMember()>
    Public Property ResultString As String
    Public Sub New()
        Lines = New List(Of Line)()
        Result = 0
        ResultString = ""
    End Sub
End Class
