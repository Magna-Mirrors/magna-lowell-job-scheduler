Imports System.Runtime.Serialization

<DataContract()>
Public Class ActivityItem
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property ActivityDate As DateTime
    <DataMember()>
    Public Property Facet As String
    <DataMember()>
    Public Property Activity As String
    <DataMember()>
    Public Property Level As LogType

    Public Sub New(Id As Integer, Facet As String, Activity As String, Level As LogType)
        Me.Id = Id
        ActivityDate = DateTime.Now
        Me.Facet = Facet
        Me.Activity = Activity
        Me.Level = Level
    End Sub

    Public Sub New(Id As Integer, Facet As String, Activity As String, Er As Exception)
        Me.Id = Id
        ActivityDate = DateTime.Now
        Me.Facet = Facet
        Me.Activity = String.Format("{0} \ {1}", Activity, Er.Message)
        Me.Level = LogType.Error
    End Sub

    Public Sub New()
        ActivityDate = DateTime.Now
        Me.Facet = ""
        Me.Activity = ""
        Me.Level = LogType.info
    End Sub
End Class
