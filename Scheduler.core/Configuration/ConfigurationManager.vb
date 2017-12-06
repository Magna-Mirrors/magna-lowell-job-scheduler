Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Namespace Classes

    Public Class ConfigurationManager(Of T)

        Public Shared Function Load() As T
            Dim Fn = SvcParams.ParamPathAndFile
            Dim serializer As New XmlSerializer(GetType(T))
            Using stream = File.OpenRead(Fn)

                Dim settings = New XmlReaderSettings() With {
                        .DtdProcessing = DtdProcessing.Ignore,
                        .ConformanceLevel = ConformanceLevel.Auto
                        }
                Using reader = XmlReader.Create(stream, settings)
                    If (serializer.CanDeserialize(reader)) Then
                        Return DirectCast(serializer.Deserialize(reader), T)
                    End If
                End Using

            End Using

            Return Nothing

        End Function

        Public Shared Sub Save(config As T)
            Try
                Dim Fn As String = SvcParams.ParamPathAndFile

                Dim serializer As New XmlSerializer(GetType(T))

                Using stream = File.Open(Fn, FileMode.Create, FileAccess.ReadWrite)
                    serializer.Serialize(stream, config)
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "error Saving Config File")
            End Try
        End Sub





    End Class



End Namespace