Option Strict On
Option Explicit On

Imports System.Net.NetworkInformation
Imports LinqAsParallel.DataSetKunde
Imports System.IO

Public Class frmMain

    Private dtsKunde As New DataSetKunde
    Private _DataFile As New FileInfo(Application.StartupPath & "\Datenbank.xml")

    Private Sub cmdGenData_Click(sender As Object, e As EventArgs) Handles cmdGenData.Click
        GeneriereDaten(5000000)
    End Sub

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        Dim Stoppuhr As New Stopwatch
        txtResult.AppendText("Datenbankabfrage NICHT parallelisiert" _
                             & Environment.NewLine)

        Stoppuhr.Start()
        Dim abfrage = From abfrageKunde _
                        In dtsKunde.dtKunde _
                        Where abfrageKunde.Geburtsdatum.Year = 1984 _
                        Select abfrageKunde
        Stoppuhr.Stop()

        txtResult.AppendText("Gefundene Kunden: " & abfrage.Count.ToString _
                             & "; Abfragedauer: " & Stoppuhr.ElapsedTicks.ToString _
                             & " Ticks" & Environment.NewLine)
        txtResult.AppendText("Datenbankabfrage parallelisiert" _
                             & Environment.NewLine)
        Stoppuhr.Reset()

        Stoppuhr.Start()
        Dim abfrageParallel = From abfrageKundeParallel _
                                In dtsKunde.dtKunde.AsParallel _
                                Where abfrageKundeParallel.Geburtsdatum.Year = 1984 _
                                Select abfrageKundeParallel
        Stoppuhr.Stop()

        txtResult.AppendText("Gefundene Kunden: " & abfrageParallel.Count.ToString _
                             & "; Abfragedauer: " & Stoppuhr.ElapsedTicks.ToString _
                             & " Ticks" & Environment.NewLine)
    End Sub

    Private Sub GeneriereDaten(ByVal anzahl As Integer)
        Me.txtResult.AppendText("Datenbankfile wird gelöscht!" _
                                & Environment.NewLine)
        If File.Exists(_DataFile.FullName) Then
            File.Delete(_DataFile.FullName)
        End If
        Me.txtResult.AppendText("Es werden " & anzahl & " Datensätze generiert." _
                                & Environment.NewLine)
        Dim Generator As System.Random = New System.Random()
        With dtsKunde
            .Clear()
            For i As Integer = 0 To anzahl
                Dim geb As New DateTime(Generator.Next(1950, 2000), _
                                        Generator.Next(1, 12), _
                                        Generator.Next(1, 28))
                .dtKunde.AdddtKundeRow("Nachname" & i, "Vorname" & i, geb)
            Next
        End With
        dtsKunde.WriteXml(_DataFile.FullName)
        MessageBox.Show("Daten wurden generiert.", "Info", MessageBoxButtons.OK, _
                        MessageBoxIcon.Information)
        Me.txtResult.AppendText("Daten generiert!" & Environment.NewLine)
    End Sub
End Class
