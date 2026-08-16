<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Staff_add
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
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cb_status = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtp_dateend = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtp_datestart = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cb_position = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_address = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_ext = New System.Windows.Forms.TextBox()
        Me.txt_lname = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_mname = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_fname = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_EmpID = New System.Windows.Forms.TextBox()
        Me.pb_empimage = New System.Windows.Forms.PictureBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_addfinger = New System.Windows.Forms.Button()
        CType(Me.pb_empimage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(184, 385)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(37, 13)
        Me.Label13.TabIndex = 50
        Me.Label13.Text = "Status"
        '
        'cb_status
        '
        Me.cb_status.FormattingEnabled = True
        Me.cb_status.Items.AddRange(New Object() {"Car Washer"})
        Me.cb_status.Location = New System.Drawing.Point(197, 401)
        Me.cb_status.Name = "cb_status"
        Me.cb_status.Size = New System.Drawing.Size(165, 21)
        Me.cb_status.TabIndex = 49
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(184, 425)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Date End"
        '
        'dtp_dateend
        '
        Me.dtp_dateend.Enabled = False
        Me.dtp_dateend.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_dateend.Location = New System.Drawing.Point(197, 441)
        Me.dtp_dateend.Name = "dtp_dateend"
        Me.dtp_dateend.Size = New System.Drawing.Size(165, 20)
        Me.dtp_dateend.TabIndex = 47
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 425)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 46
        Me.Label11.Text = "Date Start"
        '
        'dtp_datestart
        '
        Me.dtp_datestart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp_datestart.Location = New System.Drawing.Point(17, 441)
        Me.dtp_datestart.Name = "dtp_datestart"
        Me.dtp_datestart.Size = New System.Drawing.Size(162, 20)
        Me.dtp_datestart.TabIndex = 45
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 385)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Position"
        '
        'cb_position
        '
        Me.cb_position.FormattingEnabled = True
        Me.cb_position.Items.AddRange(New Object() {"Car Washer"})
        Me.cb_position.Location = New System.Drawing.Point(17, 401)
        Me.cb_position.Name = "cb_position"
        Me.cb_position.Size = New System.Drawing.Size(162, 21)
        Me.cb_position.TabIndex = 43
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 346)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Address"
        '
        'txt_address
        '
        Me.txt_address.Location = New System.Drawing.Point(14, 362)
        Me.txt_address.Name = "txt_address"
        Me.txt_address.Size = New System.Drawing.Size(348, 20)
        Me.txt_address.TabIndex = 41
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(182, 307)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Ext."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 307)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Last Name"
        '
        'txt_ext
        '
        Me.txt_ext.Location = New System.Drawing.Point(197, 323)
        Me.txt_ext.Name = "txt_ext"
        Me.txt_ext.Size = New System.Drawing.Size(165, 20)
        Me.txt_ext.TabIndex = 38
        '
        'txt_lname
        '
        Me.txt_lname.Location = New System.Drawing.Point(14, 323)
        Me.txt_lname.Name = "txt_lname"
        Me.txt_lname.Size = New System.Drawing.Size(165, 20)
        Me.txt_lname.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(182, 268)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Middle Name"
        '
        'txt_mname
        '
        Me.txt_mname.Location = New System.Drawing.Point(197, 284)
        Me.txt_mname.Name = "txt_mname"
        Me.txt_mname.Size = New System.Drawing.Size(165, 20)
        Me.txt_mname.TabIndex = 34
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 267)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "First Name"
        '
        'txt_fname
        '
        Me.txt_fname.Location = New System.Drawing.Point(14, 284)
        Me.txt_fname.Name = "txt_fname"
        Me.txt_fname.Size = New System.Drawing.Size(165, 20)
        Me.txt_fname.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Employee ID"
        '
        'Txt_EmpID
        '
        Me.Txt_EmpID.Enabled = False
        Me.Txt_EmpID.Location = New System.Drawing.Point(17, 67)
        Me.Txt_EmpID.Name = "Txt_EmpID"
        Me.Txt_EmpID.Size = New System.Drawing.Size(165, 20)
        Me.Txt_EmpID.TabIndex = 30
        '
        'pb_empimage
        '
        Me.pb_empimage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pb_empimage.Location = New System.Drawing.Point(116, 102)
        Me.pb_empimage.Name = "pb_empimage"
        Me.pb_empimage.Size = New System.Drawing.Size(147, 122)
        Me.pb_empimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_empimage.TabIndex = 35
        Me.pb_empimage.TabStop = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(147, 227)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(79, 13)
        Me.LinkLabel1.TabIndex = 51
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Click to Upload"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(14, 499)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(346, 113)
        Me.DataGridView1.TabIndex = 52
        '
        'Column1
        '
        Me.Column1.HeaderText = "Finger print"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 200
        '
        'Column2
        '
        Me.Column2.HeaderText = "Delete"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(146, 20)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "Staff Information"
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(17, 669)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(345, 52)
        Me.Panel1.TabIndex = 58
        '
        'btn_addfinger
        '
        Me.btn_addfinger.BackColor = System.Drawing.Color.Green
        Me.btn_addfinger.FlatAppearance.BorderSize = 0
        Me.btn_addfinger.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_addfinger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_addfinger.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btn_addfinger.Location = New System.Drawing.Point(28, 628)
        Me.btn_addfinger.Name = "btn_addfinger"
        Me.btn_addfinger.Size = New System.Drawing.Size(324, 31)
        Me.btn_addfinger.TabIndex = 59
        Me.btn_addfinger.Text = "Add Finger Print"
        Me.btn_addfinger.UseVisualStyleBackColor = False
        '
        'Staff_add
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 741)
        Me.Controls.Add(Me.btn_addfinger)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.pb_empimage)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cb_status)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.dtp_dateend)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dtp_datestart)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cb_position)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_address)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_ext)
        Me.Controls.Add(Me.txt_lname)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_mname)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_fname)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_EmpID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Staff_add"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Staff Register"
        CType(Me.pb_empimage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pb_empimage As PictureBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cb_status As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents dtp_dateend As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents dtp_datestart As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents cb_position As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_address As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_ext As TextBox
    Friend WithEvents txt_lname As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_mname As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_fname As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_EmpID As TextBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewButtonColumn
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btn_addfinger As Button
End Class
