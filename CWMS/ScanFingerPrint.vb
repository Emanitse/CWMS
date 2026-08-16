Imports System.Data.SqlClient

Public Class ScanFingerPrint
    Private WithEvents AxZKFPEngX1 As ZKFPEngXControl.ZKFPEngX

    Public recnumber As String
    Dim identifiedStaffId As String = ""
    Dim identifiedName As String = ""
    Dim status As String = ""
    Private Sub ScanFingerPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        AutoNumber()
        Label1.Text = "2"
        Timer1.Interval = 1000 ' Sets the clock countdown processing intervals to 1 second
        Timer1.Start()
    End Sub

    Sub CreateNewAutoNumber()
        Try
            Dim cmd As New SqlCommand
            With cmd
                .Connection = sqlconn
                .CommandText = "SP_AutoNumber"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@pfx", "LOG")
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
            .Parameters.AddWithValue("@pfx", "LOG")
            If IsDBNull(cmd.ExecuteScalar) Then
                CreateNewAutoNumber()
                Dim number1 As String
                str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
                cmd = New SqlClient.SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@pfx  ", "LOG")
                    number1 = Convert.ToString(cmd.ExecuteScalar)
                    recnumber = number1
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                number = Convert.ToString(cmd.ExecuteScalar)
                recnumber = number
            End If
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub
    Sub scanfinger()
        Try
            AxZKFPEngX1 = New ZKFPEngXControl.ZKFPEngX()
            AxZKFPEngX1.SensorIndex = 0

            If AxZKFPEngX1.InitEngine() = 0 Then
                ' Set to False because we are MATCHING/VERIFYING active profiles, not tracking enrollments
                AxZKFPEngX1.IsRegister = False
                AxZKFPEngX1.BeginCapture()

                Label1.Text = "ACTIVE"
                Label1.ForeColor = Color.Green
            Else
                MsgBox("Hardware Initialization Link Error. Verify reader connection ports.")
            End If
        Catch ex As Exception
            MessageBox.Show("ActiveX Module Allocation Instance Fault: " & ex.Message)
        End Try
    End Sub

    Private Sub AxZKFPEngX1_OnImageReceived(ByRef AImageValid As Boolean) Handles AxZKFPEngX1.OnImageReceived
        If AImageValid Then
            Try
                Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
                Using memoryGraphics As Graphics = Graphics.FromImage(bmp)
                    Dim hdc As IntPtr = memoryGraphics.GetHdc()
                    AxZKFPEngX1.PrintImageAt(hdc.ToInt32(), 0, 0, PictureBox1.Width, PictureBox1.Height)
                    memoryGraphics.ReleaseHdc(hdc)
                End Using

                If PictureBox1.Image IsNot Nothing Then PictureBox1.Image.Dispose()
                PictureBox1.Image = bmp
            Catch ex As Exception
                ' Safely bypass processing skip drops
            End Try
        End If
    End Sub

    Private Sub AxZKFPEngX1_OnCapture(ByVal ActionResult As Boolean, ByVal ATemplate As Object) Handles AxZKFPEngX1.OnCapture
        If Not ActionResult Then
            MsgBox("Bad scan reading quality. Lift and press your finger flatly against the sensor.")
            Return
        End If

        ' 1. Capture the live finger template as a string from the active scan
        Dim liveTemplateStr As String = AxZKFPEngX1.GetTemplateAsString()
        Dim matchFound As Boolean = False


        ' Construct query using your global variable architecture
        str = "SELECT F.Staff_id, S.Fname, S.Mname, S.Lname, S.Extension, F.FPrint " &
          "FROM FingerPirnt_tbl F " &
          "INNER JOIN Staff_Tbl S ON F.Staff_id = S.Staffid"

        Try
            ' Ensure connection is open if managed globally
            If sqlconn.State = ConnectionState.Closed Then sqlconn.Open()

            cmd = New SqlCommand(str, sqlconn)
            dr = cmd.ExecuteReader()
            Dim regChanged As Boolean = False

            While dr.Read()
                ' 1. Pull out the saved fingerprint string from the database row
                Dim dbTemplateStr As String = dr("FPrint").ToString()

                ' 2. MATCH STEP: Use VerFingerFromStr with its required 4 arguments:
                ' Parameter 1 (ByRef): Stored/Registered Template String from DB
                ' Parameter 2 (ByVal): Live Scanned Template String from Sensor
                ' Parameter 3 (ByVal): Boolean flag (False means do not automatically merge/update template)
                ' Parameter 4 (ByRef): Boolean flag to catch if the registration template changed
                If AxZKFPEngX1.VerFingerFromStr(dbTemplateStr, liveTemplateStr, False, regChanged) = True Then
                    matchFound = True
                    identifiedStaffId = dr("Staff_id").ToString()
                    identifiedName = dr("Fname").ToString() & " " & dr("Mname").ToString() & " " & dr("Lname").ToString() & " " & dr("Extension").ToString()
                    Exit While ' Break loop immediately when matched
                End If

            End While
        Catch ex As Exception
            MessageBox.Show("Database reading error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Always close reader safely to release resource locks
            If dr IsNot Nothing AndAlso Not dr.IsClosed Then dr.Close()
            If cmd IsNot Nothing Then cmd.Dispose()
        End Try

        ' 4. Process matches
        If matchFound Then
            MessageBox.Show("Welcome back, " & identifiedName & vbCrLf & "Staff ID: " & identifiedStaffId,
                        "Verification Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Place your post-login operations or attendance system updates here
            checkstatus()
            savetimelogs()
            CreateNewAutoNumber()
            AutoNumber()

            Dim readall As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension  from Emp_logs as 
                L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date = CAST(GETDATE() as date)"
            Staff_logs.readLogs(readall)
        Else
            MessageBox.Show("No matching fingerprint found. Please try again.",
                        "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub ScanFingerPrint_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If AxZKFPEngX1 IsNot Nothing Then AxZKFPEngX1.EndCapture()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currentCount As Integer = 0
        If Integer.TryParse(Label1.Text, currentCount) Then
            If currentCount <= 1 Then
                Timer1.Stop()
                scanfinger()
            Else
                Label1.Text = (currentCount - 1).ToString()
            End If
        End If
    End Sub

    Sub checkstatus()
        str = "Select * from Emp_logs where staff_id = '" & identifiedStaffId & "' and T_date = CAST(GETDATE() as date)"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            str = "Select TOP 1(Timelog) as lastlog, T_stat from Emp_logs where staff_id = '" & identifiedStaffId & "' group by staff_id, T_date, T_stat, Timelog order by Timelog  DESC"
            cmd = New SqlCommand(str, sqlconn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("T_stat").ToString.Equals("IN") Then
                    status = "OUT"
                ElseIf dr("T_stat").ToString.Equals("OUT") Then
                    status = "IN"
                End If
            End While
        Else
            status = "IN"
        End If

        dr.Close()
        cmd.Dispose()


    End Sub

    Sub savetimelogs()

        query = "Insert Into Emp_logs (Rec, staff_id, T_date,Timelog,T_stat) values (@Rec, @staff_id, @T_date,@Timelog,@T_stat)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@Rec", recnumber)
            .Parameters.AddWithValue("@staff_id", identifiedStaffId)
            .Parameters.AddWithValue("@T_date", Date.Now.ToString("MM/dd/yyyy"))
            .Parameters.AddWithValue("@Timelog", Date.Now.ToString("HH:mm:ss"))
            .Parameters.AddWithValue("@T_stat", status)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

End Class