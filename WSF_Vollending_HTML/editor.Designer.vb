<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class editor
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.MenuButton = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
        Me.NewButton = New System.Windows.Forms.ToolStripButton
        Me.OpenButton = New System.Windows.Forms.ToolStripButton
        Me.SaveButton = New System.Windows.Forms.ToolStripButton
        Me.SaveAsBuuton = New System.Windows.Forms.ToolStripButton
        Me.Search = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CcodeBox = New System.Windows.Forms.ToolStripComboBox
        Me.EchangeBox = New System.Windows.Forms.ToolStripComboBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TridentWebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.SourceEditor = New System.Windows.Forms.RichTextBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.undoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.delToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.allselectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuButton, Me.NewButton, Me.OpenButton, Me.SaveButton, Me.SaveAsBuuton, Me.Search, Me.ToolStripSeparator1, Me.CcodeBox, Me.EchangeBox})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(933, 26)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'MenuButton
        '
        Me.MenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripMenuItem8, Me.ToolStripMenuItem11})
        Me.MenuButton.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.menu
        Me.MenuButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuButton.Name = "MenuButton"
        Me.MenuButton.Size = New System.Drawing.Size(29, 23)
        Me.MenuButton.Text = "ToolStripDropDownButton1"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.ShortcutKeyDisplayString = "Ctrl+N"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem2.Text = "新規(&N)"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.ShortcutKeyDisplayString = "Ctrl+O"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem3.Text = "開く(&O)"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem12, Me.ToolStripMenuItem13})
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem4.Text = "文字コードを指定して開く"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripMenuItem1.Text = "Shift-JIS"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripMenuItem12.Text = "EUC-JP"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(125, 22)
        Me.ToolStripMenuItem13.Text = "UTF-8"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.ShortcutKeyDisplayString = "Ctrl+S"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem5.Text = "上書き保存(&S)"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.ShortcutKeyDisplayString = "Shift+Ctrl+S"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem6.Text = "名前を付けて保存(&A)"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem7.Text = "文字コードを指定して保存"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem9, Me.ToolStripMenuItem10})
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.ShortcutKeyDisplayString = "Ctrl+P"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem8.Text = "印刷"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem9.Text = "プレビューブラウザ"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem10.Text = "エディター"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.ShortcutKeyDisplayString = "Ctrl+W"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(274, 22)
        Me.ToolStripMenuItem11.Text = "終了(&X)"
        '
        'NewButton
        '
        Me.NewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewButton.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.file_new
        Me.NewButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(23, 23)
        Me.NewButton.Text = "新規"
        '
        'OpenButton
        '
        Me.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenButton.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.file_open
        Me.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(23, 23)
        Me.OpenButton.Text = "開く"
        '
        'SaveButton
        '
        Me.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveButton.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.file_save
        Me.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(23, 23)
        Me.SaveButton.Text = "上書き保存"
        '
        'SaveAsBuuton
        '
        Me.SaveAsBuuton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveAsBuuton.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.file_saveas
        Me.SaveAsBuuton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveAsBuuton.Name = "SaveAsBuuton"
        Me.SaveAsBuuton.Size = New System.Drawing.Size(23, 23)
        Me.SaveAsBuuton.Text = "保存"
        '
        'Search
        '
        Me.Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Search.Image = Global.WSF_Vollending_HTML.My.Resources.Resources.search11
        Me.Search.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(23, 23)
        Me.Search.Text = "ToolStripButton5"
        Me.Search.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
        '
        'CcodeBox
        '
        Me.CcodeBox.Items.AddRange(New Object() {"Shift-JIS", "EUC-JP", "UTF-8"})
        Me.CcodeBox.Name = "CcodeBox"
        Me.CcodeBox.Size = New System.Drawing.Size(121, 26)
        '
        'EchangeBox
        '
        Me.EchangeBox.Items.AddRange(New Object() {"Gecko", "Trident"})
        Me.EchangeBox.Name = "EchangeBox"
        Me.EchangeBox.Size = New System.Drawing.Size(121, 26)
        Me.EchangeBox.Sorted = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 26)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TridentWebBrowser1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SourceEditor)
        Me.SplitContainer1.Size = New System.Drawing.Size(933, 623)
        Me.SplitContainer1.SplitterDistance = 484
        Me.SplitContainer1.TabIndex = 1
        '
        'TridentWebBrowser1
        '
        Me.TridentWebBrowser1.AllowWebBrowserDrop = False
        Me.TridentWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TridentWebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.TridentWebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.TridentWebBrowser1.Name = "TridentWebBrowser1"
        Me.TridentWebBrowser1.ScriptErrorsSuppressed = True
        Me.TridentWebBrowser1.Size = New System.Drawing.Size(484, 623)
        Me.TridentWebBrowser1.TabIndex = 0
        '
        'SourceEditor
        '
        Me.SourceEditor.ContextMenuStrip = Me.ContextMenuStrip1
        Me.SourceEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SourceEditor.Location = New System.Drawing.Point(0, 0)
        Me.SourceEditor.Name = "SourceEditor"
        Me.SourceEditor.Size = New System.Drawing.Size(445, 623)
        Me.SourceEditor.TabIndex = 1
        Me.SourceEditor.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.undoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.ToolStripSeparator2, Me.cutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.pasteToolStripMenuItem, Me.delToolStripMenuItem, Me.ToolStripSeparator3, Me.allselectToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(144, 170)
        '
        'undoToolStripMenuItem
        '
        Me.undoToolStripMenuItem.Name = "undoToolStripMenuItem"
        Me.undoToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.undoToolStripMenuItem.Text = "元に戻る(&U)"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.RedoToolStripMenuItem.Text = "やり直す(&N)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(140, 6)
        '
        'cutToolStripMenuItem
        '
        Me.cutToolStripMenuItem.Enabled = False
        Me.cutToolStripMenuItem.Name = "cutToolStripMenuItem"
        Me.cutToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.cutToolStripMenuItem.Text = "切り取り(&T)"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Enabled = False
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.CopyToolStripMenuItem.Text = "コピー(&C)"
        '
        'pasteToolStripMenuItem
        '
        Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
        Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.pasteToolStripMenuItem.Text = "貼り付け(&P)"
        '
        'delToolStripMenuItem
        '
        Me.delToolStripMenuItem.Enabled = False
        Me.delToolStripMenuItem.Name = "delToolStripMenuItem"
        Me.delToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.delToolStripMenuItem.Text = "削除(&D)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(140, 6)
        '
        'allselectToolStripMenuItem
        '
        Me.allselectToolStripMenuItem.Name = "allselectToolStripMenuItem"
        Me.allselectToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.allselectToolStripMenuItem.Text = "全選択(&A)"
        '
        'editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 649)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "editor"
        Me.Text = "editor"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents NewButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsBuuton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CcodeBox As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents EchangeBox As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TridentWebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents SourceEditor As System.Windows.Forms.RichTextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents undoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents delToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents allselectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Search As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
End Class
