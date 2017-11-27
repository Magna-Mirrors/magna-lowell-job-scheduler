Imports Scheduler.core

Public Class SqlData
    Protected ReadOnly _Cfg As SvcParams
    Private ReadOnly LgSvr As iLoggingService
    Private Const PlanQuery As String = "SELECT top 100 percent Schedule_Job_History.TargetLineId, Schedule_Job_History.Position AS priority, Part_Info.PN, Schedule_Job_History.ID, SUM(Schedule_Part_Order_History.Qty) 
                      AS Ordered, SUM(Schedule_Part_Production_History.Qty) AS Built, MAX(Schedule_Job_History.Quantity) AS Qty, Schedule_Job_History.ID AS PartId, 
                      Schedule_Job_History.Flags, Part_Info.Desc1, Part_Info.Name, Schedule_Job_History.Status
                      FROM Schedule_Job_History INNER JOIN
                      Part_Info ON Schedule_Job_History.PartId = Part_Info.PartId LEFT OUTER JOIN
                      Schedule_Part_Production_History ON Schedule_Job_History.ID = Schedule_Part_Production_History.JobId LEFT OUTER JOIN
                      Schedule_Part_Order_History ON Schedule_Job_History.ID = Schedule_Part_Order_History.JobId
                      WHERE (Schedule_Job_History.Status < 4) AND (Schedule_Job_History.TargetLineId = {0})
                      GROUP BY Schedule_Job_History.Position, Schedule_Job_History.ID, Schedule_Job_History.TargetLineId, Part_Info.PN, Schedule_Job_History.Status, Part_Info.Desc1, 
                      Part_Info.Name, Schedule_Job_History.Flags
                      ORDER BY Schedule_Job_History.TargetLineId, priority"

    Private Const LineQuery As String = "SELECT eqp_Lines.Id as LineId, eqp_Lines.CustomerId, eqp_Lines.LineName, eqp_Lines.LineDefinition, eqp_Lines.MaxConcurrentLogins, eqp_Lines.WcfFileName, 
                      eqp_Lines.SelectCmd, eqp_Lines.ScheduleFolder, eqp_Lines.SchedulerMethod, eqp_Lines.WorkBufferMinutes, Part_Customers.Name as CustomerName, 
                      Part_Program_Line_Map.ProgramId, Part_Program_Line_Map.LH, Part_Program_Line_Map.RH
                      FROM eqp_Lines INNER JOIN
                      Part_Customers ON eqp_Lines.CustomerId = Part_Customers.Id LEFT OUTER JOIN
                      Part_Program_Line_Map ON eqp_Lines.Id = Part_Program_Line_Map.LineId
                      WHERE (eqp_Lines.SchedulerMethod > 0)
                      ORDER BY CustomerId"


    Private Const GetpartsQuery = "SELECT dbo.Part_Info.PN, dbo.Part_Info.Desc1 AS [Desc], dbo.Part_Program_Line_Map.LineId, dbo.Part_Info.PartId
                      FROM dbo.Part_Info INNER JOIN
                      dbo.Part_Programs ON dbo.Part_Info.ProgId = dbo.Part_Programs.ProgId INNER JOIN
                      dbo.Part_Program_Line_Map ON dbo.Part_Programs.ProgId = dbo.Part_Program_Line_Map.ProgramId AND 
                      dbo.Part_Program_Line_Map.LineId = {0} "





    Public Sub New(LgSvr As iLoggingService, Atools As AppTools)
        _Cfg = Atools.GetProgramParams
        Me.LgSvr = LgSvr
    End Sub

    Public Function ValidateParts(PartReq As ValidatePartsRequest) As ValidatePartsResponse
        Dim Rslt As New ValidatePartsResponse
        Dim PartList As New List(Of Part)

        Dim Pry = PartReq.Parts.Select(Function(x) x.PN).ToArray
        PartReq.Parts.Select(Function(x) x.Valid = False)
        For Each p In Pry
            p = String.Format("'{0}'", p)
        Next

        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} Where Lineid = {1} and Pn in({2})", GetpartsQuery, PartReq.LineData.Id, Pry.ToArray), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Pr = (From x In PartReq.Parts Where x.PN = dRead("PN")).FirstOrDefault
                    If Pr IsNot Nothing Then
                        Pr.Valid = True
                        Pr.Desc = dRead("Desc")
                    End If
                End While
            End Using
        End Using
        Rslt.parts = PartReq.Parts
        Return Rslt
    End Function

    Public Function GetParts(PartReq As GetPartsForLineRequest) As getPartsforLineResponse
        Dim Rslt As New getPartsforLineResponse
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} Where Lineid = {1}", GetpartsQuery, PartReq.LineData.Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim P As New Part
                    P.PN = dRead("PN")
                    P.Desc = dRead("Desc")
                    P.Id = dRead("PartId")
                    Rslt.parts.Add(P)
                End While
            End Using
        End Using
        Return Rslt
    End Function



    Public Function GetLinesData() As GetLinesResponse
        Dim Rslt As New GetLinesResponse

        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()

            Dim Cmd As New SqlClient.SqlCommand(LineQuery, Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New Line
                    With Itm
                        .Description = dRead("LineDefinition")
                        .Id = dRead("LineId")
                        .Name = dRead("LineName")
                        .SchedulerMethod = If(DBNull.Value.Equals(dRead("SchedulerMethod")), "", dRead("SchedulerMethod")) 'dRead("SchedulerMethod")
                        .SelectCmd = dRead("SelectCmd")
                        .WcfFileName = dRead("WcfFileName")
                        .ScheduleFolder = If(DBNull.Value.Equals(dRead("ScheduleFolder")), "", dRead("ScheduleFolder"))
                        .CustomerName = If(DBNull.Value.Equals(dRead("CustomerName")), "", dRead("CustomerName"))
                        .CustomerId = If(DBNull.Value.Equals(dRead("CustomerId")), 0, dRead("CustomerId"))
                        .ProgramId = If(DBNull.Value.Equals(dRead("ProgramId")), 0, dRead("ProgramId"))
                    End With
                    Rslt.Lines.Add(Itm)
                End While
            End Using
        End Using
        Return Rslt
    End Function

    Public Function GetLineData(LineId As Integer) As Line
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()

            Dim Cmd As New SqlClient.SqlCommand(String.Format("{0} where ID = {1}", LineQuery, LineId), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New Line
                    With Itm
                        .Description = dRead("LineDefinition")
                        .Id = dRead("LineId")
                        .Name = dRead("LineName")
                        .SchedulerMethod = dRead("SchedulerMethod")
                        .SelectCmd = dRead("SelectCmd")
                        .WcfFileName = dRead("WcfFileName")
                        .ScheduleFolder = dRead("ScheduleFolder")
                        .CustomerName = dRead("CustomerName")
                        .CustomerId = dRead("CustomerId")
                        .ProgramId = dRead("Program")
                    End With
                    Return Itm
                End While
            End Using
        End Using
        Return Nothing
    End Function

    Public Function updateJobPosition(JobId_Id As Integer, Position As Integer) As TransactionResult
        Dim Rslt As New TransactionResult
        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim UpStr As String = "Update Schedule_Job_History Set Position = {0} where Id = {1}"
                Dim Cmd As New SqlClient.SqlCommand(String.Format(UpStr, Position, JobId_Id), Cn)
                Cn.Close()
                Rslt.Result = Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Update Job Position", ex))
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try
        Return Rslt
    End Function

    Public Function updateJobStatus(JobId_Id As Integer, Status As PlanStatus) As TransactionResult
        Dim Rslt As New TransactionResult
        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim UpStr As String = "Update Schedule_Job_History Set Status = {0} where Id = {1}"
                Dim Cmd As New SqlClient.SqlCommand(String.Format(UpStr, Status, JobId_Id), Cn)
                Cn.Close()
                Rslt.Result = Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            LgSvr.SendAlert(New LogEventArgs("Update Job Status", ex))
            Rslt.Result = -1
            Rslt.ResultString = ex.Message
        End Try
        Return Rslt
    End Function





    Public Function GetPlanData(Line_Id As Integer) As GetPlanResponse
        Dim Pr As New GetPlanResponse
        Dim Items As New List(Of PlanItem)
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format(PlanQuery, Line_Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New PlanItem
                    With Itm
                            .Id = dRead("ID")
                            .Built = If(DBNull.Value.Equals(dRead("Built")), 0, dRead("Built"))
                            .Desc = dRead("Desc")
                            .DueDate = If(DBNull.Value.Equals(dRead("Desc")), "", dRead("Desc"))
                            .Ordered = If(DBNull.Value.Equals(dRead("Ordered")), 0, dRead("Ordered"))
                            .OrderId = dRead("OrderId")
                            .PartNumber = dRead("PartNumber")
                            .QTY = dRead("Qty")
                            .Status = dRead("Status")
                            .Flags = dRead("Flags")
                            .Position = dRead("Position")
                        End With
                        Items.Add(Itm)
                End While
            End Using
        End Using
        Pr.PlanData.AddRange(From x In Items Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended OrElse x.Status = PlanStatus.Planed)
        Return Pr
    End Function


    Public Function SavePlan(PlanItems As List(Of PlanItem)) As TransactionResult
        Dim Tr As New TransactionResult
        Dim Cnt As Integer = PlanItems.Count

        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Dim Cmd As SqlClient.SqlCommand

                For Each R In PlanItems
                    If R.Id > 0 Then
                        Cmd = GetPlanUpdate(R)
                    Else
                        Cmd = GetPlanInsert(R)
                    End If
                    Cmd.Connection = Cn
                    Cmd.ExecuteNonQuery()
                    Cmd.Dispose()
                Next
            End Using
        Catch ex As Exception
            Tr.Result = -1
            Tr.ResultString = "Save plan Error " & ex.Message
        End Try
        Return Tr
    End Function


    Private Function GetPlanInsert(Pi As PlanItem) As SqlClient.SqlCommand
        Dim dCmd As New SqlClient.SqlCommand
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ID", Pi.Id))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CreationDate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ShipDate", Pi.Shipdate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ScheduleDate", Pi.ScheduleDate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartId", Pi.PartId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Quantity", Pi.QTY))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@TargetLineId", Pi.TargetLineId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Position", Pi.Position))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Status", Pi.Status))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@LastUpdate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Flags", Pi.Flags))

        dCmd.CommandText = String.Format("Insert into Schedule_Job_History CreationDate,ShipDate,ScheduleDate,PartId,Quantity,TargetLineId,Position,LastUpdate,Flags 
         Values(@CreationDate,@ShipDate,@ScheduleDate,@PartId,@Quantity,@TargetLineId,@Position,@LastUpdate,@Flags)")
        Return dCmd
    End Function

    Private Function GetPlanUpdate(Pi As PlanItem) As SqlClient.SqlCommand
        Dim dCmd As New SqlClient.SqlCommand
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ID", Pi.Id))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@CreationDate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ShipDate", Pi.Shipdate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ScheduleDate", Pi.ScheduleDate))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@PartId", Pi.PartId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Quantity", Pi.QTY))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@TargetLineId", Pi.TargetLineId))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Position", Pi.Position))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Status", Pi.Status))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@LastUpdate", Now))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Flags", Pi.Flags))

        dCmd.CommandText = String.Format("Update Schedule_Job_History 
            set ShipDate = @ShipDate,
            Quantity = @Quantity,
            Position = @Position,
            LastUpdate = @LastUpdate,
            Status = @Status,
            Flags = @Flags where Status < 3")
        Return dCmd
    End Function





    Private Function GetConnectionString() As String
        Dim CnS As New Data.SqlClient.SqlConnectionStringBuilder
        With CnS
            .DataSource = _Cfg.SqlSeverName
            .UserID = _Cfg.SqlUserName
            .Password = _Cfg.SqlPw
            .InitialCatalog = _Cfg.SqlDbName
        End With

        Return CnS.ConnectionString
    End Function

End Class
