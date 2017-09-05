Imports Scheduler.core

Public Class MdbData

    Private Const ConString As String = "Provider=Microsoft.Jet.OLEDB.4.0; Ole DB Services=-4; Data Source= {0}"

    Public Sub New()

    End Sub


    Public Function ValidateParts(PartReq As ValidatePartsRequest) As ValidatePartsResponse
        Dim Res As New ValidatePartsResponse
        Try
            Using Cn As New OleDb.OleDbConnection(String.Format(ConString, PartReq.LineData.WcfPath))
                Cn.Open()
                Dim Pry = PartReq.Parts.Select(Function(x) x.PN).ToArray
                PartReq.Parts.Select(Function(x) x.Valid = False)

                For Each p In Pry
                    p = String.Format("'{0}'", p)
                Next

                Dim Cmd As New OleDb.OleDbCommand(String.Format("{0} where pn in({1})", PartReq.LineData.SelectCmd, Join(Pry, ",")))
                Using dRead As IDataReader = Cmd.ExecuteReader()
                    While dRead.Read
                        Dim Pr = (From x In PartReq.Parts Where x.PN = dRead("PN")).FirstOrDefault
                        If Pr IsNot Nothing Then
                            Pr.Valid = True
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            Res.Result = -1
            Res.ResultString = "MdbValidateParts Err " & ex.Message
        End Try
        Res.parts = PartReq.Parts
        Return Res
    End Function

    Public Function GetParts(PartReq As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Res As New getPartsforLineResponse
        Try
            Using Cn As New OleDb.OleDbConnection(String.Format(ConString, PartReq.LineData.WcfPath))
                Cn.Open()

                Dim Cmd As New OleDb.OleDbCommand(PartReq.LineData.SelectCmd)
                Using dRead As IDataReader = Cmd.ExecuteReader()
                    While dRead.Read
                        Dim Pr As New Part() With {.PN = dRead("PN"), .Desc = dRead(1), .Id = Nothing, .Valid = True}
                        Res.parts.Add(Pr)
                    End While
                End Using
            End Using
        Catch ex As Exception
            Res.Result = -1
            Res.ResultString = "MdbValidateParts Err " & ex.Message
        End Try

        Return Res
    End Function


End Class
