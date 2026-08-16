Public Class Staff_logs
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ScanFingerPrint.ShowDialog()
    End Sub

    Private Sub Staff_logs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
        Dim readall As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension  from Emp_logs as 
                L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date = CAST(GETDATE() as date)"
        readLogs(readall)
        DateTimePicker1.MaxDate = Date.Today
        DateTimePicker2.MaxDate = Date.Today

    End Sub

    Sub readLogs(ByVal str As String)
        DataGridView1.Rows.Clear()

        cmd = New SqlClient.SqlCommand(str, sqlconn)
        dr = cmd.ExecuteReader
        While dr.Read
            DataGridView1.Rows.Add(dr("Rec"), dr("Staffid"), dr("Fname") + " " + dr("Mname") + " " + dr("Lname") + " " + dr("Extension"),
                                   CDate(dr("T_date").ToString()).ToString("MM/dd/yyyy"), dr("Timelog"), dr("T_stat"))
        End While
        dr.Close()
        cmd.Dispose()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim readall As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension  from Emp_logs as 
                L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date = CAST(GETDATE() as date)"

        readLogs(readall)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim readonduty As String = "WITH LatestLogs AS (" &
      " SELECT L.Rec,L.staff_id, L.Timelog, L.T_date, L.T_stat, " &
      " S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension, " &
      " ROW_NUMBER() OVER (PARTITION BY L.staff_id ORDER BY L.Timelog DESC) AS rn " &
      " FROM Emp_logs AS L " &
      " INNER JOIN Staff_Tbl AS S ON S.Staffid = L.staff_id " &
      " WHERE L.T_date = CAST(GETDATE() AS date) " &
      ") " &
      "SELECT Rec, Timelog, T_date, T_stat, Staffid, Fname, Mname, Lname, Extension " &
      "FROM LatestLogs " &
      "WHERE rn = 1 AND T_stat = 'IN' " &
      "ORDER BY Timelog DESC"

        readLogs(readonduty)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DateTimePicker2.Value < DateTimePicker1.Value Then
            MessageBox.Show("End date cannot be earlier than start date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
            MessageBox.Show("Start date cannot be later than end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Else
            If ComboBox1.SelectedItem Is Nothing Then
                Dim readbydate As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension
                                            from Emp_logs as L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date BETWEEN
                                            CAST('" & DateTimePicker1.Value & "' as date) AND CAST('" & DateTimePicker2.Value & "' as date)"
                readLogs(readbydate)

            ElseIf ComboBox1.SelectedIndex = 0 Then
                Dim readbyid As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension
                                            from Emp_logs as L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date BETWEEN
                                            CAST('" & DateTimePicker1.Value & "' as date) AND CAST('" & DateTimePicker2.Value & "' as date) AND S.Staffid like '%" & TextBox1.Text & "%'"
                readLogs(readbyid)
            ElseIf ComboBox1.SelectedIndex = 1 Then
                Dim readbyname As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension
                                            from Emp_logs as L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date BETWEEN
                                            CAST('" & DateTimePicker1.Value & "' as date) AND CAST('" & DateTimePicker2.Value & "' as date) AND (S.Fname + ' ' + S.Mname + ' ' + S.Lname) LIKE '%" & TextBox1.Text & "%'"
                readLogs(readbyname)
            ElseIf ComboBox1.SelectedIndex = 2 Then
                Dim Status As String = "Select L.Rec, L.T_date, L.Timelog, L.T_stat, S.Staffid, S.Fname, S.Mname, S.Lname, S.Extension
                                            from Emp_logs as L Inner join Staff_Tbl as S on S.Staffid = L.staff_id where L.T_date BETWEEN
                                            CAST('" & DateTimePicker1.Value & "' as date) AND CAST('" & DateTimePicker2.Value & "' as date) AND L.T_stat  = '" & TextBox1.Text & "'"
                readLogs(Status)
            End If

        End If






    End Sub
End Class

