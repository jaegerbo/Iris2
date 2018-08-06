Public Class frmNeuronalesNetz

   Private _NeuronHeight As Integer = 60
   Private _NeuronWidth As Integer = 240
   Private _Schichtabstand As Integer = 40

   Private _TrainingsdatenPointer As Integer = 0
   Private _TestdatenPointer As Integer = 0

   Private _Netz As clsNeuronalesNetz
   Private _Trainingsdaten As clsIrisDatensatzListe
   Private _Testdaten As clsIrisDatensatzListe

   Public Sub erstelleNetz(Netz As clsNeuronalesNetz, Trainingsdaten As clsIrisDatensatzListe, Testdaten As clsIrisDatensatzListe)
      ' Zweck:    Das gegebene Netz anzeigen
      Try
         _Netz = Netz
         _Trainingsdaten = Trainingsdaten
         _Testdaten = Testdaten

         NetzAufbauen()

      Catch ex As Exception
         Stop
      End Try
   End Sub
   Private Sub NetzAufbauen()
      ' Zweck:    Das gegebene Netz anzeigen
      Try
         Dim leftOffset As Integer = 40
         Dim topOffset As Integer = 100

         ' Eingangsschicht erstellen
         Dim Nr As Integer = 0
         For Each Neuron As clsNeuron In _Netz.Eingangsneuronen
            Dim txtControl As TextBox = createNeuron(Neuron, leftOffset + (Nr * _NeuronWidth), topOffset)
            Me.Controls.Add(txtControl)
            Nr += 1
         Next

         ' Zwischenschichten erstellen
         Nr = 0
         Dim Schichtnummer As Integer = 1
         For Each Schicht As Generic.List(Of clsNeuron) In _Netz.Zwischenschichten
            topOffset += Schichtnummer * _NeuronHeight + _Schichtabstand

            For Each Neuron As clsNeuron In Schicht
               Dim txtControl As TextBox = createNeuron(Neuron, leftOffset + (Nr * _NeuronWidth), topOffset)
               Me.Controls.Add(txtControl)
               Nr += 1
            Next

            Schichtnummer += 1
         Next

         ' Ausgangsschicht erstellen
         Nr = 0
         topOffset += _NeuronHeight + _Schichtabstand
         For Each Neuron As clsNeuron In _Netz.Ausgangsneuronen
            Dim txtControl As TextBox = createNeuron(Neuron, leftOffset + (Nr * _NeuronWidth), topOffset)
            Me.Controls.Add(txtControl)
            Nr += 1
         Next

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Function createNeuron(Neuron As clsNeuron, left As Integer, top As Integer) As TextBox
      ' Zweck:    Für das gegebene Neuron eine Textbox erzeugen
      Dim txtControl As TextBox = Nothing
      Try
         ' prüfen, ob das Control bereits vorhanden ist
         If Me.Controls.ContainsKey(Neuron.Name) = True Then
            txtControl = Me.Controls.Item(Neuron.Name)
         Else
            txtControl = New TextBox
         End If

         With txtControl
            .Name = Neuron.Name
            .Multiline = True
            .Text = Neuron.ToString
            .Height = _NeuronHeight
            .Width = _NeuronWidth
            .Top = top
            .Left = left
         End With

      Catch ex As Exception
         Stop
      End Try
      Return txtControl
   End Function

   Private Sub btnNetzInitialisieren_Click(sender As Object, e As EventArgs) Handles btnNetzInitialisieren.Click
      ' Zweck:    Dem Netz zufällige Gewichtungen zwischen 1 und -1 zuordnen.
      Try
         _Netz.mitZufälligenGewichtungenInitialisieren()
         NetzAufbauen()
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnEinenSchrittAusführen_Click(sender As Object, e As EventArgs) Handles btnEinenSchrittAusführen.Click
      ' Zweck:    Einen Berechnungsschritt ausführen
      Try
         ' Testdatensatz ermitteln und die Eingangswerte des Neuron füllen
         Dim IrisDatensatz As clsIrisDatensatz = _Trainingsdaten(_TrainingsdatenPointer)
         Dim Eingangswerte(3) As Double
         Eingangswerte(0) = IrisDatensatz.BreiteBlütenblatt
         Eingangswerte(1) = IrisDatensatz.LängeBlütenblatt
         Eingangswerte(2) = IrisDatensatz.BreiteKelchblatt
         Eingangswerte(3) = IrisDatensatz.LängeKelchblatt

         ' die zu erwartenden Ausgangswerte ermitteln
         Dim erwarteteAusgangswerte As New Generic.List(Of Double)
         erwarteteAusgangswerte.Add(IrisDatensatz.Art1Normalisiert)
         erwarteteAusgangswerte.Add(IrisDatensatz.Art2Normalisiert)
         erwarteteAusgangswerte.Add(IrisDatensatz.Art3Normalisiert)

         Dim ErgebnisNeuron As clsNeuron = _Netz.calculateOnce(Eingangswerte, erwarteteAusgangswerte)

         NetzAufbauen()

         ' das erwartete Neuron einfärben
         If IrisDatensatz.Art1Normalisiert = 1 Then
            Controls.Item("A-1").BackColor = Color.LightGreen
            Controls.Item("A-2").BackColor = Color.White
            Controls.Item("A-3").BackColor = Color.White
         ElseIf IrisDatensatz.Art2Normalisiert = 1 Then
            Controls.Item("A-1").BackColor = Color.White
            Controls.Item("A-2").BackColor = Color.LightGreen
            Controls.Item("A-3").BackColor = Color.White
         Else
            Controls.Item("A-1").BackColor = Color.White
            Controls.Item("A-2").BackColor = Color.White
            Controls.Item("A-3").BackColor = Color.LightGreen
         End If

         ' das ErgebnisNeuron einfärben, wenn es nicht das erwartete Neuron ist

         _TestdatenPointer += 1
      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class