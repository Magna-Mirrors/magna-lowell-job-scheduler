Public Class Part_Program
	Public Property ProgId As Integer
	Public Property Name As String
	Public Property AttributeFilePath As String

	Public Sub New()

	End Sub

	Public ReadOnly Property AttributesFilepathandName As String
		Get
			Return String.Format("{0}\Attributes.xml", AttributeFilePath)
		End Get
	End Property

	Public ReadOnly Property ItemAttributesFilepathandName As String
		Get
			Return String.Format("{0}\ItemAttributes.xml", AttributeFilePath)
		End Get
	End Property


	Public ReadOnly Property GetItemDataFilepathandName As String
		Get
			Return String.Format("{0}\ItemData.xml", AttributeFilePath)
		End Get
	End Property

	Public ReadOnly Property GetItemExtdescDataFilepathandName() As String
		Get
			Return String.Format("{0}\itemExtDesc.xml", AttributeFilePath)
		End Get
	End Property


End Class
