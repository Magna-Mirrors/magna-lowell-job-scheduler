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
        Res.Result = 1

        Try

            Dim FilePath = Path.Combine(Me.Prms.WcfRootPath, PartReq.LineData.WcfFileName)
            Using Cn As New OleDb.OleDbConnection(String.Format(ConString, FilePath))
                Cn.Open()

                ' PartReq.Parts.Select(Function(x) x.Valid = False)
                Dim Pry() As String = (From y In PartReq.Parts Where y.Valid = False).Select(Function(x) x.PN).ToArray
                Dim SrchItms As String = String.Format("'{0}'", Join(Pry, "','"))
                Dim Cmd As New OleDb.OleDbCommand(PartReq.LineData.SelectCmd.Replace("@partNumbers", SrchItms), Cn)

                Using dRead As IDataReader = Cmd.ExecuteReader()
                    While dRead.Read
                        Dim PN = dRead("PN").ToString
                        ' Dim Desc = dRead("Desc").ToString

                        Dim Prts = From P In PartReq.Parts Where P.PN = PN
                        If Prts IsNot Nothing Then
                            For Each pt In Prts
                                pt.Valid = True
                                '  pt.Desc = Desc
                            Next
                        Else
                            Res.Result = 0
                            Res.ResultString = String.Format("Part number {0} was not found in Target Mdb file {1}", SrchItms, PartReq.LineData.WcfFileName)
                        End If
                    End While
                End Using
                If Res.Result = 1 Then
                    For Each Pr In PartReq.Parts
                        If Not Pr.Valid Then
                            Res.Result = 0
                            Res.ResultString = String.Format("Part number {0} was not found in Target Mdb file {1}", Pr.PN, PartReq.LineData.WcfFileName)
                        End If
                    Next
                End If


            End Using
            If Res.Result = 1 Then
                Res.ResultString = ""
            End If

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
                        Dim Pr As New Part() With {.PN = CStr(dRead("PN")), .Desc = CStr(dRead(1)), .Id = Nothing, .Valid = True}
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
