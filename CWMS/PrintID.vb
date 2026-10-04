Imports Microsoft.Reporting.WinForms

Public Class PrintID
    Private Sub PrintID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer = Customer_Manager.DataGridView1.CurrentRow.Index

        Me.Customer_tblTableAdapter.Fill(Me.DataSet1.Customer_tbl, Customer_Manager.DataGridView1.Item(0, i).Value)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class