Imports Gecko
'Imports WebKit.WebKitBrowser
Public Class main
    Dim ed As editor


    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        editor_load("No Title")
    End Sub

    Private Sub main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cmds() As String
        cmds = System.Environment.GetCommandLineArgs()
        If cmds.Length >= 2 Then
            editor_load(cmds(1))
        Else
            editor_load("No Title")
        End If

    End Sub

    Public Sub editor_load(ByVal sender As String)
        ed = New editor
        ed.Myname = sender
        ed.openpath = sender
        ed.Owner = Me
        ed.MdiParent = Me
        ed.WindowState = FormWindowState.Maximized
        ed.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Dim openfiledialog As New OpenFileDialog
        Dim ed As New editor
        openfiledialog.Filter = ("HTMLファイル|*.html;*.htm|すべてのファイル|*.*")
        If DialogResult.OK = openfiledialog.ShowDialog() Then
            editor_load(openfiledialog.FileName)
            editor.TextChange = True

        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub HTMLクイックリファレンスToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HTMLクイックリファレンスToolStripMenuItem.Click
        Dim hz As New Form1
        hz.Text = "HTMLクイックリファレンス"
        hz.WebBrowser1.Navigate("C:\Users\cone\Downloads\webox0.99M\Data\www.htmq.com\index.html")
        hz.Show()
    End Sub

    Private Sub HTMLリファレンスToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HTMLリファレンスToolStripMenuItem1.Click
        Dim hz As New Form1
        hz.Text = "HTMLリファレンス"
        hz.WebBrowser1.Navigate("http://www.tohoho-web.com/html/index.htm")
        hz.Show()
    End Sub

    Private Sub HTML辞典ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HTML辞典ToolStripMenuItem.Click
        Dim hz As New Form1
        hz.Text = "HTML辞典"
        hz.WebBrowser1.Navigate("http://heo.jp/tag/")
        hz.Show()
    End Sub

    Private Sub WSFVollendingHTMLについてToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WSFVollendingHTMLについてToolStripMenuItem.Click
        MsgBox("WSF_Vollending_HTML " & "6.0" & vbCrLf & "(c)2010 WinvisSoftwareFactory")

    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        MsgBox("連絡先：winvissoftware●gmail.com" & vbCrLf & "●を＠に変えてから送信してください")
    End Sub

    Private Sub 簡単で正しいHTMLの書き方ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 簡単で正しいHTMLの書き方ToolStripMenuItem.Click
        Dim hz As New Form1
        hz.Text = "簡単で正しいHTMLの書き方"
        hz.WebBrowser1.Navigate("http://homepage.mac.com/toda/html/")
        hz.Show()
    End Sub

    Public Sub infobox(ByVal image As Bitmap, ByVal message As String, ByVal ob As ToolStripLabel)

        ob.Image = image

        ob.Text = message
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Label1.Click
        infopanel.Visible = False
        paneltoggle.Visible = True
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles paneltoggle.Click
        infopanel.Visible = True
        paneltoggle.Visible = False
    End Sub
End Class
