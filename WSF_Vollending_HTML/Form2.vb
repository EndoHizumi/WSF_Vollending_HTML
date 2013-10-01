Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        editor.SourceEditor.Find(Me.TextBox1.Text)

    End Sub
End Class