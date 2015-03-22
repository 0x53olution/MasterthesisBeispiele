Option Strict On
Option Explicit On
Public Class frmMain

    Private Delegate Function Hintergrundarbeit(ByVal n As Integer) As Long
    Private DelegateFib As Hintergrundarbeit

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        'Mehrzeiligestextfeld leeren
        txtResult.Clear()

        Dim asyncResult As IAsyncResult = Nothing

        If CInt(txtN.Text) < 91 Then
            Dim FibKlasse As New Fibonacci
            DelegateFib = New Hintergrundarbeit(AddressOf FibKlasse.BerechneFibonacci)
            asyncResult = DelegateFib.BeginInvoke(CInt(txtN.Text), Nothing, Nothing)
        Else
            txtResult.Text = "n > 91 gibt einen Überlauf in der Long Variable"
        End If

        For i As Integer = 0 To 1000
            'Abbruchbedingung falls die Fibonacci Berechnung fertig ist
            If asyncResult.IsCompleted Then Exit For
            txtResult.AppendText("Schleifendurchlauf Hauptthread: " & i.ToString & vbNewLine)
            System.Threading.Thread.Sleep(100)
        Next

        'Ergebnis der Fibonacciberechnung ausgeben
        txtResult.AppendText("Ergebnis durch Nebenthread: " & DelegateFib.EndInvoke(asyncResult))
    End Sub
End Class

Public Class Fibonacci
    ''' <summary>
    ''' Fibonacci berechnung rekursiv
    ''' </summary>
    ''' <param name="n">Anzahl der Fibonacci Folgen</param>
    ''' <returns>Long</returns>
    ''' <remarks>n>91 ergibt einen Überlauf von Long!!!</remarks>
    Public Function BerechneFibonacci(ByVal n As Integer) As Long
        If n > 2 Then
            Return BerechneFibonacci(n - 1) + BerechneFibonacci(n - 2)
        Else
            Return 1
        End If
    End Function
End Class