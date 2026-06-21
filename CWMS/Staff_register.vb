Imports System.Data.SqlClient
Imports ZKFPEngXControl
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Controls
Public Class Staff_register
    Private WithEvents AxZKFPEngX1 As ZKFPEngXControl.ZKFPEngX

    Private strTemplate As String = ""


    Public empid As String
    Public fingercount As Integer = 0
    Public nextcount As Integer = 0
    Private Sub Staff_register_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        readEmployee()
        AutoNumber()


    End Sub

    Sub countfinger()
        str = "Select count(staff_id) as countfinger from FingerPirnt_tbl where Staff_id = '" & empid.ToString & " '"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            If dr("countfinger").ToString.Equals(0) Then
                nextcount = 1
            Else
                nextcount = fingercount + 1
            End If
        End While
    End Sub


    Sub readEmployee()
        DataGridView1.Rows.Clear()
        str = "Select * from Staff_Tbl order by staffid ASC"
        cmd = New SqlClient.SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            DataGridView1.Rows.Add(dr("staffid"), dr("Fullname"), "Edit", "Delete", "Add Fprint")
        End While
        dr.Close()
        cmd.Dispose()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim existingname As Boolean = False

        If Txt_Empname.Text = "" Then
            MsgBox("Please enter employee name.")
        Else
            str = "Select * from Staff_Tbl"
            cmd = New SqlCommand(str, sqlconn)
            dr = cmd.ExecuteReader
            While dr.Read
                If Txt_Empname.Text.ToString.Equals(dr("Fullname").ToString) Then
                    existingname = True
                    Exit While
                End If
            End While
            dr.Close()
            cmd.Dispose()

            If existingname = True Then
                MsgBox("Employee name already exists.")
            Else
                saveemployee()
                MsgBox("Employee saved successfully.")
                CreateNewAutoNumber()
                AutoNumber()
                readEmployee()
                clear()
            End If

        End If
    End Sub

    Sub clear()
        Txt_Empname.Clear()
    End Sub

    Sub saveemployee()
        query = "Insert Into Staff_Tbl (Staffid,Fullname) values (@Staffid,@Fullname)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@Staffid", Txt_EmpID.Text)
            .Parameters.AddWithValue("@Fullname", Txt_Empname.Text)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Sub updateemployee()
        query = "Update Staff_Tbl set Fullname = @Fullname where Staffid = @Staffid"
        cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@Staffid", Txt_EmpID.Text)
            .Parameters.AddWithValue("@Fullname", Txt_Empname.Text)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer = DataGridView1.CurrentRow.Index

        If e.ColumnIndex = 2 Then
            Txt_EmpID.Text = DataGridView1.Item(0, i).Value
            Txt_Empname.Text = DataGridView1.Item(1, i).Value
        ElseIf e.ColumnIndex = 3 Then
            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                str = "Delete from Staff_Tbl where Staffid = @Staffid"
                cmd = New SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@Staffid", DataGridView1.Item(0, i).Value)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
                MsgBox("Employee deleted successfully.")
                readEmployee()
            End If
        ElseIf e.ColumnIndex = 4 Then
            empid = DataGridView1.Item(0, i).Value

            Try
                ' 1. Allocate the backend runtime memory context instance
                AxZKFPEngX1 = New ZKFPEngXControl.ZKFPEngX()

                ' 2. Establish connection properties BEFORE initiating engine cycles
                AxZKFPEngX1.SensorIndex = 0 ' Targets the first physical SLK20R device channel

                ' 3. Boot up the hardware engine core
                If AxZKFPEngX1.InitEngine() = 0 Then

                    ' ⚡ CRUCIAL: Instruct the library to pass native image streams during event loops 
                    AxZKFPEngX1.IsRegister = True

                    ' Activate active background capture loops
                    AxZKFPEngX1.BeginCapture()
                    Me.Text = "Scanner Status: CONNECTED & LISTENING..."
                Else
                    MessageBox.Show("Failed to establish hardware engine hooks. Verify device is plugged in.", "Hardware Link Error")
                End If

            Catch ex As Exception
                MessageBox.Show("ActiveX Dynamic Allocation Error: " & ex.Message, "Runtime Link Failure")
            End Try
            Panel1.Show()
            countfinger()
            scanfingerprint()

        End If
    End Sub

    Private Sub AxZKFPEngX1_OnCapture(ByVal ActionResult As Boolean, ByVal ATemplate As Object) Handles AxZKFPEngX1.OnCapture
        Try
            ' 1. Pull the textual mathematical string data into local system memory allocations
            strTemplate = AxZKFPEngX1.GetTemplateAsString()

            ' 2. Use a Direct Graphics pipeline to paste the image directly onto your PictureBox canvas matrix
            ' Note: Update 'PictureBox1' to match whatever your layout component name is set to.
            Using g As Graphics = PictureBox1.CreateGraphics()
                Dim hdc As IntPtr = g.GetHdc()

                ' Force the ZK image processing framework to paint inside the PictureBox window bounds
                AxZKFPEngX1.PrintImageAt(hdc.ToInt32(), 0, 0, PictureBox1.Width, PictureBox1.Height)

                ' Release the system device pointer immediately to prevent memory leaks or frozen frames
                g.ReleaseHdc(hdc)
            End Using

            ' Refresh the screen layout container to present structural changes instantly
            PictureBox1.Invalidate()
            Me.Text = "Scan Status: Fingerprint Matrix Captured Successfully!"

        Catch ex As Exception
            MessageBox.Show("Drawing Loop Execution Error: " & ex.Message, "UI Paint Error")
        End Try
    End Sub

    Private Sub AxZKFPEngX1_OnImageReceived(ByRef AImageValid As Boolean) Handles AxZKFPEngX1.OnImageReceived
        If AImageValid Then
            Try
                ' 1. Create a blank permanent drawing surface in system memory that matches your PictureBox dimensions
                Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)

                ' 2. Use a persistent memory graphics object instead of temporary screen pixels
                Using memoryGraphics As Graphics = Graphics.FromImage(bmp)
                    Dim hdc As IntPtr = memoryGraphics.GetHdc()

                    ' Force the biometric device framework to paint onto our stable memory bitmap
                    AxZKFPEngX1.PrintImageAt(hdc.ToInt32(), 0, 0, PictureBox1.Width, PictureBox1.Height)

                    memoryGraphics.ReleaseHdc(hdc)
                End Using

                ' 3. Safely dispose of any old visual image data left in the picturebox to prevent memory leaks
                If PictureBox1.Image IsNot Nothing Then
                    PictureBox1.Image.Dispose()
                End If

                ' 4. Lock the bitmap permanently onto your WinForm layout control!
                PictureBox1.Image = bmp
                Label5.Text = 2
                Timer1.Start()
            Catch ex As Exception
                ' Safely absorb minor background streaming frame drop exceptions
            End Try
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        If Txt_Empname.Text = "" Then
            MsgBox("Please enter employee name.")
        Else
            updateemployee()
            MsgBox("Employee updated successfully.")
            AutoNumber()
            readEmployee()
            clear()
        End If

    End Sub

    Sub scanfingerprint()
        ' 1. Form Validation (Make sure they filled in the Staff ID)
        ' Note: If your textbox is named TextBox1, leave it as is. If you renamed it to txtStaffID, change it here.
        'If String.IsNullOrWhiteSpace(empid.ToString) Then
        '    MessageBox.Show("Please enter a valid Staff ID before scanning.", "ID Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        '    Return
        'End If

        ' 2. UI Reset: Clear out old scanned graphics from your PictureBox box
        ' If your PictureBox is named PictureBox1, change the name below to match it
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = Nothing
        PictureBox1.Refresh()

        ' 3. Reset your background template data variable
        strTemplate = ""

        ' 4. Visually prompt the user to touch the glass
        Me.Text = "Scanner Status: READY! Place finger down now."
        MessageBox.Show("The hardware is active and listening. Please place your finger firmly on the scanner sensor now.", "Scanner Status")
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        clear()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If AxZKFPEngX1 IsNot Nothing Then AxZKFPEngX1.EndCapture()
        Catch ex As Exception
        End Try
    End Sub

    Sub savefingerprint()
        If String.IsNullOrEmpty(strTemplate) Then
            MessageBox.Show("No biometric fingerprint template data found in cache memory. Please scan your finger first.", "Biometric Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        query = "Insert Into FingerPirnt_tbl (Staff_id,Finger,FPrint) values (@Staff_id,@Finger,@FPrint)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@Staff_id", empid.ToString)
            .Parameters.AddWithValue("@Finger", "Fingerprint " + nextcount.ToString)
            .Parameters.AddWithValue("@FPrint", strTemplate)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label5.Text = 0 Then
            Timer1.Stop()
            savefingerprint()
        Else
            Label5.Text = Label5.Text - 1
        End If
    End Sub
End Class
