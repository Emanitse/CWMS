Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO

Public Class Customer_Manager
    Private Sub Customer_Manager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        readcustomer()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With Customer_add
            .AutoNumber()
            .RecordNumber()
            .generateqrcode()
            .addregisterbutton()
            .add_vhcl_registerbutton()
            .readvehicle()
        End With
        Customer_add.ShowDialog()



    End Sub


    Sub readcustomer()
        DataGridView1.Rows.Clear()
        str = "SELECT c.CID,c.Fname,c.Fname,c.mname,c.Lname,c.Extension,c.ContactNumber,COUNT(cv.CID) AS RegisteredVehicleCount FROM 
    Customer_tbl c LEFT JOIN vehicle_reg cv ON c.CID = cv.CID GROUP BY c.CID, c.Fname,c.mname,c.Lname,c.Extension,c.ContactNumber"
        cmd = New SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            DataGridView1.Rows.Add(dr("CID"), dr("Fname") + " " + dr("Mname") + " " + dr("Lname") + " " + dr("Extension"), dr("ContactNumber"), dr("RegisteredVehicleCount"), "Edit", "Print ID", "Delete")
        End While
        dr.Close()
        cmd.Dispose()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer = DataGridView1.CurrentRow.Index

        If e.ColumnIndex = 4 Then
            Dim qr() As Byte
            str = "Select * from Customer_tbl where CID = '" & DataGridView1.Item(0, i).Value & "'"
            cmd = New SqlCommand(str, sqlconn)
            dr = cmd.ExecuteReader
            While dr.Read
                qr = dr("QRcode")
                Dim ms As New MemoryStream(qr)
                With Customer_add
                    .TextBox1.Text = dr("CID")
                    .TextBox2.Text = dr("Fname")
                    .TextBox3.Text = dr("Mname")
                    .TextBox4.Text = dr("Lname")
                    .TextBox5.Text = dr("Extension")
                    .TextBox10.Text = dr("ContactNumber")
                    .PictureBox1.Image = Image.FromStream(ms)
                    .addupdatebutton()
                End With
            End While
            cmd.Dispose()
            dr.Close()
            Customer_add.cleartextbox2()
            Customer_add.ShowDialog()
        ElseIf e.ColumnIndex = 5 Then
            PrintID.ShowDialog()
        End If
    End Sub
End Class