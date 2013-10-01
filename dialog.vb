Public Class dialog
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Preview.Text = previewLink(TextBox1.Text, TextBox2.Text)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Preview.Text = previewLink(TextBox1.Text, TextBox2.Text)
    End Sub

    Function previewLink(ByVal link As String, ByVal text As String)
        Return "<a href=" + """" + link + """" + ">" + text + "</a>"
    End Function
End Class