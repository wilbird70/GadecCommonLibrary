<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HistoryDialog
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
    'Do not modify it using the code editor
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistoryDialog))
        Me.ltClose = New System.Windows.Forms.Button()
        Me.OutputTextBox = New System.Windows.Forms.TextBox()
        Me.HorizontalScrollBar = New System.Windows.Forms.HScrollBar()
        Me.ltPrevious = New System.Windows.Forms.Button()
        Me.ltNext = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ltClose
        '
        Me.ltClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltClose.Location = New System.Drawing.Point(687, 259)
        Me.ltClose.Name = "ltClose"
        Me.ltClose.Size = New System.Drawing.Size(85, 23)
        Me.ltClose.TabIndex = 5
        Me.ltClose.Text = "XXX"
        Me.ltClose.UseVisualStyleBackColor = True
        '
        'OutputTextBox
        '
        Me.OutputTextBox.AcceptsReturn = True
        Me.OutputTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.OutputTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutputTextBox.Location = New System.Drawing.Point(146, 12)
        Me.OutputTextBox.Multiline = True
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.ReadOnly = True
        Me.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.OutputTextBox.Size = New System.Drawing.Size(626, 223)
        Me.OutputTextBox.TabIndex = 6
        '
        'HorizontalScrollBar
        '
        Me.HorizontalScrollBar.LargeChange = 1
        Me.HorizontalScrollBar.Location = New System.Drawing.Point(146, 238)
        Me.HorizontalScrollBar.Name = "HorizontalScrollBar"
        Me.HorizontalScrollBar.Size = New System.Drawing.Size(626, 18)
        Me.HorizontalScrollBar.TabIndex = 9
        '
        'ltPrevious
        '
        Me.ltPrevious.Location = New System.Drawing.Point(505, 259)
        Me.ltPrevious.Name = "ltPrevious"
        Me.ltPrevious.Size = New System.Drawing.Size(85, 23)
        Me.ltPrevious.TabIndex = 10
        Me.ltPrevious.Text = "XXX"
        Me.ltPrevious.UseVisualStyleBackColor = True
        '
        'ltNext
        '
        Me.ltNext.Location = New System.Drawing.Point(596, 259)
        Me.ltNext.Name = "ltNext"
        Me.ltNext.Size = New System.Drawing.Size(85, 23)
        Me.ltNext.TabIndex = 11
        Me.ltNext.Text = "XXX"
        Me.ltNext.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(180, 180)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'HistoryDialog
        '
        Me.AcceptButton = Me.ltClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltClose
        Me.ClientSize = New System.Drawing.Size(784, 294)
        Me.Controls.Add(Me.ltNext)
        Me.Controls.Add(Me.ltPrevious)
        Me.Controls.Add(Me.HorizontalScrollBar)
        Me.Controls.Add(Me.OutputTextBox)
        Me.Controls.Add(Me.ltClose)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HistoryDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "XXX"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ltClose As System.Windows.Forms.Button
    Friend WithEvents OutputTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HorizontalScrollBar As System.Windows.Forms.HScrollBar
    Friend WithEvents ltPrevious As System.Windows.Forms.Button
    Friend WithEvents ltNext As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
