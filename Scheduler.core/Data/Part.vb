Imports System.Runtime.Serialization

<DataContract(Name:="Part"), Serializable>
Public Class Part
	Private _D As String
	<DataMember()>
	Public Property Id As Integer?
	<DataMember()>
	Public Property [PN] As String
	<DataMember()>
	Public Property [ColorName] As String
	<DataMember()>
	Public Property [Desc] As String
	<DataMember()>
	Public Property Hand As String

	<DataMember()>
	Public Property Valid As Boolean

	<DataMember()>
	Public Property Qty As Integer

	<DataMember()>
	Public Property HandAndDesc As String
		Get
			Return String.Format("({0}) {1}", Hand, Desc)
		End Get
		Set(value As String)
			_D = value
		End Set

	End Property

	Public Sub New()
		Id = Nothing
		Desc = String.Empty
		ColorName = String.Empty
		Valid = False
		Hand = String.Empty
		Qty = 0
	End Sub

End Class
