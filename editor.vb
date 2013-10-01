'既存のバグ
'文字色変更後にカーソルが先頭に戻る
'タグ閉じ記号のあとに数字があると色を指定する記号と混ざってリッチテキストから「定義されていない指定記号」と見なされて削除される
Imports Gecko
Public Class editor
    Friend openpath As String
    Friend Myname As String
    Friend TextChange As Boolean = False
    Private SaveText As String
    Private currentpath As String = System.IO.Directory.GetCurrentDirectory
    Private previewpath As String = currentpath + "\pre.html"
    Private startpath = My.Application.Info.DirectoryPath
    Private savepath As String
    Private afterCharacterCode As String
    Private NowRows As String
    Private source As String()
    Private loadcomplete As Boolean = False
    Private savetexts As String
    Private errorwindow As New ArrayList
    Private errorlines As New ArrayList(New Integer)
    Private _error As String() = {String.Empty, String.Empty, String.Empty}
    Private evi As ListViewItem
    Private rendercount As Integer = 0
    Private ButtonState As Boolean = False
    Private ButtonState2 As Boolean = False
    Private EditingRow As Integer()
    Private changesrow As Integer
    Private changescol As Integer
    Private cols As Integer
    Private rows As Integer
    Private colorcode As String
    Private yaziri As Integer = 0
    Private norefresh As Boolean = False
    Private blank As Integer = 0
    Private blank_pre As Integer = 0
    Private rblnk As New System.Text.RegularExpressions.Regex("^\r|^\n|^\s|^t", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Multiline)
    Private yamakakko As New System.Text.RegularExpressions.Regex("<|>", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Multiline)
    Private s2 As String = String.Empty
    Private rs As String = String.Empty
    Private defaultsource As String = "<!DOCTYPE HTML PUBLIC " & "-//W3C//DTD HTML 4.01 Transitional//EN" & "http://www.w3.org/TR/html4/loose.dtd" & ">" & vbCrLf & "<HTML>" & vbCrLf & "<HEAD>" & vbCrLf & "<TITLE>ここにホームページのタイトルを入力します</TITLE>" & vbCrLf & "<META http-equiv=" & "Content-Type" & "content=" & "text/html;" & " charset=shift_jis" & ">" & vbCrLf & "<meta http-equiv=""""X-UA-Compatible"""" content=""""IE=9"""" >" & vbCrLf & "</HEAD>" & vbCrLf & vbCrLf & vbCrLf & "<BODY>" & vbCrLf & vbCrLf & vbCrLf & "</BODY>" & vbCrLf & vbCrLf & vbCrLf & "</HTML>"
    Private containerorientation = My.Settings.SplitOrientation
    Private flipvertical = My.Settings.flipvertical
    Dim Red As Integer = 0
    Dim Green As Integer = 158
    Dim Blue As Integer = 255
    Dim textsize As Integer = 6
    Private dw As RTF_View
    Private consoles As MyConsole
