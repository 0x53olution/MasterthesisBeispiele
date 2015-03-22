Option Strict On
Option Explicit On

Public Class frmMain
    Dim WithEvents bgw As New System.ComponentModel.BackgroundWorker _
        With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True}

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim BackGroundWork As New KomplexeAufgabe
        BackGroundWork._directory = Application.StartupPath & "\Testdokumente\"
        Me.bgw.RunWorkerAsync(BackGroundWork)
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.bgw.CancelAsync()
    End Sub

    ''' <summary>
    ''' Backgroundworker - wird im Hintergrundthread ausgeführt
    ''' </summary>
    Private Sub bgw_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) _
        Handles bgw.DoWork

        Dim bgWorker As System.ComponentModel.BackgroundWorker _
            = CType(sender, System.ComponentModel.BackgroundWorker)
        Dim Aufgabe As KomplexeAufgabe = CType(e.Argument, KomplexeAufgabe)

        Aufgabe.BeschreibeDokumente(bgWorker, e)
    End Sub

    ''' <summary>
    ''' Zeigt den aktuellen Zustand - wird im Mainthread ausgeführt
    ''' </summary>
    Private Sub bgw_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) _
        Handles bgw.ProgressChanged

        Dim status As KomplexeAufgabe.Status = CType(e.UserState, KomplexeAufgabe.Status)
        Me.txtResult.AppendText("Dokument: " & status.AnzahlDokumnete.ToString _
                                & " wird bearbeitet." & Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Gibt eine Rueckmeldung über das Beenden - wird im Mainthread ausgeführt
    ''' </summary>
    Private Sub bgw_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) _
        Handles bgw.RunWorkerCompleted

        If e.Error IsNot Nothing Then
            Me.txtResult.AppendText("Es ist eine Fehler aufgetreten: " & e.Error.Message)
            'MessageBox.Show("Es ist eine Fehler aufgetreten: " & e.Error.Message)
        ElseIf e.Cancelled Then
            Me.txtResult.AppendText("Vorgang vom User abgebrochen!")
            'MessageBox.Show("Vorgang vom User abgebrochen!")
        Else
            Me.txtResult.AppendText("Alle Dokumente beareitet.")
            'MessageBox.Show("Alle Dokumente beareitet.")
        End If
    End Sub

End Class
