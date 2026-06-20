Imports System.Drawing.Drawing2D
Imports System.Windows.Media.TextFormatting

Public Class Scan

    Private scanEnabled As Boolean = False
    Private scanProcessed As Boolean = False
    Private Sub RoundLabel(lbl As Label, radius As Integer)
        Dim path As New GraphicsPath()

        path.StartFigure()
        path.AddArc(0, 0, radius, radius, 180, 90) ' top-left
        path.AddArc(lbl.Width - radius, 0, radius, radius, 270, 90) ' top-right
        path.AddArc(lbl.Width - radius, lbl.Height - radius, radius, radius, 0, 90) ' bottom-right
        path.AddArc(0, lbl.Height - radius, radius, radius, 90, 90) ' bottom-left
        path.CloseFigure()

        lbl.Region = New Region(path)
    End Sub





    Private Sub BtnScan_Click(sender As Object, e As EventArgs)
        btnScan.Text = "Scanning..."
        txtScan.Enabled = True
        scanEnabled = True
        scanProcessed = False
        txtScan.Clear()
        txtScan.Focus()


    End Sub


    Private Sub TxtScan_KeyDown(sender As Object, e As KeyEventArgs)

        If Not scanEnabled Then Exit Sub
        If scanProcessed Then Exit Sub

        If e.KeyCode = Keys.Enter Then

            Dim qr As String = txtScan.Text.Trim()

            MessageBox.Show("Scanned: " & qr)

            scanProcessed = True
            scanEnabled = False


            btnScan.Text = "Scan"
            txtScan.Clear()
            txtScan.Enabled = False
        End If

    End Sub
End Class