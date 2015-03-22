Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMain

    Private _autos As Integer = 0

    Public Property Autos() As Integer
        Get
            Return _autos
        End Get
        Set(ByVal Value As Integer)
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

    Public Sub Produzent()
        Do While Me._autos < 100
            Thread.Sleep(100)
            Monitor.Enter(Me)
            Do While Me._autos >= 90
                Me.Invoke(Sub() Me.txtResult.AppendText( _
                    "Produzent legt die Arbeit nieder, da das Lager voll ist!" _
                    & Environment.NewLine))
                Monitor.Wait(Me)
            Loop
            Me._autos += 1
            Me.Invoke(Sub() Me.txtResult.AppendText("Produzierte Autos: " _
                                & _autos.ToString & Environment.NewLine))
            Monitor.PulseAll(Me)
            Monitor.Exit(Me)
        Loop
    End Sub

    Public Sub Konsument(ByVal parameter As Object)
        Dim aufuehrenderThread = CType(parameter, Thread)
        Do
            Thread.Sleep(300)
            Monitor.Enter(Me)
            Do While Me._autos = 0
                Me.Invoke(Sub() Me.txtResult.AppendText( _
                    "Konsumenten können keine Autos kaufen da der Vorrat erschöpft ist." _
                    & Environment.NewLine))
                Monitor.Wait(Me)
            Loop
            Me._autos -= 1
            Me.Invoke(Sub() Me.txtResult.AppendText("Auto mit der Nr. " _
                                & _autos.ToString & " vom Kunden: " _
                                & aufuehrenderThread.ManagedThreadId _
                                & " abgeholt." & Environment.NewLine))
            Monitor.PulseAll(Me)
            Monitor.Exit(Me)
        Loop
    End Sub

End Class