#Region "コメントアウトされたメソッド・定義"
    ' Private htmlsearch As New System.Text.RegularExpressions.Regex("<(""""[^""""]*""""|'[^']*'|[^'"""">])*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Multiline)
    'Private colortb As String = "{\colortbl ;\red0\green0\blue255;\red0\green128\blue0;}"
    'Private Function blanksearcher(ByVal text As String) As Integer
    '    Dim matchs As Integer
    '    Dim m As System.Text.RegularExpressions.Match = rblnk.Match(text)
    '    While m.Success
    '        matchs = matchs + 1
    '        m = m.NextMatch()
    '    End While
    '    Return matchs
    'End Function

    'Private Sub Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    TridentWebBrowser1.GoBack()

    'End Sub

    'Private Sub forward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    TridentWebBrowser1.GoForward()

    'End Sub

    'Private Sub Refresh_Click()
    '    Refresh_preview()
    'End Sub
    'Private Function AddNew(ByVal strNewItem As String)

    '    Dim lstItm As ListViewItem

    '    For Each lstItm In main.ErrorView.Items

    '        If lstItm.Text = strNewItem Then Return False
    '    Next lstItm

    '    Return True
    'End Function
#End Region
    'Public Sub New()
    '    InitializeComponent()
    '    MsgBox(System.IO.Directory.Exists(My.Application.Info.DirectoryPath & "\xulrunner"))
    '    Xpcom.Initialize(My.Application.Info.DirectoryPath & "\xulrunner")
    'End Sub
#Region "フォーム関連"
    Private Sub editor_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        checksource()
        System.IO.Directory.SetCurrentDirectory(currentpath)
#If DEBUG Then
        If Not dw Is Nothing Then
            dw.Activate()
        End If
#End If

        'blank = blanksearcher(SourceEditor.Text)
    End Sub

    Private Sub editor_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        My.Settings.SplitOrientation = containerorientation
        My.Settings.flipvertical = flipvertical
        My.Settings.Save()
    End Sub


    Private Sub main_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If TextChange = True Then
            If DialogResult.Yes = MessageBox.Show(Me.Text & "を閉じてもよろしいでしょうか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then

            Else
                e.Cancel = True
            End If
        End If
        System.IO.File.Delete(previewpath)
    End Sub

    Private Sub editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ToolStripContainer1.TopToolStripPanel.Join(ToolStrip1, 1)
        CcodeBox.SelectedIndex = 0
        EchangeBox.SelectedItem = "Trident"
        Array.Resize(source, 0)
        Array.Resize(EditingRow, 0)
        Me.Text = Myname
        If Not openpath = "No Title" Then
            Readfile(openpath)
        Else
            System.IO.Directory.SetCurrentDirectory(startpath)
            Me.SourceEditor.Text = defaultsource
            savetexts = SourceEditor.Text
            TextChange = False
            CcodeBox.Enabled = False
            TridentWebBrowser1.DocumentText = defaultsource
        End If
        If containerorientation Then
            SplitContainer1.Orientation = Orientation.Horizontal
            SplitContainer1.SplitterDistance = Me.Height / 2
            verticals.Checked = False
            horizons.Checked = True
        Else
            SplitContainer1.Orientation = Orientation.Vertical
            SplitContainer1.SplitterDistance = Me.Width / 2
            verticals.Checked = True
            horizons.Checked = False
        End If
        If flipvertical = False Then
            reverces.Text = "ブラウザ|エディタ"
            Me.SplitContainer1.Panel1.Controls.Add(TridentWebBrowser1)
            Me.SplitContainer1.Panel2.Controls.Add(SourceEditor)

        Else

            reverces.Text = "エディタ|ブラウザ"
            Me.SplitContainer1.Panel1.Controls.Add(SourceEditor)
            Me.SplitContainer1.Panel2.Controls.Add(TridentWebBrowser1)
        End If
        SourceEditor.Select()
        loadcomplete = True
        main.infobox(My.Resources.dummy, "行：" & countrows(), main.infolabel)
        main.infobox(My.Resources.dummy, Nothing, main.infolabel2)
#If DEBUG Then
        dw = New RTF_View
        dw.RichTextBox1.Text = SourceEditor.Rtf
        dw.Show()
        consoles = New MyConsole
        consoles.write("Hello!")
        consoles.Show()
#End If

    End Sub

#End Region

    Private Sub SourceEditor_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SourceEditor.SelectionChanged
        main.infobox(My.Resources.dummy, "行：" & countrows(), main.infolabel)

    End Sub

    Public Function countrows()
        Dim str As String = SourceEditor.Text

        Dim selectPos As Integer = SourceEditor.SelectionStart


        Dim row As Integer = 1
        Dim startPos As Integer = 0
        Dim endPos As Integer
        While (True)
            endPos = str.IndexOf(vbLf, startPos)
            If (endPos < 0 Or endPos >= selectPos) Then
                Exit While
            End If
            startPos = endPos + 1
            row += 1
        End While

        Dim col As Integer = selectPos - startPos + 1
        cols = col
        rows = row

        Return row
    End Function

    Private Sub infobox(ByVal image As Bitmap, ByVal message As String, ByVal ob As ToolStripLabel)

        ob.Image = image

        ob.Text = message
    End Sub
#Region "プレビュー更新"
    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceEditor.TextChanged
        If norefresh = False Then
            Dim m As System.Text.RegularExpressions.Match = yamakakko.Match(SourceEditor.Text)
            yaziri = 0
            While m.Success
                If m.Value = "<" Then
                    yaziri += 1
                ElseIf m.Value = ">" Then
                    yaziri -= 1
                End If
                m = m.NextMatch()
            End While

            If loadcomplete = True Then
                Timer1.Enabled = True
#If DEBUG Then
                dw.RichTextBox1.Text = SourceEditor.Rtf
#End If

            End If
            Preview_Change()
        Else
            norefresh = False
        End If
    End Sub

    Private Sub Preview_Change()
        'blank_pre = blank
        'blank = blanksearcher(SourceEditor.Text)



        Dim checker As Integer() = checksource()

        If checker(0) = 0 Then
            'If blank = blank_pre Then
            Refresh_preview()
            main.infobox(My.Resources.dummy, Nothing, main.infolabel2)
            main.ErrorView.Items.Clear()
            main.infopanel.Visible = False
            'End If
        ElseIf checker(0) < 0 Then
            Beep()
            main.infobox(My.Resources._error, checker(1) & "個の記述エラー", main.infolabel2)
            main.infopanel.Visible = True
        ElseIf checker(0) >= 1 Then
            If Not rows = changesrow Then
                Refresh_preview()
            End If

        End If
        TextChange = True


    End Sub

    Private Function checksource()
        Dim result As Integer() = {0, 0, 0}
        Dim f2 As Integer = 0
        Dim f3 As Integer = 0
        Dim g As Integer = SourceEditor.Lines.Length
        Dim f As Integer = g - 1


        Dim itemsCount As Integer = main.ErrorView.Items.Count
        main.ErrorView.Items.Clear()

        If g <= 0 Then
            Return result
        End If
        Array.Resize(source, g)
        For i As Integer = 0 To f
            source(i) = (SourceEditor.Lines(i))
        Next

        For i As Integer = 0 To g - 1
            f2 = CountChar(source(i), "<") - CountChar(source(i), ">")

            If f2 > 0 Then
                result(0) = 1
                changesrow = i + 1
            ElseIf f2 < 0 Then '方法を変えたので全行が記述エラー扱いになってる（汗
                result(1) += 1
                _error(0) = "記述エラー"
                _error(1) = Me.Text
                _error(2) = i + 1
                evi = New ListViewItem
                evi.Text = _error(0)
                evi.SubItems.Add(_error(1))
                evi.SubItems.Add(_error(2))
                main.ErrorView.Items.Add(evi)
            End If
        Next
        result(0) = yaziri
        Return result
    End Function



    Public Shared Function CountChar(ByVal s As String, ByVal c As Char) As Integer
        Return s.Length - s.Replace(c.ToString(), "").Length
    End Function

    Private Sub Refresh_preview()
        savetexts = SourceEditor.Text
        If loadcomplete = True Then
            If norefresh = False Then
                If ButtonState = True Then
                    savetexts = System.Text.RegularExpressions.Regex.Replace(savetexts, "<img .*src\s?=\s?""(.*?)"".*?>", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                ElseIf ButtonState2 = True Then
                    savetexts = System.Text.RegularExpressions.Regex.Replace(savetexts, "<img .*src\s?=\s?""(.*?)"".*?", "<img src=" + """" + startpath + "\dummy.png" + """", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                End If

                Select Case EchangeBox.Text
                    Case "Trident"
                        System.IO.File.WriteAllText(previewpath, savetexts, System.Text.Encoding.GetEncoding(CcodeBox.Text))
                        Me.TridentWebBrowser1.Navigate(previewpath)

                    Case "Gecko"
                        'Me.GeckoWebBrowser1.Document.DocumentElement.InnerHtml = RichTextBox1.Text
                    Case "Webkit"
                        'Me.WebKitBrowser1.DocumentText = RichTextBox1.Text
                End Select
            Else
                norefresh = False
            End If
        End If
    End Sub
#End Region
#Region "ツールバー関連"
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton.Click
        main.editor_load("No Title")
    End Sub

    Private Sub DummyToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DummyToggle.Click

        If ButtonState = True Then '画像の非表示ボタンがオンの場合オフに変える動作
            ButtonState = False
            DummyToggle.Text = "オフ"
            DummyToggle.ForeColor = Color.Black
            DummyToggle.BackColor = Nothing
        Else '画像の非表示ボタンがオフの場合オンに変える動作
            ButtonState = True
            DummyToggle.Text = "オン"
            DummyToggle.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(Red, Green, Blue))
            DummyToggle.ForeColor = Color.White
            If ButtonState2 = True Then '画像の非表示ボタンがオンの場合ダミーに差し替えスイッチをオフに変える動作
                ButtonState2 = False
                DummyToggle2.Text = "オフ"
                DummyToggle2.ForeColor = Color.Black
                DummyToggle2.BackColor = Nothing
            End If
        End If
        Preview_Change()
    End Sub

    Private Sub DummyToggle2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DummyToggle2.Click
        'DummyToggle_Clickと挙動はほぼ同じ
        If ButtonState2 = True Then
            ButtonState2 = False
            DummyToggle2.Text = "オフ"
            DummyToggle2.ForeColor = Color.Black
            DummyToggle2.BackColor = Nothing
        Else
            ButtonState2 = True
            DummyToggle2.Text = "オン"
            DummyToggle2.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(Red, Green, Blue))
            DummyToggle2.ForeColor = Color.White
            If ButtonState = True Then
                ButtonState = False
                DummyToggle.Text = "オフ"
                DummyToggle.ForeColor = Color.Black
                DummyToggle.BackColor = Nothing
            End If
        End If
        Preview_Change()
    End Sub

    Private Sub texxtsizebox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textsizebox.TextChanged
        textsize = textsizebox.Text
        updatetags("font", "size")
    End Sub


    Private Sub textsizecombbbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSizeCombbox.TextChanged
        textsize = textsizebox.Text
        updatetags("font", "size")
    End Sub


    Private Sub ToolStripComboBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CcodeBox.TextChanged '文字コード変更
        If Not SourceEditor.Text = String.Empty And Not Me.Text = "No Title" Then '編集済みテキストがあるか？

            If DialogResult.Yes = MessageBox.Show("変更を破棄し文字コードを変更しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                Dim sr As New System.IO.StreamReader(openpath, System.Text.Encoding.GetEncoding(CcodeBox.Text))
                Dim s As String = sr.ReadToEnd()
                sr.Close()
                Me.SourceEditor.Text = s
            Else
                CcodeBox.Text = afterCharacterCode
                Exit Sub
            End If
        End If
        TextChange = False
    End Sub

    Private Sub ToolStripComboBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EchangeBox.TextChanged
        'レンダリングエンジン切り替え
        Select Case EchangeBox.Text
            Case "Trident"
                TridentWebBrowser1.Visible = True
                Refresh_preview()
            Case "Gecko"
                callEngine(1)
                Refresh_preview()
            Case "Webkit"
                'GeckoWebBrowser1.Visible = False
                TridentWebBrowser1.Visible = False
                Refresh_preview()
        End Select
    End Sub


    Private Sub verticals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles verticals.Click

        SplitContainer1.Orientation = Orientation.Vertical
        containerorientation = False
        SplitContainer1.SplitterDistance = Me.Width / 2
        verticals.Checked = True
        horizons.Checked = False

    End Sub

    Private Sub horizons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles horizons.Click
        SplitContainer1.Orientation = Orientation.Horizontal
        containerorientation = True
        SplitContainer1.SplitterDistance = Me.Height / 2
        verticals.Checked = False
        horizons.Checked = True
    End Sub

    Private Sub reverces_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reverces.Click
        If flipvertical = False Then
            reverces.Text = "エディタ|ブラウザ"
            Me.SplitContainer1.Panel1.Controls.Add(SourceEditor)
            Me.SplitContainer1.Panel2.Controls.Add(TridentWebBrowser1)
            flipvertical = True
        Else
            reverces.Text = "ブラウザ|エディタ"
            Me.SplitContainer1.Panel1.Controls.Add(TridentWebBrowser1)
            Me.SplitContainer1.Panel2.Controls.Add(SourceEditor)
            flipvertical = False
        End If

    End Sub

#End Region
#Region "ファイル操作"
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenButton.Click
        openfile()
    End Sub

    Private Sub openfile()
        Dim openfiledialog As New OpenFileDialog
        Dim ed As New editor
        openfiledialog.Filter = ("HTMLファイル|*.html;*.htm|テキストファイル|*.txt|すべてのファイル|*.*")
        openfiledialog.AddExtension = True
        If DialogResult.OK = openfiledialog.ShowDialog() Then
            Readfile(openfiledialog.FileName)
        End If
    End Sub

    Private Sub Readfile(ByVal filepath As String)
        Dim sr As New System.IO.StreamReader(filepath, System.Text.Encoding.GetEncoding(CcodeBox.Text))
        openpath = filepath
        Dim s As String = sr.ReadToEnd()
        currentpath = System.IO.Path.GetDirectoryName(filepath)
        System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(currentpath))
        previewpath = currentpath + "\pre.html"
        savepath = filepath
        savetexts = s
        loadcomplete = True
        Me.SourceEditor.Text = s
        Me.Text = System.IO.Path.GetFileName(filepath)
        TextChange = False
        CcodeBox.Enabled = True
        sr.Close()
    End Sub

    Private Sub TextSave(ByVal value As String)
        If Not Me.Text = "No Title" Then
            System.IO.File.WriteAllText(value, SourceEditor.Text, System.Text.Encoding.GetEncoding(CcodeBox.Text))
            TextChange = False
        Else
            Dim savefiledialog1 As New SaveFileDialog
            savefiledialog1.FileName = System.IO.Path.GetFileName(SaveText)
            savefiledialog1.Filter = ("HTMLファイル|*.html;*.htm|テキストファイル|*.txt|すべてのファイル|*.*")
            If DialogResult.OK = savefiledialog1.ShowDialog() Then
                SaveFile(savefiledialog1.FileName)
                Me.Text = savefiledialog1.FileName
                TextChange = False
                currentpath = System.IO.Path.GetDirectoryName(savefiledialog1.FileName)
            End If
        End If
    End Sub
    Private Sub TextSaveAs()
        Dim savefiledialog1 As New SaveFileDialog
        savefiledialog1.FileName = System.IO.Path.GetFileName(SaveText)
        savefiledialog1.Filter = ("HTMLファイル|*.html;*.htm|テキストファイル|*.txt|すべてのファイル|*.*")
        If DialogResult.OK = savefiledialog1.ShowDialog() Then
            SaveFile(savefiledialog1.FileName)
            Me.Text = savefiledialog1.FileName
            TextChange = False
            currentpath = System.IO.Path.GetDirectoryName(savefiledialog1.FileName)
        End If
    End Sub
    Private Sub SaveFile(ByVal value As String)
        System.IO.File.WriteAllText(value, SourceEditor.Text, System.Text.Encoding.GetEncoding(CcodeBox.Text))
    End Sub

#End Region
#Region "切り取りその他編集関連"
    Private Sub undoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles undoToolStripMenuItem.Click
        undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        redo()
    End Sub

    Private Sub cutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cutToolStripMenuItem.Click
        cut()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        copy()
    End Sub

    Private Sub pasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pasteToolStripMenuItem.Click
        paste()
    End Sub

    Private Sub delToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delToolStripMenuItem.Click
        del()
    End Sub

    Private Sub allselectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles allselectToolStripMenuItem.Click
        allselect()
    End Sub

    Private Sub cut()
        If SourceEditor.SelectionLength > 0 Then
            SourceEditor.Cut()
        End If
    End Sub
    Private Sub copy()
        If SourceEditor.SelectionLength > 0 Then
            SourceEditor.Copy()
        End If
    End Sub
    Private Sub paste()
        If Clipboard.GetDataObject(). _
            GetDataPresent(DataFormats.Text) = True Then
            SourceEditor.Paste()
        End If
    End Sub
    Private Sub del()
        If SourceEditor.SelectionLength > 0 Then
            SourceEditor.SelectedText = ""
        End If
    End Sub
    Private Sub allselect()
        SourceEditor.Focus()
        SourceEditor.SelectAll()
    End Sub
    Private Sub undo()
        SourceEditor.Focus()
        SourceEditor.Undo()
    End Sub

    Private Sub redo()
        SourceEditor.Focus()
        SourceEditor.Redo()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click

    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        TextSave(savepath)
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsBuuton.Click
        TextSaveAs()
    End Sub

    Private Sub ToolStripComboBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CcodeBox.Click

    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        main.editor_load("No Title")
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        openfile()
    End Sub

    Private Sub ToolStripDropDownButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuButton.Click

    End Sub

    Private Sub CcodeBox_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CcodeBox.DropDown
        afterCharacterCode = CcodeBox.SelectedItem
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If SourceEditor.SelectionLength > 0 Then
            cutToolStripMenuItem.Enabled = True
            CopyToolStripMenuItem.Enabled = True
            delToolStripMenuItem.Enabled = True
            Ctextcolor.Enabled = True
            Ctextsize.Enabled = True
            Dselecttext.Enabled = True

        Else
            cutToolStripMenuItem.Enabled = False
            CopyToolStripMenuItem.Enabled = False
            delToolStripMenuItem.Enabled = False
            Ctextcolor.Enabled = False
            Ctextsize.Enabled = False
            Dselecttext.Enabled = False
        End If
    End Sub
#End Region
#Region "HTMLタグ生成・その他関連"
    Private Sub NCselct(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer, Optional ByVal codeonly As Integer = 0)
        Dim red As String
        Dim green As String
        Dim blue As String
        red = r.ToString("x2")
        Console.WriteLine(red.ToString)
        green = g.ToString("x2")
        Console.WriteLine(green.ToString)
        blue = b.ToString("x2")
        Console.WriteLine(blue.ToString)

        colorcode = "#" + red + green + blue
        Console.WriteLine(colorcode)
        If codeonly = 1 Then
            updatetags("code", "color")
        Else
            updatetags("font", "color")
        End If
    End Sub

    Private Sub CreateHyperLinks()
        Dim mc As System.Text.RegularExpressions.MatchCollection = _
      System.Text.RegularExpressions.Regex.Matches( _
          SourceEditor.SelectedText, _
          "<a\s+[^>]*href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|" + _
              "(?<url>[^\s>]+))[^>]*>(?<text>.*?)</a>", _
          System.Text.RegularExpressions.RegexOptions.IgnoreCase Or _
          System.Text.RegularExpressions.RegexOptions.Singleline)
        Dim d As New dialog
        d.Text = "リンクの挿入"
        For Each m As System.Text.RegularExpressions.Match In mc
            d.TextBox1.Text = m.Groups("url").Value
            d.TextBox2.Text = m.Groups("text").Value
        Next
        If mc.Count <= 0 Then
            Dim r As New System.Text.RegularExpressions.Regex( _
            "href\s*=\s*(?:(?<quot>[""'])(?<url>.*?)\k<quot>|(?<url>[^\s>]+))[^>]*", _
            System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            Dim m As System.Text.RegularExpressions.Match = r.Match(SourceEditor.SelectedText)
            d.TextBox1.Text = m.Groups("url").Value
            If m.Length <= 0 Then
                d.TextBox2.Text = SourceEditor.SelectedText
            End If
        End If
        If d.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            updatetags("HyperLink", d.TextBox1.Text, d.TextBox2.Text)
        End If
    End Sub


    Private Sub implementpict()
        Dim dialog As New OpenFileDialog
        Dim filepath As String = String.Empty
        Dim filename As String = String.Empty
        Dim picwidth As Integer = 0
        Dim pictheight As Integer = 0

        dialog.Filter = "画像ファイル|*.jpg;*.jpeg;*.png;*.gif|JPEGファイル(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIFファイル(*.gif)|*.gif|PNGファイル(*.png)|*.png|すべてのファイル(*.*)|*.*"
        dialog.Title = "挿入する画像を選択してください"
        If dialog.ShowDialog = DialogResult.OK Then
            filepath = dialog.FileName
            filename = System.IO.Path.GetFileName(filepath)
            picwidth = System.Drawing.Image.FromFile(filepath).Width
            pictheight = System.Drawing.Image.FromFile(filepath).Height
            If filepath.IndexOf(currentpath) > 0 Then
                SourceEditor.SelectedText = "<img src=" + """" + filepath + """" + " width=" + picwidth.ToString + " height=" + pictheight.ToString + " alt=" + filename + ">"
            Else
                If System.IO.Directory.Exists(currentpath + "\images") Then
                Else
                    System.IO.Directory.CreateDirectory(currentpath + "\images")
                End If
                Console.WriteLine(System.IO.File.Exists(currentpath + "\images\" + filename))
                If System.IO.File.Exists(currentpath + "\images\" + filename) Then
                Else
                    System.IO.File.Copy(filepath, currentpath + "\images\" + filename)
                End If
                SourceEditor.SelectedText = "<img src=" + """" + "images\" + filename + """" + " width=" + picwidth.ToString + " height=" + pictheight.ToString + " alt=" + filename + ">"
            End If

        End If
    End Sub

    Private Sub updatetags(ByVal tags As String, ByVal op As String, Optional ByVal op2 As String = Nothing)
        Dim selecttext As String = SourceEditor.SelectedText
        Select Case tags
            Case "font"
                If selecttext.IndexOf("font", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    Select Case op
                        Case "color"
                            If selecttext.IndexOf("color", StringComparison.OrdinalIgnoreCase) >= 0 Then
                                selecttext = System.Text.RegularExpressions.Regex.Replace(selecttext, "color\s?=\s?""(.*?)"".*?", "color=""" + colorcode.ToString + """", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            Else
                                selecttext = System.Text.RegularExpressions.Regex.Replace(selecttext, "<font", "<font color=""" + colorcode.ToString + """", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                            SourceEditor.SelectedText = selecttext
                        Case "size"
                            If selecttext.IndexOf("size", StringComparison.OrdinalIgnoreCase) >= 0 Then
                                selecttext = System.Text.RegularExpressions.Regex.Replace(selecttext, "size\s?=\s?""(.*?)"".*?", "size=""" + textsize.ToString + """", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            Else
                                selecttext = System.Text.RegularExpressions.Regex.Replace(selecttext, "<font", "<font size=""" + textsize.ToString + """", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                            End If
                            SourceEditor.SelectedText = selecttext

                    End Select
                Else
                    Select Case op
                        Case "color"
                            SourceEditor.SelectedText = "<font color = """ + colorcode + """>" + SourceEditor.SelectedText + "</font>"
                        Case "size"
                            SourceEditor.SelectedText = "<font size = """ + textsize.ToString + """>" + SourceEditor.SelectedText + "</font>"
                    End Select
                End If
            Case "bold"
                selecttext = "<b>" + selecttext + "</b>"
                SourceEditor.SelectedText = selecttext
            Case "skew"
                selecttext = "<i>" + selecttext + "</i>"
                SourceEditor.SelectedText = selecttext
            Case "under"
                selecttext = "<u>" + selecttext + "</u>"
                SourceEditor.SelectedText = selecttext
            Case "delete"
                selecttext = "<s>" + selecttext + "</s>"
                SourceEditor.SelectedText = selecttext
            Case "HyperLink"
                SourceEditor.SelectedText = "<a href = """ + op + """>" + op2 + "</a>"
            Case "code"
                SourceEditor.SelectedText = colorcode
        End Select


    End Sub
#End Region

#Region "コンテキストメニュー関連"
    Private Sub Ctextcolor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ctextcolor.Click
        '選択文字列の色を変更
        If ColorDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            NCselct(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
        End If
    End Sub

    Private Sub ToolStripMenuItem14_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        implementpict()
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        CreateHyperLinks()
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        updatetags("bold", "null")
    End Sub

    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem18.Click
        updatetags("under", "null")
    End Sub

    Private Sub ToolStripMenuItemd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemd.Click
        updatetags("delete", "null")
    End Sub

    Private Sub ToolStripMenuItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItems.Click
        updatetags("skew", "null")
    End Sub
#End Region



    Private Sub SourceEditor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SourceEditor.KeyDown
        If e.KeyCode = Keys.Enter Then
            norefresh = True
        End If
        If e.KeyCode = Keys.F5 Then
            Refresh_preview()
        End If
    End Sub


    ''Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    ''    norefresh = True
    ''    If System.Text.RegularExpressions.Regex.IsMatch(SourceEditor.Text, "<.*?>", System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
    ''        SetTagColor(SourceEditor, Color.Blue)
    ''        Timer1.Enabled = False
    ''    End If

    'End Sub

    Private Sub SetTagColor2()
        norefresh = True
        Dim source As String = SourceEditor.Text
        Dim m As New System.Text.RegularExpressions.Regex("<.*?>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim m2 As System.Text.RegularExpressions.Match = m.Match(source)
        While m2.Success
            source += m2.Value
            m2 = m2.NextMatch
        End While
        consoles.write(source)
        Dim d As New System.Text.RegularExpressions.Regex(""".*?""", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim d2 As System.Text.RegularExpressions.Match = d.Match(source)
        While d2.Success
            norefresh = True
            SetSelectionColor(SourceEditor, d2.Value, Color.Green)
            d2 = d2.NextMatch
        End While
        consoles.write(source)
    End Sub

    Private Sub editor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown, SplitContainer1.KeyDown
#If DEBUG Then
        If e.KeyCode = Keys.D AndAlso e.Control Then
            SetTagColor2()
        End If
#End If
    End Sub

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click, ToolStripButton1.Click
        If ColorDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            NCselct(ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B, 1)
        End If
    End Sub

    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem20.Click

    End Sub
    Private GeckoView As GeckoWebBrowser
    Private Sub callEngine(ByVal engine As Integer)

        If engine = 0 Then

        ElseIf engine = 1 Then
            TridentWebBrowser1.Dispose()
            Me.GeckoView = New GeckoWebBrowser()

            'Buttonコントロールのプロパティを設定する
            Me.GeckoView.Name = "GeckoView"
            Me.GeckoView.Navigate(previewpath)
            'サイズと位置を設定する
            Me.GeckoView.Dock = DockStyle.Fill

            'フォームに追加する
            Me.Controls.Add(Me.GeckoView)
            Me.SplitContainer1.Panel1.Controls.Add(Me.GeckoView)
        ElseIf engine = 2 Then

        End If
    End Sub
End Class