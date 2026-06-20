Imports System.Data.SqlClient
Imports System.IO
Imports QRCoder
Imports System.Drawing.Imaging
Public Class GenerateQR
    Private Sub Generate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        AutoNumber()
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
                    text1.Text = number1
                End With
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                number = Convert.ToString(cmd.ExecuteScalar)
                text1.Text = number
            End If
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub


    Sub savedata()
        If PictureBox1.Image Is Nothing Then
            MsgBox("Generate QR first.")
            Exit Sub
        End If

        Dim ms As New MemoryStream

        PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        query = "Insert Into Cacc (CID,Name,PlateNumber,ContactNumber,Userimage,User_stamp,Date_stamp) values (@CID,@Name,@PlateNumber,@ContactNumber,@Userimage,@User_stamp,@Date_stamp)"
        cmd = New SqlClient.SqlCommand(query, sqlconn)

        With cmd.Parameters
            .AddWithValue("@CID", text1.Text)
            .AddWithValue("@Name", nm.Text)
            .AddWithValue("@PlateNumber", Platenumber.Text)
            .AddWithValue("@ContactNumber", TextBox1.Text)
            .AddWithValue("@Userimage", ms.ToArray())
            .AddWithValue("@User_stamp", Userlevel)
            .AddWithValue("@Date_stamp", CDate(Date.Now.ToString("MM/dd/yyyy")))
        End With
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub










    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrData As QRCodeData = qrGenerator.CreateQrCode(text1.Text, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrData)

        Dim qrImage As Bitmap = qrCode.GetGraphic(20)

        PictureBox1.Image = qrImage
    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles text1.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles CustomerID.Click

    End Sub

    Private Sub Name_TextChanged(sender As Object, e As EventArgs) Handles nm.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub GenerateQR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        savedata
    End Sub
End Class