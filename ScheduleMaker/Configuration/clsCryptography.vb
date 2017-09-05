Imports System.Text
Imports System.Security.Cryptography
Imports System.IO

Public Class clsCryptography
    Const PassPhrase As String = "Magna8Yl)7Tyhwyg"

    Public Shared Function Encrypt(Pw As String) As String

        If String.IsNullOrWhiteSpace(Pw) Then
            Return String.Empty
        End If

        ' Encrypt the data. 
        Try
            Dim encAlg As TripleDES = TripleDES.Create()

            encAlg.Key = Encoding.ASCII.GetBytes(PassPhrase)

            Using memory As MemoryStream = New MemoryStream()

                Dim writer As New BinaryWriter(memory)

                writer.Write(CByte(encAlg.IV.Length))
                writer.Write(encAlg.IV, 0, encAlg.IV.Length)

                Using cs As CryptoStream = New CryptoStream(memory, encAlg.CreateEncryptor(), CryptoStreamMode.Write)
                    Dim EncodedData As Byte() = Encoding.ASCII.GetBytes(Pw)
                    cs.Write(EncodedData, 0, EncodedData.Length)
                    cs.FlushFinalBlock()

                    Return Convert.ToBase64String(memory.ToArray())
                End Using

            End Using
        Catch ex As Exception
            Return String.Empty
        End Try


        Return String.Empty

    End Function

    Public Shared Function Decrypt(EncBase64 As String) As String
        If String.IsNullOrWhiteSpace(EncBase64) Then
            Return String.Empty
        End If

        Try
            Dim alg As TripleDES = TripleDES.Create()
            Dim data() As Byte = Convert.FromBase64String(EncBase64)
            Dim size As Integer = data(0)
            Dim iv(size - 1) As Byte

            Buffer.BlockCopy(data, 1, iv, 0, size)

            alg.IV = iv
            alg.Key = Encoding.ASCII.GetBytes(PassPhrase)

            Using memory As New MemoryStream()
                Using cs As New CryptoStream(memory, alg.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(data, size + 1, data.Length - size - 1)
                    cs.Flush()
                End Using
                Return Encoding.ASCII.GetString(memory.ToArray())
            End Using
        Catch ex As Exception
            Return String.Empty
        End Try


        Return String.Empty

    End Function


End Class

