Imports System.Data.SqlClient
Imports ZKFPEngXControl
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Controls
Imports System.IO
Imports System.Drawing
Imports System.Reflection.Emit



Public Class FingerprintScan
    Public recnumber As String
    Public countfinger As Integer
    Private WithEvents AxZKFPEngX1 As ZKFPEngXControl.ZKFPEngX

    Private strTemplate As String = ""
    Private Sub FingerprintScan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        AutoNumber()
        countfingerprint()

    End Sub

    Sub countfingerprint()
        str = "Select count(*) as countf from FingerPirnt_tbl where Staff_id = '" & Staff_add.Txt_EmpID.Text & "'"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            countfinger = dr("countf")
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
                .Parameters.AddWithValue("@pfx", "REC")
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
            .Parameters.AddWithValue("@pfx", "REC")
            If IsDBNull(cmd.ExecuteScalar) Then
                CreateNewAutoNumber()
                Dim number1 As String
                str = "SELECT Max(NewNumber) FROM Autonumber where pfx = @pfx"
                cmd = New SqlClient.SqlCommand(str, sqlconn)
                With cmd
                    .Parameters.AddWithValue("@pfx  ", "REC")
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
                Label2.Text = 2
                Timer1.Start()
                countfingerprint()
            Catch ex As Exception
                ' Safely absorb minor background streaming frame drop exceptions
            End Try
        End If


    End Sub
    Sub scanfingerprint()

        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = Nothing
        PictureBox1.Refresh()


        strTemplate = ""


        Label1.Text = "Scanner Status: READY! Place finger down now."
        MessageBox.Show("The hardware is active and listening. Please place your finger firmly on the scanner sensor now.", "Scanner Status")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

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
        scanfingerprint()
    End Sub

    Private Sub ResetFormLayout()

        PictureBox1.Image = Nothing

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label2.Text = 0 Then
            Timer1.Stop()
            If countfinger >= 3 Then
                MsgBox("Failed to save finger print, reached the maximum record/s")
            Else
                savefingerprint(recnumber, Staff_add.Txt_EmpID.Text)
                MsgBox("Finger Print Save Sucess")
                PictureBox1.Image = Nothing
                CreateNewAutoNumber()
                AutoNumber()
                Staff_add.readfingerprint()
            End If
        Else
                Label2.Text = Label2.Text - 1
        End If
    End Sub

    Sub savefingerprint(ByVal recno As String, empid As String)

        query = "Insert Into FingerPirnt_tbl (RecNo, Staff_id, FPrint) values (@RecNo, @Staff_id, @FPrint)"
        cmd = New SqlCommand(query, sqlconn)
        With cmd
            .Parameters.AddWithValue("@RecNo", recno)
            .Parameters.AddWithValue("@Staff_id", empid)
            .Parameters.AddWithValue("@FPrint", strTemplate)
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class