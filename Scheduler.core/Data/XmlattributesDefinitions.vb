Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports Scheduler.core.XmlHandling

''' <summary>
''' host object for the xml files needed to populate part information
''' </summary>
Public Class XmlAttributeData
	Public Sub New()

	End Sub
	Public Property Attributes As attributesROWSET
	Public Property ItemAttributes As ItemattributesROWSET
	Public Property Itemdata As ItemDataROWSET
	Public Property ItemExtDesc As ItemExtDescROWSET
End Class


Namespace XmlHandling
	<XmlRoot(ElementName:="ROW")>
	Public Class attributesXml
		Private _AttributeValue As String
		<XmlElement(ElementName:="workcell")>
		Public Property Workcell As String
		<XmlElement(ElementName:="attributeName")>
		Public Property AttributeName As String
		<XmlElement(ElementName:="attributeTitle")>
		Public Property AttributeTitle As String
		<XmlElement(ElementName:="attributeShortDesc")>
		Public Property AttributeShortDesc As String
		<XmlElement(ElementName:="attributeIndex")>
		Public Property AttributeIndex As String
		<XmlElement(ElementName:="attributeValue")>
		Public Property AttributeValue As String
			Get
				Return Trim(_AttributeValue)
			End Get
			Set(value As String)
				_AttributeValue = value
			End Set
		End Property
		<XmlElement(ElementName:="attributeValueDesc")>
		Public Property AttributeValueDesc As String
	End Class

	<XmlRoot(ElementName:="ROWSET")>
	Public Class attributesROWSET
		<XmlElement(ElementName:="ROW")>
		Public Property ROW As List(Of attributesXml)
	End Class
End Namespace



Namespace XmlHandling
	<XmlRoot(ElementName:="ROW")>
	Public Class ItemattributesXml
		Private It_em As String
		<XmlElement(ElementName:="workcell")>
		Public Property Workcell As String
		<XmlElement(ElementName:="item")>
		Public Property Item As String
			Get
				Return Trim(It_em)
			End Get
			Set(value As String)
				It_em = value
			End Set
		End Property
		<XmlElement(ElementName:="attributeName")>
		Public Property AttributeName As String
		<XmlElement(ElementName:="attributeTitle")>
		Public Property AttributeTitle As String
		<XmlElement(ElementName:="attributeValue")>
		Public Property AttributeValue As String
		<XmlElement(ElementName:="displayOnSummary")>
		Public Property DisplayOnSummary As String
	End Class

	<XmlRoot(ElementName:="ROWSET")>
	Public Class ItemattributesROWSET
		<XmlElement(ElementName:="ROW")>
		Public Property ROW As List(Of ItemattributesXml)
	End Class
End Namespace



Namespace XmlHandling
	<XmlRoot(ElementName:="ROW")>
	Public Class ItemDataXml
		Private mItem As String
		<XmlElement(ElementName:="workcell")>
		Public Property Workcell As String
		<XmlElement(ElementName:="item")>
		Public Property Item As String
			Get
				Return Trim(mItem)
			End Get
			Set(value As String)
				mItem = value
			End Set
		End Property
		<XmlElement(ElementName:="description")>
		Public Property Description As String
		<XmlElement(ElementName:="itemStatus")>
		Public Property ItemStatus As String
		<XmlElement(ElementName:="rtnContainerItem")>
		Public Property RtnContainerItem As String
		<XmlElement(ElementName:="rtnCartonQty")>
		Public Property RtnCartonQty As String
		<XmlElement(ElementName:="rtnPalletQty")>
		Public Property RtnPalletQty As String
		<XmlElement(ElementName:="expContainerItem")>
		Public Property ExpContainerItem As String
		<XmlElement(ElementName:="expCartonQty")>
		Public Property ExpCartonQty As String
		<XmlElement(ElementName:="expPalletQty")>
		Public Property ExpPalletQty As String
		<XmlElement(ElementName:="itemLabelFormat")>
		Public Property ItemLabelFormat As String
		<XmlElement(ElementName:="itemProdFormat")>
		Public Property ItemProdFormat As String
		<XmlElement(ElementName:="unitLabelFormat")>
		Public Property UnitLabelFormat As String
		<XmlElement(ElementName:="serviceLabelFormat")>
		Public Property ServiceLabelFormat As String
		<XmlElement(ElementName:="cartonLabelType")>
		Public Property CartonLabelType As String
		<XmlElement(ElementName:="masterLabelType")>
		Public Property MasterLabelType As String
		<XmlElement(ElementName:="cartonLabelTypeAlt")>
		Public Property CartonLabelTypeAlt As String
		<XmlElement(ElementName:="masterLabelTypeAlt")>
		Public Property MasterLabelTypeAlt As String
		<XmlElement(ElementName:="customerPart")>
		Public Property CustomerPart As String
		<XmlElement(ElementName:="enableAutoLink")>
		Public Property EnableAutoLink As String
		<XmlElement(ElementName:="checkList")>
		Public Property CheckList As String
		<XmlElement(ElementName:="signalCode")>
		Public Property SignalCode As String
	End Class

	<XmlRoot(ElementName:="ROWSET")>
	Public Class ItemDataROWSET
		<XmlElement(ElementName:="ROW")>
		Public Property ROW As List(Of ItemDataXml)
	End Class
End Namespace



Namespace XmlHandling
	<XmlRoot(ElementName:="ROW")>
	Public Class ItemExtDescXml
		Private mItem As String
		<XmlElement(ElementName:="workcell")>
		Public Property Workcell As String
		<XmlElement(ElementName:="item")>
		Public Property Item As String
			Get
				Return Trim(mItem)
			End Get
			Set(value As String)
				mItem = value
			End Set
		End Property
		<XmlElement(ElementName:="company")>
		Public Property Company As String
		<XmlElement(ElementName:="fieldName")>
		Public Property FieldName As String
		<XmlElement(ElementName:="fieldValue")>
		Public Property FieldValue As String
	End Class

	<XmlRoot(ElementName:="ROWSET")>
	Public Class ItemExtDescROWSET
		<XmlElement(ElementName:="ROW")>
		Public Property ROW As List(Of ItemExtDescXml)
	End Class
End Namespace




