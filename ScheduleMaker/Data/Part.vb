Imports System.Runtime.Serialization

<DataContract(Name:="Part"), Serializable>
Public Class Part
    <DataMember()>
    Public Property Id As Integer?
    <DataMember()>
    Public Property [PN] As String
    <DataMember()>
    Public Property [Desc] As String

    <DataMember()>
    Public Property Valid As Boolean

    Public Sub New()

    End Sub

End Class
