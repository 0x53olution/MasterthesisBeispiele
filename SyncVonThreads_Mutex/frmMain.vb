Option Strict On
Option Explicit On

Imports System.Threading

Public Class frmMain

    Private _isRunning As Boolean = False
    Private _mutexInstanz As New Mutex(True, Application.ProductName, _isRunning)
    Sub New()
        If Not _isRunning Then
            MessageBox.Show( _
                "Eine Anwendung mit gleichem Namen _wird schon ausgeführt!", _
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

    End Sub
End Class
