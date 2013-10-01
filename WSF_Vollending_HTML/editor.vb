Public Class editor
    Friend openpath As String
    Friend Myname As String
    Friend TextChange As Boolean = False
    Private SaveText As String
    Private previewpath As String = System.IO.Directory.GetCurrentDirectory + "\pre.html"
    Private startpath = My.Application.Info.DirectoryPath
    Private afterCharacterCode As String
    Private NowRows As String
    Private source As String()
    Private loadcomplete As Boolean = False
    Private savetexts As String
    Private errorwindow As New ArrayList
    Private errorlines As New ArrayList(New Integer)
    Private _error As String() = {"", "", ""}
    Dim evi As ListViewItem

    Private Sub editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CcodeBox.SelectedIndex = 0
        EchangeBox.SelectedItem = "Trident"
        Array.Resize(source, 0)
        Me.Text = Myname
        If Not openpath = "No Title" Then
            Dim sr As New System.IO.StreamReader(openpath, _
        System.Text.Encoding.GetEncoding(CcodeBox.Text))
            Dim s As String = sr.ReadToEnd()
            sr.Close()
            savetexts = s
            Me.SourceEditor.Text = s
            TextChange = False
        Else
            Me.SourceEditor.Text = "<!DOCTYPE HTML PUBLIC " & "-//W3C//DTD HTML 4.01 Transitional//EN" & "http://www.w3.org/TR/html4/loose.dtd" & ">" & vbCrLf & "<HTML>" & vbCrLf & "<HEAD>" & vbCrLf & "<TITLE>ここにホームページのタイトルを入力します</TITLE>" & vbCrLf & "<META http-equiv=" & "Content-Type" & "content=" & "text/html;" & " charset=shift_jis" & ">" & vbCrLf & "</HEAD>" & vbCrLf & vbCrLf & vbCrLf & "<BODY>" & vbCrLf & vbCrLf & vbCrLf & "</BODY>" & vbCrLf & vbCrLf & vbCrLf & "</HTML>"
            savetexts = SourceEditor.Text
            TextChange = False
            CcodeBox.Enabled = False
        End If
        loadcomplete = True
        main.infobox(My.Resources.dummy, "行：" & countrows(), main.infolabel)
    End Sub

    Private Sub SourceEditor_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SourceEditor.SelectionChanged
        main.infobox(My.Resources.dummy, "行：" & countrows(), main.infolabel)

    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceEditor.TextChanged
        If loadcomplete = True Then
            savetexts = SourceEditor.Text
            Dim checker As Integer() = checksource()
            If checker(0) = 0 Then
                Refresh_preview()
                main.infobox(My.Resources.dummy, Nothing, main.infolabel2)
                main.ErrorView.Items.Clear()
            ElseIf checker(0) < 0 Then

                Beep()
                main.infobox(My.Resources._error, checker(1) & "個の記述エラー", main.infolabel2)
                main.infopanel.Visible = True
            End If
            TextChange = True
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If SourceEditor.SelectionLength > 0 Then
            cutToolStripMenuItem.Enabled = True
            CopyToolStripMenuItem.Enabled = True
            delToolStripMenuItem.Enabled = True
        Else
            cutToolStripMenuItem.Enabled = False
            CopyToolStripMenuItem.Enabled = False
            delToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub Refresh_preview()
        If loadcomplete = True Then
            savetexts = System.Text.RegularExpressions.Regex.Replace(savetexts, "<img .*src=""(.*?)"".*?", "<img src=dummy.png ")
        End If
        Select Case EchangeBox.Text
            Case "Trident"
                System.IO.File.WriteAllText(previewpath, savetexts, System.Text.Encoding.GetEncoding(CcodeBox.Text))

                Me.TridentWebBrowser1.Navigate(previewpath)
            Case "Gecko"
                ' Me.GeckoWebBrowser1.Document.DocumentElement.InnerHtml = RichTextBox1.Text
            Case "Webkit"
                '    Me.WebKitBrowser1.DocumentText = RichTextBox1.Text
        End Select

    End Sub

    Private Sub infobox(ByVal image As Bitmap, ByVal message As String, ByVal ob As ToolStripLabel)

        ob.Image = image

        ob.Text = message
    End Sub

    Private Sub ToolStripComboBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CcodeBox.TextChanged

        If Not SourceEditor.Text = "" Then
            If Not Me.Text = "No Title" Then
                If DialogResult.Yes = MessageBox.Show("変更を破棄し文字コードを変更しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                    Dim sr As New System.IO.StreamReader(openpath, _
               System.Text.Encoding.GetEncoding(CcodeBox.Text))
                    Dim s As String = sr.ReadToEnd()
                    sr.Close()
                    Me.SourceEditor.Text = s
                Else
                    CcodeBox.Text = afterCharacterCode
                    Exit Sub
                End If
            End If
        End If
        TextChange = False
    End Sub

    Private Sub ToolStripComboBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EchangeBox.TextChanged
        Select Case EchangeBox.Text
            Case "Trident"
                'GeckoWebBrowser1.Visible = False
                TridentWebBrowser1.Visible = True
                Refresh_preview()
            Case "Gecko"
                TridentWebBrowser1.Visible = False

                Refresh_preview()
            Case "Webkit"
                'GeckoWebBrowser1.Visible = False
                TridentWebBrowser1.Visible = False
                Refresh_preview()
        End Select
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton.Click
        main.editor_load("No Title")
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenButton.Click
        openfile()
    End Sub
    Private Sub openfile()
        Dim openfiledialog As New OpenFileDialog
        Dim ed As New editor
        openfiledialog.Filter = ("HTMLファイル|*.html;*.htm|テキストファイル|*.txt|すべてのファイル|*.*")
        openfiledialog.AddExtension = True
        If DialogResult.OK = openfiledialog.ShowDialog() Then
            Dim sr As New System.IO.StreamReader(openfiledialog.FileName, _
        System.Text.Encoding.GetEncoding(CcodeBox.Text))
            openpath = openfiledialog.FileName
            Dim s As String = sr.ReadToEnd()
            sr.Close()
            System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(openfiledialog.FileName))
            previewpath = System.IO.Directory.GetCurrentDirectory + "\pre.html"
            savetexts = s
            Me.SourceEditor.Text = s

            Me.Text = openfiledialog.FileName
            TextChange = False
            CcodeBox.Enabled = True
        End If
    End Sub

    Private Sub TextSave(ByVal value As String)
        If Not Me.Text = "No Title" Then
            System.IO.File.WriteAllText(value, SourceEditor.Text, System.Text.Encoding.GetEncoding(CcodeBox.Text))
            TextChange = False
        Else
            TextSaveAs()
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
        End If
    End Sub
    Private Sub SaveFile(ByVal value As String)
        System.IO.File.WriteAllText(value, SourceEditor.Text, System.Text.Encoding.GetEncoding(CcodeBox.Text))
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
        TextSave(Me.Text)
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

    Private Function checksource()
        Dim result As Integer() = {0, 0, 0}
        Dim f2 As Integer = 0
        Dim g As Integer = SourceEditor.Lines.Length
        Dim f = g - 1

        Dim itemsCount As Integer = main.ErrorView.Items.Count
        main.ErrorView.Items.Clear()

        If g <= 0 Then
            Return 0
        End If
        Array.Resize(source, g)
        For i As Integer = 0 To f
            source(i) = (SourceEditor.Lines(i))
        Next

        For i As Integer = 0 To g - 1
            f2 = CountChar(source(i), "<") - CountChar(source(i), ">")

            If f2 > 0 Then
                result(0) = 1

            ElseIf f2 < 0 Then
                result(1) += 1
                result(0) = -1

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


        Return result
    End Function

    Public Shared Function CountChar(ByVal s As String, ByVal c As Char) As Integer
        Return s.Length - s.Replace(c.ToString(), "").Length
    End Function

    Private Function AddNew(ByVal strNewItem As String)

        Dim lstItm As ListViewItem

        For Each lstItm In main.ErrorView.Items

            If lstItm.Text = strNewItem Then Return False
        Next lstItm

        Return True
    End Function

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
        Return row
    End Function
End Class