Imports System.ServiceModel

Module Root
    Public ClientAccess As SchedulerClient


    Public Sub ConnectToService()
        Dim binding As New BasicHttpBinding()
        Dim Address As New EndpointAddress("http://localhost:8045/SchedulerService")
        ClientAccess = New SchedulerClient(binding, Address)
        ClientAccess.Open()

    End Sub

End Module
