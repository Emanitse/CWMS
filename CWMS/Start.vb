Public Class Start
    Private Sub Start_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Scan.Close()
        GenerateQR.BringToFront()
        GenerateQR.Show()
        GenerateQR.Location = New Point(274, 368)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GenerateQR.Close()
        Scan.BringToFront()
        Scan.Show()
        Scan.Location = New Point(274, 368)
    End Sub
End Class