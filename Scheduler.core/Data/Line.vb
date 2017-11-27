<DataContract>
Public Class Line
    <DataMember>
    Public Property Id As Integer
    <DataMember>
    Public Property Name As String
    <DataMember>
    Public Property Description As String
    <DataMember>
    Public Property WcfFileName As String
    <DataMember>
    Public Property SelectCmd As String
    <DataMember>
    Public Property ScheduleFolder As String
    <DataMember>
    Public Property SchedulerMethod As SchedulerMethods
    <DataMember>
    Public Property CustomerName As String
    <DataMember>
    Public Property CustomerId As Integer
    <DataMember>
    Public Property ProgramId As Integer

    Public Sub New()
        Id = 0
        Name = ""
        Description = ""
        WcfFileName = ""
        SelectCmd = ""
        ScheduleFolder = ""
        CustomerName = ""
        SchedulerMethod = SchedulerMethods.None
        CustomerId = 0
        ProgramId = 0
    End Sub

End Class
