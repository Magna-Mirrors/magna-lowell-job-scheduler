Public Class ClientCfg
    Public Sub New()
        ServiceAddress = "http://10.69.104.20:8045"
        Dim arguments As String() = Environment.GetCommandLineArgs()
        If arguments IsNot Nothing AndAlso arguments.Length > 0 Then
            If arguments(0) = "local" Then
                ServiceAddress = "http://localhost:8045"
            End If
        End If
    End Sub
    Public Property ServiceAddress As String
End Class
