Public Class WipOrder

    Public Property LineId As Integer
    Public Property Status As PlanStatus
    Public Property Position As Long
    Public Property OrderId As Integer
    Public Property PartNumber As String
    Public Property TargetQty As Integer
    Public Property Built As Integer
    Public Property Ordered As Integer
    Public Property RequestOrderQty As Integer
    Public Property PartsPerHourPerPerson As Integer
    Public Property WorkCell As String
    Public Property WipHours As Double
    Public Property ReOrderAtPercent As Double

    Public Sub New()
        RequestOrderQty = 0
        Ordered = 0
        Built = 0
        PartsPerHourPerPerson = 0
    End Sub

End Class
