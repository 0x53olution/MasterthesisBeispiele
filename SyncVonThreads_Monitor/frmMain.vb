Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMain

    Private _anzahlVersuche As Integer = 10

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim threadOne As New Thread(AddressOf SperreMonitor)
        Dim threadTwo As New Thread(AddressOf SperreMonitor)

        threadOne.Start(threadOne)
        threadTwo.Start(threadTwo)

    End Sub

    Private Sub SperreMonitor(ByVal parameter As Object)
        Dim ausfuehrenderThread As Thread = CType(parameter, Thread)

        For i As Integer = 0 To _anzahlVersuche
            If Not Monitor.TryEnter(Me) Then
                Me.Invoke(Sub() Me.txtResult.AppendText("Thread: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & " Monitor wird gerade von einem anderen Thread benutzt" _
                                & Environment.NewLine))
                Thread.Sleep(100)
            Else
                Me.Invoke(Sub() Me.txtResult.AppendText("Thread: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & " führt eine Threadsichere Aktion aus" _
                                & Environment.NewLine))
                Thread.Sleep(10)
                Monitor.Exit(Me)
            End If
        Next
    End Sub
End Class