Imports QRCoder

Public Class GenerateQR


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrData As QRCodeData = qrGenerator.CreateQrCode(CusID.Text, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrData)

        Dim qrImage As Bitmap = qrCode.GetGraphic(20)

        PictureBox1.Image = qrImage
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles CusID.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles CustomerID.Click

    End Sub

    Private Sub Name_TextChanged(sender As Object, e As EventArgs) Handles nm.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class