Imports System.IO
Imports System.Text.RegularExpressions
Imports Scheduler.core.Classes

Public Class AppTools
    Public Sub DetectPath(path As String)
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If
    End Sub

    ''' <summary>
    ''' Returns a time stamp with the 1s and 10s place set to zero for indexing
    ''' </summary>
    Public Function GetUnitxTimeStamp() As Long
        'using integer division "\" to knock off the 1s and 10s place
        Return (DateTimeOffset.Now.ToUnixTimeMilliseconds() \ 100) * 100
        'Return CLng((DateTime.Now - New DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds * 10000) * 100
    End Function

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
        Dim Cpath As String = Path.GetDirectoryName(SvcParams.ParamPathAndFile)
        DetectPath(Cpath)
        If Not File.Exists(SvcParams.ParamPathAndFile) Then
            ConfigurationManager(Of SvcParams).Save(SvcParams.getDefaults)
        End If

        Return ConfigurationManager(Of SvcParams).Load()

    End Function
End Class
