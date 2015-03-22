Option Strict On
Option Explicit On
Public Class frmMain

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim Prime As New Primefactor
        Dim zaehler As Integer = 0
        Dim SchleifenErgebnis As ParallelLoopResult
        Dim _ergebnisParallelFor() As Integer = Nothing
        Dim x As Integer = 0
        Dim number As Double = 600851475143

        Dim Stoppuhr As New Stopwatch
        Stoppuhr.Start()

        'Nirmale For-Schleife
        For i As Integer = 3 To CInt(Math.Sqrt(number)) Step 2
            If number Mod i = 0 AndAlso Prime.IsPrime(i) = 1 Then
                txtResult.AppendText("For-Schleife Ergebnis: " & i.ToString & Environment.NewLine)
            End If
        Next

        Stoppuhr.Stop()
        txtResult.AppendText("Dauer For-Schleife: " & Stoppuhr.ElapsedMilliseconds.ToString & _
                             " in Millisekunden" & Environment.NewLine)

        Stoppuhr.Reset()
        Stoppuhr.Start()

        'Parallel.For-Schleife
        SchleifenErgebnis = Parallel.For(3, CInt(Math.Sqrt(number)), _
                Sub(i)
                    If number Mod i = 0 AndAlso Prime.IsPrime(i) = 1 Then
                        ReDim Preserve _ergebnisParallelFor(x)
                        _ergebnisParallelFor(x) = i
                        x += 1
                    End If
                End Sub)

        Stoppuhr.Stop()

        For j As Integer = 0 To _ergebnisParallelFor.Length - 1
            txtResult.AppendText("ParallelFor Ergebnis: " & _ergebnisParallelFor(j).ToString _
                                 & Environment.NewLine)
        Next
        txtResult.AppendText("Dauer Parallel.For-Schleife: " & Stoppuhr.ElapsedMilliseconds.ToString _
                             & " in Millisekunden" & Environment.NewLine)
    End Sub
End Class

''' <summary>
''' Primfaktorzerlegung
''' </summary>
''' <remarks></remarks>
Public Class Primefactor
    Public Function IsPrime(ByVal p As Double) As Integer
        If p Mod 2 = 0 Then
            Return 0
        End If
        For i As Integer = 3 To CInt(Math.Sqrt(p)) Step 2
            If p Mod i = 0 Then
                Return 0
            End If
        Next
        Return 1
    End Function
End Class