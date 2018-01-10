
Imports System
Imports System.Runtime.Serialization

<DataContract>
Public Class LogEventArgs
    Inherits EventArgs
    <DataMember>
    Public Property LogType As LogType
    <DataMember>
    Public Property Message As String
    <DataMember>
    Public Property TimeStamp As DateTime
    <DataMember>
    Public Property Facet As String
    <DataMember>
    Public Property Options As NotifyOptions
    <DataMember>
    Public Property Ex As Exception
    <DataMember>
    Public Property Subject As String
    <DataMember>
    Public Property Body As String
    <DataMember>
    Public Property TagId As Integer
    <DataMember>
    Public Property SystemPath As String

    Public Sub New()
        Me.LogType = LogType.info
        Me.Message = ""
        Me.TimeStamp = DateTime.Now
        Me.Facet = Facet
        Me.Options = NotifyOptions.Display
        Me.Ex = Nothing
        Me.Subject = ""
        Me.Body = ""

        Me.TagId = 0
        Me.SystemPath = ""
    End Sub

    Public Sub New(Facet As String, Er As Exception, Optional Options As NotifyOptions = NotifyOptions.Display)
        Me.LogType = LogType.Error
        Me.Message = Er.Message
        Me.TimeStamp = DateTime.Now
        Me.Facet = Facet
        Me.Options = Options
        Me.Ex = Er
        Me.Subject = Facet
        Me.Body = Er.Message

        Me.TagId = 0
        Me.SystemPath = ""
    End Sub

    Public Sub New(MsgText As String, facet As String)
        Me.LogType = LogType.info
        Me.Message = MsgText
        TimeStamp = DateTime.Now
        Me.Facet = facet
        Me.Options = NotifyOptions.Display
        Me.Subject = facet
        Me.Body = MsgText

        Me.TagId = 0
        Me.SystemPath = ""
    End Sub

    Public Sub New(ByVal type As LogType, msg As String, facet As String)
        Me.LogType = type
        Me.Message = msg
        Me.TimeStamp = DateTime.Now
        Me.Facet = facet
        Me.Options = NotifyOptions.Display
        Me.Subject = facet
        Me.Body = msg

        Me.TagId = 0
        Me.SystemPath = ""
    End Sub

    Public Sub New(ByVal type As LogType, msg As String, facet As String, Options As NotifyOptions)
        Me.LogType = type
        Me.Message = msg
        Me.TimeStamp = DateTime.Now
        Me.Facet = facet
        Me.Options = Options
        Me.Subject = facet
        Me.Body = msg
        Me.TagId = 0
        Me.SystemPath = ""
    End Sub

End Class


