Imports System
Imports System.Data
Imports System.IO


Public Class clsSnapShot
    Dim connectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0; Ole DB Services=-4; Data Source= {0}"
    Dim DatabaseName As String
    Dim SQLCall As String
    Dim isDataPresent As Boolean
    Public Data As System.Data.DataSet
    Dim LastError As String

    Public ReadOnly Property DataPresent()
        Get
            Return isDataPresent
        End Get
    End Property

    Public ReadOnly Property dataError()
        Get
            Return LastError
        End Get
    End Property
    Public Function Fill() As DataSet
        If SQLCall <> vbNullString Then
            If File.Exists(DatabaseName) Then
                LastError = vbNullString
                Data = RefreshData()
                If LastError = vbNullString Then
                    With Data
                        If .Tables.Count > 0 Then
                            If .Tables(0).Rows.Count > 0 Then
                                isDataPresent = True
                            Else
                                isDataPresent = False
                            End If
                        Else
                            isDataPresent = False
                        End If
                    End With
                Else
                    isDataPresent = False
                End If
            Else
                isDataPresent = False
            End If
        Else
            isDataPresent = False
        End If
        Return Data
    End Function

    Public Sub New(myDatabaseName As String, mySQLCall As String)
        DatabaseName = myDatabaseName
        SQLCall = mySQLCall
    End Sub

    Public Function Refresh() As DataSet
        Data = RefreshData()
        Return Data
    End Function

    Private Function RefreshData() As System.Data.DataSet
        Dim FullConString As String = connectionString + DatabaseName
        Dim returnThis As DataSet
        Try
            Dim dbConnection As System.Data.IDbConnection = New System.Data.OleDb.OleDbConnection(FullConString)
            Dim dbCommand As System.Data.IDbCommand = New System.Data.OleDb.OleDbCommand
            dbCommand.CommandText = SQLCall
            dbCommand.Connection = dbConnection
            Dim dataAdapter As System.Data.IDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommand
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet
            dataAdapter.Fill(dataSet)
            Dim key(0) As DataColumn
            key(0) = dataSet.Tables(0).Columns(0)
            dataSet.Tables(0).PrimaryKey = key
            returnThis = dataSet
        Catch ex As Exception
            LastError = ex.Message
            returnThis = Data
        End Try
        Return returnThis
    End Function


End Class
