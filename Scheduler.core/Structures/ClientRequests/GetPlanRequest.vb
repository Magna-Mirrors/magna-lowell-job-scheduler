<DataContract()>
Public Class GetPlanRequest
    Public Sub New(Ln As Line)
        LineData = Ln

    End Sub
    <DataMember()>
    Public Property LineData As Line

End Class
