Imports System.IO
Imports Scheduler.core

Public Class MdbData
    Protected ReadOnly ATools As AppTools
    Protected ReadOnly Prms As SvcParams
    Private Const ConString As String = "Provider=Microsoft.Jet.OLEDB.4.0; Ole DB Services=-4; Data Source= {0}"



    Public Sub New(Apt As AppTools)
        ATools = Apt
        Me.Prms = Apt.GetProgramParams
    End Sub

    Public Function ValidateParts(PartReq As ValidatePartsRequest) As ValidatePartsResponse
        Dim Res As New ValidatePartsResponse
        Try
            Dim FilePath = Path.Combine(Me.Prms.WcfRootPath, PartReq.LineData.WcfFileName)
            Using Cn As New OleDb.OleDbConnection(String.Format(ConString, FilePath))
                Cn.Open()
                Dim Pry() As String = (From y In PartReq.Parts Where y.Valid = False).Select(Function(x) x.PN).ToArray
                ' PartReq.Parts.Select(Function(x) x.Valid = False)
                Dim Cmd As New OleDb.OleDbCommand(String.Format("{0} where Magna_pn in('{1}')", PartReq.LineData.SelectCmd, Join(Pry, "','")), Cn)
                Dim RetParts As New List(Of Part)

                Using dRead As IDataReader = Cmd.ExecuteReader()
                    While dRead.Read
                        RetParts.Add(New Part() With {.PN = dRead("PN"), .Desc = dRead("Desc")})
                    End While
                End Using
                For Each Pr In PartReq.Parts
                    If Not Pr.Valid Then
                        Pr.Valid = RetParts.Any(Function(x) x.PN = Pr.PN)
                    End If
                Next

            End Using
            Res.ResultString = ""
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
            Dim FilePath = Path.Combine(Me.Prms.WcfRootPath, PartReq.LineData.WcfFileName)
            Using Cn As New OleDb.OleDbConnection(String.Format(ConString, FilePath))
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
