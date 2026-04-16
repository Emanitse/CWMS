Imports System.Drawing.Drawing2D

Public Class GenEmReport

    'round label
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
    'round label
    Private Sub Label1_Resize(sender As Object, e As EventArgs) Handles Label1.Resize
        RoundLabel(Label1, 20)
    End Sub


End Class