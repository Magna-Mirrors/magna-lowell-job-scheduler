Imports System.IO
Imports Scheduler.core
Imports Scheduler.Program
Public Class TextData

    Protected ReadOnly _AppTools As AppTools
    Private mRsltInt As Integer
    Private mRsltString As String = ""
    Protected ReadOnly Prms As SvcParams

    Public Sub New()

    End Sub

    Public Sub New(ApTool As AppTools, Prms As SvcParams)
        _AppTools = ApTool
        Me.Prms = Prms
    End Sub

    Public Function getPlanData(LineData As Line) As GetPlanResponse
        Dim Resp As New GetPlanResponse
        Try
            Dim IdSeed As Integer = 0
            If GetScheduleTxtFileData(Resp, LineData, IdSeed) Then
                GetPlanTxtFileData(Resp, LineData, IdSeed)
            End If
            If Resp.Result = 0 Then
                Resp.Result = 1
            End If
        Catch ex As Exception

        End Try

        Return Resp
    End Function


    Private Function GetPlanTxtFileData(ByRef Resp As GetPlanResponse, LineData As Line, IdSeed As Integer) As Boolean
        Dim FName As String = "plan.txt"

        Dim filename As String = Path.Combine(LineData.ScheduleFolder, FName)
        If File.Exists(filename) Then
            Try
                Dim FDate = IO.File.GetLastWriteTime(filename)
                Using ScheduleReader As StreamReader = File.OpenText(filename)
                    If ScheduleReader.EndOfStream Then
                        mRsltString = String.Format("There is no {0} for this Line at this time", FName)
                        mRsltInt = 0
                    Else
                        Do Until ScheduleReader.EndOfStream
                            Dim PlanLine As String = ScheduleReader.ReadLine
                            Dim Data2Add As String() = PlanLine.Split(",")
                            If Data2Add.GetUpperBound(0) = 5 OrElse Data2Add.GetUpperBound(0) = 6 Then
                                Dim Pi As New PlanItem
                                IdSeed += 1
                                Pi.OrderId = IdSeed
                                Pi.Position = _AppTools.GetOrderPosition(Data2Add(0), Pi.MMDDYY, Pi.HHMM)
                                Pi.PartNumber = Data2Add(1)
                                Pi.QTY = Data2Add(2)
                                Pi.Ordered = Data2Add(3)
                                Pi.Built = Data2Add(4)
                                Pi.Desc = Data2Add(5)
                                Pi.Status = PlanStatus.Planed
                                Pi.Flags = 0
                                Pi.Truck = Data2Add.Length > 6 AndAlso Data2Add(6).ToLowerInvariant = "t"
                                Pi.Chk = "OK"
                                Pi.LastLoadTime = FDate
                                Resp.PlanData.Add(Pi)
                            End If
                        Loop
                    End If
                    ScheduleReader.Close()
                End Using
            Catch ex As Exception
                Resp.Result = -2
                Resp.ResultString = String.Format("There was an error {0} while trying to load {1}", ex.Message, FName)
            End Try
        Else
            Resp.Result = 0
            Resp.ResultString = String.Format("There is no Build {0} for this Line at this time", FName)
        End If
        Return Resp.Result = 0
    End Function


    Private Function GetScheduleTxtFileData(ByRef Resp As GetPlanResponse, LineData As Line, IdSeed As Integer) As Boolean
        Dim FName As String = "Schedule.txt"

        Dim filename As String = Path.Combine(LineData.ScheduleFolder, FName)
        If File.Exists(filename) Then
            Try
                Using ScheduleReader As StreamReader = File.OpenText(filename)
                    If ScheduleReader.EndOfStream Then
                        mRsltString = String.Format("There is no {0} for this Line at this time", FName)
                        mRsltInt = 0
                    Else
                        Do Until ScheduleReader.EndOfStream
                            Dim ScheduleLine As String = ScheduleReader.ReadLine
                            Dim Data2Add As String() = ScheduleLine.Split(",")
                            If Data2Add.GetUpperBound(0) = 6 OrElse Data2Add.GetUpperBound(0) = 7 Then
                                Dim Pi As New PlanItem
                                IdSeed += 1
                                Pi.OrderId = IdSeed
                                Pi.Position = _AppTools.GetOrderPosition(Data2Add(0), Pi.MMDDYY, Pi.HHMM)
                                Pi.PartNumber = Data2Add(1)
                                Pi.QTY = Data2Add(2)
                                Pi.Ordered = Data2Add(3)
                                Pi.Built = Data2Add(4)

                                If Pi.Ordered > 0 Then
                                    Pi.Status = PlanStatus.Scheduled
                                End If

                                If Pi.Built >= Pi.QTY Then
                                    Pi.Status = PlanStatus.Complete
                                End If

                                If Data2Add(5).Substring(0, 4) = "RMVD" Then
                                    Pi.Status = PlanStatus.Removed
                                    Pi.Desc = Data2Add(5).Remove(0, 4)
                                Else
                                    Pi.Desc = Data2Add(5)
                                End If

                                Pi.DueDate = If(Data2Add.Length > 6 AndAlso IsDate(Data2Add(6).ToString), CDate(Data2Add(6)), Now)


                                Pi.Chk = "OK"
                                Pi.LastLoadTime = Now
                                If Pi.Status = PlanStatus.Scheduled Then
                                    Resp.PlanData.Add(Pi)
                                End If

                            End If
                        Loop
                    End If
                    ScheduleReader.Close()
                End Using
            Catch ex As Exception
                Resp.Result = -2
                Resp.ResultString = String.Format("There was an error {0} while trying to load {1}", ex.Message, FName)
            End Try
        Else
            Resp.Result = 0
            Resp.ResultString = String.Format("There is no Build {0} for this Line at this time", FName)
        End If
        Return Resp.Result = 0
    End Function

    Public Function SavePlan(Req As SavePlanRequest) As TransactionResult
        'TODO: get PlanData-------------------------------------
        'open the plan and get the date of the plan see if the plan is diferent form the date of the plan when it was opened

        Dim Rslt As New TransactionResult
        Dim filename As String = Path.Combine(Req.LineData.ScheduleFolder, "plan.txt")
        If File.Exists(filename) Then
            'check to make sure plan has not been changed while we were working on it
            If File.GetLastWriteTime(filename) > Req.LastLoadTime Then
                Rslt.Result = -1
                Rslt.ResultString = "The Plan File has been changed by the tester since you have last read it. You will need to reload the Plan and redo your edits"
                Return Rslt
            End If
        End If
        Try
            Using PathWriter As StreamWriter = File.CreateText(filename)
                For Each R In Req.PlanData
                    With R
                        If R.Status = PlanStatus.Planed Or R.Status = PlanStatus.Unknown Then
                            If .Built = 0 AndAlso .Ordered = 0 Then
                                Dim data2Write As String = String.Format("{0},{1},{2},{3},{4},{5},{6}",
                                                    .BuildID,
                                                    .PartNumber,
                                                    .QTY,
                                                    .Built,
                                                    .Ordered,
                                                    .Desc,
                                                      If(.Flags.HasFlag(OrderFlags.Truck), "T", ""))
                                PathWriter.WriteLine(data2Write)
                            End If
                        End If
                    End With
                Next
                PathWriter.Close()
                Rslt.Result = 1
            End Using

        Catch ex As Exception
            Rslt.ResultString = String.Format("There was an error {0} while trying to write the Plan file", ex.Message)
            Rslt.Result = -1
        End Try
        Return Rslt
    End Function



End Class
