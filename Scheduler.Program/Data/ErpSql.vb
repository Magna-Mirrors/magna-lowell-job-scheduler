Imports Scheduler.core

Public Class ErpSql
    ' .ErpSqlServername = "HOLMSDBDEV02"  'Test server = "HOLMSDBDEV02" '"holmssqlinst01\instance01"
    '.ErpSqlDbName = "MALshopfloorTD"
    '.ErpSqlUserName = "MALsfEOL"
    '.NewErpSqlPw = "test123"
    Protected ReadOnly _Cfg As SvcParams

    Public Sub New(Atools As AppTools)
        _Cfg = Atools.GetProgramParams
    End Sub


    Public Function CommitpartOrder(PrtOrder As core.PartOrder) As TransactionResult
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
