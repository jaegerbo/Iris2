Public Class clsNeuronalesNetz

   Public Sub New(Topologie As Generic.List(Of Integer))
      ' Zweck: über die Topologieliste wird definiert, aus wieviel Schichten das Netz besteht, und wie groß jede Schicht ist
      Try
         _Topologie = Topologie

         ' Eingangsschicht erstellen
         _Eingangsneuronen = New Generic.List(Of clsNeuron)
         For i As Integer = 1 To Topologie(0)
            Dim Neuron As New clsNeuron($"E-{i.ToString}")

            ' Eingangswerte und Gewichtungen erstellen
            Neuron.Eingangswerte.Add(0.0)
            Neuron.Gewichtungen.Add(0.0)

            _Eingangsneuronen.Add(Neuron)
            _NeuronListe.Add(Neuron)
         Next

         ' Zwischenschichten erstellen
         Dim AnzahlEingangswerte As Integer = _Eingangsneuronen.Count
         _Zwischenschichten = New Generic.List(Of Generic.List(Of clsNeuron))
         For s As Integer = 1 To _Topologie.Count - 2
            Dim Schicht As New Generic.List(Of clsNeuron)
            For i As Integer = 1 To Topologie(s)

               Dim Neuron As New clsNeuron($"Z-{s.ToString}-{i.ToString}")

               ' Eingangswerte und Gewichtungen erstellen
               For Nr As Integer = 1 To AnzahlEingangswerte
                  Neuron.Eingangswerte.Add(0.0)
                  Neuron.Gewichtungen.Add(0.0)
               Next

               Schicht.Add(Neuron)
               _NeuronListe.Add(Neuron)
            Next
            _Zwischenschichten.Add(Schicht)
            AnzahlEingangswerte = Schicht.Count
         Next

         ' Ausgangsschicht erstellen
         _Ausgangsneuronen = New Generic.List(Of clsNeuron)
         For i As Integer = 1 To Topologie(_Topologie.Count - 1)
            Dim Neuron As New clsNeuron($"A-{i.ToString}")

            ' Eingangswerte und Gewichtungen erstellen
            For Nr As Integer = 1 To AnzahlEingangswerte
               Neuron.Eingangswerte.Add(0.0)
               Neuron.Gewichtungen.Add(0.0)
            Next

            _Ausgangsneuronen.Add(Neuron)
            _NeuronListe.Add(Neuron)
         Next

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private _Topologie As Generic.List(Of Integer)
   Public Property Topologie() As Generic.List(Of Integer)
      Get
         Return _Topologie
      End Get
      Set(ByVal value As Generic.List(Of Integer))
         _Topologie = value
      End Set
   End Property

   Private _Eingangsneuronen As Generic.List(Of clsNeuron)
   Public Property Eingangsneuronen() As Generic.List(Of clsNeuron)
      Get
         Return _Eingangsneuronen
      End Get
      Set(ByVal value As Generic.List(Of clsNeuron))
         _Eingangsneuronen = value
      End Set
   End Property

   Private _Ausgangsneuronen As Generic.List(Of clsNeuron)
   Public Property Ausgangsneuronen() As Generic.List(Of clsNeuron)
      Get
         Return _Ausgangsneuronen
      End Get
      Set(ByVal value As Generic.List(Of clsNeuron))
         _Ausgangsneuronen = value
      End Set
   End Property

   Private _Zwischenschichten As Generic.List(Of Generic.List(Of clsNeuron))
   Public Property Zwischenschichten() As Generic.List(Of Generic.List(Of clsNeuron))
      Get
         Return _Zwischenschichten
      End Get
      Set(ByVal value As Generic.List(Of Generic.List(Of clsNeuron)))
         _Zwischenschichten = value
      End Set
   End Property

   Private _NeuronListe As New Generic.List(Of clsNeuron)
   Public ReadOnly Property Neuronliste() As Generic.List(Of clsNeuron)
      Get
         Return _NeuronListe
      End Get
   End Property

   Private _Lernrate As Double = -0.1
   Public Property Lernrate() As Double
      Get
         Return _Lernrate
      End Get
      Set(ByVal value As Double)
         _Lernrate = value
      End Set
   End Property

   Public Sub mitZufälligenGewichtungenInitialisieren()
      ' Zweck:    Dem Netz zufällige Gewichtungen zwischen 1 und -1 zuordnen.
      Try
         Dim Zufall As New Random(Now.Millisecond)

         For Each Neuron As clsNeuron In _NeuronListe
            Dim neueGewichtungen As New Generic.List(Of Double)

            For Each Gewichtung As Double In Neuron.Gewichtungen
               Gewichtung = CDbl(1 - (Zufall.NextDouble * 2))
               neueGewichtungen.Add(Gewichtung)
            Next

            Neuron.Gewichtungen = neueGewichtungen
         Next

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Public Function calculateOnce(Eingangswerte As Double(), erwarteteAusgangswerte As Generic.List(Of Double)) As clsNeuron
      ' Zweck:    Das Netz einmal rechnen lassen
      Dim ErgebnisNeuron As clsNeuron = Nothing
      Try
         ' Eingangsneuronen füllen
         _Eingangsneuronen(0).Eingangswerte = New Generic.List(Of Double)({Eingangswerte(0)})
         _Eingangsneuronen(1).Eingangswerte = New Generic.List(Of Double)({Eingangswerte(1)})
         _Eingangsneuronen(2).Eingangswerte = New Generic.List(Of Double)({Eingangswerte(2)})
         _Eingangsneuronen(3).Eingangswerte = New Generic.List(Of Double)({Eingangswerte(3)})

         ' Eingangsneuronen berechnen, und dabei ein Array mit den Ausgangswerten erstellen
         Dim Ausgangswerte As New Generic.List(Of Double)
         For Each Neuron As clsNeuron In _Eingangsneuronen
            Neuron.AktivierungsfunktionBerechnen()
            Ausgangswerte.Add(Neuron.Ausgangswert)
         Next

         ' Zwischenschichten berechnen
         For Each Schicht As Generic.List(Of clsNeuron) In _Zwischenschichten

            ' einzelne Zwischenschicht berechnen
            For Each Neuron As clsNeuron In Schicht
               ' Neuronen mit den Ausgangswerten der vorherigen Schicht füllen und berechnen
               Neuron.Eingangswerte = Ausgangswerte
               Neuron.AktivierungsfunktionBerechnen()
            Next

            ' neue Ausgangswerte ermitteln
            Ausgangswerte.Clear()
            For Each Neuron As clsNeuron In Schicht
               Ausgangswerte.Add(Neuron.Ausgangswert)
            Next

         Next

         ' Ausgangsschicht berechnen
         For Each Neuron As clsNeuron In _Ausgangsneuronen
            ' Neuronen mit den Ausgangswerten der vorherigen Schicht füllen und berechnen
            Neuron.Eingangswerte = Ausgangswerte
            Neuron.AktivierungsfunktionBerechnen()
         Next

         ' Das Neuron mit dem größten Ausgangswert ermitteln
         ErgebnisNeuron = _Ausgangsneuronen(0)
         For Each Neuron As clsNeuron In _Ausgangsneuronen
            If Neuron.Ausgangswert > ErgebnisNeuron.Ausgangswert Then
               ErgebnisNeuron = Neuron
            End If
         Next

         ' Fehler berechnen, als Summe der quadratischen Abweichungen
         Dim Fehler As Double = 0
         For i As Integer = 0 To _Ausgangsneuronen.Count - 1
            Fehler += Math.Pow(_Ausgangsneuronen(i).Ausgangswert * erwarteteAusgangswerte(i), 2)
         Next
         Fehler *= -1

         ' die Gewichtungen jedes Neurons anpassen
         For Each Neuron As clsNeuron In _NeuronListe
            Dim neueGewichtungen As New Generic.List(Of Double)

            For Each Gewichtung As Double In Neuron.Gewichtungen
               Dim neueGewichtung As Double = Fehler / Gewichtung * _Lernrate
               neueGewichtungen.Add(neueGewichtung)
            Next

            Neuron.Gewichtungen = neueGewichtungen

         Next
      Catch ex As Exception
         Stop
      End Try
      Return ErgebnisNeuron
   End Function

End Class
