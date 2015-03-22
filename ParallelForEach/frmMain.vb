Option Strict On
Option Explicit On
Public Class frmMain

    Private _directory1 As String _
        = Application.StartupPath & "\Testdokumente1\"
    Private _directory2 As String _
        = Application.StartupPath & "\Testdokumente2\"
    Private _files1 As String() _
        = System.IO.Directory.GetFiles(_directory1)
    Private _files2 As String() _
        = System.IO.Directory.GetFiles(_directory2)

    Private arrayInput(99999999) As Byte
    Private rndNumberGen As New Random()

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        
        rndNumberGen.NextBytes(arrayInput)

        Dim Stoppuhr As New Stopwatch
        Stoppuhr.Start()

        For Each currentFile In _files1
            Dim fileStream As System.IO.FileStream
            fileStream = New System.IO.FileStream(currentFile, IO.FileMode.Open, IO.FileAccess.Write)
            For i As Integer = 0 To arrayInput.Length - 1
                fileStream.WriteByte(arrayInput(i))
            Next
            fileStream.Close()
        Next

        Stoppuhr.Stop()
        txtResult.AppendText("Normale For Each-Schleife: " & Stoppuhr.ElapsedMilliseconds _
                             & " in Millisekunden" & Environment.NewLine)

        Stoppuhr.Reset()
        Stoppuhr.Start()

        Parallel.ForEach(_files2, Sub(currentFile)
                                      Dim filename As String = System.IO.Path.GetFileName(currentFile)
                                      Dim fileStream As System.IO.FileStream
                                      fileStream = New System.IO.FileStream(currentFile, IO.FileMode.Open)
                                      For i As Integer = 0 To arrayInput.Length - 1
                                          fileStream.WriteByte(arrayInput(i))
                                      Next
                                      fileStream.Close()
                                  End Sub)
        Stoppuhr.Stop()
        txtResult.AppendText("Parallel For Each-Schleife: " & Stoppuhr.ElapsedMilliseconds _
                             & " in Millisekunden" & Environment.NewLine)
    End Sub
End Class
