Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMaiin

    Private _waitHandle As New ManualResetEvent(False)

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim threadOne As New Thread(AddressOf KomplexeAufgabe)
        Dim threadTwo As New Thread(AddressOf KomplexeAufgabe)
        Dim threadThree As New Thread(AddressOf KomplexeAufgabe)

        threadOne.Start(threadOne)
        threadTwo.Start(threadTwo)
        threadThree.Start(threadThree)

        Me.Invoke(Sub() Me.txtResult.AppendText("Erster Aufruf von Set" _
                                                & Environment.NewLine))
        _waitHandle.Set()
        Thread.Sleep(500)
        Me.Invoke(Sub() Me.txtResult.AppendText("Zweiter Aufruf von Set" _
                                                & Environment.NewLine))
        _waitHandle.Set()
        Thread.Sleep(600)
        Me.Invoke(Sub() Me.txtResult.AppendText("Dritter Aufruf von Set" _
                                                & Environment.NewLine))
        _waitHandle.Set()
    End Sub

    Private Sub KomplexeAufgabe(ByVal parameter As Object)
        _waitHandle.WaitOne()
        Dim ausfuehrenderThread As Thread = CType(parameter, Thread)
        Me.Invoke(Sub() Me.txtResult.AppendText("ThreadID: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & Environment.NewLine))
    End Sub
End Class
