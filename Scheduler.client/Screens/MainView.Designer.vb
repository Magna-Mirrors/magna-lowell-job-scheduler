﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainView))
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.LineTree = New System.Windows.Forms.TreeView()
		Me.ilCustomers = New System.Windows.Forms.ImageList(Me.components)
		Me.lblLineName = New System.Windows.Forms.Label()
		Me.dgvEdit = New System.Windows.Forms.DataGridView()
		Me.PartNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.QTYDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ShipdateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Truck = New System.Windows.Forms.DataGridViewCheckBoxColumn()
		Me.Chk = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.CustOrderId = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.mnuEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.AppendPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OverwritePasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.InsertPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.PlandataSource = New System.Windows.Forms.BindingSource(Me.components)
		Me.dgv = New System.Windows.Forms.DataGridView()
		Me.PartNumberDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ShipdateDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.QTYDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.BuiltDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.OrderedDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
		Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ScheduleDataSource = New System.Windows.Forms.BindingSource(Me.components)
		Me.lblNew = New System.Windows.Forms.Label()
		Me.btnRefresh = New System.Windows.Forms.Button()
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.cmdSendPlan = New System.Windows.Forms.Button()
		Me.BtnApproveEdits = New System.Windows.Forms.Button()
		Me.cmdReadPlan = New System.Windows.Forms.Button()
		Me.cmdUp = New System.Windows.Forms.Button()
		Me.cmdDown = New System.Windows.Forms.Button()
		Me.lblMsg = New System.Windows.Forms.Label()
		Me.DgvHistory = New System.Windows.Forms.DataGridView()
		Me.PartNumberDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ShipdateDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.QTYDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.BuiltDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.OrderedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.StatusDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.CompleteAndRemovedDataSource = New System.Windows.Forms.BindingSource(Me.components)
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.GetPartListPb = New System.Windows.Forms.Button()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.PullXmlPartAttributeDataNowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		CType(Me.dgvEdit, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.mnuEdit.SuspendLayout()
		CType(Me.PlandataSource, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.ScheduleDataSource, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TableLayoutPanel1.SuspendLayout()
		CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CompleteAndRemovedDataSource, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		Me.MenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'LineTree
		'
		Me.LineTree.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.LineTree.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LineTree.HideSelection = False
		Me.LineTree.ImageIndex = 0
		Me.LineTree.ImageList = Me.ilCustomers
		Me.LineTree.ItemHeight = 20
		Me.LineTree.Location = New System.Drawing.Point(13, 57)
		Me.LineTree.Name = "LineTree"
		Me.TableLayoutPanel1.SetRowSpan(Me.LineTree, 10)
		Me.LineTree.SelectedImageIndex = 0
		Me.LineTree.Size = New System.Drawing.Size(194, 521)
		Me.LineTree.TabIndex = 0
		'
		'ilCustomers
		'
		Me.ilCustomers.ImageStream = CType(resources.GetObject("ilCustomers.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.ilCustomers.TransparentColor = System.Drawing.Color.Transparent
		Me.ilCustomers.Images.SetKeyName(0, "12.ico")
		Me.ilCustomers.Images.SetKeyName(1, "Chrysler-logo5375.jpg")
		Me.ilCustomers.Images.SetKeyName(2, "GM.jpg")
		Me.ilCustomers.Images.SetKeyName(3, "Toyota.jpg")
		Me.ilCustomers.Images.SetKeyName(4, "ford.jpg")
		Me.ilCustomers.Images.SetKeyName(5, "magna.jpg")
		Me.ilCustomers.Images.SetKeyName(6, "BMW.jpg")
		Me.ilCustomers.Images.SetKeyName(7, "Mercedes.jpg")
		Me.ilCustomers.Images.SetKeyName(8, "mazda_logo.jpg")
		Me.ilCustomers.Images.SetKeyName(9, "dcx.jpg")
		Me.ilCustomers.Images.SetKeyName(10, "mazda1.jpg")
		Me.ilCustomers.Images.SetKeyName(11, "dc-logo.jpg")
		Me.ilCustomers.Images.SetKeyName(12, "TOYOTA_logo.jpg")
		Me.ilCustomers.Images.SetKeyName(13, "editing.ico")
		'
		'lblLineName
		'
		Me.lblLineName.Dock = System.Windows.Forms.DockStyle.Top
		Me.lblLineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblLineName.Location = New System.Drawing.Point(0, 0)
		Me.lblLineName.Name = "lblLineName"
		Me.lblLineName.Size = New System.Drawing.Size(1349, 33)
		Me.lblLineName.TabIndex = 2
		Me.lblLineName.Text = "Select Line to Edit"
		'
		'dgvEdit
		'
		Me.dgvEdit.AutoGenerateColumns = False
		Me.dgvEdit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.dgvEdit.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dgvEdit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PartNumberDataGridViewTextBoxColumn, Me.QTYDataGridViewTextBoxColumn, Me.ShipdateDataGridViewTextBoxColumn, Me.Truck, Me.Chk, Me.CustOrderId})
		Me.dgvEdit.ContextMenuStrip = Me.mnuEdit
		Me.dgvEdit.DataSource = Me.PlandataSource
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.dgvEdit.DefaultCellStyle = DataGridViewCellStyle4
		Me.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dgvEdit.Location = New System.Drawing.Point(213, 57)
		Me.dgvEdit.MultiSelect = False
		Me.dgvEdit.Name = "dgvEdit"
		Me.dgvEdit.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
		Me.dgvEdit.RowHeadersVisible = False
		Me.dgvEdit.RowHeadersWidth = 20
		Me.TableLayoutPanel1.SetRowSpan(Me.dgvEdit, 9)
		Me.dgvEdit.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.dgvEdit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.dgvEdit.Size = New System.Drawing.Size(504, 475)
		Me.dgvEdit.TabIndex = 3
		'
		'PartNumberDataGridViewTextBoxColumn
		'
		Me.PartNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.PartNumberDataGridViewTextBoxColumn.DataPropertyName = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn.HeaderText = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn.MaxInputLength = 82
		Me.PartNumberDataGridViewTextBoxColumn.Name = "PartNumberDataGridViewTextBoxColumn"
		Me.PartNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'QTYDataGridViewTextBoxColumn
		'
		Me.QTYDataGridViewTextBoxColumn.DataPropertyName = "QTY"
		DataGridViewCellStyle2.Format = "N0"
		DataGridViewCellStyle2.NullValue = Nothing
		Me.QTYDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
		Me.QTYDataGridViewTextBoxColumn.HeaderText = "QTY"
		Me.QTYDataGridViewTextBoxColumn.Name = "QTYDataGridViewTextBoxColumn"
		Me.QTYDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'ShipdateDataGridViewTextBoxColumn
		'
		Me.ShipdateDataGridViewTextBoxColumn.DataPropertyName = "Desc"
		DataGridViewCellStyle3.Format = "g"
		DataGridViewCellStyle3.NullValue = Nothing
		Me.ShipdateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
		Me.ShipdateDataGridViewTextBoxColumn.HeaderText = "Description"
		Me.ShipdateDataGridViewTextBoxColumn.MaxInputLength = 48
		Me.ShipdateDataGridViewTextBoxColumn.Name = "ShipdateDataGridViewTextBoxColumn"
		Me.ShipdateDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'Truck
		'
		Me.Truck.DataPropertyName = "Truck"
		Me.Truck.FalseValue = "0"
		Me.Truck.HeaderText = "Truck"
		Me.Truck.Name = "Truck"
		Me.Truck.TrueValue = "1"
		'
		'Chk
		'
		Me.Chk.DataPropertyName = "Chk"
		Me.Chk.HeaderText = "Chk"
		Me.Chk.MaxInputLength = 12
		Me.Chk.Name = "Chk"
		Me.Chk.ReadOnly = True
		Me.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
		Me.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'CustOrderId
		'
		Me.CustOrderId.DataPropertyName = "CustOrderId"
		Me.CustOrderId.HeaderText = "CustOrderId"
		Me.CustOrderId.Name = "CustOrderId"
		'
		'mnuEdit
		'
		Me.mnuEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AppendPasteToolStripMenuItem, Me.OverwritePasteToolStripMenuItem, Me.InsertPasteToolStripMenuItem})
		Me.mnuEdit.Name = "mnuEdit"
		Me.mnuEdit.Size = New System.Drawing.Size(157, 70)
		'
		'AppendPasteToolStripMenuItem
		'
		Me.AppendPasteToolStripMenuItem.Name = "AppendPasteToolStripMenuItem"
		Me.AppendPasteToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
		Me.AppendPasteToolStripMenuItem.Text = "Append Paste"
		'
		'OverwritePasteToolStripMenuItem
		'
		Me.OverwritePasteToolStripMenuItem.Name = "OverwritePasteToolStripMenuItem"
		Me.OverwritePasteToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
		Me.OverwritePasteToolStripMenuItem.Text = "Overwrite Paste"
		'
		'InsertPasteToolStripMenuItem
		'
		Me.InsertPasteToolStripMenuItem.Name = "InsertPasteToolStripMenuItem"
		Me.InsertPasteToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
		Me.InsertPasteToolStripMenuItem.Text = "Insert Paste"
		'
		'PlandataSource
		'
		Me.PlandataSource.DataSource = GetType(Scheduler.core.PlanItem)
		'
		'dgv
		'
		Me.dgv.AllowUserToAddRows = False
		Me.dgv.AllowUserToDeleteRows = False
		Me.dgv.AllowUserToResizeColumns = False
		Me.dgv.AllowUserToResizeRows = False
		Me.dgv.AutoGenerateColumns = False
		Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
		Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PartNumberDataGridViewTextBoxColumn1, Me.ShipdateDataGridViewTextBoxColumn1, Me.QTYDataGridViewTextBoxColumn1, Me.BuiltDataGridViewTextBoxColumn1, Me.OrderedDataGridViewTextBoxColumn1, Me.DataGridViewCheckBoxColumn1, Me.Status})
		Me.dgv.DataSource = Me.ScheduleDataSource
		DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.dgv.DefaultCellStyle = DataGridViewCellStyle8
		Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
		Me.dgv.Location = New System.Drawing.Point(823, 57)
		Me.dgv.MultiSelect = False
		Me.dgv.Name = "dgv"
		Me.dgv.ReadOnly = True
		Me.dgv.RowHeadersVisible = False
		Me.dgv.RowHeadersWidth = 20
		Me.TableLayoutPanel1.SetRowSpan(Me.dgv, 4)
		Me.dgv.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.dgv.ShowEditingIcon = False
		Me.dgv.Size = New System.Drawing.Size(504, 219)
		Me.dgv.TabIndex = 5
		'
		'PartNumberDataGridViewTextBoxColumn1
		'
		Me.PartNumberDataGridViewTextBoxColumn1.DataPropertyName = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn1.HeaderText = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn1.Name = "PartNumberDataGridViewTextBoxColumn1"
		Me.PartNumberDataGridViewTextBoxColumn1.ReadOnly = True
		'
		'ShipdateDataGridViewTextBoxColumn1
		'
		Me.ShipdateDataGridViewTextBoxColumn1.DataPropertyName = "Shipdate"
		DataGridViewCellStyle6.Format = "g"
		DataGridViewCellStyle6.NullValue = Nothing
		Me.ShipdateDataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle6
		Me.ShipdateDataGridViewTextBoxColumn1.HeaderText = "Shipdate"
		Me.ShipdateDataGridViewTextBoxColumn1.Name = "ShipdateDataGridViewTextBoxColumn1"
		Me.ShipdateDataGridViewTextBoxColumn1.ReadOnly = True
		'
		'QTYDataGridViewTextBoxColumn1
		'
		Me.QTYDataGridViewTextBoxColumn1.DataPropertyName = "QTY"
		DataGridViewCellStyle7.Format = "N0"
		DataGridViewCellStyle7.NullValue = Nothing
		Me.QTYDataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle7
		Me.QTYDataGridViewTextBoxColumn1.HeaderText = "QTY"
		Me.QTYDataGridViewTextBoxColumn1.Name = "QTYDataGridViewTextBoxColumn1"
		Me.QTYDataGridViewTextBoxColumn1.ReadOnly = True
		'
		'BuiltDataGridViewTextBoxColumn1
		'
		Me.BuiltDataGridViewTextBoxColumn1.DataPropertyName = "Built"
		Me.BuiltDataGridViewTextBoxColumn1.HeaderText = "Built"
		Me.BuiltDataGridViewTextBoxColumn1.Name = "BuiltDataGridViewTextBoxColumn1"
		Me.BuiltDataGridViewTextBoxColumn1.ReadOnly = True
		'
		'OrderedDataGridViewTextBoxColumn1
		'
		Me.OrderedDataGridViewTextBoxColumn1.DataPropertyName = "Ordered"
		Me.OrderedDataGridViewTextBoxColumn1.HeaderText = "Ordered"
		Me.OrderedDataGridViewTextBoxColumn1.Name = "OrderedDataGridViewTextBoxColumn1"
		Me.OrderedDataGridViewTextBoxColumn1.ReadOnly = True
		'
		'DataGridViewCheckBoxColumn1
		'
		Me.DataGridViewCheckBoxColumn1.DataPropertyName = "Truck"
		Me.DataGridViewCheckBoxColumn1.HeaderText = "Truck"
		Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
		Me.DataGridViewCheckBoxColumn1.ReadOnly = True
		'
		'Status
		'
		Me.Status.DataPropertyName = "Status"
		Me.Status.HeaderText = "Status"
		Me.Status.Name = "Status"
		Me.Status.ReadOnly = True
		'
		'ScheduleDataSource
		'
		Me.ScheduleDataSource.DataSource = GetType(Scheduler.core.PlanItem)
		'
		'lblNew
		'
		Me.lblNew.BackColor = System.Drawing.Color.White
		Me.lblNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNew.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblNew.Location = New System.Drawing.Point(213, 0)
		Me.lblNew.Name = "lblNew"
		Me.lblNew.Size = New System.Drawing.Size(504, 54)
		Me.lblNew.TabIndex = 6
		Me.lblNew.Text = "Plan Items"
		Me.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnRefresh
		'
		Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill
		Me.btnRefresh.Location = New System.Drawing.Point(13, 3)
		Me.btnRefresh.Name = "btnRefresh"
		Me.btnRefresh.Size = New System.Drawing.Size(194, 48)
		Me.btnRefresh.TabIndex = 16
		Me.btnRefresh.Text = "Refresh"
		Me.btnRefresh.UseVisualStyleBackColor = True
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel1.ColumnCount = 6
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.btnRefresh, 1, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.LineTree, 1, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.dgvEdit, 2, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.lblNew, 2, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.Label1, 4, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.cmdSendPlan, 3, 10)
		Me.TableLayoutPanel1.Controls.Add(Me.BtnApproveEdits, 3, 6)
		Me.TableLayoutPanel1.Controls.Add(Me.cmdReadPlan, 3, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.cmdUp, 3, 3)
		Me.TableLayoutPanel1.Controls.Add(Me.dgv, 4, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.cmdDown, 3, 4)
		Me.TableLayoutPanel1.Controls.Add(Me.lblMsg, 1, 11)
		Me.TableLayoutPanel1.Controls.Add(Me.DgvHistory, 4, 7)
		Me.TableLayoutPanel1.Controls.Add(Me.Label2, 4, 6)
		Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 4, 5)
		Me.TableLayoutPanel1.Controls.Add(Me.GetPartListPb, 2, 10)
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(-1, 60)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 13
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.64225!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.678595!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.820668!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.786913!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(1350, 616)
		Me.TableLayoutPanel1.TabIndex = 18
		'
		'Label1
		'
		Me.Label1.BackColor = System.Drawing.Color.White
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(823, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(504, 54)
		Me.Label1.TabIndex = 17
		Me.Label1.Text = "Scheduled Items"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'cmdSendPlan
		'
		Me.cmdSendPlan.BackColor = System.Drawing.Color.Yellow
		Me.cmdSendPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.briefcase_upload
		Me.cmdSendPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.cmdSendPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSendPlan.Location = New System.Drawing.Point(723, 538)
		Me.cmdSendPlan.Name = "cmdSendPlan"
		Me.cmdSendPlan.Size = New System.Drawing.Size(94, 40)
		Me.cmdSendPlan.TabIndex = 8
		Me.cmdSendPlan.Text = "Save Plan"
		Me.cmdSendPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSendPlan.UseVisualStyleBackColor = False
		'
		'BtnApproveEdits
		'
		Me.BtnApproveEdits.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.approve
		Me.BtnApproveEdits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.BtnApproveEdits.Dock = System.Windows.Forms.DockStyle.Fill
		Me.BtnApproveEdits.Enabled = False
		Me.BtnApproveEdits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BtnApproveEdits.Location = New System.Drawing.Point(723, 333)
		Me.BtnApproveEdits.Name = "BtnApproveEdits"
		Me.BtnApproveEdits.Size = New System.Drawing.Size(94, 45)
		Me.BtnApproveEdits.TabIndex = 4
		Me.BtnApproveEdits.Text = "Validate Edits"
		Me.BtnApproveEdits.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.BtnApproveEdits.UseVisualStyleBackColor = True
		Me.BtnApproveEdits.Visible = False
		'
		'cmdReadPlan
		'
		Me.cmdReadPlan.BackColor = System.Drawing.Color.Yellow
		Me.cmdReadPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.briefcase_download
		Me.cmdReadPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.cmdReadPlan.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cmdReadPlan.Enabled = False
		Me.cmdReadPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdReadPlan.Location = New System.Drawing.Point(723, 57)
		Me.cmdReadPlan.Name = "cmdReadPlan"
		Me.cmdReadPlan.Size = New System.Drawing.Size(94, 66)
		Me.cmdReadPlan.TabIndex = 9
		Me.cmdReadPlan.Text = "Get Plan"
		Me.cmdReadPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdReadPlan.UseVisualStyleBackColor = False
		'
		'cmdUp
		'
		Me.cmdUp.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.arrow_up
		Me.cmdUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.cmdUp.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cmdUp.Enabled = False
		Me.cmdUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdUp.Location = New System.Drawing.Point(723, 180)
		Me.cmdUp.Name = "cmdUp"
		Me.cmdUp.Size = New System.Drawing.Size(94, 45)
		Me.cmdUp.TabIndex = 14
		Me.cmdUp.Text = "Move Up"
		Me.cmdUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdUp.UseVisualStyleBackColor = True
		'
		'cmdDown
		'
		Me.cmdDown.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.arrow_down
		Me.cmdDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.cmdDown.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cmdDown.Enabled = False
		Me.cmdDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdDown.Location = New System.Drawing.Point(723, 231)
		Me.cmdDown.Name = "cmdDown"
		Me.cmdDown.Size = New System.Drawing.Size(94, 45)
		Me.cmdDown.TabIndex = 15
		Me.cmdDown.Text = "Move Down"
		Me.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdDown.UseVisualStyleBackColor = True
		'
		'lblMsg
		'
		Me.lblMsg.BackColor = System.Drawing.Color.Black
		Me.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TableLayoutPanel1.SetColumnSpan(Me.lblMsg, 5)
		Me.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMsg.ForeColor = System.Drawing.Color.White
		Me.lblMsg.Location = New System.Drawing.Point(13, 581)
		Me.lblMsg.Name = "lblMsg"
		Me.lblMsg.Size = New System.Drawing.Size(1334, 24)
		Me.lblMsg.TabIndex = 19
		'
		'DgvHistory
		'
		Me.DgvHistory.AllowUserToAddRows = False
		Me.DgvHistory.AllowUserToDeleteRows = False
		Me.DgvHistory.AllowUserToResizeColumns = False
		Me.DgvHistory.AllowUserToResizeRows = False
		Me.DgvHistory.AutoGenerateColumns = False
		DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DgvHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
		Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PartNumberDataGridViewTextBoxColumn2, Me.ShipdateDataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn2, Me.QTYDataGridViewTextBoxColumn2, Me.BuiltDataGridViewTextBoxColumn, Me.OrderedDataGridViewTextBoxColumn, Me.StatusDataGridViewTextBoxColumn})
		Me.DgvHistory.DataSource = Me.CompleteAndRemovedDataSource
		Me.DgvHistory.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DgvHistory.Location = New System.Drawing.Point(823, 384)
		Me.DgvHistory.Name = "DgvHistory"
		Me.DgvHistory.ReadOnly = True
		Me.DgvHistory.RowHeadersVisible = False
		Me.TableLayoutPanel1.SetRowSpan(Me.DgvHistory, 4)
		Me.DgvHistory.Size = New System.Drawing.Size(504, 194)
		Me.DgvHistory.TabIndex = 20
		'
		'PartNumberDataGridViewTextBoxColumn2
		'
		Me.PartNumberDataGridViewTextBoxColumn2.DataPropertyName = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn2.HeaderText = "PartNumber"
		Me.PartNumberDataGridViewTextBoxColumn2.Name = "PartNumberDataGridViewTextBoxColumn2"
		Me.PartNumberDataGridViewTextBoxColumn2.ReadOnly = True
		Me.PartNumberDataGridViewTextBoxColumn2.Width = 95
		'
		'ShipdateDataGridViewTextBoxColumn2
		'
		Me.ShipdateDataGridViewTextBoxColumn2.DataPropertyName = "LastUpdate"
		Me.ShipdateDataGridViewTextBoxColumn2.HeaderText = "Last Updated"
		Me.ShipdateDataGridViewTextBoxColumn2.Name = "ShipdateDataGridViewTextBoxColumn2"
		Me.ShipdateDataGridViewTextBoxColumn2.ReadOnly = True
		Me.ShipdateDataGridViewTextBoxColumn2.Width = 95
		'
		'DataGridViewTextBoxColumn2
		'
		Me.DataGridViewTextBoxColumn2.DataPropertyName = "CustOrderId"
		Me.DataGridViewTextBoxColumn2.HeaderText = "CustOrderId"
		Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
		Me.DataGridViewTextBoxColumn2.ReadOnly = True
		Me.DataGridViewTextBoxColumn2.Width = 105
		'
		'QTYDataGridViewTextBoxColumn2
		'
		Me.QTYDataGridViewTextBoxColumn2.DataPropertyName = "QTY"
		Me.QTYDataGridViewTextBoxColumn2.HeaderText = "QTY"
		Me.QTYDataGridViewTextBoxColumn2.Name = "QTYDataGridViewTextBoxColumn2"
		Me.QTYDataGridViewTextBoxColumn2.ReadOnly = True
		Me.QTYDataGridViewTextBoxColumn2.Width = 35
		'
		'BuiltDataGridViewTextBoxColumn
		'
		Me.BuiltDataGridViewTextBoxColumn.DataPropertyName = "Built"
		Me.BuiltDataGridViewTextBoxColumn.HeaderText = "Built"
		Me.BuiltDataGridViewTextBoxColumn.Name = "BuiltDataGridViewTextBoxColumn"
		Me.BuiltDataGridViewTextBoxColumn.ReadOnly = True
		Me.BuiltDataGridViewTextBoxColumn.Width = 35
		'
		'OrderedDataGridViewTextBoxColumn
		'
		Me.OrderedDataGridViewTextBoxColumn.DataPropertyName = "Ordered"
		Me.OrderedDataGridViewTextBoxColumn.HeaderText = "Ordered"
		Me.OrderedDataGridViewTextBoxColumn.Name = "OrderedDataGridViewTextBoxColumn"
		Me.OrderedDataGridViewTextBoxColumn.ReadOnly = True
		Me.OrderedDataGridViewTextBoxColumn.Width = 35
		'
		'StatusDataGridViewTextBoxColumn
		'
		Me.StatusDataGridViewTextBoxColumn.DataPropertyName = "Status"
		Me.StatusDataGridViewTextBoxColumn.HeaderText = "Status"
		Me.StatusDataGridViewTextBoxColumn.Name = "StatusDataGridViewTextBoxColumn"
		Me.StatusDataGridViewTextBoxColumn.ReadOnly = True
		Me.StatusDataGridViewTextBoxColumn.Width = 92
		'
		'CompleteAndRemovedDataSource
		'
		Me.CompleteAndRemovedDataSource.DataSource = GetType(Scheduler.core.PlanItem)
		'
		'Label2
		'
		Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(823, 358)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(504, 23)
		Me.Label2.TabIndex = 21
		Me.Label2.Text = "Completed and Removed Order History View Last 24HR"
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.Button4)
		Me.Panel1.Controls.Add(Me.Button3)
		Me.Panel1.Controls.Add(Me.Button2)
		Me.Panel1.Controls.Add(Me.Button1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(823, 282)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(504, 45)
		Me.Panel1.TabIndex = 22
		'
		'Button4
		'
		Me.Button4.Location = New System.Drawing.Point(367, 3)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(115, 44)
		Me.Button4.TabIndex = 3
		Me.Button4.Text = "Remove"
		Me.Button4.UseVisualStyleBackColor = True
		Me.Button4.Visible = False
		'
		'Button3
		'
		Me.Button3.Location = New System.Drawing.Point(246, 3)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(115, 44)
		Me.Button3.TabIndex = 2
		Me.Button3.Text = "Resume Order"
		Me.Button3.UseVisualStyleBackColor = True
		Me.Button3.Visible = False
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(125, 3)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(115, 44)
		Me.Button2.TabIndex = 1
		Me.Button2.Text = "Suspend Order"
		Me.Button2.UseVisualStyleBackColor = True
		Me.Button2.Visible = False
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(4, 3)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(115, 44)
		Me.Button1.TabIndex = 0
		Me.Button1.Text = "Skip"
		Me.Button1.UseVisualStyleBackColor = True
		Me.Button1.Visible = False
		'
		'GetPartListPb
		'
		Me.GetPartListPb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GetPartListPb.Location = New System.Drawing.Point(612, 538)
		Me.GetPartListPb.Name = "GetPartListPb"
		Me.GetPartListPb.Size = New System.Drawing.Size(105, 40)
		Me.GetPartListPb.TabIndex = 23
		Me.GetPartListPb.Text = "Lookup Parts"
		Me.GetPartListPb.UseVisualStyleBackColor = True
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.DataPropertyName = "Status"
		Me.DataGridViewTextBoxColumn1.HeaderText = "Status"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.Width = 246
		'
		'MenuStrip1
		'
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsToolStripMenuItem})
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 33)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Size = New System.Drawing.Size(1349, 24)
		Me.MenuStrip1.TabIndex = 19
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'ToolsToolStripMenuItem
		'
		Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PullXmlPartAttributeDataNowToolStripMenuItem})
		Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
		Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
		Me.ToolsToolStripMenuItem.Text = "Tools"
		'
		'PullXmlPartAttributeDataNowToolStripMenuItem
		'
		Me.PullXmlPartAttributeDataNowToolStripMenuItem.Name = "PullXmlPartAttributeDataNowToolStripMenuItem"
		Me.PullXmlPartAttributeDataNowToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
		Me.PullXmlPartAttributeDataNowToolStripMenuItem.Text = "Pull Xml Part Attribute Data Now"
		'
		'MainView
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1349, 679)
		Me.Controls.Add(Me.MenuStrip1)
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.Controls.Add(Me.lblLineName)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MainMenuStrip = Me.MenuStrip1
		Me.MaximizeBox = False
		Me.Name = "MainView"
		Me.Text = "Magna Mirrors Schedule"
		CType(Me.dgvEdit, System.ComponentModel.ISupportInitialize).EndInit()
		Me.mnuEdit.ResumeLayout(False)
		CType(Me.PlandataSource, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.ScheduleDataSource, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TableLayoutPanel1.ResumeLayout(False)
		CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CompleteAndRemovedDataSource, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel1.ResumeLayout(False)
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents LineTree As System.Windows.Forms.TreeView
    Friend WithEvents ilCustomers As System.Windows.Forms.ImageList
    Friend WithEvents lblLineName As System.Windows.Forms.Label
    Friend WithEvents dgvEdit As System.Windows.Forms.DataGridView
    Friend WithEvents BtnApproveEdits As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents mnuEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AppendPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OverwritePasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InsertPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSendPlan As System.Windows.Forms.Button
    Friend WithEvents cmdReadPlan As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Private WithEvents btnRefresh As Button
    Private WithEvents TableLayoutPanel1 As TableLayoutPanel
    Private WithEvents ScheduleDataSource As BindingSource
    Private WithEvents PlandataSource As BindingSource
    Friend WithEvents Label1 As Label
    Private WithEvents lblMsg As Label
	Friend WithEvents PartNumberDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents QTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents ShipdateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents Truck As DataGridViewCheckBoxColumn
	Friend WithEvents Chk As DataGridViewTextBoxColumn
	Friend WithEvents CustOrderId As DataGridViewTextBoxColumn
	Friend WithEvents DgvHistory As DataGridView
	Friend WithEvents Label2 As Label
	Friend WithEvents Panel1 As Panel
	Friend WithEvents Button3 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents Button1 As Button
	Friend WithEvents PartNumberDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents ShipdateDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents QTYDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents BuiltDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents OrderedDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
	Friend WithEvents Status As DataGridViewTextBoxColumn
	Friend WithEvents Button4 As Button
	Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Private WithEvents CompleteAndRemovedDataSource As BindingSource
	Friend WithEvents PartNumberDataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents ShipdateDataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents QTYDataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents BuiltDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents OrderedDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents StatusDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
	Friend WithEvents GetPartListPb As Button
	Friend WithEvents MenuStrip1 As MenuStrip
	Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents PullXmlPartAttributeDataNowToolStripMenuItem As ToolStripMenuItem
End Class
