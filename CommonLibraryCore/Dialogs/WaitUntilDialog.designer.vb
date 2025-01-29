<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WaitUntilDialog
    Inherits Form

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
        Me.ltCancel = New Button()
        Me.ProgressBar = New ProgressBar()
        Me.SuspendLayout()
        '
        'ltCancel
        '
        Me.ltCancel.Location = New System.Drawing.Point(146, 28)
        Me.ltCancel.Name = "ltCancel"
        Me.ltCancel.Size = New System.Drawing.Size(85, 23)
        Me.ltCancel.TabIndex = 11
        Me.ltCancel.Text = "XXX"
        Me.ltCancel.UseVisualStyleBackColor = True
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(12, 12)
        Me.ProgressBar.Maximum = 2000000000
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(219, 8)
        Me.ProgressBar.TabIndex = 35
        '
        'WaitUntilDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(244, 63)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.ltCancel)
        Me.Name = "WaitUntilDialog"
        Me.Text = "WaitDialog"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ltCancel As Button
    Friend WithEvents ProgressBar As ProgressBar
End Class
