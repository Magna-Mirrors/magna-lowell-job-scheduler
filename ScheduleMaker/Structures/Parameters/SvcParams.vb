Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
<DataContract()>
Public Class SvcParams
    Public Shared ReadOnly ParamPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MagnaSchedulerService", "Settings")
    Public Shared ReadOnly ParamPathAndFile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MagnaSchedulerService", "Settings", "Config.xml")
    Private _encUserPw As String
    <DataMember()>
    Public Property SqlSeverName As String
    <DataMember()>
    Public Property SqlDbName As String
    <DataMember()>
    Public Property SqlUserName As String

    ''' <summary>
    ''' Decrypted Pw
    ''' </summary>
    ''' <returns></returns>
    <XmlIgnore()>
    Public Property SqlPw As String
        Get
            Return clsCryptography.Decrypt(_encUserPw)
        End Get
        Set(value As String)
            _encUserPw = clsCryptography.Encrypt(value)
        End Set
    End Property


    <DataMember()>
    Public Property UserPw As String
        Get
            Return _encUserPw
        End Get
        Set(value As String)
            _encUserPw = value
        End Set
    End Property

    Public Property TempPw As String
        Get
            Return ""
        End Get
        Set(value As String)
            If value.Length > 0 Then
                _encUserPw = clsCryptography.Encrypt(value)
            End If
        End Set
    End Property

    Public Shared Function getDefaults() As SvcParams
        Dim p As New SvcParams
        With p
            .SqlSeverName = "Localhost"
            .SqlDbName = "Magna_Lowell"
            .SqlUserName = "sa"
            .SqlPw = "1234"
        End With
        Return p
    End Function
End Class
