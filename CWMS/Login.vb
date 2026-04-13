Imports System.IO
Imports System.Text.RegularExpressions

Public Class Login

    Dim trynumber As Integer = 0

    'Resetlogin
    Public Sub Resetlogin()
        txtUsername.Text = ""
        txtpassword.Text = ""
    End Sub



    'Username and Password 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim resultat As Integer = 0
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtpassword.Text

        If username.Length <= 0 And password.Length <= 0 Then
            MsgBox("Input Username & Password", MsgBoxStyle.Information, "Input Invalid")
            txtUsername.Focus()
            Exit Sub
        End If

        If username.Length <= 0 Then
            MsgBox("Input username", MsgBoxStyle.Information, "Incomplete")
            txtUsername.Focus()
            Exit Sub
        End If

        If password.Length <= 0 Then
            MsgBox("Input password", MsgBoxStyle.Information, "Incomplete")
            txtpassword.Focus()
            Exit Sub
        End If

        str = "Select * from Users"
        cmd = New SqlClient.SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader

        While dr.Read
            If dr("Username").ToString.Equals(txtUsername.Text) And dr("Password").ToString.Equals(txtpassword.Text) Then
                resultat = 1
                UserID = dr("UserID")
                Fname = dr("Lname") + " "
                Userlevel = dr("Userlevel")

            End If
        End While

        dr.Close()
        cmd.Dispose()

        If resultat = 1 Then
            MsgBox("Welcome " + Fname + "you are now logged in", MsgBoxStyle.Information, "Login Success")

            With Dashboard
                .Label1.Text = Fname
                .Label2.Text = Userlevel
                .Show()
            End With
            Me.Hide() '
        Else
            MsgBox("Wrong username and password", MsgBoxStyle.Critical, "Invalid Credentials")
            trynumber += 1
            If trynumber >= 3 Then
                MsgBox("You've reached the maximum logged in attempts!", MsgBoxStyle.Critical, "System Disabled")
                Timer1.Start()
                Timer.Text = 15
                Timer.Visible = True
                Me.Enabled = False
                Countdown.Show()
            End If
        End If

    End Sub



    'Check Box
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            txtpassword.PasswordChar = ""
        Else
            txtpassword.PasswordChar = "•"
        End If

    End Sub


    'Timer for system locked
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Timer.Text = 0 Then
            Me.Enabled = True
            Timer1.Stop()
            Timer.Visible = False
            Countdown.Visible = False

        Else
            Timer.Text = Val(Timer.Text) - 1
        End If
    End Sub



    'Login Load
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub
End Class
