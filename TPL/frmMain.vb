Option Strict On
Option Explicit On

Imports System.Threading.Tasks
Public Class frmMain

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim FibKlasse As New Fibonacci
        Dim FibTask As Task(Of Long) = Task(Of Long).Factory.StartNew(Function() _
                                                        FibKlasse.BerechneFibonacci(35))

        While FibTask.IsCompleted = False
            txtResult.AppendText("Task ist " & FibTask.Status.ToString _
                                 & Environment.NewLine)
        End While
        txtResult.AppendText("Ausgabe: " & FibTask.Result)
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