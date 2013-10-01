
Imports System.Text
Imports System
Imports System.Object

Module ModuleRichText

    'カラーテーブルの最初の文字
    Const strColorHeder As String = "{\colortbl"

    '***********************************************************
    '        タグの色変更
    '***********************************************************
    Public Sub SetTagColor(ByRef RichText As System.Windows.Forms.RichTextBox, _
                            ByVal objColor As Object)
        '--------------------------------------------------------
        '引数：
        '第1引数---RichTextBox
        '第2引数---カラークラス又はRGB値が順番に入ったバイト配列
        '動作:
        '第2引数のRichTextBoxのRtfの中のタグを検索して色を付ける
        '--------------------------------------------------------

        Try
            'Rtfのコピーを作成
            'Rtfのコピー上で作業して最後に書き換える
            Dim strRtf As String = RichText.Rtf

            'カラーテーブルに色を追加する
            'AddColorはカラーテーブルを返す
            Dim strNeWCtbl As String = AddColor(strRtf, objColor)

            '新しいカラーテーブルを書き換える
            If strNeWCtbl <> String.Empty Then
                strRtf = ChangeCtbl(strRtf, strNeWCtbl)
            End If

            '追加した色の番号の文字列を取得する(例：\cf2
            Dim strColorNo As String = getColorNo(strRtf, objColor)

            'タグの先頭を置き換える文字作成
            Dim strColorNoString As String = strColorNo + "<"

            '既に設定してあるカラー文字を待避する
            strRtf = strRtf.Replace("\cf<", Chr(2))

            'カラー文字を挿入する
            strRtf = strRtf.Replace("<", strColorNoString)

            'タグの最後を置き換える文字、色を戻す
            strRtf = strRtf.Replace(">", ">\cf0")

            '待避していたカラー文字を元に戻す
            strRtf = strRtf.Replace(Chr(2), "\cf<")

            'Rtfを置き換える
            RichText.Rtf = strRtf
        Catch ex As Exception
            'エラー発生
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SetTagColor")
        End Try
    End Sub

    '***************************************************
    '文字を検索して色を変更する
    '***************************************************
    Public Sub SetSelectionColor(ByRef RichText As System.Windows.Forms.RichTextBox, _
                                ByVal strSelectString As String, _
                                 ByVal objColor As Object)
        '--------------------------------------------------------
        '引数:
        '第1引数---RichTextBox
        '第2引数(---検索文字)
        '第3引数(---設定色)
        '動作:
        '第2引数のRichTextBoxのRtfの中から、第3引数の文字を
        '検索して、カラーコードを付加し、結果的にTextに色を付ける
        '--------------------------------------------------------

        'Rtfのコピーを作成
        'Rtfのコピー上で作業して最後に書き換える
        Dim strRtf As String = RichText.Rtf

        'カラーテーブルに色を追加する
        'AddColorはカラーテーブルを返す
        Dim strNeWCtbl As String = AddColor(strRtf, objColor)

        'カラーテーブルを置き換える
        If strNeWCtbl <> String.Empty Then
            strRtf = ChangeCtbl(strRtf, strNeWCtbl)
        End If

        '追加した色の番号を取得する
        Dim strColorNo As String = getColorNo(strRtf, objColor)

        'カラー番号文字は最後が空白(次に続く文字が\の場合は空白はいらない)
        strColorNo = strColorNo & " "

        '色づけする文字をRtfの文字列に変換する
        Dim strSearch As String = getEncodeString(strSelectString)
        Dim intStart As Integer = 0
        Dim strColor As String = String.Empty
        Do
            '文字列の最後に来たら抜ける
            If intStart <= strRtf.Length Then
                '文字を探す
                intStart = strRtf.IndexOf(strSearch, intStart)
                If intStart <> -1 Then
                    '現在の色を取得する
                    strColor = getCurrentColor(strRtf, intStart)
                    '先に終了の色をセットする
                    strRtf = strRtf.Insert(intStart + strSearch.Length, strColor)
                    '色の文字列を挿入する
                    strRtf = strRtf.Insert(intStart, strColorNo)
                    '次の文字列の検索開始番号
                    intStart += strColor.Length + strSearch.Length + strColor.Length
                    '中止を受け付ける
                    Application.DoEvents()
                End If
            Else
                Exit Do
            End If
        Loop While (intStart <> -1)
        '新しいRtfを設定する
        RichText.Rtf = strRtf

    End Sub

    '********************************************************
    'カラーテーブルに新しいカラー要素を加える
    '********************************************************
    Public Function AddColor(ByRef strRtf As String, _
                             ByVal objColor As Object) As String
        '-------------------------------------------------------
        '引数：
        '第１引数---カラーテーブルを含む文字列
        '第２引数---Colorクラス又はRGB順のバイト配列
        '動作：
        '指定の色を加えたカラーテーブルを返す
        'カラーテーブルが無い場合は指定の色でカラーテーブルを作成する
        '既にカラー要素が存在する場合は現在のカラーテーブルを返す
        'エラーの場合は空白を返す
        '-------------------------------------------------------

        '指定された色を、カラーテーブルの色の形に変換する
        Dim strColor As String = GetColorString(objColor)
        If strColor = String.Empty Then
            '色の変換が出来なかった
            MsgBox("色の指定が不正です", MsgBoxStyle.Exclamation, "AddColor")
            Return String.Empty
        End If

        'カラーテーブルのカラー要素を配列に入れる
        Dim strArray() As String = GetColorArray(strRtf)

        '配列にデーターは有るか
        If strArray Is Nothing Then
            'まだ何も色が設定されていない
            '最初のカラーテーブルを返す
            ';の前に空白が必要
            Return strColorHeder + " ;" + strColor + ";}"
        End If

        Try
            '配列のデーターを引数の色と比較して同じ物があれば抜ける()
            For i As Integer = 0 To strArray.Length - 1
                If strArray(i) = strColor Then
                    '同じ物があれば現在のカラーテーブルを返す
                    Return GetColorTbl(strRtf)
                End If
            Next

            '同じ物が無かった
            '配列の上限を一つ増やしてそこに引数のデーターを入れる
            ReDim Preserve strArray(strArray.Length)
            strArray(strArray.Length - 1) = strColor

            '配列を;で結合してカラーテーブルを作成
            Dim strTemp As String = String.Join(";"c, strArray)
            strTemp = strColorHeder + " ;" + strTemp + ";}"
            Return strTemp

        Catch ex As Exception
            'エラー発生
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AddColor")
            Return String.Empty
        End Try
    End Function


    '*******************************************************
    '引数の文字列からカラーテーブルを取得し返す
    '*******************************************************
    Public Function GetColorTbl(ByRef strRtf As String) As String
        '---------------------------------------------
        '引数:
        '第1引数---カラーテーブルを含む文字列
        '動作:
        '引数の文字列からカラーテーブル文字列を取得して返す
        'カラーテーブルが無い場合とエラーの場合は空白を返す
        '---------------------------------------------

        '渡された文字列が空白だったら、空白を返す
        If Trim(strRtf).Equals(String.Empty) Then
            Return String.Empty
        End If

        '戻り値
        Dim strReturn As String = String.Empty
        Try
            '"{\colortbl"が有るかすなわちカラーテーブルが有るか？ 
            Dim intStart As Integer = strRtf.IndexOf(strColorHeder)
            If intStart >= 0 Then
                'カラーテーブルが有った
                'カラーテーブルの終端を探す
                Dim intEnd As Integer = strRtf.IndexOf("}"c, intStart)
                'カラーテーブルの最後を取得してカラーテーブルを切り出す
                Return strRtf.Substring(intStart, intEnd - intStart + 1)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            'エラー発生
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColortbl1")
            Return String.Empty
        End Try
    End Function


    '***********************************************************
    '       カラーテーブルからカラーの配列を取得
    '**********************************************************
    Private Function GetColorArray(ByRef strRtf As String) As String()
        '---------------------------------------------------
        '引数:
        '第1引数---カラーテーブルを含む文字列
        '動作:
        'カラーテーブルの中から、カラーコードを取得して
        '配列に格納する
        'カラーコードはred255,green0,blue0(redの場合)となる
        '---------------------------------------------------
        Dim strArray() As String = Nothing

        strRtf = Trim(strRtf) '前後の空白の削除

        '渡されたテーブルが空白だったら、空の配列を返す
        If Trim(strRtf).Equals(String.Empty) Then
            Return Nothing
        End If

        '渡された文字列がカラーテーブルではない場合はカラーテーブルを取得
        '1番最初がカラーヘッダーか？
        Dim strCtbl As String = String.Empty
        If Not strRtf.Substring(0, strColorHeder.Length).Equals(strColorHeder) Then
            'カラーテーブルでは無かった
            strCtbl = GetColorTbl(strRtf)  'カラーテーブルを切り出す
            If strCtbl.Equals(String.Empty) Then
                Return Nothing
            End If
        End If

        Try
            '{\colortbl"を取り除く
            strCtbl = strCtbl.Remove(0, strColorHeder.Length)

            '前後の空白を取り除く
            strCtbl = strCtbl.Trim

            '前後の";" 及び "}"を取り除く
            Dim charTrim() As Char = {"}"c, ";"c}
            strCtbl = strCtbl.Trim(charTrim)

            '";"区切りで配列に取り込む
            strArray = Split(strCtbl, ";")
            Return strArray
        Catch ex As Exception
            'エラー発生
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColorArray")
            Return Nothing
        End Try
        '配列を返す
    End Function

    '******************************************************
    'カラーテーブルを置き換える
    '******************************************************
    Public Function ChangeCtbl(ByRef strRtf As String, ByVal strNewCtbl As String) As String

        '---------------------------------------------------
        '引数:
        '第１引数---カラーテーブルを含む文字列
        '第２引数---置換するカラーテーブルの文字列

        '動作
        '第１引数の文字の中のカラーテーブルを、第２引数のカラーテーブル
        '書き換える
        '成功するとTrueを、失敗するとFalseを返す
        '------------------------------------------------------------

        '   既にカラーテーブルがあるか判別して、有る場合は消してしまう
        Try
            'カラーテーブルは有るか
            Dim intColorTbl As Integer = strRtf.IndexOf(strColorHeder)
            If intColorTbl >= 0 Then
                'カラーテーブルあり
                'カラーテーブルの最後を取得
                Dim intEndTbl As Integer = strRtf.IndexOf("}"c, intColorTbl)

                'カラーテーブルを削除
                strRtf = strRtf.Remove(intColorTbl, intEndTbl - intColorTbl + 1)
            End If

            '新しいカラーテーブルを挿入する
            '#カラーテーブルは必ず 「\viewkind」 の前にある
            strRtf = strRtf.Replace("\viewkind", _
                                    strNewCtbl & Environment.NewLine & "\viewkind")
            Return strRtf

        Catch ex As Exception
            'エラー発生
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ChangeCtbl")
            Return strRtf
        End Try
    End Function


    '*************************************************************
    '    現在の位置のカラーコードを探す
    '*************************************************************
    Public Function getCurrentColor(ByRef strRtf As String, ByVal intPos As Integer) As String
        '----------------------------------------------------------------
        '引数:
        '第１引数---Rtf
        '第２引数---Rtfの文字の位置

        '動作:
        'Rtf上の第２引数の位置のカラーコードを探して
        'Rtfのカラーコード番号を文字列で返す([例]cf1,cf2等)
        '見つからない場合は、デフォルトのcf0を返す

        Try
            '後方検索(左側検索)でカラー指定文字を探す()
            Dim intStart As Integer = strRtf.LastIndexOf("\cf", intPos)

            If intStart <> -1 Then
                'カラー指定も文字が見たかった場合はその文字の最後を検索
                '続く文字が１バイトの場合は空白が、２バイト文字の場合は
                '\が区切りになる。
                Dim intEnd1 As Integer = strRtf.IndexOf(" ", intStart + 1)
                Dim intEnd2 As Integer = strRtf.IndexOf("\", intStart + 1)
                Dim intEnd As Integer

                '最初に見つかった方を区切り文字とする
                If intEnd1 < intEnd2 And intEnd1 <> -1 Then
                    intEnd = intEnd1
                Else
                    intEnd = intEnd2
                End If

                'カラー指定文字を切り出して返す
                Return strRtf.Substring(intStart, intEnd - intStart - 1)
            End If
            'カラー指定文字が無い場合は、デフォルトの\cf0を返す
            Return "\cf0"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "getCurrentColor")
            Return String.Empty
        End Try

    End Function

    '*****************************************************************
    'カラーテーブルの中の色の番号を返す
    '*****************************************************************
    Public Function getColorNo(ByRef strRtf As String, ByVal objColor As Object) As String
        '----------------------------------------------------------------
        '引数
        '第１引数----カラーテーブルを含む文字列
        '第２引数---カラークラス又はByte配列

        '動作
        '第１引数の中から第２引数のカラーを探しその番号を返す
        '(cf0,cf1,cf2等)
        'カラーテーブルが無い場合と、カラーが見つからない場合は
        'String.Emptyを返す
        '----------------------------------------------------------------
        Try

            '色を配列に取り込む
            Dim strArray() As String = GetColorArray(strRtf)

            If strArray.Length <> 0 Then
                'If strArray IsNot Nothing Then
                '色をカラーテーブルの形式に変換する
                Dim strColor As String = GetColorString(objColor)
                If strColor.Equals(String.Empty) Then
                    'カラーテーブルが見つからない
                    Return String.Empty
                End If


                '配列の中から同じ色を探す
                For i As Integer = 0 To strArray.Length - 1
                    If strArray(i) = strColor Then
                        '色があればcfを付加して返す
                        Return "\cf" + (i + 1).ToString
                    End If
                Next
            Else
                Return "\cf1"
            End If


            '色が無かった
            Return String.Empty
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "getColorNo")
            Return String.Empty
        End Try
    End Function

    '***************************************************************
    'RichTExtBoxのカラーテーブルに挿入されるカラーコードを作成する
    '引数はColorクラス又はRGBのバイト配列のどちらでも良い
    '***************************************************************
    Public Function GetColorString(ByVal objColor As Object) As String
        '---------------------------------------------------
        '引数
        'カラークラス又はRGBが入ってバイト配列

        '動作
        'カラークラス又はバイト配列からRtfで使用される
        'カラー文字を作成して返す
        '---------------------------------------------------
        Dim strColor As String = String.Empty
        Try
            If TypeOf (objColor) Is Color Then
                '引数がカラークラスの場合
                Dim colorColor As Color = DirectCast(objColor, Color)
                'RGBの値に\erd,\green,\blueの文字をつけて返す
                Return "\red" + colorColor.R.ToString + _
                        "\green" + colorColor.G.ToString + _
                        "\blue" + colorColor.B.ToString

            ElseIf TypeOf (objColor) Is Byte Then
                '引数がバイト配列の場合
                Dim byteColor As Byte() = DirectCast(objColor, Byte())
                'RGBの値に\erd,\green,\blueの文字をつけて返す
                Return "\red" + byteColor(0).ToString + _
                       "\green" + byteColor(1).ToString + _
                       "\blue" + byteColor(2).ToString
            Else
                '渡された引数がカラークラス又は、Byte配列でなかった
                Return String.Empty
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColorString")
            Return String.Empty
        End Try
    End Function

    '**********************************************************
    '        2バイト文字をRtf用に変換
    '**********************************************************
    Public Function getEncodeString(ByVal strS As String) As String
        '------------------------------------------------------------
        '引数
        '変換する文字列

        '動作
        '渡された文字列をSift-Jisに変換して
        '２バイト文字であれば、Rtfで使用する\'文字番号に変換する
        '-----------------------------------------------------------
        Try
            'ユニコードをSift-Jisに変換するエンコードクラスを作成
            Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")
            'Sift-Jisの文字を入れるバイト配列を確保
            Dim bytes() As Byte

            Dim strReturn As String = String.Empty
            Dim strTmp As String = String.Empty

            '１文字ずつ変換して、ひとつの文字列に足しこむ
            For i As Integer = 0 To strS.Length - 1

                ReDim bytes(0)
                'Sift-Jisに変換
                bytes = sjisEnc.GetBytes(strS.Substring(i, 1))

                If bytes.Length = 1 Then
                    '変換後の長さが１バイトなら、変換前の文字を
                    'そのまま文字列に足しこむ
                    strReturn += strS.Substring(i, 1)
                Else
                    '変換後のバイトが２バイトなら
                    'Rtfの形式に変換して足しこむ
                    strReturn += "\'" + bytes(0).ToString("x2")
                    strReturn += "\'" + bytes(1).ToString("x2")
                End If
            Next
            Return strReturn
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "getEncodeString")
            Return String.Empty
        End Try
    End Function

    Public Sub SetSelectionColorRegex(ByRef RichText As System.Windows.Forms.RichTextBox, _
                                ByVal strSelectString As String, _
                                 ByVal objColor As Object)
        '--------------------------------------------------------
        '引数:
        '第1引数---RichTextBox
        '第2引数(---検索文字)
        '第3引数(---設定色)
        '動作:
        '第2引数のRichTextBoxのRtfの中から、第3引数の文字を
        '検索して、カラーコードを付加し、結果的にTextに色を付ける
        '--------------------------------------------------------
        Dim d As New System.Text.RegularExpressions.Regex(""".*?""", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        'Rtfのコピーを作成
        'Rtfのコピー上で作業して最後に書き換える
        Dim strRtf As String = RichText.Rtf

        'カラーテーブルに色を追加する
        'AddColorはカラーテーブルを返す
        Dim strNeWCtbl As String = AddColor(strRtf, objColor)

        'カラーテーブルを置き換える
        If strNeWCtbl <> String.Empty Then
            strRtf = ChangeCtbl(strRtf, strNeWCtbl)
        End If

        '追加した色の番号を取得する
        Dim strColorNo As String = getColorNo(strRtf, objColor)

        'カラー番号文字は最後が空白(次に続く文字が\の場合は空白はいらない)
        strColorNo = strColorNo & " "

        '色づけする文字をRtfの文字列に変換する
        Dim strSearch As String = getEncodeString(strSelectString)
        Dim intStart As Integer = 0
        Dim strColor As String = String.Empty
        Do
            '文字列の最後に来たら抜ける
            If intStart <= strRtf.Length Then
                '文字を探す
                intStart = strRtf.IndexOf(strSearch, intStart)
                If intStart <> -1 Then
                    '現在の色を取得する
                    strColor = getCurrentColor(strRtf, intStart)
                    '先に終了の色をセットする
                    strRtf = strRtf.Insert(intStart + strSearch.Length, strColor)
                    '色の文字列を挿入する
                    strRtf = strRtf.Insert(intStart, strColorNo)
                    '次の文字列の検索開始番号
                    intStart += strColor.Length + strSearch.Length + strColor.Length
                    '中止を受け付ける
                    Application.DoEvents()
                End If
            Else
                Exit Do
            End If
        Loop While (intStart <> -1)
        '新しいRtfを設定する
        RichText.Rtf = strRtf

    End Sub
End Module
