<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Scan
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
        Me.txtScan = New System.Windows.Forms.TextBox()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtScan
        '
        Me.txtScan.Location = New System.Drawing.Point(364, 146)
        Me.txtScan.Name = "txtScan"
        Me.txtScan.Size = New System.Drawing.Size(241, 20)
        Me.txtScan.TabIndex = 0
        '
        'btnScan
        '
        Me.btnScan.Location = New System.Drawing.Point(410, 172)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(130, 41)
        Me.btnScan.TabIndex = 1
        Me.btnScan.Text = "Button1"
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'Scan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1626, 450)
        Me.Controls.Add(Me.btnScan)
        Me.Controls.Add(Me.txtScan)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(0, 368)
        Me.Name = "Scan"
        Me.Text = "Scan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtScan As TextBox
    Friend WithEvents btnScan As Button
End Class
