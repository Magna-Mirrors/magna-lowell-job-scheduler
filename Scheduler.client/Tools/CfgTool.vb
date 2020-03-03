Imports System.IO
Imports System.Xml.Serialization

Public Class CfgTool


    Private p As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FeyenZylstra", "Magna-Job-Scheduler-client")
    Private f As String = Path.Combine(p, "cfg.xml")

    Public Function Read() As ClientCfg
        If Not File.Exists(f) Then
            Save(New ClientCfg())
        End If
        Dim serializer As New XmlSerializer(GetType(ClientCfg))
		Dim tmp As ClientCfg = Nothing
		Dim UpdateIt As Boolean
		Using file = System.IO.File.OpenRead(f)
			tmp = DirectCast(serializer.Deserialize(file), ClientCfg)
			If tmp.ServicePort = 0 Then
				tmp.ServicePort = 8045
				UpdateIt = True
			End If
		End Using

		If tmp.ServiceAddress.Contains("//") Then
			tmp.ServiceAddress = "10.69.104.20"
			tmp.ServicePort = 8045
			UpdateIt = True
		End If
		If UpdateIt Then
			Save(tmp)
		End If
		Return tmp
    End Function
    Public Function Save(cfg As ClientCfg) As Boolean
        CheckDir()
		Dim serializer As New XmlSerializer(GetType(ClientCfg))
		Try
			Using file = System.IO.File.OpenWrite(f)
				serializer.Serialize(file, cfg)
			End Using
		Catch ex As Exception

		End Try

		Return True
    End Function

    Private Sub CheckDir()
        If Not Directory.Exists(p) Then
            Directory.CreateDirectory(p)
        End If
    End Sub
End Class
