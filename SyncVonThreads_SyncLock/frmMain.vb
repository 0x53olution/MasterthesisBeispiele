Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMain

    Private Shared SperrObjekt As New Object
    Private _wert As Integer

    Private spLock As New SpinLock
    Private MyLock As Boolean

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim threadOne As New Thread(AddressOf CountLock)
        Dim parameterOne = New Object() {threadOne, 1}
        Dim threadTwo As New Thread(AddressOf CountLock)
        Dim parameterTwo = New Object() {threadTwo, 100}

        threadOne.Start(parameterOne)
        threadTwo.Start(parameterTwo)

        'Dim threadSpinLock As New Thread(AddressOf CountSpinLock)
        'Dim parameterSpinLock = New Object() {threadSpinLock, 1}
    End Sub

    Private Sub Count(ByVal parameter As Object)
        Dim ausfuehrenderThread As Thread = CType(CType(parameter, Object())(0), Thread)
        _wert = CType(CType(parameter, Object())(1), Integer)
        Dim Schleifendurchlaeufe As Integer = 10000
        Dim wertEingabe = _wert
        For i As Integer = 0 To Schleifendurchlaeufe
            _wert += 1
        Next
        Me.Invoke(Sub() Me.txtResult.AppendText("ThreadID: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & Environment.NewLine _
                                & "Eingabewert: " & wertEingabe.ToString _
                                & Environment.NewLine _
                                & "Schleifendurchläufe: " & Schleifendurchlaeufe.ToString _
                                & " Veränderter Wert: " & _wert.ToString _
                                & Environment.NewLine))
    End Sub

    Private Sub CountLock(ByVal parameter As Object)
        SyncLock SperrObjekt
            Dim ausfuehrenderThread As Thread = CType(CType(parameter, Object())(0), Thread)
            _wert = CType(CType(parameter, Object())(1), Integer)
            Dim Schleifendurchlaeufe As Integer = 10000
            Dim wertEingabe = _wert
            For i As Integer = 0 To Schleifendurchlaeufe
                _wert += 1
            Next
            Me.Invoke(Sub() Me.txtResult.AppendText("ThreadID: " _
                                & ausfuehrenderThread.ManagedThreadId.ToString _
                                & Environment.NewLine _
                                & "Eingabewert: " & wertEingabe.ToString _
                                & Environment.NewLine _
                                & "Schleifendurchläufe: " & Schleifendurchlaeufe.ToString _
                                & " Veränderter Wert: " & _wert.ToString _
                                & Environment.NewLine))
        End SyncLock
    End Sub

    Private Sub CountSpinLock(ByVal parameter As Object)
        spLock.Enter(MyLock)
        'Ausfuehrender Code
        spLock.Exit()
    End Sub
End Class
