<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtN = New System.Windows.Forms.TextBox()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.lblN = New System.Windows.Forms.Label()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblLaufzeit = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtN
        '
        Me.txtN.Location = New System.Drawing.Point(324, 41)
        Me.txtN.Name = "txtN"
        Me.txtN.Size = New System.Drawing.Size(128, 20)
        Me.txtN.TabIndex = 0
        '
        'cmdStart
        '
        Me.cmdStart.Location = New System.Drawing.Point(296, 12)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(75, 23)
        Me.cmdStart.TabIndex = 1
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'lblN
        '
        Me.lblN.AutoSize = True
        Me.lblN.Location = New System.Drawing.Point(296, 44)
        Me.lblN.Name = "lblN"
        Me.lblN.Size = New System.Drawing.Size(22, 13)
        Me.lblN.TabIndex = 3
        Me.lblN.Text = "f(n)"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(12, 12)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResult.Size = New System.Drawing.Size(278, 373)
        Me.txtResult.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(296, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Laufzeit:"
        '
        'lblLaufzeit
        '
        Me.lblLaufzeit.AutoSize = True
        Me.lblLaufzeit.Location = New System.Drawing.Point(349, 64)
        Me.lblLaufzeit.Name = "lblLaufzeit"
        Me.lblLaufzeit.Size = New System.Drawing.Size(16, 13)
        Me.lblLaufzeit.TabIndex = 6
        Me.lblLaufzeit.Text = "..."
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 397)
        Me.Controls.Add(Me.lblLaufzeit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.lblN)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.txtN)
        Me.Name = "frmMain"
        Me.Text = "DelegateAsync"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtN As System.Windows.Forms.TextBox
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents lblN As System.Windows.Forms.Label
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLaufzeit As System.Windows.Forms.Label

End Class
