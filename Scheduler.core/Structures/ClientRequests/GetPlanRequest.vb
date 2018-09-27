<DataContract()>
Public Class GetPlanRequest
    Public Sub New(Ln As Line)
        LineData = Ln
        Me.IncludeHistory = False
    End Sub
    Public Sub New(Ln As Line, IncludeHistory As Boolean)
        LineData = Ln
        Me.IncludeHistory = IncludeHistory
    End Sub
    <DataMember()>
    Public Property LineData As Line
    <DataMember()>
    Public Property IncludeHistory As Boolean
End Class
