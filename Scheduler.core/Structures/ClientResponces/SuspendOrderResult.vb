<DataContract()>
Public Class SuspendOrderResult
	<DataMember()> 'return this as the new Schedule item
	Public Property Item As PlanItem
	<DataMember()>
	Public Property Result As Integer
	<DataMember()>
	Public Property ResultString As String
	Public Sub New()
		Item = New PlanItem
		Result = 0
		ResultString = ""
	End Sub
End Class
