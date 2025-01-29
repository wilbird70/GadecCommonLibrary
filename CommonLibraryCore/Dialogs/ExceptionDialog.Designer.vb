<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExceptionDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExceptionDialog))
        Me.ltSend = New System.Windows.Forms.Button()
        Me.ltClose = New System.Windows.Forms.Button()
        Me.CaptionLabel = New System.Windows.Forms.Label()
        Me.OutputTextBox = New System.Windows.Forms.RichTextBox()
        Me.QuestionLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ltSend
        '
        Me.ltSend.Location = New System.Drawing.Point(595, 259)
        Me.ltSend.Name = "ltSend"
        Me.ltSend.Size = New System.Drawing.Size(86, 23)
        Me.ltSend.TabIndex = 10
        Me.ltSend.Text = "XXX"
        Me.ltSend.UseVisualStyleBackColor = True
        '
        'ltClose
        '
        Me.ltClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltClose.Location = New System.Drawing.Point(687, 259)
        Me.ltClose.Name = "ltClose"
        Me.ltClose.Size = New System.Drawing.Size(85, 23)
        Me.ltClose.TabIndex = 11
        Me.ltClose.Text = "XXX"
        Me.ltClose.UseVisualStyleBackColor = True
        '
        'CaptionLabel
        '
        Me.CaptionLabel.AutoSize = True
        Me.CaptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaptionLabel.Location = New System.Drawing.Point(118, 248)
        Me.CaptionLabel.Name = "CaptionLabel"
        Me.CaptionLabel.Size = New System.Drawing.Size(31, 15)
        Me.CaptionLabel.TabIndex = 12
        Me.CaptionLabel.Text = "XXX"
        '
        'OutputTextBox
        '
        Me.OutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OutputTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutputTextBox.Location = New System.Drawing.Point(118, 12)
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.Size = New System.Drawing.Size(654, 232)
        Me.OutputTextBox.TabIndex = 13
        Me.OutputTextBox.Text = ""
        Me.OutputTextBox.WordWrap = False
        '
        'QuestionLabel
        '
        Me.QuestionLabel.AutoSize = True
        Me.QuestionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QuestionLabel.Location = New System.Drawing.Point(118, 263)
        Me.QuestionLabel.Name = "QuestionLabel"
        Me.QuestionLabel.Size = New System.Drawing.Size(31, 15)
        Me.QuestionLabel.TabIndex = 15
        Me.QuestionLabel.Text = "XXX"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 120)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'UnhandledException
        '
        Me.AcceptButton = Me.ltSend
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltClose
        Me.ClientSize = New System.Drawing.Size(784, 294)
        Me.Controls.Add(Me.CaptionLabel)
        Me.Controls.Add(Me.ltSend)
        Me.Controls.Add(Me.ltClose)
        Me.Controls.Add(Me.OutputTextBox)
        Me.Controls.Add(Me.QuestionLabel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UnhandledException"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XXX"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ltSend As System.Windows.Forms.Button
    Friend WithEvents ltClose As System.Windows.Forms.Button
    Friend WithEvents CaptionLabel As System.Windows.Forms.Label
    Friend WithEvents OutputTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents QuestionLabel As Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
