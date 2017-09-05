<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ilCustomers = New System.Windows.Forms.ImageList(Me.components)
        Me.lblLineName = New System.Windows.Forms.Label()
        Me.dgvEdit = New System.Windows.Forms.DataGridView()
        Me.mnuEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AppendPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OverwritePasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InsertPasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdDown = New System.Windows.Forms.Button()
        Me.cmdUp = New System.Windows.Forms.Button()
        Me.Btn_UnloadfromPlan = New System.Windows.Forms.Button()
        Me.Btn_LoadToPlan = New System.Windows.Forms.Button()
        Me.cmdReadPlan = New System.Windows.Forms.Button()
        Me.Btn_ClearPlan = New System.Windows.Forms.Button()
        Me.cmdSendPlan = New System.Windows.Forms.Button()
        Me.cmdClearPlan = New System.Windows.Forms.Button()
        Me.BtnApproveEdits = New System.Windows.Forms.Button()
        CType(Me.dgvEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuEdit.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.ItemHeight = 20
        Me.TreeView1.Location = New System.Drawing.Point(5, 12)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(161, 700)
        Me.TreeView1.TabIndex = 0
        '
        'ilCustomers
        '
        Me.ilCustomers.ImageStream = CType(resources.GetObject("ilCustomers.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilCustomers.TransparentColor = System.Drawing.Color.Transparent
        Me.ilCustomers.Images.SetKeyName(0, "editing.ico")
        '
        'lblLineName
        '
        Me.lblLineName.AutoSize = True
        Me.lblLineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLineName.Location = New System.Drawing.Point(200, 9)
        Me.lblLineName.Name = "lblLineName"
        Me.lblLineName.Size = New System.Drawing.Size(268, 33)
        Me.lblLineName.TabIndex = 2
        Me.lblLineName.Text = "Select Line to Edit"
        '
        'dgvEdit
        '
        Me.dgvEdit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEdit.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEdit.ContextMenuStrip = Me.mnuEdit
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvEdit.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvEdit.Location = New System.Drawing.Point(172, 83)
        Me.dgvEdit.Name = "dgvEdit"
        Me.dgvEdit.RowHeadersWidth = 20
        Me.dgvEdit.Size = New System.Drawing.Size(375, 629)
        Me.dgvEdit.TabIndex = 3
        '
        'mnuEdit
        '
        Me.mnuEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AppendPasteToolStripMenuItem, Me.OverwritePasteToolStripMenuItem, Me.InsertPasteToolStripMenuItem})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(153, 70)
        '
        'AppendPasteToolStripMenuItem
        '
        Me.AppendPasteToolStripMenuItem.Name = "AppendPasteToolStripMenuItem"
        Me.AppendPasteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AppendPasteToolStripMenuItem.Text = "Append Paste"
        '
        'OverwritePasteToolStripMenuItem
        '
        Me.OverwritePasteToolStripMenuItem.Name = "OverwritePasteToolStripMenuItem"
        Me.OverwritePasteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OverwritePasteToolStripMenuItem.Text = "Overwrite Paste"
        '
        'InsertPasteToolStripMenuItem
        '
        Me.InsertPasteToolStripMenuItem.Name = "InsertPasteToolStripMenuItem"
        Me.InsertPasteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.InsertPasteToolStripMenuItem.Text = "Insert Paste"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeColumns = False
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.Location = New System.Drawing.Point(674, 83)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.RowHeadersWidth = 20
        Me.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.ShowEditingIcon = False
        Me.dgv.Size = New System.Drawing.Size(399, 629)
        Me.dgv.TabIndex = 5
        '
        'lblNew
        '
        Me.lblNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNew.Location = New System.Drawing.Point(172, 54)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(375, 26)
        Me.lblNew.TabIndex = 6
        Me.lblNew.Text = "Plan Edit"
        Me.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(947, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 74)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Existing Schedule / Plan"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdDown
        '
        Me.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdDown.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.arrow_down
        Me.cmdDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDown.Location = New System.Drawing.Point(572, 223)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(79, 67)
        Me.cmdDown.TabIndex = 15
        Me.cmdDown.Text = "Move Down"
        Me.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'cmdUp
        '
        Me.cmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdUp.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.arrow_up
        Me.cmdUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUp.Location = New System.Drawing.Point(572, 150)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(79, 67)
        Me.cmdUp.TabIndex = 14
        Me.cmdUp.Text = "Move Up"
        Me.cmdUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'Btn_UnloadfromPlan
        '
        Me.Btn_UnloadfromPlan.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Btn_UnloadfromPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.fast_backward
        Me.Btn_UnloadfromPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_UnloadfromPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_UnloadfromPlan.Location = New System.Drawing.Point(572, 515)
        Me.Btn_UnloadfromPlan.Name = "Btn_UnloadfromPlan"
        Me.Btn_UnloadfromPlan.Size = New System.Drawing.Size(79, 67)
        Me.Btn_UnloadfromPlan.TabIndex = 13
        Me.Btn_UnloadfromPlan.Text = "Move Schedule"
        Me.Btn_UnloadfromPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_UnloadfromPlan.UseVisualStyleBackColor = True
        '
        'Btn_LoadToPlan
        '
        Me.Btn_LoadToPlan.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Btn_LoadToPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.fast_forward
        Me.Btn_LoadToPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_LoadToPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_LoadToPlan.Location = New System.Drawing.Point(570, 442)
        Me.Btn_LoadToPlan.Name = "Btn_LoadToPlan"
        Me.Btn_LoadToPlan.Size = New System.Drawing.Size(79, 67)
        Me.Btn_LoadToPlan.TabIndex = 12
        Me.Btn_LoadToPlan.Text = "Move Edits"
        Me.Btn_LoadToPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_LoadToPlan.UseVisualStyleBackColor = True
        '
        'cmdReadPlan
        '
        Me.cmdReadPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdReadPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.briefcase_download
        Me.cmdReadPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdReadPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReadPlan.Location = New System.Drawing.Point(674, 1)
        Me.cmdReadPlan.Name = "cmdReadPlan"
        Me.cmdReadPlan.Size = New System.Drawing.Size(78, 76)
        Me.cmdReadPlan.TabIndex = 9
        Me.cmdReadPlan.Text = "Read"
        Me.cmdReadPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdReadPlan.UseVisualStyleBackColor = True
        '
        'Btn_ClearPlan
        '
        Me.Btn_ClearPlan.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Btn_ClearPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.delete
        Me.Btn_ClearPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_ClearPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_ClearPlan.Location = New System.Drawing.Point(570, 369)
        Me.Btn_ClearPlan.Name = "Btn_ClearPlan"
        Me.Btn_ClearPlan.Size = New System.Drawing.Size(81, 67)
        Me.Btn_ClearPlan.TabIndex = 11
        Me.Btn_ClearPlan.Text = "Clear Edits"
        Me.Btn_ClearPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Btn_ClearPlan.UseVisualStyleBackColor = True
        '
        'cmdSendPlan
        '
        Me.cmdSendPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSendPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.briefcase_upload
        Me.cmdSendPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdSendPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSendPlan.Location = New System.Drawing.Point(849, 2)
        Me.cmdSendPlan.Name = "cmdSendPlan"
        Me.cmdSendPlan.Size = New System.Drawing.Size(92, 76)
        Me.cmdSendPlan.TabIndex = 8
        Me.cmdSendPlan.Text = "Send"
        Me.cmdSendPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSendPlan.UseVisualStyleBackColor = True
        '
        'cmdClearPlan
        '
        Me.cmdClearPlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClearPlan.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.delete
        Me.cmdClearPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdClearPlan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearPlan.Location = New System.Drawing.Point(759, 3)
        Me.cmdClearPlan.Name = "cmdClearPlan"
        Me.cmdClearPlan.Size = New System.Drawing.Size(84, 74)
        Me.cmdClearPlan.TabIndex = 10
        Me.cmdClearPlan.Text = "Clear"
        Me.cmdClearPlan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClearPlan.UseVisualStyleBackColor = True
        '
        'BtnApproveEdits
        '
        Me.BtnApproveEdits.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnApproveEdits.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.approve
        Me.BtnApproveEdits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnApproveEdits.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApproveEdits.Location = New System.Drawing.Point(572, 296)
        Me.BtnApproveEdits.Name = "BtnApproveEdits"
        Me.BtnApproveEdits.Size = New System.Drawing.Size(79, 67)
        Me.BtnApproveEdits.TabIndex = 4
        Me.BtnApproveEdits.Text = "Approve Edits"
        Me.BtnApproveEdits.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnApproveEdits.UseVisualStyleBackColor = True
        '
        'MainView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1079, 714)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.cmdClearPlan)
        Me.Controls.Add(Me.dgvEdit)
        Me.Controls.Add(Me.cmdReadPlan)
        Me.Controls.Add(Me.cmdUp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Btn_UnloadfromPlan)
        Me.Controls.Add(Me.cmdSendPlan)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.BtnApproveEdits)
        Me.Controls.Add(Me.lblLineName)
        Me.Controls.Add(Me.Btn_LoadToPlan)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.Btn_ClearPlan)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainView"
        Me.Text = "Magna Mirrors Schedule"
        CType(Me.dgvEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuEdit.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ilCustomers As System.Windows.Forms.ImageList
    Friend WithEvents lblLineName As System.Windows.Forms.Label
    Friend WithEvents dgvEdit As System.Windows.Forms.DataGridView
    Friend WithEvents BtnApproveEdits As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mnuEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AppendPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OverwritePasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InsertPasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSendPlan As System.Windows.Forms.Button
    Friend WithEvents cmdReadPlan As System.Windows.Forms.Button
    Friend WithEvents cmdClearPlan As System.Windows.Forms.Button
    Friend WithEvents Btn_ClearPlan As System.Windows.Forms.Button
    Friend WithEvents Btn_LoadToPlan As System.Windows.Forms.Button
    Friend WithEvents Btn_UnloadfromPlan As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
End Class
