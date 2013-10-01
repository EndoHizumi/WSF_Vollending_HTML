
Imports System.Text
Imports System
Imports System.Object

Module ModuleRichText

    '�J���[�e�[�u���̍ŏ��̕���
    Const strColorHeder As String = "{\colortbl"

    '***********************************************************
    '        �^�O�̐F�ύX
    '***********************************************************
    Public Sub SetTagColor(ByRef RichText As System.Windows.Forms.RichTextBox, _
                            ByVal objColor As Object)
        '--------------------------------------------------------
        '�����F
        '��1����---RichTextBox
        '��2����---�J���[�N���X����RGB�l�����Ԃɓ������o�C�g�z��
        '����:
        '��2������RichTextBox��Rtf�̒��̃^�O���������ĐF��t����
        '--------------------------------------------------------

        Try
            'Rtf�̃R�s�[���쐬
            'Rtf�̃R�s�[��ō�Ƃ��čŌ�ɏ���������
            Dim strRtf As String = RichText.Rtf

            '�J���[�e�[�u���ɐF��ǉ�����
            'AddColor�̓J���[�e�[�u����Ԃ�
            Dim strNeWCtbl As String = AddColor(strRtf, objColor)

            '�V�����J���[�e�[�u��������������
            If strNeWCtbl <> String.Empty Then
                strRtf = ChangeCtbl(strRtf, strNeWCtbl)
            End If

            '�ǉ������F�̔ԍ��̕�������擾����(��F\cf2
            Dim strColorNo As String = getColorNo(strRtf, objColor)

            '�^�O�̐擪��u�������镶���쐬
            Dim strColorNoString As String = strColorNo + "<"

            '���ɐݒ肵�Ă���J���[������Ҕ�����
            strRtf = strRtf.Replace("\cf<", Chr(2))

            '�J���[������}������
            strRtf = strRtf.Replace("<", strColorNoString)

            '�^�O�̍Ō��u�������镶���A�F��߂�
            strRtf = strRtf.Replace(">", ">\cf0")

            '�Ҕ����Ă����J���[���������ɖ߂�
            strRtf = strRtf.Replace(Chr(2), "\cf<")

            'Rtf��u��������
            RichText.Rtf = strRtf
        Catch ex As Exception
            '�G���[����
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SetTagColor")
        End Try
    End Sub

    '***************************************************
    '�������������ĐF��ύX����
    '***************************************************
    Public Sub SetSelectionColor(ByRef RichText As System.Windows.Forms.RichTextBox, _
                                ByVal strSelectString As String, _
                                 ByVal objColor As Object)
        '--------------------------------------------------------
        '����:
        '��1����---RichTextBox
        '��2����(---��������)
        '��3����(---�ݒ�F)
        '����:
        '��2������RichTextBox��Rtf�̒�����A��3�����̕�����
        '�������āA�J���[�R�[�h��t�����A���ʓI��Text�ɐF��t����
        '--------------------------------------------------------

        'Rtf�̃R�s�[���쐬
        'Rtf�̃R�s�[��ō�Ƃ��čŌ�ɏ���������
        Dim strRtf As String = RichText.Rtf

        '�J���[�e�[�u���ɐF��ǉ�����
        'AddColor�̓J���[�e�[�u����Ԃ�
        Dim strNeWCtbl As String = AddColor(strRtf, objColor)

        '�J���[�e�[�u����u��������
        If strNeWCtbl <> String.Empty Then
            strRtf = ChangeCtbl(strRtf, strNeWCtbl)
        End If

        '�ǉ������F�̔ԍ����擾����
        Dim strColorNo As String = getColorNo(strRtf, objColor)

        '�J���[�ԍ������͍Ōオ��(���ɑ���������\�̏ꍇ�͋󔒂͂���Ȃ�)
        strColorNo = strColorNo & " "

        '�F�Â����镶����Rtf�̕�����ɕϊ�����
        Dim strSearch As String = getEncodeString(strSelectString)
        Dim intStart As Integer = 0
        Dim strColor As String = String.Empty
        Do
            '������̍Ō�ɗ����甲����
            If intStart <= strRtf.Length Then
                '������T��
                intStart = strRtf.IndexOf(strSearch, intStart)
                If intStart <> -1 Then
                    '���݂̐F���擾����
                    strColor = getCurrentColor(strRtf, intStart)
                    '��ɏI���̐F���Z�b�g����
                    strRtf = strRtf.Insert(intStart + strSearch.Length, strColor)
                    '�F�̕������}������
                    strRtf = strRtf.Insert(intStart, strColorNo)
                    '���̕�����̌����J�n�ԍ�
                    intStart += strColor.Length + strSearch.Length + strColor.Length
                    '���~���󂯕t����
                    Application.DoEvents()
                End If
            Else
                Exit Do
            End If
        Loop While (intStart <> -1)
        '�V����Rtf��ݒ肷��
        RichText.Rtf = strRtf

    End Sub

    '********************************************************
    '�J���[�e�[�u���ɐV�����J���[�v�f��������
    '********************************************************
    Public Function AddColor(ByRef strRtf As String, _
                             ByVal objColor As Object) As String
        '-------------------------------------------------------
        '�����F
        '��P����---�J���[�e�[�u�����܂ޕ�����
        '��Q����---Color�N���X����RGB���̃o�C�g�z��
        '����F
        '�w��̐F���������J���[�e�[�u����Ԃ�
        '�J���[�e�[�u���������ꍇ�͎w��̐F�ŃJ���[�e�[�u�����쐬����
        '���ɃJ���[�v�f�����݂���ꍇ�͌��݂̃J���[�e�[�u����Ԃ�
        '�G���[�̏ꍇ�͋󔒂�Ԃ�
        '-------------------------------------------------------

        '�w�肳�ꂽ�F���A�J���[�e�[�u���̐F�̌`�ɕϊ�����
        Dim strColor As String = GetColorString(objColor)
        If strColor = String.Empty Then
            '�F�̕ϊ����o���Ȃ�����
            MsgBox("�F�̎w�肪�s���ł�", MsgBoxStyle.Exclamation, "AddColor")
            Return String.Empty
        End If

        '�J���[�e�[�u���̃J���[�v�f��z��ɓ����
        Dim strArray() As String = GetColorArray(strRtf)

        '�z��Ƀf�[�^�[�͗L�邩
        If strArray Is Nothing Then
            '�܂������F���ݒ肳��Ă��Ȃ�
            '�ŏ��̃J���[�e�[�u����Ԃ�
            ';�̑O�ɋ󔒂��K�v
            Return strColorHeder + " ;" + strColor + ";}"
        End If

        Try
            '�z��̃f�[�^�[�������̐F�Ɣ�r���ē�����������Δ�����()
            For i As Integer = 0 To strArray.Length - 1
                If strArray(i) = strColor Then
                    '������������Ό��݂̃J���[�e�[�u����Ԃ�
                    Return GetColorTbl(strRtf)
                End If
            Next

            '����������������
            '�z��̏��������₵�Ă����Ɉ����̃f�[�^�[������
            ReDim Preserve strArray(strArray.Length)
            strArray(strArray.Length - 1) = strColor

            '�z���;�Ō������ăJ���[�e�[�u�����쐬
            Dim strTemp As String = String.Join(";"c, strArray)
            strTemp = strColorHeder + " ;" + strTemp + ";}"
            Return strTemp

        Catch ex As Exception
            '�G���[����
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "AddColor")
            Return String.Empty
        End Try
    End Function


    '*******************************************************
    '�����̕����񂩂�J���[�e�[�u�����擾���Ԃ�
    '*******************************************************
    Public Function GetColorTbl(ByRef strRtf As String) As String
        '---------------------------------------------
        '����:
        '��1����---�J���[�e�[�u�����܂ޕ�����
        '����:
        '�����̕����񂩂�J���[�e�[�u����������擾���ĕԂ�
        '�J���[�e�[�u���������ꍇ�ƃG���[�̏ꍇ�͋󔒂�Ԃ�
        '---------------------------------------------

        '�n���ꂽ�����񂪋󔒂�������A�󔒂�Ԃ�
        If Trim(strRtf).Equals(String.Empty) Then
            Return String.Empty
        End If

        '�߂�l
        Dim strReturn As String = String.Empty
        Try
            '"{\colortbl"���L�邩���Ȃ킿�J���[�e�[�u�����L�邩�H 
            Dim intStart As Integer = strRtf.IndexOf(strColorHeder)
            If intStart >= 0 Then
                '�J���[�e�[�u�����L����
                '�J���[�e�[�u���̏I�[��T��
                Dim intEnd As Integer = strRtf.IndexOf("}"c, intStart)
                '�J���[�e�[�u���̍Ō���擾���ăJ���[�e�[�u����؂�o��
                Return strRtf.Substring(intStart, intEnd - intStart + 1)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            '�G���[����
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColortbl1")
            Return String.Empty
        End Try
    End Function


    '***********************************************************
    '       �J���[�e�[�u������J���[�̔z����擾
    '**********************************************************
    Private Function GetColorArray(ByRef strRtf As String) As String()
        '---------------------------------------------------
        '����:
        '��1����---�J���[�e�[�u�����܂ޕ�����
        '����:
        '�J���[�e�[�u���̒�����A�J���[�R�[�h���擾����
        '�z��Ɋi�[����
        '�J���[�R�[�h��red255,green0,blue0(red�̏ꍇ)�ƂȂ�
        '---------------------------------------------------
        Dim strArray() As String = Nothing

        strRtf = Trim(strRtf) '�O��̋󔒂̍폜

        '�n���ꂽ�e�[�u�����󔒂�������A��̔z���Ԃ�
        If Trim(strRtf).Equals(String.Empty) Then
            Return Nothing
        End If

        '�n���ꂽ�����񂪃J���[�e�[�u���ł͂Ȃ��ꍇ�̓J���[�e�[�u�����擾
        '1�ԍŏ����J���[�w�b�_�[���H
        Dim strCtbl As String = String.Empty
        If Not strRtf.Substring(0, strColorHeder.Length).Equals(strColorHeder) Then
            '�J���[�e�[�u���ł͖�������
            strCtbl = GetColorTbl(strRtf)  '�J���[�e�[�u����؂�o��
            If strCtbl.Equals(String.Empty) Then
                Return Nothing
            End If
        End If

        Try
            '{\colortbl"����菜��
            strCtbl = strCtbl.Remove(0, strColorHeder.Length)

            '�O��̋󔒂���菜��
            strCtbl = strCtbl.Trim

            '�O���";" �y�� "}"����菜��
            Dim charTrim() As Char = {"}"c, ";"c}
            strCtbl = strCtbl.Trim(charTrim)

            '";"��؂�Ŕz��Ɏ�荞��
            strArray = Split(strCtbl, ";")
            Return strArray
        Catch ex As Exception
            '�G���[����
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColorArray")
            Return Nothing
        End Try
        '�z���Ԃ�
    End Function

    '******************************************************
    '�J���[�e�[�u����u��������
    '******************************************************
    Public Function ChangeCtbl(ByRef strRtf As String, ByVal strNewCtbl As String) As String

        '---------------------------------------------------
        '����:
        '��P����---�J���[�e�[�u�����܂ޕ�����
        '��Q����---�u������J���[�e�[�u���̕�����

        '����
        '��P�����̕����̒��̃J���[�e�[�u�����A��Q�����̃J���[�e�[�u��
        '����������
        '���������True���A���s�����False��Ԃ�
        '------------------------------------------------------------

        '   ���ɃJ���[�e�[�u�������邩���ʂ��āA�L��ꍇ�͏����Ă��܂�
        Try
            '�J���[�e�[�u���͗L�邩
            Dim intColorTbl As Integer = strRtf.IndexOf(strColorHeder)
            If intColorTbl >= 0 Then
                '�J���[�e�[�u������
                '�J���[�e�[�u���̍Ō���擾
                Dim intEndTbl As Integer = strRtf.IndexOf("}"c, intColorTbl)

                '�J���[�e�[�u�����폜
                strRtf = strRtf.Remove(intColorTbl, intEndTbl - intColorTbl + 1)
            End If

            '�V�����J���[�e�[�u����}������
            '#�J���[�e�[�u���͕K�� �u\viewkind�v �̑O�ɂ���
            strRtf = strRtf.Replace("\viewkind", _
                                    strNewCtbl & Environment.NewLine & "\viewkind")
            Return strRtf

        Catch ex As Exception
            '�G���[����
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ChangeCtbl")
            Return strRtf
        End Try
    End Function


    '*************************************************************
    '    ���݂̈ʒu�̃J���[�R�[�h��T��
    '*************************************************************
    Public Function getCurrentColor(ByRef strRtf As String, ByVal intPos As Integer) As String
        '----------------------------------------------------------------
        '����:
        '��P����---Rtf
        '��Q����---Rtf�̕����̈ʒu

        '����:
        'Rtf��̑�Q�����̈ʒu�̃J���[�R�[�h��T����
        'Rtf�̃J���[�R�[�h�ԍ��𕶎���ŕԂ�([��]cf1,cf2��)
        '������Ȃ��ꍇ�́A�f�t�H���g��cf0��Ԃ�

        Try
            '�������(��������)�ŃJ���[�w�蕶����T��()
            Dim intStart As Integer = strRtf.LastIndexOf("\cf", intPos)

            If intStart <> -1 Then
                '�J���[�w��������������������ꍇ�͂��̕����̍Ō������
                '�����������P�o�C�g�̏ꍇ�͋󔒂��A�Q�o�C�g�����̏ꍇ��
                '\����؂�ɂȂ�B
                Dim intEnd1 As Integer = strRtf.IndexOf(" ", intStart + 1)
                Dim intEnd2 As Integer = strRtf.IndexOf("\", intStart + 1)
                Dim intEnd As Integer

                '�ŏ��Ɍ�������������؂蕶���Ƃ���
                If intEnd1 < intEnd2 And intEnd1 <> -1 Then
                    intEnd = intEnd1
                Else
                    intEnd = intEnd2
                End If

                '�J���[�w�蕶����؂�o���ĕԂ�
                Return strRtf.Substring(intStart, intEnd - intStart - 1)
            End If
            '�J���[�w�蕶���������ꍇ�́A�f�t�H���g��\cf0��Ԃ�
            Return "\cf0"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "getCurrentColor")
            Return String.Empty
        End Try

    End Function

    '*****************************************************************
    '�J���[�e�[�u���̒��̐F�̔ԍ���Ԃ�
    '*****************************************************************
    Public Function getColorNo(ByRef strRtf As String, ByVal objColor As Object) As String
        '----------------------------------------------------------------
        '����
        '��P����----�J���[�e�[�u�����܂ޕ�����
        '��Q����---�J���[�N���X����Byte�z��

        '����
        '��P�����̒������Q�����̃J���[��T�����̔ԍ���Ԃ�
        '(cf0,cf1,cf2��)
        '�J���[�e�[�u���������ꍇ�ƁA�J���[��������Ȃ��ꍇ��
        'String.Empty��Ԃ�
        '----------------------------------------------------------------
        Try

            '�F��z��Ɏ�荞��
            Dim strArray() As String = GetColorArray(strRtf)

            If strArray.Length <> 0 Then
                'If strArray IsNot Nothing Then
                '�F���J���[�e�[�u���̌`���ɕϊ�����
                Dim strColor As String = GetColorString(objColor)
                If strColor.Equals(String.Empty) Then
                    '�J���[�e�[�u����������Ȃ�
                    Return String.Empty
                End If


                '�z��̒����瓯���F��T��
                For i As Integer = 0 To strArray.Length - 1
                    If strArray(i) = strColor Then
                        '�F�������cf��t�����ĕԂ�
                        Return "\cf" + (i + 1).ToString
                    End If
                Next
            Else
                Return "\cf1"
            End If


            '�F����������
            Return String.Empty
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "getColorNo")
            Return String.Empty
        End Try
    End Function

    '***************************************************************
    'RichTExtBox�̃J���[�e�[�u���ɑ}�������J���[�R�[�h���쐬����
    '������Color�N���X����RGB�̃o�C�g�z��̂ǂ���ł��ǂ�
    '***************************************************************
    Public Function GetColorString(ByVal objColor As Object) As String
        '---------------------------------------------------
        '����
        '�J���[�N���X����RGB�������ăo�C�g�z��

        '����
        '�J���[�N���X���̓o�C�g�z�񂩂�Rtf�Ŏg�p�����
        '�J���[�������쐬���ĕԂ�
        '---------------------------------------------------
        Dim strColor As String = String.Empty
        Try
            If TypeOf (objColor) Is Color Then
                '�������J���[�N���X�̏ꍇ
                Dim colorColor As Color = DirectCast(objColor, Color)
                'RGB�̒l��\erd,\green,\blue�̕��������ĕԂ�
                Return "\red" + colorColor.R.ToString + _
                        "\green" + colorColor.G.ToString + _
                        "\blue" + colorColor.B.ToString

            ElseIf TypeOf (objColor) Is Byte Then
                '�������o�C�g�z��̏ꍇ
                Dim byteColor As Byte() = DirectCast(objColor, Byte())
                'RGB�̒l��\erd,\green,\blue�̕��������ĕԂ�
                Return "\red" + byteColor(0).ToString + _
                       "\green" + byteColor(1).ToString + _
                       "\blue" + byteColor(2).ToString
            Else
                '�n���ꂽ�������J���[�N���X���́AByte�z��łȂ�����
                Return String.Empty
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GetColorString")
            Return String.Empty
        End Try
    End Function

    '**********************************************************
    '        2�o�C�g������Rtf�p�ɕϊ�
    '**********************************************************
    Public Function getEncodeString(ByVal strS As String) As String
        '------------------------------------------------------------
        '����
        '�ϊ����镶����

        '����
        '�n���ꂽ�������Sift-Jis�ɕϊ�����
        '�Q�o�C�g�����ł���΁ARtf�Ŏg�p����\'�����ԍ��ɕϊ�����
        '-----------------------------------------------------------
        Try
            '���j�R�[�h��Sift-Jis�ɕϊ�����G���R�[�h�N���X���쐬
            Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")
            'Sift-Jis�̕���������o�C�g�z����m��
            Dim bytes() As Byte

            Dim strReturn As String = String.Empty
            Dim strTmp As String = String.Empty

            '�P�������ϊ����āA�ЂƂ̕�����ɑ�������
            For i As Integer = 0 To strS.Length - 1

                ReDim bytes(0)
                'Sift-Jis�ɕϊ�
                bytes = sjisEnc.GetBytes(strS.Substring(i, 1))

                If bytes.Length = 1 Then
                    '�ϊ���̒������P�o�C�g�Ȃ�A�ϊ��O�̕�����
                    '���̂܂ܕ�����ɑ�������
                    strReturn += strS.Substring(i, 1)
                Else
                    '�ϊ���̃o�C�g���Q�o�C�g�Ȃ�
                    'Rtf�̌`���ɕϊ����đ�������
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
        '����:
        '��1����---RichTextBox
        '��2����(---��������)
        '��3����(---�ݒ�F)
        '����:
        '��2������RichTextBox��Rtf�̒�����A��3�����̕�����
        '�������āA�J���[�R�[�h��t�����A���ʓI��Text�ɐF��t����
        '--------------------------------------------------------
        Dim d As New System.Text.RegularExpressions.Regex(""".*?""", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        'Rtf�̃R�s�[���쐬
        'Rtf�̃R�s�[��ō�Ƃ��čŌ�ɏ���������
        Dim strRtf As String = RichText.Rtf

        '�J���[�e�[�u���ɐF��ǉ�����
        'AddColor�̓J���[�e�[�u����Ԃ�
        Dim strNeWCtbl As String = AddColor(strRtf, objColor)

        '�J���[�e�[�u����u��������
        If strNeWCtbl <> String.Empty Then
            strRtf = ChangeCtbl(strRtf, strNeWCtbl)
        End If

        '�ǉ������F�̔ԍ����擾����
        Dim strColorNo As String = getColorNo(strRtf, objColor)

        '�J���[�ԍ������͍Ōオ��(���ɑ���������\�̏ꍇ�͋󔒂͂���Ȃ�)
        strColorNo = strColorNo & " "

        '�F�Â����镶����Rtf�̕�����ɕϊ�����
        Dim strSearch As String = getEncodeString(strSelectString)
        Dim intStart As Integer = 0
        Dim strColor As String = String.Empty
        Do
            '������̍Ō�ɗ����甲����
            If intStart <= strRtf.Length Then
                '������T��
                intStart = strRtf.IndexOf(strSearch, intStart)
                If intStart <> -1 Then
                    '���݂̐F���擾����
                    strColor = getCurrentColor(strRtf, intStart)
                    '��ɏI���̐F���Z�b�g����
                    strRtf = strRtf.Insert(intStart + strSearch.Length, strColor)
                    '�F�̕������}������
                    strRtf = strRtf.Insert(intStart, strColorNo)
                    '���̕�����̌����J�n�ԍ�
                    intStart += strColor.Length + strSearch.Length + strColor.Length
                    '���~���󂯕t����
                    Application.DoEvents()
                End If
            Else
                Exit Do
            End If
        Loop While (intStart <> -1)
        '�V����Rtf��ݒ肷��
        RichText.Rtf = strRtf

    End Sub
End Module
