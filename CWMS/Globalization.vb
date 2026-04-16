Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO

Module Globalization
    Public sqlconn As New SqlConnection
    Public cmd As New SqlCommand
    Public dr As SqlDataReader
    Public query, str As String
    Public MenuID, UserID, Fname, Userlevel As String
    Public img() As Byte

    Sub Connect()
        Try
            If sqlconn.State = ConnectionState.Open Then sqlconn.Close() 'refresh connection
            sqlconn.ConnectionString = "Server =LocalHost\SQLEXPRESS; Database = db_CWMS; Trusted_Connection = True; MultipleActiveResultSets = True; "
            sqlconn.Open()


        Catch ex As Exception

            MsgBox("Error in Connection please contact Administrator", MsgBoxStyle.Critical, "Connection error")

        End Try


    End Sub

End Module
