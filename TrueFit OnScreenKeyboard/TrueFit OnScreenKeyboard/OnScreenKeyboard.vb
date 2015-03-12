Public Class OnScreenKeyboard

    Private Sub OnScreenKeyboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Button_11.Focus()
        Cursor.Position = Me.PointToScreen(Button_11.Location)
    End Sub

    Private Sub Button_Start_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call LoadSet()
    End Sub

    Private Sub LoadSet()
        Call ReadFile()
    End Sub

    Private Sub ReadFile()
        Dim str_FilePath, str_SampFile, str_OutFile, str_LineofText As String
        Dim oRead As System.IO.StreamReader

        'str_FilePath = "D:\Documents\Visual Studio 2012\Projects\TrueFit OnScreenKeyboard\TrueFit OnScreenKeyboard\Files\Sample Input.TXT"
        str_FilePath = My.Computer.FileSystem.CurrentDirectory
        str_FilePath = str_FilePath.Substring(0, My.Computer.FileSystem.CurrentDirectory.LastIndexOf("bin")) & "Files\"
        str_SampFile = str_FilePath & "Sample Input.TXT"
        str_OutFile = str_FilePath & "Sample Output.TXT"

        If My.Computer.FileSystem.FileExists(str_OutFile) = True Then
            My.Computer.FileSystem.DeleteFile(str_OutFile)
        End If

        If My.Computer.FileSystem.FileExists(str_SampFile) = True Then
            oRead = IO.File.OpenText(str_SampFile)

            While oRead.Peek <> -1
                str_LineofText = oRead.ReadLine()

                Call OnScreenPointer(str_OutFile, str_FilePath, str_LineofText)
            End While
            oRead.Close()
        Else
            MessageBox.Show("Missing file: " & vbCr & str_SampFile, "Missing File")
        End If
    End Sub

    Private Sub OnScreenPointer(str_OutFile, str_FilePath, str_LineofText)
        Dim str_BtnPre, str_BtnStartRow, str_BtnStartCol, int_InputLen, int_LenCont, str_Record, str_NewSelRow, str_InputChar, str_CtlBtn, str_BtnName, str_BtnRow, str_BtnCol, _
            int_BtnRowMove, int_BtnColMove, str_NewSelCol, str_LineofTextOut As String
        Dim int_BtnCont, int_CommaCnt As Integer
        Dim oWrite As System.IO.StreamWriter

        str_BtnPre = "Button_"
        str_BtnStartRow = 1
        str_BtnStartCol = 1
        int_InputLen = str_LineofText.Length
        int_LenCont = 0
        str_Record = ""
        str_NewSelRow = str_BtnPre & str_BtnStartRow & str_BtnStartCol

        'SET FOCUS AND POINTER ON CURRENT SELECTION GOING **UP**
        Me.Controls.Item(str_NewSelRow).Focus()
        Cursor.Position = Me.PointToScreen(Me.Controls.Item(str_NewSelRow).Location)
        System.Threading.Thread.Sleep(500)

        Do Until int_LenCont = int_InputLen
            str_InputChar = str_LineofText.ToString.Substring(int_LenCont, 1)
            str_InputChar = str_InputChar.ToUpper
            int_BtnCont = 1

            If (Asc(str_InputChar) >= 48 And Asc(str_InputChar) <= 57) Or (Asc(str_InputChar) >= 65 And Asc(str_InputChar) <= 90) Then
                For int_BtnCont = 0 To Me.Controls.Count
                    str_CtlBtn = CType(Me.Controls.Item(int_BtnCont), Button).Text
                    If str_CtlBtn = str_InputChar Then
                        str_BtnName = CType(Me.Controls.Item(int_BtnCont), Button).Name
                        str_BtnRow = str_BtnName.Replace(str_BtnPre, "")
                        str_BtnRow = str_BtnRow.Substring(0, 1)
                        str_BtnCol = str_BtnName.Replace(str_BtnPre, "")
                        str_BtnCol = str_BtnCol.Substring(1, 1)

                        Do Until str_BtnStartRow = str_BtnRow
                            If str_BtnStartRow < str_BtnRow Then
                                int_BtnRowMove = str_BtnStartRow + 1

                                str_NewSelRow = str_BtnPre & int_BtnRowMove & str_BtnStartCol

                                'SET FOCUS AND POINTER ON CURRENT SELECTION GOING **UP**
                                Me.Controls.Item(str_NewSelRow).Focus()
                                Cursor.Position = Me.PointToScreen(Me.Controls.Item(str_NewSelRow).Location)

                                str_BtnStartRow += 1
                                System.Threading.Thread.Sleep(500)
                                str_Record = str_Record + "D"
                            ElseIf str_BtnStartRow > str_BtnRow Then
                                int_BtnRowMove = str_BtnStartRow - 1

                                str_NewSelRow = str_BtnPre & int_BtnRowMove & str_BtnStartCol

                                'SET FOCUS AND POINTER ON CURRENT SELECTION GOING **UP**
                                Me.Controls.Item(str_NewSelRow).Focus()
                                Cursor.Position = Me.PointToScreen(Me.Controls.Item(str_NewSelRow).Location)

                                str_BtnStartRow -= 1
                                System.Threading.Thread.Sleep(500)
                                str_Record = str_Record + "U"
                            End If
                        Loop

                        Do Until str_BtnStartCol = str_BtnCol
                            If str_BtnStartCol < str_BtnCol Then
                                int_BtnColMove = str_BtnStartCol + 1

                                str_NewSelCol = str_BtnPre & str_BtnStartRow & int_BtnColMove

                                'SET FOCUS AND POINTER ON CURRENT SELECTION GOING **UP**
                                Me.Controls.Item(str_NewSelCol).Focus()
                                Cursor.Position = Me.PointToScreen(Me.Controls.Item(str_NewSelCol).Location)

                                str_BtnStartCol += 1
                                System.Threading.Thread.Sleep(500)
                                str_Record = str_Record + "R"
                            ElseIf str_BtnStartCol > str_BtnCol Then
                                int_BtnColMove = str_BtnStartCol - 1

                                str_NewSelCol = str_BtnPre & str_BtnStartRow & int_BtnColMove

                                'SET FOCUS AND POINTER ON CURRENT SELECTION GOING **UP**
                                Me.Controls.Item(str_NewSelCol).Focus()
                                Cursor.Position = Me.PointToScreen(Me.Controls.Item(str_NewSelCol).Location)

                                str_BtnStartCol -= 1
                                System.Threading.Thread.Sleep(500)
                                str_Record = str_Record + "L"
                            End If
                        Loop

                        System.Threading.Thread.Sleep(1000)
                        str_Record = str_Record + "#"
                        Exit For
                    End If
                Next
            ElseIf Asc(str_InputChar) = 32 Then
                str_Record = str_Record + "S"
            End If

            int_LenCont += 1
        Loop

        int_CommaCnt = 0
        str_LineofTextOut = ""

        Do Until int_CommaCnt = str_Record.Length
            If int_CommaCnt = 0 Then
                str_LineofTextOut = str_Record.Substring(int_CommaCnt, 1)
            Else
                str_LineofTextOut = str_LineofTextOut & "," & str_Record.Substring(int_CommaCnt, 1)
            End If

            int_CommaCnt += 1
        Loop

        oWrite = My.Computer.FileSystem.OpenTextFileWriter(str_OutFile, True)
        oWrite.WriteLine(str_LineofTextOut)
        oWrite.Close()
    End Sub
End Class