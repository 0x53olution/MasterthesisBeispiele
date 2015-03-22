Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMain

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        'ThreadpoolSimpel()
        'ThreadpoolKomplex()
    End Sub

    Private Sub ThreadpoolSimpel()
        Dim text As String = "Donaudampfschifffahrtsgesellschaft"
        txtResult.AppendText("Eingabewort: Donaudampfschifffahrtsgesellschaft" _
                             & Environment.NewLine)
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Wortlaenge), text)
    End Sub

    Private Sub Wortlaenge(ByVal status As Object)
        Dim eingabe As String = CType(status, String)
        Me.Invoke(Sub() txtResult.AppendText("Wortlänge: " & eingabe.Length.ToString))
    End Sub

    Private Sub ThreadpoolKomplex()
        Dim FibRechner As New Fibonacci
        ThreadPool.QueueUserWorkItem(AddressOf FibRechner.FibWrapper, 40)
        While FibRechner.FibResul = 0
            Thread.Sleep(100)
            Me.txtResult.AppendText("Rechnet..." & Environment.NewLine)
        End While
        Me.txtResult.AppendText("FERTIG; Ergebnis: " & FibRechner.FibResul.ToString)
    End Sub
End Class

Public Class Fibonacci

    Private _fibResult As Long

    Public ReadOnly Property FibResul() As Long
        Get
            Return _fibResult
        End Get
    End Property

    ''' <summary>
    ''' Wrapper für Fibonacciberechnung
    ''' </summary>
    Public Sub FibWrapper(ByVal n As Object)
        _fibResult = BerechneFibonacci(CType(n, Integer))
    End Sub

    ''' <summary>
    ''' Fibonacci berechnung rekursiv
    ''' </summary>
    Private Function BerechneFibonacci(ByVal n As Integer) As Long
        If n > 2 Then
            Return BerechneFibonacci(n - 1) + BerechneFibonacci(n - 2)
        Else
            Return 1
        End If
    End Function
End Class