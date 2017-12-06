
Imports System
Imports System.Runtime.Serialization

<DataContract>
Public Class PlanItem
    <DataMember>
    Public Property BuildID As String

    <DataMember>
    Public Property OrderId As Integer
    '    <DataMember>
    '    Public Property Id As Integer
    <DataMember>
    Public Property PartNumber As String
    <DataMember>
    Public Property Shipdate As DateTime
    <DataMember>
    Public Property QTY As Integer
    <DataMember>
    Public Property Built As Integer
    <DataMember>
    Public Property Ordered As Integer
    <DataMember>
    Public Property Desc As String
    <DataMember>
    Public Property Flags As OrderFlags
    <DataMember>
    Public Property DueDate As Date
    <DataMember>
    Public Property ScheduleDate As Date
    <DataMember>
    Public Property Status As PlanStatus

    <DataMember>
    Public Property Position As Long
    <DataMember>
    Public Property MMDDYY As String
    <DataMember>
    Public Property HHMM As String
    <DataMember>
    Public Property Chk As String 'NOTE: Why is chk a string?
    <DataMember>
    Public Property LastLoadTime As DateTime
    <DataMember>
    Public Property CreationDate As DateTime
    <DataMember>
    Public Property PartId As Integer
    <DataMember>
    Public Property TargetLineId As Integer

    <DataMember>
    Public Property PPHPP As Single


    <DataMember>
    Public Property Truck As Boolean
        Get
            Return Flags.HasFlag(OrderFlags.Truck)
        End Get
        Set(value As Boolean)
            If value Then
                Flags = Flags Or OrderFlags.Truck
            Else
                If Flags.HasFlag(OrderFlags.Truck) Then
                    Flags = Flags - OrderFlags.Truck
                End If
            End If

        End Set
    End Property



    Public Sub New()
        OrderId = 0
        PartNumber = ""
        QTY = 0
        Built = 0
        Ordered = 0
        Desc = ""
        Status = PlanStatus.Unknown
        Position = 0
        Chk = False 'NOTE: Why is chk a string?
        Flags = 0
        Position = 0
        Shipdate = Date.Now
        CreationDate = Date.Now
        PPHPP = 15
        MMDDYY = CreationDate.ToString("MMddyy")
        LastLoadTime = Date.MinValue
        BuildID = String.Format("{0:MMddyy}-{1:hhmm}-{2:0ss}", CreationDate, CreationDate, CreationDate)
    End Sub

End Class
