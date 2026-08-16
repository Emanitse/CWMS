Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock

Public Class Dashboard
    'picturebox round corners
    Private Sub RoundCorners(pb As PictureBox, radius As Integer)
        Dim path As New GraphicsPath()

        path.StartFigure()
        path.AddArc(0, 0, radius, radius, 180, 90) ' top-left
        path.AddArc(pb.Width - radius, 0, radius, radius, 270, 90) ' top-right
        path.AddArc(pb.Width - radius, pb.Height - radius, radius, radius, 0, 90) ' bottom-right
        path.AddArc(0, pb.Height - radius, radius, radius, 90, 90) ' bottom-left
        path.CloseFigure()

        pb.Region = New Region(path)
    End Sub

    ' picturebox1 round corners
    Private Sub PictureBox1_Resize(sender As Object, e As EventArgs) Handles PictureBox1.Resize
        RoundCorners(PictureBox1, 20)
    End Sub
    'picturebox2 round corners
    Private Sub PictureBox2_Resize(sender As Object, e As EventArgs) Handles PictureBox2.Resize
        RoundCorners(PictureBox2, 20)
    End Sub

    'Panel Round Corners
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(
    ByVal nLeftRect As Integer,
    ByVal nTopRect As Integer,
    ByVal nRightRect As Integer,
    ByVal nBottomRect As Integer,
    ByVal nWidthEllipse As Integer,
    ByVal nHeightEllipse As Integer) As IntPtr
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Panel1.Width, Panel1.Height, 30, 30))
    End Sub


    'Day 
    Private Sub Panel2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Today As Date = Date.Today
        Dim dayName As String = Today.DayOfWeek.ToString()
        Daymain.Text = dayName

    End Sub

    'Date 
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dts.Text = CDate(Date.Now.ToString("MM/dd/yy"))
    End Sub

    'Time
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Time.Text = Date.Now.ToString("HH:mm:ss")
    End Sub





    'Start Button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With Start
            .TopLevel = False
            Panel1.Controls.Add(Start)
            Payment.Close()
            GenTransaction.Close()
            GenEmReport.Close()
            GenComReport.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub

    'Payment Button
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With Staff_logs
            .TopLevel = False
            Panel1.Controls.Add(Staff_logs)
            Start.Close()
            GenTransaction.Close()
            GenEmReport.Close()
            GenComReport.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub

    'Generate Transaction Button
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With GenTransaction
            .TopLevel = False
            Panel1.Controls.Add(GenTransaction)
            Payment.Close()
            Start.Close()
            GenEmReport.Close()
            GenComReport.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub

    'Generate Employee Report Button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With GenEmReport
            .TopLevel = False
            Panel1.Controls.Add(GenEmReport)
            Payment.Close()
            GenTransaction.Close()
            Start.Close()
            GenComReport.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub

    'Generate Company Report Button
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        With GenComReport
            .TopLevel = False
            Panel1.Controls.Add(GenComReport)
            Payment.Close()
            GenTransaction.Close()
            GenEmReport.Close()
            Start.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub



    'Clickable for PictureBox2
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        GenComReport.Close()
        GenEmReport.Close()
        GenTransaction.Close()
        Payment.Close()
        Start.Close()

    End Sub
    'Cursor for PictureBox2
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox2.Cursor = Cursors.Hand
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Hide()
            Login.Show()
            Login.Resetlogin()
            Application.Restart()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        With Staff_register
            .TopLevel = False
            Panel1.Controls.Add(Staff_register)
            Payment.Close()
            GenTransaction.Close()
            GenEmReport.Close()
            Start.Close()
            .BringToFront()
            .Show()
            Time.Show()
            Dts.Show()

        End With
    End Sub
End Class