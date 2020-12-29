Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
<DataContract()>
Public Class SvcParams
    Public Shared ReadOnly ParamPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MagnaSchedulerService", "Settings")
    Public Shared ReadOnly ParamPathAndFile As String = Path.Combine(ParamPath, "Config.xml")
	Private _PartInfoUpdateTime As DateTime
	Private _ErpEncUserPw As String
	Private _PwUpdate As Boolean
	Private _LocalSqlPwChange As String
	Private _RemoteErpSqlPwChange As String

	Private _encUserPw As String
    <DataMember()>
    Public Property SqlSeverName As String
    <DataMember()>
    Public Property SqlDbName As String
	<DataMember()>
	Public Property SqlUserName As String

	<DataMember()>
	Public Property TcpPortNumberForWcf As Integer


	<XmlIgnore()>
    Public ReadOnly Property PasswordUpdate As Boolean
        Get
            Return _PwUpdate
        End Get
    End Property


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

    Public Property NewSqlPw As String
        Get
			Return _LocalSqlPwChange
		End Get
        Set(value As String)
			If value.Length > 1 Then
				_LocalSqlPwChange = value
				_PwUpdate = True
			End If
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
	<DataMember()>
	Public Property ErpSqlServername As String
	<DataMember()>
	Public Property ErpSqlDbName As String
	<DataMember()>
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

	<DataMember()>
	Public Property ErpSqlPw As String 'encrypted
        Get
            Return _ErpEncUserPw
        End Get
        Set(value As String)
            _ErpEncUserPw = value
        End Set
    End Property


	<DataMember()>
	Public Property NewErpSqlPw As String 'InjectedValue
        Get
			Return _RemoteErpSqlPwChange
		End Get
        Set(value As String)
			If value.Length > 1 Then
				_RemoteErpSqlPwChange = value
				_PwUpdate = True
			End If
		End Set
    End Property
	Public Sub New()

	End Sub

	Public Sub ApplyPwChange()

		If _RemoteErpSqlPwChange IsNot Nothing AndAlso _RemoteErpSqlPwChange.Length > 1 Then
			_ErpEncUserPw = clsCryptography.Encrypt(_RemoteErpSqlPwChange)
			_RemoteErpSqlPwChange = "?"
		End If

		If _LocalSqlPwChange IsNot Nothing AndAlso _LocalSqlPwChange.Length > 1 Then
			_encUserPw = clsCryptography.Encrypt(_LocalSqlPwChange)
			_LocalSqlPwChange = "?"
		End If

	End Sub


	<DataMember()>
	Public Property UpdateOrdersIntervalMinutes As Integer
	<DataMember()>
	Public Property UpdatePartInfoTime As DateTime
		Get
			Return _PartInfoUpdateTime
		End Get
		Set(value As DateTime)
			If IsDate(value) Then
				_PartInfoUpdateTime = value
			Else
				_PartInfoUpdateTime = New Date(2020, 1, 1, 19, 0, 0)
			End If
		End Set
	End Property

	Public ReadOnly Property XferTime As TimeSpan
		Get
			Return New TimeSpan(UpdatePartInfoTime.Hour, UpdatePartInfoTime.Minute, UpdatePartInfoTime.Second)
		End Get
	End Property



	Public Shared Function getDefaults() As SvcParams
        Dim p As New SvcParams
        With p
            .SqlSeverName = "Localhost"
			.SqlDbName = "MagnaLowell"
			.SqlUserName = "sa"
			.SqlPw = "Winston"
			.WcfRootPath = "E:\_Projects\M\Magna\Lowell\DainaWare\SupportFiles\WCF\"
            .PlanTextRootPath = "E:\_Projects\M\Magna\Lowell\DainaWare\SupportFiles\Schedule\"
			.ErpSqlServername = "HOLMSDBDEV02"  'Test server = "HOLMSDBDEV02" '"holmssqlinst01\instance01"
			.ErpSqlDbName = "MALshopfloorTD"
            .ErpSqlUserName = "MALsfEOL"
			.NewErpSqlPw = "Myron.002"
			.UpdateOrdersIntervalMinutes = 5
			.UpdatePartInfoTime = New Date(2020, 1, 1, 19, 0, 0)
			.TcpPortNumberForWcf = 8045
		End With
        Return p
    End Function
End Class
