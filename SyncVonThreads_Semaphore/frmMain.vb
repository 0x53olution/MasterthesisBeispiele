Option Strict On
Option Explicit On

Imports System.Threading
Imports System.Threading.Interlocked

Public Class frmMain

    Private Shared _semaphoreSperre As New Semaphore(0, 6)
    Private _wert As Integer = 0
    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        For i As Integer = 0 To 9
            Dim threads As New Thread(AddressOf KomplexeBerechnung)
            threads.Start(threads)
        Next i
        _semaphoreSperre.Release(6)
    End Sub

    Private Sub KomplexeBerechnung(ByVal parameter As Object)
        Dim ausfuehrenderThread As Thread = CType(parameter, Thread)
        Me.Invoke(Sub() Me.txtResult.AppendText( _
                      "Thread: " _
                      & ausfuehrenderThread.ManagedThreadId.ToString _
                      & " hat möchte das Semaphore betreten." _
                      & Environment.NewLine))
        _semaphoreSperre.WaitOne()

        Me.Invoke(Sub() Me.txtResult.AppendText("Die Variable hat den Wert: " _
                                & Interlocked.Increment(_wert) & " von Thread: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & " ausgeführt." _
                                & Environment.NewLine))
        Thread.Sleep(200)

        _semaphoreSperre.Release()
        Me.Invoke(Sub() Me.txtResult.AppendText( _
                      "Thread: " _
                      & ausfuehrenderThread.ManagedThreadId.ToString _
                      & " hat das Semaphore verlassen." _
                      & Environment.NewLine))
    End Sub
End Class
