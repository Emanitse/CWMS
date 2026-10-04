Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports QRCoder
Imports System.Drawing.Imaging
Public Class Customer_add
    Private Sub Customer_add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        'AutoNumber()
        RecordNumber()
        'generateqrcode()
        'addregisterbutton()
        add_vhcl_registerbutton()
        readvehicle()
    End Sub


    Sub generateqrcode()
        Dim qrgen As New QRCodeGenerator
        Dim data = qrgen.CreateQrCode(TextBox1.Text, QRCodeGenerator.ECCLevel.Q)
        Dim code As New QRCode(data)
        PictureBox1.Image = code.GetGraphic(14)
    End Sub
    Sub CreateNewAutoNumber()
        Try
            Dim cmd As New SqlCommand
            With cmd
                .Connection = sqlconn
                .CommandText = "SP_AutoNumber"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@pfx", "CID")
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
            .Parameters.AddWithValue("@pfx", "CID")
            If IsDBNull(cmd.ExecuteScalar) Then
                CreateNewAutoNumber()
                Dim number1 As String
                str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
                cmd = New SqlClient.SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@pfx  ", "CID")
                    number1 = Convert.ToString(cmd.ExecuteScalar)
                    TextBox1.Text = number1
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                number = Convert.ToString(cmd.ExecuteScalar)
                TextBox1.Text = number
            End If
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub


    Sub CreateNewRecordNumber()
        Try
            Dim cmd As New SqlCommand
            With cmd
                .Connection = sqlconn
                .CommandText = "SP_AutoNumber"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@pfx", "VHC")
            End With
            cmd.ExecuteScalar()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub cleartext()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox10.Clear()
    End Sub

    Sub RecordNumber()
        Dim number As String
        str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
        cmd = New SqlClient.SqlCommand(str, sqlconn)
        With cmd
            .Parameters.AddWithValue("@pfx", "VHC")
            If IsDBNull(cmd.ExecuteScalar) Then
                CreateNewRecordNumber()
                Dim number1 As String
                str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
                cmd = New SqlClient.SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@pfx  ", "VHC")
                    number1 = Convert.ToString(cmd.ExecuteScalar)
                    TextBox6.Text = number1
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                number = Convert.ToString(cmd.ExecuteScalar)
                TextBox6.Text = number
            End If
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Sub addregisterbutton()

        Panel1.Controls.Clear()

        Dim btnRgstr As New Button()
        btnRgstr.Text = "Register"
        btnRgstr.Name = "btn_reg"
        btnRgstr.Size = New Size(217, 26)
        btnRgstr.Location = New Point(169, 10)
        btnRgstr.BackColor = Color.RoyalBlue
        btnRgstr.ForeColor = Color.White
        btnRgstr.FlatStyle = FlatStyle.Flat


        ' Link the Save button to its click event
        AddHandler btnRgstr.Click, AddressOf Btnregister_Click
        Panel1.Controls.Add(btnRgstr)
    End Sub
    Sub addupdatebutton()

        Panel1.Controls.Clear()

        Dim btnupdate As New Button()
        btnupdate.Text = "Update"
        btnupdate.Name = "btn_Update"
        btnupdate.Size = New Size(217, 26)
        btnupdate.Location = New Point(169, 10)
        btnupdate.BackColor = Color.DarkGreen
        btnupdate.ForeColor = Color.White
        btnupdate.FlatStyle = FlatStyle.Flat


        ' Link the Save button to its click event
        AddHandler btnupdate.Click, AddressOf btnupdate_Click
        Panel1.Controls.Add(btnupdate)
    End Sub
    Sub add_vhcl_registerbutton()

        Panel2.Controls.Clear()

        Dim btnvhclrgstr As New Button()
        btnvhclrgstr.Text = "Register Vehicle"
        btnvhclrgstr.Name = "btn_vhclreg"
        btnvhclrgstr.Size = New Size(217, 26)
        btnvhclrgstr.Location = New Point(169, 10)
        btnvhclrgstr.BackColor = Color.RoyalBlue
        btnvhclrgstr.ForeColor = Color.White
        btnvhclrgstr.FlatStyle = FlatStyle.Flat


        ' Link the Save button to its click event
        AddHandler btnvhclrgstr.Click, AddressOf savevehicle_Click
        Panel2.Controls.Add(btnvhclrgstr)
    End Sub


    Sub vhcl_updatebutton()

        Panel2.Controls.Clear()

        Dim btnvhclupdt As New Button()
        btnvhclupdt.Text = "Update Vehicle"
        btnvhclupdt.Name = "btn_vhclupdt"
        btnvhclupdt.Size = New Size(217, 26)
        btnvhclupdt.Location = New Point(169, 10)
        btnvhclupdt.BackColor = Color.DarkGreen
        btnvhclupdt.ForeColor = Color.White
        btnvhclupdt.FlatStyle = FlatStyle.Flat


        ' Link the Save button to its click event
        AddHandler btnvhclupdt.Click, AddressOf updatevehicle_Click
        Panel2.Controls.Add(btnvhclupdt)
    End Sub
    Sub savecustomer()
        Dim ms As New MemoryStream  'ito yung sa image kaabang na to
        Dim qr As New IO.MemoryStream 'ito sa qrcode
        PictureBox1.Image.Save(qr, System.Drawing.Imaging.ImageFormat.Png)
        query = "Insert into Customer_tbl (CID,Fname,Mname,Lname,Extension,ContactNumber,QRcode,User_stamp,Date_stamp) values
                 (@CID,@Fname,@Mname,@Lname,@Extension,@ContactNumber,@QRcode,@User_stamp,@Date_stamp)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd.Parameters
            .AddWithValue("@CID", TextBox1.Text)
            .AddWithValue("@Fname", TextBox2.Text)
            .AddWithValue("@Mname", TextBox3.Text)
            .AddWithValue("@Lname", TextBox4.Text)
            .AddWithValue("@Extension", TextBox5.Text)
            .AddWithValue("@ContactNumber", TextBox10.Text)
            .AddWithValue("@QRcode", qr.ToArray())
            .AddWithValue("@User_stamp", Fname.ToString)
            .AddWithValue("@Date_stamp", Date.Now)
        End With
        cmd.ExecuteNonQuery()
    End Sub
    Private Sub btnupdate_Click(sender As Object, e As EventArgs)
        If TextBox2.Text = "" Then
            MsgBox("First name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox3.Text = "" Then
            MsgBox("Middle name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox4.Text = "" Then
            MsgBox("Last name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox10.Text = "" Then
            MsgBox("Contact details invalid", MsgBoxStyle.Critical)
        ElseIf TextBox10.Text.Length < 11 Then
            MsgBox("Contact number invalid", MsgBoxStyle.Critical)
        Else
            updatecustomer()
            disabletextbox()
            Customer_Manager.readcustomer()
            MsgBox("Customer Updated Successfull", MsgBoxStyle.Information)
        End If
    End Sub

    Sub updatecustomer()
        str = "Update Customer_tbl set Fname = @Fname, Mname = @Mname, Lname = @Lname, Extension = @Extension, ContactNumber = @ContactNumber where CID = @CID"
        cmd = New SqlCommand(str, sqlconn)
        With cmd.Parameters
            .AddWithValue("@Fname", TextBox2.Text)
            .AddWithValue("@Mname", TextBox3.Text)
            .AddWithValue("@Lname", TextBox4.Text)
            .AddWithValue("@Extension", TextBox5.Text)
            .AddWithValue("@ContactNumber", TextBox10.Text)
            .AddWithValue("@CID", TextBox1.Text)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Private Sub updatevehicle_Click(sender As Object, e As EventArgs)
        If TextBox7.Text = "" Then
            MsgBox("Plate number invalid", MsgBoxStyle.Critical)
        ElseIf TextBox8.Text = "" Then
            MsgBox("vehicle model invalid", MsgBoxStyle.Critical)
        ElseIf ComboBox1.Text = "" Then
            MsgBox("vehicle type invalid", MsgBoxStyle.Critical)
        ElseIf ComboBox2.Text = "" Then
            MsgBox("vehicle size invalid", MsgBoxStyle.Critical)
        ElseIf TextBox9.Text = "" Then
            MsgBox("vehicle color invalid", MsgBoxStyle.Critical)
        Else
            vehicle_update()
            Customer_Manager.readcustomer()
            RecordNumber()
            clearvehicletextbox()
            MsgBox("Vehicle Updated Successfull", MsgBoxStyle.Information)
            readvehicle()
            cleartextbox2()
        End If

    End Sub
    Sub cleartextbox2()
        TextBox7.Clear()
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        TextBox8.Clear()
        TextBox9.Clear()
    End Sub
    Sub vehicle_update()
        query = "Update vehicle_reg set plateNumber = @plateNumber, v_type = @v_type, v_size = @v_size, v_model = @v_model,
                v_color = @v_color where rec = @rec"
        cmd = New SqlCommand(query, sqlconn)
        With cmd.Parameters
            .AddWithValue("@rec", TextBox6.Text)
            .AddWithValue("@plateNumber", TextBox7.Text)
            .AddWithValue("@v_type", ComboBox1.Text)
            .AddWithValue("@v_size", ComboBox2.Text)
            .AddWithValue("@v_model", TextBox8.Text)
            .AddWithValue("@v_color", TextBox9.Text)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()

    End Sub
    Private Sub savevehicle_Click(sender As Object, e As EventArgs)
        Dim existingplate As Boolean = False
        Dim exisitingrecord As Boolean = False
        str = "Select * from vehicle_reg"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            If dr("plateNumber").ToString.Equals(TextBox7.Text) Then
                existingplate = True
            ElseIf dr("rec").ToString.Equals(TextBox6.Text) Then
                exisitingrecord = True
            End If
        End While
        dr.Close()
        cmd.Dispose()
        If existingplate = True Then
            MsgBox("Plate number already exist", MsgBoxStyle.Critical)
        ElseIf exisitingrecord = True Then
            MsgBox("Record number already exist", MsgBoxStyle.Critical)
        ElseIf TextBox7.Text = "" Then
            MsgBox("Plate number invalid", MsgBoxStyle.Critical)
        ElseIf TextBox8.Text = "" Then
            MsgBox("vehicle model invalid", MsgBoxStyle.Critical)
        ElseIf TextBox9.Text = "" Then
            MsgBox("vehicle color invalid", MsgBoxStyle.Critical)
        Else
            registervehicle()
            Customer_Manager.readcustomer()
            CreateNewRecordNumber()
            clearvehicletextbox()
            MsgBox("Vehicle Registered Successfull", MsgBoxStyle.Information)
            readvehicle()
        End If
    End Sub

    Sub clearvehicletextbox()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
    End Sub
    Private Sub Btnregister_Click(sender As Object, e As EventArgs)
        Dim existingid As Boolean = False
        Dim existingname As Boolean = False
        str = "Select * from Customer_tbl"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            If dr("CID").ToString.Equals(TextBox1.Text) Then
                existingid = True
            ElseIf dr("Fname").ToString.Equals(TextBox2.Text) And dr("Mname").ToString.Equals(TextBox3.Text) And dr("Lname").ToString.Equals(TextBox4.Text) And
                     dr("Extension").ToString.Equals(TextBox5.Text) Then
                existingname = True
            End If

        End While
        dr.Close()
        cmd.Dispose()


        If existingid = True Then
            MsgBox("CID already exist", MsgBoxStyle.Critical)
        ElseIf existingname = True Then
            MsgBox("Customer name already exist", MsgBoxStyle.Critical)
        ElseIf TextBox2.Text = "" Then
            MsgBox("First name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox3.Text = "" Then
            MsgBox("Middle name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox4.Text = "" Then
            MsgBox("Last name invalid", MsgBoxStyle.Critical)
        ElseIf TextBox10.Text = "" Then
            MsgBox("Contact details invalid", MsgBoxStyle.Critical)
        ElseIf TextBox10.Text.Length < 11 Then
            MsgBox("Contact number invalid", MsgBoxStyle.Critical)
        Else
            savecustomer()
            Customer_Manager.readcustomer()
            saveindirectory()
            CreateNewAutoNumber()
            'AutoNumber()
            ' generateqrcode()
            'cleartextbox()
            disabletextbox()
            MsgBox("Customer Registered Successfull", MsgBoxStyle.Information)
        End If


    End Sub
    Sub disabletextbox()
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox10.Enabled = False
    End Sub

    Sub cleartextbox()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox10.Clear()
    End Sub

    Sub saveindirectory()
        Dim folder As String = Path.Combine(Application.StartupPath, "QR Code")
        Dim customerID As String = TextBox1.Text.Trim()
        Dim filePath As String = Path.Combine(folder, customerID & ".png")

        ' Save QR code
        PictureBox1.Image.Save(filePath, ImageFormat.Png)

        MessageBox.Show("QR code saved to:" & vbCrLf & filePath)
    End Sub

    Sub registervehicle()
        query = "Insert into vehicle_reg (rec,CID,plateNumber,v_type,v_size,v_model,v_color,user_stamp,date_stamp) values
                (@rec,@CID,@plateNumber,@v_type,@v_size,@v_model,@v_color,@user_stamp,@date_stamp)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd.Parameters
            .AddWithValue("@rec", TextBox6.Text)
            .AddWithValue("@CID", TextBox1.Text)
            .AddWithValue("@plateNumber", TextBox7.Text)
            .AddWithValue("@v_type", ComboBox1.Text)
            .AddWithValue("@v_size", ComboBox2.Text)
            .AddWithValue("@v_model", TextBox8.Text)
            .AddWithValue("@v_color", TextBox9.Text)
            .AddWithValue("@user_stamp", Fname.ToString)
            .AddWithValue("@date_stamp", Date.Now)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        With ComboBox2
            .Items.Clear()

            If ComboBox1.SelectedIndex = 0 Then
                .Items.Add("Small")
                .Items.Add("Large")
            ElseIf ComboBox1.SelectedIndex = 1 Then
                .Items.Add("Small")
                .Items.Add("Medium")
                .Items.Add("Large")
                .Items.Add("Extra Large")
            End If

        End With

    End Sub

    Sub readvehicle()
        DataGridView1.Rows.Clear()
        str = "Select * from vehicle_reg where CID = @CID"
        cmd = New SqlCommand(str, sqlconn)
        cmd.Parameters.AddWithValue("@CID", TextBox1.Text)
        dr = cmd.ExecuteReader
        While dr.Read
            DataGridView1.Rows.Add(dr("rec"), dr("plateNumber"), dr("v_type") + " " + dr("v_size") + " " + dr("v_model") + " " +
                                   dr("v_color"), "Edit", "Delete")
        End While
        dr.Close()
        cmd.Dispose()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer = DataGridView1.CurrentRow.Index


        If e.ColumnIndex = 3 Then
            vhcl_updatebutton()
            TextBox6.Text = DataGridView1.Item(0, i).Value

            str = "Select * from vehicle_reg where rec = '" & TextBox6.Text & "'"
            cmd = New SqlCommand(str, sqlconn)
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox7.Text = dr("plateNumber").ToString
                ComboBox1.Text = dr("v_type").ToString
                ComboBox2.Text = dr("v_size").ToString
                TextBox8.Text = dr("v_model").ToString
                TextBox9.Text = dr("v_color").ToString

            End While
            dr.Close()
            cmd.Dispose()
        ElseIf e.ColumnIndex = 4 Then
            Dim confirm = MsgBox("Are you sure you want to delete this vehicle?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Delete")
            If confirm = MsgBoxResult.Yes Then
                str = "Delete from vehicle_reg where rec = '" & DataGridView1.Item(0, i).Value & "'"
                cmd = New SqlCommand(str, sqlconn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                MsgBox("Vehicle Deleted Successfully", MsgBoxStyle.Information)
                readvehicle()
                Customer_Manager.readcustomer()
            End If
        End If
    End Sub
End Class