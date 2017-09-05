

Public Class PlanItem

    Public ReadOnly Property BuildID As String
        Get
            Return String.Format("{0:MMddyy}-{1:hhmm}-{2:000}", Shipdate, Now, Position)
        End Get
    End Property
    Public Property Id As Integer
    Public Property PartNumber As String
    Public Property Shipdate As DateTime
    Public Property QTY As Integer
    Public Property Built As Integer
    Public Property Ordered As Integer
    Public Property Desc As String
    Public Property Flags As OrderFlags
    Public Property DueDate As Date
    Public Property ScheduleDate As Date
    Public Property Status As PlanStatus
    Public Property OrderId As Integer
    Public Property Position As Integer
    Public Property MMDDYY As String
    Public Property HHMM As String
    Public Property Chk As Boolean
    Public Property LastLoadTime As DateTime
    Public Property CreationDate As DateTime
    Public Property PartId As Integer
    Public Property TargetLineId As Integer


    Public Sub New()
        Id = 0
        OrderId = 0
        PartNumber = ""
        QTY = 0
        Built = 0
        Ordered = 0
        Desc = ""
        Status = PlanStatus.Unknown
        Position = 0
        MMDDYY = ""
        Chk = False
        Flags = False
        Position = 0
        LastLoadTime = Date.MinValue
        Shipdate = Now
        CreationDate = Now
    End Sub

End Class
