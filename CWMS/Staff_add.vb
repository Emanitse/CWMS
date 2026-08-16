Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class Staff_add
    Private Sub Staff_add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        cb_position.SelectedIndex = 0
        cb_status.SelectedIndex = 0
        load_status()
        'AutoNumber()
        dtp_datestart.MaxDate = DateTime.Now
        readfingerprint()
    End Sub
    Sub CreateNewAutoNumber()
        Try
            Dim cmd As New SqlCommand
            With cmd
                .Connection = sqlconn
                .CommandText = "SP_AutoNumber"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@pfx", "EMP")
            End With
            cmd.ExecuteScalar()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub AutoNumber()
        Dim number As String
        str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
        cmd = New SqlClient.SqlCommand(str, sqlconn)
        With cmd
            .Parameters.AddWithValue("@pfx", "EMP")
            If IsDBNull(cmd.ExecuteScalar) Then
                CreateNewAutoNumber()
                Dim number1 As String
                str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
                cmd = New SqlClient.SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@pfx  ", "EMP")
                    number1 = Convert.ToString(cmd.ExecuteScalar)
                    Txt_EmpID.Text = number1
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                number = Convert.ToString(cmd.ExecuteScalar)
                Txt_EmpID.Text = number
            End If
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub




    Private Sub cb_status_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Sub load_status()

        cb_status.Items.Clear()
        With cb_status
            .Items.Add("Active")
            .Items.Add("Resign")
            .SelectedIndex = 0
        End With
    End Sub

    Sub readfingerprint()
        DataGridView1.Rows.Clear()
        str = "Select * from FingerPirnt_tbl where Staff_id = '" & Txt_EmpID.Text & "'"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            DataGridView1.Rows.Add(dr("RecNo"), "Delete")
        End While
        dr.Close()
        cmd.Dispose()
    End Sub
    Sub clearall()

        txt_fname.Clear()
        txt_mname.Clear()
        txt_lname.Clear()
        txt_ext.Clear()
        txt_address.Clear()
        cb_position.SelectedIndex = 0
        cb_status.SelectedIndex = 0
        pb_empimage.Image = Nothing

    End Sub
    Sub createnewemployee()
        Dim ms As New MemoryStream
        pb_empimage.Image.Save(ms, pb_empimage.Image.RawFormat)

        query = "Insert into Staff_Tbl (Staffid,Fname,Mname,Lname,Extension,Address,Position,Status,Date_Start,Emp_image) values
                (@Staffid,@Fname,@Mname,@Lname,@Extension,@Address,@Position,@Status,@Date_Start,@Emp_image)"
            cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@Staffid", Txt_EmpID.Text)
            .Parameters.AddWithValue("@Fname", txt_fname.Text)
            .Parameters.AddWithValue("@Mname", txt_mname.Text)
            .Parameters.AddWithValue("@Lname", txt_lname.Text)
            .Parameters.AddWithValue("@Extension", txt_ext.Text)
            .Parameters.AddWithValue("@Address", txt_address.Text)
            .Parameters.AddWithValue("@Position", cb_position.Text)
            .Parameters.AddWithValue("@Status", cb_status.Text)
            .Parameters.AddWithValue("@Date_Start", dtp_datestart.Value)
            .Parameters.AddWithValue("@Emp_image", ms.ToArray())
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            pb_empimage.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If


    End Sub

    Private Sub cb_status_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cb_status.SelectedIndexChanged
        If cb_status.SelectedIndex = 1 Then
            dtp_dateend.Enabled = True
        Else
            dtp_dateend.Enabled = False

        End If
    End Sub





    Sub updateEmployee()
        Dim ms As New MemoryStream
        pb_empimage.Image.Save(ms, pb_empimage.Image.RawFormat)


        If cb_status.SelectedIndex = 0 Then
            query = "Update Staff_Tbl set Fname= @Fname, Mname = @Mname, Lname = @Lname, Extension = @Extension, Address = @Address, Position = @Position,
                Status = @Status, Date_Start = @Date_Start, Emp_image = @Emp_image where Staffid = @Staffid"
            cmd = New SqlCommand(query, sqlconn)
            With cmd
                .Parameters.AddWithValue("@Staffid", Txt_EmpID.Text)
                .Parameters.AddWithValue("@Fname", txt_fname.Text)
                .Parameters.AddWithValue("@Mname", txt_mname.Text)
                .Parameters.AddWithValue("@Lname", txt_lname.Text)
                .Parameters.AddWithValue("@Extension", txt_ext.Text)
                .Parameters.AddWithValue("@Address", txt_address.Text)
                .Parameters.AddWithValue("@Position", cb_position.Text)
                .Parameters.AddWithValue("@Status", cb_status.Text)
                .Parameters.AddWithValue("@Date_Start", dtp_datestart.Value)
                .Parameters.AddWithValue("@Emp_image", ms.ToArray())
            End With
        ElseIf cb_status.SelectedIndex = 1 Then
            query = "Update Staff_Tbl set Fname= @Fname, Mname = @Mname, Lname = @Lname, Extension = @Extension, Address = @Address, Position = @Position,
                Status = @Status, Date_Start = @Date_Start, Date_End = @Date_End, Emp_image = @Emp_image where Staffid = @Staffid"
            cmd = New SqlCommand(query, sqlconn)
            With cmd
                .Parameters.AddWithValue("@Staffid", Txt_EmpID.Text)
                .Parameters.AddWithValue("@Fname", txt_fname.Text)
                .Parameters.AddWithValue("@Mname", txt_mname.Text)
                .Parameters.AddWithValue("@Lname", txt_lname.Text)
                .Parameters.AddWithValue("@Extension", txt_ext.Text)
                .Parameters.AddWithValue("@Address", txt_address.Text)
                .Parameters.AddWithValue("@Position", cb_position.Text)
                .Parameters.AddWithValue("@Status", cb_status.Text)
                .Parameters.AddWithValue("@Date_Start", dtp_datestart.Value)
                .Parameters.AddWithValue("@Date_End", dtp_dateend.Value)
                .Parameters.AddWithValue("@Emp_image", ms.ToArray())
            End With

        End If

        cmd.ExecuteNonQuery()
        cmd.Dispose()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 1 Then

            Dim confirm = MsgBox("Are you sure you want to delete this fingerprint record?", MsgBoxStyle.YesNo, "Confirm Delete")
            If confirm = MsgBoxResult.Yes Then
                query = "Delete from FingerPirnt_tbl where RecNo = @RecNo"
                cmd = New SqlCommand(query, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@RecNo", DataGridView1.Item(0, e.RowIndex).Value)
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                readfingerprint()
            End If
        End If
    End Sub


    'this will add a save button to the form
    Sub addsavebutton()

        Panel1.Controls.Clear()

        Dim btnSave As New Button()
        btnSave.Text = "Save"
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(159, 35)
        btnSave.Location = New Point(10, 10) ' X=10, Y=10 (Top left of the panel)
        btnSave.BackColor = Color.RoyalBlue
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat


        ' Link the Save button to its click event
        AddHandler btnSave.Click, AddressOf BtnSave_Click

        ' 2. Create the Cancel Button
        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(159, 35)
        ' Position it 15 pixels to the right of the Save button
        btnCancel.Location = New Point(btnSave.Right + 15, 10)
        btnCancel.BackColor = Color.Crimson
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat

        ' Link the Cancel button to its click event
        AddHandler btnCancel.Click, AddressOf BtnCancel_Click

        ' 3. Add both buttons to your Panel (replace "Panel1" with your panel's actual name)
        Panel1.Controls.Add(btnSave)
        Panel1.Controls.Add(btnCancel)

    End Sub



    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        Dim existingid As Boolean = False
        Dim existingname As Boolean = False

        'check if existing id and name

        str = "Select * from Staff_Tbl"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            If dr("Staffid").ToString.Equals(Txt_EmpID.Text) Then
                existingid = True
            ElseIf dr("Fname").ToString.Equals(txt_fname.Text) AndAlso dr("Mname").ToString.Equals(txt_mname.Text) AndAlso dr("Lname").ToString.Equals(txt_lname.Text) Then
                existingname = True
            End If

        End While
        dr.Close()
        cmd.Dispose()

        If existingid = True Then
            MsgBox("Employee ID already exists. Please use a different ID.", MsgBoxStyle.Exclamation, "Duplicate ID")
        ElseIf existingname = True Then
            MsgBox("Employee name already exists. Please use a different name.", MsgBoxStyle.Exclamation, "Duplicate Name")
        ElseIf txt_fname.Text = "" Or txt_mname.Text = "" Or txt_lname.Text = "" Or txt_address.Text = "" Then
            MsgBox("Please fill in all required fields.", MsgBoxStyle.Exclamation, "Missing Information")
        ElseIf pb_empimage.Image Is Nothing Then
            MsgBox("Please select an employee image.", MsgBoxStyle.Exclamation, "Missing Image")
        Else
            createnewemployee()
            CreateNewAutoNumber()
            AutoNumber()
            clearall()
            MsgBox("New Employee Added Successfully!", MsgBoxStyle.Information, "biglang Success")
            Staff_register.readEmployee("Select * from Staff_Tbl where Status = 'Active' order by Staffid ASC")
            Me.Close()
        End If

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Sub addupdatebutton()
        Panel1.Controls.Clear()

        Dim btnUpdate As New Button()
        With btnUpdate

            .Text = "Update"
            .Name = "btnUpdate"
            .Size = New Size(159, 35)
            .Location = New Point(10, 10) ' X=10, Y=10 (Top left of the panel)
            .BackColor = Color.RoyalBlue
            .ForeColor = Color.White
            .FlatStyle = FlatStyle.Flat
        End With

        ' Link the Save button to its click event
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click

        ' 2. Create the Cancel Button
        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(159, 35)
        ' Position it 15 pixels to the right of the Save button
        btnCancel.Location = New Point(btnUpdate.Right + 15, 10)
        btnCancel.BackColor = Color.Crimson
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat

        ' Link the Cancel button to its click event
        AddHandler btnCancel.Click, AddressOf BtnCancel_Click

        ' 3. Add both buttons to your Panel (replace "Panel1" with your panel's actual name)
        Panel1.Controls.Add(btnUpdate)
        Panel1.Controls.Add(btnCancel)

    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        If txt_fname.Text = "" Or txt_mname.Text = "" Or txt_lname.Text = "" Or txt_address.Text = "" Then
            MsgBox("Please fill in all required fields.", MsgBoxStyle.Exclamation, "Missing Information")
        ElseIf pb_empimage.Image Is Nothing Then
            MsgBox("Please select an employee image.", MsgBoxStyle.Exclamation, "Missing Image")
        Else
            updateEmployee()
            AutoNumber()
            clearall()
            MsgBox("New Employee Updated Successfully!", MsgBoxStyle.Information, "biglang Success")
            Staff_register.readEmployee("Select * from Staff_Tbl where Status = 'Active' order by Staffid ASC")
            Me.Close()
        End If

    End Sub

    Private Sub btn_addfinger_Click(sender As Object, e As EventArgs) Handles btn_addfinger.Click
        FingerprintScan.ShowDialog()
    End Sub
End Class