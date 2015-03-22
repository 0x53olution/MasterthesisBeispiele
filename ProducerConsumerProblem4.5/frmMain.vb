Option Strict On
Option Explicit On

Imports System.Threading
Imports System.Collections.Concurrent

Public Class frmMain

    Private _autos As New ConcurrentQueue(Of Integer)

    Public Property Autos() As ConcurrentQueue(Of Integer)
        Get
            Return _autos
        End Get
        Set(ByVal Value As ConcurrentQueue(Of Integer))
            _autos = Value
        End Set
    End Property

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim ProduzentenThread As New Thread(AddressOf Produzent)
        ProduzentenThread.IsBackground = True
        ProduzentenThread.Start()
        Dim KonsumentenThread1 As New Thread(AddressOf Konsument)
        KonsumentenThread1.IsBackground = True
        KonsumentenThread1.Start(KonsumentenThread1)
        Dim KonsumentenThread2 As New Thread(AddressOf Konsument)
        KonsumentenThread2.IsBackground = True
        KonsumentenThread2.Start(KonsumentenThread2)
    End Sub

    Private Sub Produzent()
        Dim Fahrgestellnummer As Integer = 0
        Do While Me._autos.Count < 100
            Thread.Sleep(100)
            Fahrgestellnummer += 1
            _autos.Enqueue(Fahrgestellnummer)
            Me.Invoke(Sub() Me.txtResult.AppendText("Produziertes Auto mit der Fahrgestellnummer: " _
                           & Fahrgestellnummer & Environment.NewLine))
        Loop
    End Sub

    Private Sub Konsument(ByVal parameter As Object)
        'Der Produzent muss erst Autos produziert haben,
        'damit die While-Schleife ausgeführt werden kann
        Thread.Sleep(800)

        Dim aufuehrenderThread = CType(parameter, Thread)
        Dim Fahrgestellnummer As Integer = 0
        Dim safe As Boolean = False
        Do While Me._autos.Count > 0
            Thread.Sleep(300)
            safe = _autos.TryDequeue(Fahrgestellnummer)
            If safe Then
                Me.Invoke(Sub() Me.txtResult.AppendText("Kunde " & aufuehrenderThread.ManagedThreadId.ToString() _
                            & " kauft Auto mit der Fahrgestellnummer: " _
                            & Fahrgestellnummer.ToString() & Environment.NewLine))
            End If
        Loop
    End Sub
End Class
