Imports System.Windows.Forms

Public Class PartSelectDialog

	Public Property LineData As Line
	Public Property PartData As getPartsforLineResponse
	Public Property SelectedParts As List(Of Part)


	Private Sub RefreshData()
		SelectedParts = New List(Of Part)
		PartsDataSource.DataSource = PartData.parts.OrderBy((Function(x) x.PN))
		SelectedPartsBs.DataSource = SelectedParts
	End Sub

	Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
		Me.DialogResult = System.Windows.Forms.DialogResult.OK

		Me.Close()
	End Sub

	Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
		Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub PartSelectDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		RefreshData()
		ResetColW()
		LinepartsLabel.Text = String.Format("These are the Available {0} parts for Production Line {1}", LineData.CustomerName, LineData.Name)
	End Sub

	Private Sub PartSelectDialog_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
		ResetColW()
	End Sub

	Private Sub ResetColW()
		DataGridView1.Columns(0).Width = DataGridView1.Width * 0.3
		DataGridView1.Columns(1).Width = DataGridView1.Width * 0.3
		DataGridView1.Columns(2).Width = DataGridView1.Width * 0.3


		DataGridView2.Columns(0).Width = DataGridView1.Width * 0.35
		DataGridView2.Columns(1).Width = DataGridView1.Width * 0.5
		DataGridView2.Columns(2).Width = DataGridView1.Width * 0.15
	End Sub

	Private Sub AddToSelected_Click(sender As Object, e As EventArgs) Handles AddToSelected.Click

		If DataGridView1.SelectedRows.Count > 0 Then
			'get the datasouce items from 1 move them to 2 then remove them from 1
			For Each I As DataGridViewRow In DataGridView1.SelectedRows
				Dim Itm As Part = CType(I.DataBoundItem, Part)
				If Itm IsNot Nothing Then
					SelectedParts.Add(Itm)
					PartData.parts.Remove(Itm)
				End If

			Next
			PartsDataSource.ResetBindings(False)
			SelectedPartsBs.ResetBindings(False)
		End If

		OK_Button.Visible = SelectedParts.Count > 0

	End Sub

	Private Sub RemoveFromSelected_Click(sender As Object, e As EventArgs) Handles RemoveFromSelected.Click
		If DataGridView2.SelectedRows.Count > 0 Then
			'get the datasouce items from 2 move them to 1 then remove them from 2
			For Each I As DataGridViewRow In DataGridView2.SelectedRows
				Dim Itm As Part = CType(I.DataBoundItem, Part)
				If Itm IsNot Nothing Then
					Itm.Qty = 0
					PartData.parts.Add(Itm)
					SelectedParts.Remove(Itm)
				End If

			Next
			PartsDataSource.ResetBindings(False)
			SelectedPartsBs.ResetBindings(False)
		End If
		OK_Button.Visible = SelectedParts.Count > 0
	End Sub

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

	End Sub
End Class
