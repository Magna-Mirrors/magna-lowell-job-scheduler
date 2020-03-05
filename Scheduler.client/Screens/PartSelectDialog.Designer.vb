<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PartSelectDialog
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.RemoveFromSelected = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.AddToSelected = New System.Windows.Forms.Button()
		Me.DataGridView2 = New System.Windows.Forms.DataGridView()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.OK_Button = New System.Windows.Forms.Button()
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.LinepartsLabel = New System.Windows.Forms.Label()
		Me.DataGridView1 = New System.Windows.Forms.DataGridView()
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
		Me.SelectedPartsBs = New System.Windows.Forms.BindingSource(Me.components)
		Me.PartsDataSource = New System.Windows.Forms.BindingSource(Me.components)
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.PNDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ColorName = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TableLayoutPanel2.SuspendLayout()
		CType(Me.SelectedPartsBs, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.PartsDataSource, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'RemoveFromSelected
		'
		Me.RemoveFromSelected.Dock = System.Windows.Forms.DockStyle.Fill
		Me.RemoveFromSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RemoveFromSelected.Location = New System.Drawing.Point(396, 249)
		Me.RemoveFromSelected.Name = "RemoveFromSelected"
		Me.RemoveFromSelected.Size = New System.Drawing.Size(117, 44)
		Me.RemoveFromSelected.TabIndex = 17
		Me.RemoveFromSelected.Text = "<< Remove"
		Me.RemoveFromSelected.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(709, 12)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(155, 13)
		Me.Label3.TabIndex = 15
		Me.Label3.Text = "Enter Quantities You need here"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(519, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(184, 25)
		Me.Label2.TabIndex = 14
		Me.Label2.Text = "Selected parts"
		'
		'AddToSelected
		'
		Me.AddToSelected.Dock = System.Windows.Forms.DockStyle.Fill
		Me.AddToSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AddToSelected.Location = New System.Drawing.Point(396, 99)
		Me.AddToSelected.Name = "AddToSelected"
		Me.AddToSelected.Size = New System.Drawing.Size(117, 44)
		Me.AddToSelected.TabIndex = 16
		Me.AddToSelected.Text = "ADD   >>"
		Me.AddToSelected.UseVisualStyleBackColor = True
		'
		'DataGridView2
		'
		Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DataGridView2.AutoGenerateColumns = False
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Desc, Me.DataGridViewTextBoxColumn3})
		Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridView2, 2)
		Me.DataGridView2.DataSource = Me.SelectedPartsBs
		Me.DataGridView2.Location = New System.Drawing.Point(519, 28)
		Me.DataGridView2.Name = "DataGridView2"
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView2.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
		Me.TableLayoutPanel1.SetRowSpan(Me.DataGridView2, 6)
		Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.DataGridView2.Size = New System.Drawing.Size(355, 342)
		Me.DataGridView2.TabIndex = 13
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(3, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(237, 25)
		Me.Label1.TabIndex = 12
		Me.Label1.Text = "Available Parts"
		'
		'OK_Button
		'
		Me.OK_Button.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
		Me.OK_Button.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.OK_Button.Location = New System.Drawing.Point(3, 3)
		Me.OK_Button.Name = "OK_Button"
		Me.OK_Button.Size = New System.Drawing.Size(130, 56)
		Me.OK_Button.TabIndex = 0
		Me.OK_Button.Text = "Add To Plan"
		Me.OK_Button.UseVisualStyleBackColor = False
		Me.OK_Button.Visible = False
		'
		'Cancel_Button
		'
		Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.Cancel_Button.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(139, 3)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(130, 56)
		Me.Cancel_Button.TabIndex = 1
		Me.Cancel_Button.Text = "Cancel"
		'
		'LinepartsLabel
		'
		Me.LinepartsLabel.BackColor = System.Drawing.Color.Black
		Me.LinepartsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.LinepartsLabel.Dock = System.Windows.Forms.DockStyle.Top
		Me.LinepartsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LinepartsLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
		Me.LinepartsLabel.Location = New System.Drawing.Point(0, 0)
		Me.LinepartsLabel.Name = "LinepartsLabel"
		Me.LinepartsLabel.Size = New System.Drawing.Size(882, 22)
		Me.LinepartsLabel.TabIndex = 11
		Me.LinepartsLabel.Tag = "Available parts for {0}"
		Me.LinepartsLabel.Text = "These are the Available {0} parts for Production Line {1}, LineData.CustomerName," &
	" LineData.Name"
		Me.LinepartsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		'
		'DataGridView1
		'
		Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DataGridView1.AutoGenerateColumns = False
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PNDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn2, Me.ColorName})
		Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridView1, 2)
		Me.DataGridView1.DataSource = Me.PartsDataSource
		Me.DataGridView1.Location = New System.Drawing.Point(3, 28)
		Me.DataGridView1.Name = "DataGridView1"
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
		Me.TableLayoutPanel1.SetRowSpan(Me.DataGridView1, 6)
		Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.DataGridView1.Size = New System.Drawing.Size(387, 342)
		Me.DataGridView1.TabIndex = 10
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel2.ColumnCount = 2
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.OK_Button, 0, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.Cancel_Button, 1, 0)
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(607, 411)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 1
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(272, 62)
		Me.TableLayoutPanel2.TabIndex = 9
		'
		'SelectedPartsBs
		'
		Me.SelectedPartsBs.DataSource = GetType(Scheduler.core.Part)
		'
		'PartsDataSource
		'
		Me.PartsDataSource.DataSource = GetType(Scheduler.core.Part)
		Me.PartsDataSource.Sort = "PN"
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel1.ColumnCount = 5
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.76573!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.13666!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.09978!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.69197!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.30585!))
		Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.RemoveFromSelected, 2, 5)
		Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.AddToSelected, 2, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.Label3, 4, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.DataGridView2, 3, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.Label2, 3, 0)
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 25)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 8
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.706587!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.16168!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.38323!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.38323!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.38323!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.38323!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.5988!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(877, 382)
		Me.TableLayoutPanel1.TabIndex = 18
		'
		'PNDataGridViewTextBoxColumn
		'
		Me.PNDataGridViewTextBoxColumn.DataPropertyName = "PN"
		Me.PNDataGridViewTextBoxColumn.HeaderText = "PN"
		Me.PNDataGridViewTextBoxColumn.Name = "PNDataGridViewTextBoxColumn"
		Me.PNDataGridViewTextBoxColumn.ReadOnly = True
		'
		'DataGridViewTextBoxColumn2
		'
		Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.DataGridViewTextBoxColumn2.DataPropertyName = "Desc"
		Me.DataGridViewTextBoxColumn2.HeaderText = "Desc"
		Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
		'
		'ColorName
		'
		Me.ColorName.DataPropertyName = "ColorName"
		Me.ColorName.HeaderText = "ColorName"
		Me.ColorName.Name = "ColorName"
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.DataPropertyName = "PN"
		Me.DataGridViewTextBoxColumn1.HeaderText = "PN"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.ReadOnly = True
		'
		'Desc
		'
		Me.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.Desc.DataPropertyName = "Desc"
		Me.Desc.HeaderText = "Desc"
		Me.Desc.Name = "Desc"
		'
		'DataGridViewTextBoxColumn3
		'
		Me.DataGridViewTextBoxColumn3.DataPropertyName = "Qty"
		Me.DataGridViewTextBoxColumn3.HeaderText = "QTY"
		Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
		'
		'PartSelectDialog
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(882, 477)
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.Controls.Add(Me.LinepartsLabel)
		Me.Controls.Add(Me.TableLayoutPanel2)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "PartSelectDialog"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "PartSelectDialog"
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TableLayoutPanel2.ResumeLayout(False)
		CType(Me.SelectedPartsBs, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.PartsDataSource, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents RemoveFromSelected As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents SelectedPartsBs As BindingSource
	Friend WithEvents AddToSelected As Button
	Friend WithEvents DataGridView2 As DataGridView
	Friend WithEvents Label1 As Label
	Friend WithEvents PartsDataSource As BindingSource
	Friend WithEvents OK_Button As Button
	Friend WithEvents Cancel_Button As Button
	Friend WithEvents LinepartsLabel As Label
	Friend WithEvents DataGridView1 As DataGridView
	Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
	Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents Desc As DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents PNDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents ColorName As DataGridViewTextBoxColumn
End Class
