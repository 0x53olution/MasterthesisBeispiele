Option Strict On
Option Explicit On
Public Class KomplexeAufgabe

    Public Class Status
        Public AnzahlDokumnete As Integer
    End Class

    Public _directory As String
    Private arrayInput(99999999) As Byte
    Private rndNumberGen As New Random()
    Private AnzahlDokumnete As Integer = 0
    Public Sub BeschreibeDokumente(ByVal worker As System.ComponentModel.BackgroundWorker, _
                                 ByVal e As System.ComponentModel.DoWorkEventArgs)

        Dim files As String() = System.IO.Directory.GetFiles(_directory)
        Dim dokumnetStatus As New Status
        rndNumberGen.NextBytes(arrayInput)

        For Each currentFile In files
            dokumnetStatus.AnzahlDokumnete = AnzahlDokumnete
            worker.ReportProgress(0, dokumnetStatus)
            If worker.CancellationPending Then
                e.Cancel = True
                Exit For
            Else
                AnzahlDokumnete += 1
                Dim fileStream As System.IO.FileStream
                fileStream = New System.IO.FileStream(currentFile, IO.FileMode.Open, IO.FileAccess.Write)
                For i As Integer = 0 To arrayInput.Length - 1
                    fileStream.WriteByte(arrayInput(i))
                Next
                fileStream.Close()
            End If
        Next
        dokumnetStatus.AnzahlDokumnete = AnzahlDokumnete
    End Sub
End Class
