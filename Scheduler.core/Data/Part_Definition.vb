Public Class Part_Definition
	Public Property Part_Id As Integer
	Public Property ProgramId As Integer
	Public Property PartNumber As String
	''' <summary>
	''' Customer part number
	''' </summary>
	''' <returns></returns>
	Public Property CPN As String
	Public Property NestIdx As Integer
	Public Property ColIdx As Integer
	Public Property SpecialCode As String
	Public Property Service As Boolean
	Public Property StyleIdx As Integer
	Public Property PartsPerTote As Integer

	''' <summary>
	''' Production Readieness
	''' </summary>
	''' <returns></returns>
	Public Property Status As Part_Definition_Status
	Public Property Note As String
	Public Property AddIt As Boolean
	Public Property UpdateIt As Boolean

	Public Sub New()
		CPN = String.Empty
		SpecialCode = String.Empty
		Note = String.Empty
		Status = Part_Definition_Status.NewleyInserted
	End Sub

End Class
