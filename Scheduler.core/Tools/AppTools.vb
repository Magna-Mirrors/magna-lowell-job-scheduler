Imports System.IO
Imports System.Text.RegularExpressions
Imports Scheduler.core.Classes

Public Class AppTools
    Public Sub DetectPath(path As String)
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If
    End Sub


    Public Function GetOrderPosition(BuildId As String, ByRef mmddyy As String, ByRef hhmm As String) As Integer
        Dim Pos As Integer = 0
        If BuildIdIsValid(BuildId) Then
            Pos = CInt(BuildId.Substring(12, 3))
            mmddyy = BuildId.Substring(0, 6)
            hhmm = BuildId.Substring(7, 4)
        End If
        Return Pos
    End Function

    Public Function BuildIdIsValid(BuildId As String) As Boolean
        Return Regex.IsMatch(BuildId, "[0-9]{6}-[0-9]{4}-[0-9]{3}")
    End Function

    Public Function GetProgramParams() As SvcParams
        Dim Cpath As String = Path.GetDirectoryName(SvcParams.ParamPath)
        DetectPath(Cpath)
        If Not File.Exists(SvcParams.ParamPathAndFile) Then
            ConfigurationManager(Of SvcParams).Save(SvcParams.getDefaults)
        End If

        Return ConfigurationManager(Of SvcParams).Load()

    End Function
End Class
