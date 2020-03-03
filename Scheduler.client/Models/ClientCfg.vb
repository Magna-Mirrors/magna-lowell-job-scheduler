Public Class ClientCfg
	Public Sub New()
		ServicePort = 8045
		ServiceAddress = "10.69.104.20"

	End Sub
	Public Property ServiceAddress As String
	Public Property ServicePort As Integer



	Public ReadOnly Property ServiceUrl As String
		Get
			Return String.Format("http://{0}:{1}/SchedulerService", ServiceAddress, ServicePort)
		End Get
	End Property



End Class
