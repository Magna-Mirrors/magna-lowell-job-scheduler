<DataContract()>
Public Class ValidatePartsRequest
    <DataMember()>
    Public Property Parts As List(Of Part)
    <DataMember()>
    Public Property LineData As Line

    Public Sub New(Line As Line)
        Parts = New List(Of Part)

    End Sub

End Class
