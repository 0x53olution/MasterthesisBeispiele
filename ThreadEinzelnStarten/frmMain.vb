Option Strict On
Option Explicit On

Imports System.Threading
Imports System.Text.RegularExpressions

Public Class frmMain

    ''' <summary>
    ''' Einstiegspunkt. Bitte die gewünschte Methode einkommentieren.
    ''' </summary>
    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        'ThreadStartBeispiel()
        'ThreadJoinBeispiel()
        'ThreadAbortBeispiel()
        'ThreadInterruptBeispiel()
        'ThreadPriorityBeispiel()
    End Sub

    Private Sub ThreadStartBeispiel()
        Dim hintergrundThread As New Thread(AddressOf KomplexeBerechnung)
        hintergrundThread.IsBackground = True
        hintergrundThread.Start(1)
        KomplexeBerechnung(2)
    End Sub

    Private Sub ThreadJoinBeispiel()
        Dim hintergrundThread As New Thread(AddressOf KomplexeBerechnungMsg)
        hintergrundThread.IsBackground = True
        hintergrundThread.Start(1)
        hintergrundThread.Join()
        Me.SetText("Der GUI Thread wartet bis der Join vollzogen ist." _
                                & Environment.NewLine)
        'Me.txtResult.AppendText("Der GUI Thread wartet bis der Join vollzogen ist." _
        '                        & Environment.NewLine)
        KomplexeBerechnung(2)
        Me.SetText("Hintergrundthread ist mit dem GUI Thread gejoint worden.")
    End Sub

    Private Sub ThreadAbortBeispiel()
        Dim hintergrundThread As New Thread(AddressOf KomplexeBerechnungMsg)
        hintergrundThread.IsBackground = True
        hintergrundThread.Start(1)
        Dim hintergrundthreadFertig = hintergrundThread.Join(500)
        hintergrundThread.Abort()
        If hintergrundthreadFertig Then
            Me.SetText("Der Hintergrundthread hat die Zeit NICHT überschritten." _
                                    & Environment.NewLine)
        Else
            Me.SetText("Der Hintergrundthread wurde abgebrochen." _
                                    & Environment.NewLine)
        End If
        KomplexeBerechnung(2)
    End Sub

    Private Sub ThreadInterruptBeispiel()
        Dim hintergrundThread As New Thread(AddressOf StringParsen)
        hintergrundThread.IsBackground = True
        Dim parameter = New Object() {hintergrundThread, _
                                      "paarweise und paare in der gleichen paarung"}
        Me.txtResult.AppendText(parameter(1).ToString)
        hintergrundThread.Start(parameter)
        Try
            hintergrundThread.Join()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            hintergrundThread.Interrupt()
        End Try
    End Sub

    Private Sub ThreadPriorityBeispiel()
        Dim hintergrundThread As New Thread(AddressOf KomplexeBerechnung)
        hintergrundThread.Priority = ThreadPriority.Highest
        hintergrundThread.Start(1)
        KomplexeBerechnung(2)
    End Sub

    Private Sub KomplexeBerechnung(ByVal threadnr As Object)
        For i As Integer = 0 To 10
            Thread.Sleep(500)
            Me.Invoke(Sub() txtResult.AppendText("ThreadId: " & CType(threadnr, String) & _
                       " KomplexeBerechnung wird berechnet" & Environment.NewLine))
            'Me.SetText("ThreadId: " & CType(threadnr, String) & _
            '           " KomplexeBerechnung wird berechnet" & Environment.NewLine)
        Next
    End Sub

    Private Sub KomplexeBerechnungMsg(ByVal threadnr As Object)
        Try
            Thread.Sleep(1000)
            MessageBox.Show("ThreadId: " & CType(threadnr, String) & _
                            " KomplexeBerechnung fertig", "Information", _
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As ThreadAbortException
            MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub StringParsen(ByVal parameter As Object)
        Dim ausfuehrenderThread As Thread = CType(CType(parameter, Object())(0), Thread)
        Dim textToParse As String = CType(CType(parameter, Object())(1), String)
        Dim filterPattern As String = "((paar)[a-z]*)"
        Dim foundMatches As MatchCollection
        foundMatches = Regex.Matches(textToParse, filterPattern)
        For Each match In foundMatches
            If ausfuehrenderThread.ThreadState = ThreadState.WaitSleepJoin Then
                ausfuehrenderThread.Interrupt()
                Try
                    ausfuehrenderThread.Join()
                Catch ex As Exception
                    MessageBox.Show(ex.Message.ToString)
                End Try
            End If
            MessageBox.Show(match.ToString)
        Next
    End Sub

    ''' <summary>
    ''' Threadsicherer Aufruf auf ein Form Control
    ''' Delegate übernimmt den Aufruf des Steuerelements
    ''' </summary>
    Private Delegate Sub SetTextCallback(ByVal [text] As String)
    Private Sub SetText(ByVal [text] As String)
        If Me.txtResult.InvokeRequired Then
            Dim del As New SetTextCallback(AddressOf SetText)
            Me.Invoke(del, New Object() {[text]})
        Else
            'Wird vom GUI Thread benutzt
            Me.txtResult.AppendText([text])
        End If
    End Sub
End Class