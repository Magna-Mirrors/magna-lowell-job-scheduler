Imports Scheduler.core

Public Class SqlData
    Protected ReadOnly _Cfg As SvcParams

    Private Const PlanQuery As String = "SELECT TOP (100) PERCENT dbo.Schedule_Job_History.TargetLineId,dbo.Schedule_Job_History.Id, MAX(dbo.Schedule_Job_History.Priority) AS Priority, dbo.Part_Info.PN, 
                      SUM(dbo.Schedule_Part_Order_History.Qty) AS Ordered, SUM(dbo.Schedule_Part_Production_History.Qty) AS Built, MAX(dbo.Schedule_Job_History.Quantity) AS Q, 
                      dbo.Schedule_Job_History.ID AS PartId,dbo.Schedule_Job_History.Flags, dbo.Part_Info.Desc1, dbo.Part_Info.Name
                      FROM dbo.Schedule_Job_History INNER JOIN
                      dbo.Schedule_Part_Order_History ON dbo.Schedule_Job_History.ID = dbo.Schedule_Part_Order_History.JobId INNER JOIN
                      dbo.Schedule_Part_Production_History ON dbo.Schedule_Job_History.ID = dbo.Schedule_Part_Production_History.JobId INNER JOIN
                      dbo.Part_Info ON dbo.Schedule_Job_History.PartId = dbo.Part_Info.PartId
                      WHERE (dbo.Schedule_Job_History.Status < 5) AND (dbo.Schedule_Job_History.TargetLineId = {0})
                      GROUP BY dbo.Schedule_Job_History.Priority, dbo.Schedule_Job_History.ID, dbo.Schedule_Job_History.TargetLineId, dbo.Part_Info.PN, dbo.Schedule_Job_History.Status, 
                      dbo.Part_Info.Desc1, dbo.Part_Info.Name"

    Private Const LineQuery As String = "SELECT dbo.eqp_Lines.Id, dbo.eqp_Lines.LineName, dbo.eqp_Lines.LineDefinition, dbo.eqp_Lines.MaxConcurrentLogins, dbo.eqp_Lines.WcfFileName, dbo.eqp_Lines.SelectCmd, 
                                         dbo.eqp_Lines.ScheduleFolder, dbo.eqp_Lines.SchedularMethod, dbo.Part_Programs.Customer, dbo.Part_Programs.Name AS Program
                                         FROM dbo.eqp_Lines INNER JOIN
                                         dbo.Schedule_Line_Program_Compatability_Map ON dbo.eqp_Lines.Id = dbo.Schedule_Line_Program_Compatability_Map.LineId INNER JOIN
                                         dbo.Part_Programs ON dbo.Schedule_Line_Program_Compatability_Map.ProgramId = dbo.Part_Programs.ProgId"

    Private Const GetpartsQuery = "SELECT dbo.Part_Info.PN, dbo.Part_Info.Desc1 AS [Desc], dbo.Schedule_Line_Program_Compatability_Map.LineId, dbo.Part_Info.PartId
                      FROM dbo.Part_Info INNER JOIN
                      dbo.Part_Programs ON dbo.Part_Info.ProgId = dbo.Part_Programs.ProgId INNER JOIN
                      dbo.Schedule_Line_Program_Compatability_Map ON dbo.Part_Programs.ProgId = dbo.Schedule_Line_Program_Compatability_Map.ProgramId AND 
                      dbo.Schedule_Line_Program_Compatability_Map.LineId = {0} "


    Public Sub New()

    End Sub

    Public Sub New(Prms As SvcParams)
        _Cfg = Prms
    End Sub

    Public Function ValidateParts(PartReq As ValidatePartsRequest) As ValidatePartsResponse
        Dim Rslt As New ValidatePartsResponse
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
                        .Id = dRead("Id")
                        .Name = dRead("LineName")
                        .SchedulerMethod = dRead("SchedularMethod")
                        .SelectCmd = dRead("SelectCmd")
                        .WcfFileName = dRead("WcfFileName")
                        .ScheduleFolder = dRead("ScheduleFolder")
                        .Customer = dRead("Customer")
                        .Program = dRead("Program")
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
                        .Id = dRead("Id")
                        .Name = dRead("LineName")
                        .SchedulerMethod = dRead("SchedularMethod")
                        .SelectCmd = dRead("SelectCmd")
                        .WcfFileName = dRead("WcfFileName")
                        .ScheduleFolder = dRead("ScheduleFolder")
                        .Customer = dRead("Customer")
                        .Program = dRead("Program")
                    End With
                    Return Itm
                End While
            End Using
        End Using
        Return Nothing
    End Function



    Public Function GetPlanData(LineData As Line) As GetPlanResponse
        Dim Pr As New GetPlanResponse
        Dim Items As New List(Of PlanItem)
        Using Cn As New SqlClient.SqlConnection(GetConnectionString)
            Cn.Open()
            Dim Cmd As New SqlClient.SqlCommand(String.Format(PlanQuery, LineData.Id), Cn)
            Using dRead As IDataReader = Cmd.ExecuteReader()
                While dRead.Read
                    Dim Itm As New PlanItem
                    With Itm
                        .Id = dRead("ID")
                        .Built = dRead("Built")
                        .Desc = dRead("Desc")
                        .DueDate = dRead("Desc")
                        .Ordered = dRead("Ordered")
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

        Pr.ScheduleData = From x In Items Where x.Status = PlanStatus.Scheduled OrElse x.Status = PlanStatus.Suspended
        Pr.PlanData = From x In Items Where x.Status = PlanStatus.Planed

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
            .Password = _Cfg.UserPw
            .InitialCatalog = _Cfg.SqlDbName
        End With

        Return CnS.ConnectionString
    End Function

End Class
