<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNeuronalesNetz
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
      Me.btnEinenSchrittAusführen = New Infragistics.Win.Misc.UltraButton()
      Me.btnNetzInitialisieren = New Infragistics.Win.Misc.UltraButton()
      Me.txtFehler = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
      Me.lblFehler = New Infragistics.Win.Misc.UltraLabel()
      CType(Me.txtFehler, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'btnEinenSchrittAusführen
      '
      Me.btnEinenSchrittAusführen.Location = New System.Drawing.Point(191, 12)
      Me.btnEinenSchrittAusführen.Name = "btnEinenSchrittAusführen"
      Me.btnEinenSchrittAusführen.Size = New System.Drawing.Size(180, 35)
      Me.btnEinenSchrittAusführen.TabIndex = 0
      Me.btnEinenSchrittAusführen.Text = "einen Schritt ausführen"
      '
      'btnNetzInitialisieren
      '
      Me.btnNetzInitialisieren.Location = New System.Drawing.Point(5, 12)
      Me.btnNetzInitialisieren.Name = "btnNetzInitialisieren"
      Me.btnNetzInitialisieren.Size = New System.Drawing.Size(180, 35)
      Me.btnNetzInitialisieren.TabIndex = 1
      Me.btnNetzInitialisieren.Text = "Netz initialisieren"
      '
      'txtFehler
      '
      Me.txtFehler.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.txtFehler.Location = New System.Drawing.Point(1313, 25)
      Me.txtFehler.Name = "txtFehler"
      Me.txtFehler.Size = New System.Drawing.Size(113, 21)
      Me.txtFehler.TabIndex = 2
      '
      'lblFehler
      '
      Me.lblFehler.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.lblFehler.Location = New System.Drawing.Point(1246, 28)
      Me.lblFehler.Name = "lblFehler"
      Me.lblFehler.Size = New System.Drawing.Size(61, 18)
      Me.lblFehler.TabIndex = 3
      Me.lblFehler.Text = "Fehler"
      '
      'frmNeuronalesNetz
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(1460, 705)
      Me.Controls.Add(Me.lblFehler)
      Me.Controls.Add(Me.txtFehler)
      Me.Controls.Add(Me.btnNetzInitialisieren)
      Me.Controls.Add(Me.btnEinenSchrittAusführen)
      Me.Name = "frmNeuronalesNetz"
      Me.Text = "frmNeuronalesNetz"
      CType(Me.txtFehler, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub

   Friend WithEvents btnEinenSchrittAusführen As Infragistics.Win.Misc.UltraButton
   Friend WithEvents btnNetzInitialisieren As Infragistics.Win.Misc.UltraButton
   Friend WithEvents txtFehler As Infragistics.Win.UltraWinEditors.UltraTextEditor
   Friend WithEvents lblFehler As Infragistics.Win.Misc.UltraLabel
End Class
