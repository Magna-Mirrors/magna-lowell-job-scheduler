<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "add.ico")
        Me.ImageList.Images.SetKeyName(1, "approve.ico")
        Me.ImageList.Images.SetKeyName(2, "arrow_down.ico")
        Me.ImageList.Images.SetKeyName(3, "arrow_left.ico")
        Me.ImageList.Images.SetKeyName(4, "arrow_right.ico")
        Me.ImageList.Images.SetKeyName(5, "arrow_up.ico")
        Me.ImageList.Images.SetKeyName(6, "Bc_Image.ico")
        Me.ImageList.Images.SetKeyName(7, "briefcase_download.ico")
        Me.ImageList.Images.SetKeyName(8, "briefcase_upload.ico")
        Me.ImageList.Images.SetKeyName(9, "delete.ico")
        Me.ImageList.Images.SetKeyName(10, "editing.ico")
        Me.ImageList.Images.SetKeyName(11, "fast_backward.ico")
        Me.ImageList.Images.SetKeyName(12, "fast_forward.ico")
        Me.ImageList.Images.SetKeyName(13, "save_approve.ico")
        '
        'btn
        '
        Me.btn.BackgroundImage = Global.Scheduler.client.My.Resources.Resources.save_approve
        Me.btn.Location = New System.Drawing.Point(87, 29)
        Me.btn.Name = "btn"
        Me.btn.Size = New System.Drawing.Size(102, 96)
        Me.btn.TabIndex = 0
        Me.btn.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.btn)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents ImageList As ImageList
    Private WithEvents btn As Button
End Class
