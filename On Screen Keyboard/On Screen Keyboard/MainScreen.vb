Imports System.IO

Public Class MainScreen

    Private ResetPosition As Boolean = False

    Private Function LookupInputChar(InputString As String) As String

        Dim i As Integer = 0
        Dim ArrayString As String = ""
        Dim CharPos As Integer = 0
        Dim StrArray(6) As String
        Dim CharFound As Boolean = False
        Dim OutputString As String = ""

        StrArray(0) = "ABCDEF"
        StrArray(1) = "GHIJKL"
        StrArray(2) = "MNOPQR"
        StrArray(3) = "STUVWX"
        StrArray(4) = "YZ1234"
        StrArray(5) = "567890"

        'Loop through items in array
        For i = 0 To 5
            ArrayString = StrArray(i).ToString
            'Find character position
            CharPos = ArrayString.IndexOf(InputString)
            If CharPos > -1 Then
                'match found
                CharFound = True
                Exit For
            End If
        Next

        If CharFound Then
            'Convert results
            OutputString = ConvertResults(i, CharPos + 1)
        End If

        Return OutputString

    End Function

    Private Function ConvertResults(row As Integer, col As Integer) As String

        Dim ConvertedResults As String = ""
        Dim ChangeInRows As Integer = 0
        Dim ChangeInColumns As Integer = 0
        Static PriorRow As Integer = 0
        Static PriorCol As Integer = 1

        'If a new line is being read the cursor needs to reset to it's initial position
        If ResetPosition Then
            PriorRow = 0
            PriorCol = 1
        End If

        ChangeInRows = row - PriorRow
        If ChangeInRows > 0 Then
            For i = 1 To ChangeInRows
                ConvertedResults = ConvertedResults & "D,"
            Next
        Else
            For i = -1 To ChangeInRows Step -1
                ConvertedResults = ConvertedResults & "U,"
            Next
        End If

        ChangeInColumns = col - PriorCol
        If ChangeInColumns > 0 Then
            For i = 1 To ChangeInColumns
                ConvertedResults = ConvertedResults & "R,"
            Next
        Else
            For i = -1 To ChangeInColumns Step -1
                ConvertedResults = ConvertedResults & "L,"
            Next
        End If

        PriorRow = row
        PriorCol = col

        ConvertedResults = ConvertedResults & "#,"

        Return ConvertedResults

    End Function

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click

        'Dim InputString As String = UCase("IT Crowd")
        Dim InputString As String = ""
        Dim ret As String = ""
        Dim OutputString As String = ""
        Dim TextLine As String = ""

        Dim FileName As String = "C:\sample.txt"
        txtInputFile.Text = FileName

        If System.IO.File.Exists(FileName) Then
            Dim objReader As New System.IO.StreamReader(FileName)
            Do While objReader.Peek() <> -1
                TextLine = objReader.ReadLine()
                InputString = UCase(TextLine)
                ResetPosition = True

                For Each element As Char In InputString
                    If element.ToString <> " " Then
                        ret = LookupInputChar(element)
                        OutputString = OutputString & ret
                    Else
                        OutputString = OutputString & "S,"
                    End If
                    If ResetPosition Then ResetPosition = False
                Next

                txtOutputResults.Text = OutputString.ToString

                'remove last comma
                'this is the converted DVR output for the input line 
                'check for blank links due to invalid characters in text file
                If OutputString.Length > 0 Then
                    txtOutputResults.Text = OutputString.Substring(0, OutputString.Length - 1)
                End If
                OutputString = ""
            Loop
        End If

    End Sub

End Class
