Imports System.Data.SqlClient
Imports ZKFPEngXControl
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Controls
Imports System.IO
Imports System.Drawing
Public Class Staff_register
    Private WithEvents AxZKFPEngX1 As ZKFPEngXControl.ZKFPEngX

    Private strTemplate As String = ""


    Public empid As String
    Public fingercount As Integer = 0
    Public nextcount As Integer = 0
    Private Sub Staff_register_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        readEmployee("Select * from Staff_Tbl where Status = 'Active' order by Staffid ASC")


    End Sub

    Sub readEmployee(ByVal sortquery As String)
        DataGridView1.Rows.Clear()
        cmd = New SqlClient.SqlCommand(sortquery, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            Dim dateend As String = dr("Date_End").ToString()

            DataGridView1.Rows.Add(dr("staffid"), dr("Fname") + " " + dr("Mname") + " " + dr("Lname") + " " + dr("Extension"),
                                   dr("Address"), dr("Position"), dr("Status"), CDate(dr("Date_Start").ToString()).ToString("MM/dd/yyyy"),
                                  dateend, "view")
        End While
        dr.Close()
        cmd.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer = DataGridView1.CurrentRow.Index

        If e.ColumnIndex = 7 Then

            With Staff_add
                Dim img As Byte()
                str = "Select * from Staff_Tbl where staffid = '" & DataGridView1.Item(0, i).Value & "'"
                cmd = New SqlCommand(str, sqlconn)
                dr = cmd.ExecuteReader
                While dr.Read

                    .Txt_EmpID.Text = dr("staffid")
                    .txt_fname.Text = dr("Fname")
                    .txt_mname.Text = dr("Mname")
                    .txt_lname.Text = dr("Lname")
                    .txt_ext.Text = dr("Extension")
                    .txt_address.Text = dr("Address")
                    .cb_position.Text = dr("Position")
                    .cb_status.Text = dr("Status")
                    .dtp_datestart.Value = CDate(dr("Date_Start").ToString())
                    img = dr("Emp_image")
                    Dim ms1 As New MemoryStream(img)
                    .pb_empimage.Image = System.Drawing.Image.FromStream(ms1)



                End While
                dr.Close()
                cmd.Dispose()

                .cb_status.Enabled = True
                .addupdatebutton()
                .ShowDialog()


            End With
        End If

    End Sub



    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


    '    If txt_fname.Text = "" Then
    '        MsgBox("Please enter employee name.")
    '    Else
    '        updateemployee()
    '        MsgBox("Employee updated successfully.")
    '        AutoNumber()
    '        readEmployee()
    '        clear()
    '    End If

    'End Sub





    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    '    clear()
    'End Sub

    'Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    Try
    '        If AxZKFPEngX1 IsNot Nothing Then AxZKFPEngX1.EndCapture()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Sub savefingerprint()
    '    If String.IsNullOrEmpty(strTemplate) Then
    '        MessageBox.Show("No biometric fingerprint template data found in cache memory. Please scan your finger first.", "Biometric Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Return
    '    End If

    '    query = "Insert Into FingerPirnt_tbl (Staff_id,Finger,FPrint) values (@Staff_id,@Finger,@FPrint)"
    '    cmd = New SqlCommand(query, sqlconn)
    '    With cmd
    '        .Parameters.AddWithValue("@Staff_id", empid.ToString)
    '        .Parameters.AddWithValue("@Finger", "Fingerprint " + nextcount.ToString)
    '        .Parameters.AddWithValue("@FPrint", strTemplate)
    '    End With
    '    cmd.ExecuteNonQuery()
    '    cmd.Dispose()
    'End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        With Staff_add
            .cb_status.Enabled = False
            .addsavebutton()
            .clearall()
            .AutoNumber()
            .ShowDialog()
        End With



    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        readEmployee("Select * from Staff_Tbl order by Staffid ASC")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        readEmployee("Select * from Staff_Tbl where Status = 'Active' order by Staffid ASC")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        readEmployee("Select * from Staff_Tbl where Status = 'Resign' order by Staffid ASC")
    End Sub

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '   
    'End Sub


End Class
