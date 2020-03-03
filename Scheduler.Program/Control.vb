Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Threading
Imports Autofac
Imports Autofac.Integration.Wcf
Imports Scheduler.core

Public Class Control
	Private LogService As iLoggingService
	Private ct As New CancellationTokenSource()
	Private ct2 As New CancellationTokenSource()
	Private ReadOnly mErpHandler As BaanOrderHandling
	Private ReadOnly _partMaint As PartInformationUpdateManager
	Private ErpTask As Task
	Private PartUpdateTask As Task
	Private ReadOnly _Cfg As SvcParams
	Private ReadOnly _DataMgr As DataManager

	Public Sub New(LgSvc As iLoggingService, ErpHandling As BaanOrderHandling, PartMaint As PartInformationUpdateManager, Atools As AppTools, DataMgr As DataManager)
		LogService = LgSvc
		mErpHandler = ErpHandling
		_partMaint = PartMaint
		_Cfg = Atools.GetProgramParams
		_DataMgr = DataMgr
		AddHandler _DataMgr.RequestPnDataUpdateNow, AddressOf ExtPnRefreshFromXmlFilesRequest
	End Sub

	Public Sub StartController(Con As IContainer)
		StartServiceHost(Con)
		StartOrderHandling()
		StartPartUdateservice()
	End Sub


	Public Sub StopController()
		StopOrderHandling()
		StopServiceHost()
		StopPartUdateservice()
	End Sub


	Private Sub ExtPnRefreshFromXmlFilesRequest()
		_partMaint.ExecuteNowRequest()
	End Sub


	Private Sub StartServiceHost(Cont As IContainer)
		Try
			Dim contract As Type = GetType(core.iSchedulerService)
			Dim implementation As Type = GetType(SchedulerService)
			Dim baseAddress As New Uri(String.Format("http://localhost:{0}/SchedulerService", _Cfg.TcpPortNumberForWcf))
			SvcHost = New ServiceHost(implementation, baseAddress)
			Dim Binding As New BasicHttpBinding()
			SvcHost.AddServiceEndpoint(contract, Binding, baseAddress)
			Binding.MaxReceivedMessageSize = Int32.MaxValue
			Dim smb As New ServiceMetadataBehavior()
			smb.HttpGetEnabled = True
			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15
			SvcHost.AddDependencyInjectionBehavior(Of core.iSchedulerService)(Cont)
			SvcHost.Description.Behaviors.Add(smb)
			SvcHost.Open()
			LogService.SendAlert(New Scheduler.core.LogEventArgs(Scheduler.core.LogType.info, "Scheduler service was started", "Service"))
		Catch ex As Exception
			LogService.SendAlert(New Scheduler.core.LogEventArgs("Service Host start", ex))
		End Try
	End Sub


	Public Sub StopServiceHost()
		Try
			If SvcHost IsNot Nothing Then
				SvcHost.Close()
				SvcHost = Nothing
				StopOrderHandling()
			End If
		Catch ex As Exception
			LogService.SendAlert(New Scheduler.core.LogEventArgs("Err @ StopServiceHost ", ex))
		End Try

	End Sub

	Private Sub StartOrderHandling()
		Try
			ErpTask = mErpHandler.RunAsync(ct.Token)
		Catch ex As Exception
			LogService.SendAlert(New Scheduler.core.LogEventArgs("StartOrderHandling Error ErpTask ", ex))
		Finally

		End Try

	End Sub

	Public Async Sub StopOrderHandling()
		ct.Cancel()
		If ErpTask IsNot Nothing Then
			Await ErpTask
		End If

	End Sub


	Private Sub StartPartUdateservice()
		Try
			PartUpdateTask = _partMaint.RunAsync(ct2.Token)
		Catch ex As Exception
			LogService.SendAlert(New Scheduler.core.LogEventArgs("Part Update service Error PartUpdateTask ", ex))
		Finally

		End Try
	End Sub

	Public Async Sub StopPartUdateservice()
		ct2.Cancel()
		If PartUpdateTask IsNot Nothing Then
			Await PartUpdateTask
		End If

	End Sub
End Class
