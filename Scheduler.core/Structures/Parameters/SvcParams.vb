Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
<DataContract()>
Public Class SvcParams
    Public Shared ReadOnly ParamPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MagnaSchedulerService", "Settings")
    Public Shared ReadOnly ParamPathAndFile As String = Path.Combine(ParamPath, "Config.xml")

    Private _ErpEncUserPw As String

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
    Public Property WcfRootPath As String

    <DataMember()>
    Public Property PlanTextRootPath As String

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


    Public Property ErpSqlServername As String
    Public Property ErpSqlDbName As String
    Public Property ErpSqlUserName As String

    <XmlIgnore>
    Public Property ErpSqlPassword As String 'encrypted
        Get
            Return clsCryptography.Decrypt(_ErpEncUserPw)
        End Get
        Set(value As String)
            _ErpEncUserPw = clsCryptography.Encrypt(value)
        End Set
    End Property


    Public Property ErpSqlPw As String 'encrypted
        Get
            Return _ErpEncUserPw
        End Get
        Set(value As String)
            _ErpEncUserPw = value
        End Set
    End Property



    Public Property NewErpSqlPw As String 'InjectedValue
        Get
            Return ""
        End Get
        Set(value As String)
            If value.Length > 0 Then
                _ErpEncUserPw = clsCryptography.Encrypt(value)
            End If
        End Set
    End Property


    Public Property UpdateOrdersIntervalMinutes As Integer


    Public Shared Function getDefaults() As SvcParams
        Dim p As New SvcParams
        With p
            .SqlSeverName = ".\sqlexpress"
            .SqlDbName = "Magna_Lowell"
            .SqlUserName = "debugUser"
            .SqlPw = "123456789"
            .WcfRootPath = "C:\WORK\Magna\DainaWare\SupportFiles\WCF\"
            .PlanTextRootPath = "C:\WORK\Magna\DainaWare\SupportFiles\Schedule\"

            .ErpSqlServername = "HOLMSDBDEV02"  'Test server = "HOLMSDBDEV02" '"holmssqlinst01\instance01"
            .ErpSqlDbName = "MALshopfloorTD"
            .ErpSqlUserName = "MALsfEOL"
            .NewErpSqlPw = "test123"
            .UpdateOrdersIntervalMinutes = 5
        End With
        Return p
    End Function
End Class
