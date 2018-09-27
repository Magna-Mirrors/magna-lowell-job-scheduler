Imports System.Text
Imports Scheduler.core

Public Class ErpSql
    ' .ErpSqlServername = "HOLMSDBDEV02"  'Test server = "HOLMSDBDEV02" '"holmssqlinst01\instance01"
    '.ErpSqlDbName = "MALshopfloorTD"
    '.ErpSqlUserName = "MALsfEOL"
    '.NewErpSqlPw = "test123"
    Protected ReadOnly _Cfg As SvcParams
    Private ReadOnly LgSvr As iLoggingService

    Public Sub New(Atools As AppTools, mLgSvr As iLoggingService)
        _Cfg = Atools.GetProgramParams
        LgSvr = mLgSvr
    End Sub


    Public Function Commit_To_PartCounts_Table(PrtOrder As core.PartOrder) As TransactionResult
        Dim Tr As New TransactionResult
        Try
            Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                Cn.Open()
                Dim Cmd = GetOrderCommitCommand(PrtOrder)
                Cmd.Connection = Cn
                Tr.Result = Cmd.ExecuteNonQuery()
                Cmd.Dispose()
            End Using
        Catch ex As Exception
            Tr.Result = -1
            Tr.ResultString = "Save plan Error " & ex.Message
        End Try
        Return Tr

    End Function




    Private Function GetOrderCommitCommand(PrtOrder As core.PartOrder) As SqlClient.SqlCommand
        'ProdCounts_id   Workcell	ProdDate	ProdOrder	ProdItem	Qty_Passed	Qty_Failed	ProdCode	Operator	Posted
        Dim dCmd As New SqlClient.SqlCommand
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Workcell", PrtOrder.WC))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ProdDate", SqlDbType.DateTimeOffset) With {.SqlValue = Now}) ' Format(Now, “MM/DD/YY HH:mm:SS”)
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ProdOrder", PrtOrder.Id))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ProdItem", PrtOrder.partnumber))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Qty_Passed", PrtOrder.Qty))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Qty_Failed", 0))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@ProdCode", If(PrtOrder.PartRequestType = PartOrderType.Built, "BUILT", "ORDER")))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Operator", ""))
        dCmd.Parameters.Add(New SqlClient.SqlParameter("@Posted", 0))
        dCmd.CommandText = String.Format("INSERT INTO [dbo].[ProdCounts]([Workcell], [ProdDate], [ProdOrder], [ProdItem], [Qty_Passed], [Qty_Failed], [ProdCode], [Operator], [Posted])
        VALUES(@Workcell,@ProdDate,@ProdOrder,@ProdItem,@Qty_Passed,@Qty_Failed,@ProdCode,@Operator,@Posted)")
        Return dCmd
    End Function





    Private Function GetBuildCommitCommand(ParamArray BuiltItems() As core.BuildItem) As SqlClient.SqlCommand
        Dim dCmd As New SqlClient.SqlCommand
        Dim Sb As New StringBuilder
        Dim P As core.BuildItem
        For i = 0 To BuiltItems.Count - 1
            P = BuiltItems(i)
            Sb.AppendLine(String.Format("('{0}','{1}',{2},'{3}',{4},0,'BUILT',{5},0)", P.Workcell, P.ProdDate, P.ProdOrder, P.ProdItem, P.Qty_Passed, P.Operator))
            If i < (BuiltItems.Count - 1) Then
                Sb.Append(",")
            End If
        Next
        dCmd.CommandText = String.Format("INSERT INTO [dbo].[ProdCounts]([Workcell], [ProdDate], [ProdOrder], [ProdItem], [Qty_Passed], [Qty_Failed], [ProdCode], [Operator], [Posted]) VALUES ") & Sb.ToString
        Return dCmd
    End Function

    Public Function Commit_Built_Items_To_ProdCounts(Items As List(Of BuildItem)) As TransactionResult
        Dim Tr As New TransactionResult
        Try
            If Items IsNot Nothing AndAlso Items.Count > 0 Then
                Using Cn As New SqlClient.SqlConnection(GetConnectionString)
                    Cn.Open()
                    Dim Cmd = GetBuildCommitCommand(Items.ToArray)
                    Cmd.Connection = Cn
                    Tr.Result = Cmd.ExecuteNonQuery()
                    Cmd.Dispose()
                End Using
            Else
                Tr.Result = 0
                Tr.ResultString = "Nothing To post"
            End If

        Catch ex As Exception
            Tr.Result = -1
            Tr.ResultString = "Commit_Built_Items_To_ProdCounts " & ex.Message
            LgSvr.SendAlert(New LogEventArgs("Commit_Built_Items_To_ProdCounts", ex))
        End Try
        Return Tr
    End Function


    Private Function GetConnectionString() As String
        Dim CnS As New Data.SqlClient.SqlConnectionStringBuilder
        With CnS
            .DataSource = _Cfg.ErpSqlServername
            .UserID = _Cfg.ErpSqlUserName
            .Password = _Cfg.ErpSqlPassword
            .InitialCatalog = _Cfg.ErpSqlDbName
        End With

        Return CnS.ConnectionString
    End Function
End Class
