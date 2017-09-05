<DataContract()>
Public Class ValidatePartsRequest
    <DataMember()>
    Public Property Parts As List(Of Part)
    <DataMember()>
    Public Property LineData As Line

End Class
