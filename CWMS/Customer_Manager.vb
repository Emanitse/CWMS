Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO

Public Class Customer_Manager
    Private Sub Customer_Manager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connect()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Customer_add.ShowDialog()
    End Sub
End Class