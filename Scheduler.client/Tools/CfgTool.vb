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
        Using file = System.IO.File.OpenRead(f)
            tmp = DirectCast(serializer.Deserialize(file), ClientCfg)
        End Using
        Return tmp
    End Function
    Public Function Save(cfg As ClientCfg) As Boolean
        CheckDir()
        Dim serializer As New XmlSerializer(GetType(ClientCfg))
        Using file = System.IO.File.OpenWrite(f)
            serializer.Serialize(file, cfg)
        End Using
        Return True
    End Function

    Private Sub CheckDir()
        If Not Directory.Exists(p) Then
            Directory.CreateDirectory(p)
        End If
    End Sub
End Class
