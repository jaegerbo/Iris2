Public Class clsNeuron

   Public Sub New(Name As String)
      _Name = Name
   End Sub

   Private _Id As Guid = Guid.NewGuid
   Public ReadOnly Property Id() As Guid
      Get
         Return _Id
      End Get
   End Property

   Private _Name As String = String.Empty
   Public Property Name() As String
      Get
         Return _Name
      End Get
      Set(ByVal value As String)
         _Name = value
      End Set
   End Property

   Private _Eingangswerte As New Generic.List(Of Double)
   Public Property Eingangswerte() As Generic.List(Of Double)
      Get
         Return _Eingangswerte
      End Get
      Set(ByVal value As Generic.List(Of Double))
         _Eingangswerte = value
      End Set
   End Property

   Private _Gewichtungen As New Generic.List(Of Double)
   Public Property Gewichtungen() As Generic.List(Of Double)
      Get
         Return _Gewichtungen
      End Get
      Set(ByVal value As Generic.List(Of Double))
         _Gewichtungen = value
      End Set
   End Property

   Private _Schwellenwert As Double = 0
   Public Property Schwellenwert() As Double
      Get
         Return _Schwellenwert
      End Get
      Set(ByVal value As Double)
         _Schwellenwert = value
      End Set
   End Property

   Private _Ausgangswert As Double = 0
   Public ReadOnly Property Ausgangswert() As Double
      Get
         Return _Ausgangswert
      End Get
   End Property

   Private Function Übertragungsfunktion() As Double
      ' Zweck:    Die Eingabewerte unter berücksichtigung der Gewichtungen zusammenfassen
      Dim gewichteterEingang As Double = 0
      Try
         For i As Integer = 0 To _Eingangswerte.Count - 1
            gewichteterEingang += _Eingangswerte(i) * _Gewichtungen(i)
         Next
      Catch ex As Exception
         Stop
      End Try
      Return gewichteterEingang
   End Function

   Public Sub AktivierungsfunktionBerechnen()
      ' Zweck:    den gewichteten Eingang verarbeiten und einen Ausgabewert erzeugen
      Try
         ' die Eingangswerte mit ihrer Gewichtung multilizieren und addieren
         Dim gewichteterEingang As Double = Übertragungsfunktion()

         ' den Ausgangswert berechnen
         _Ausgangswert = sigmoid(gewichteterEingang) + _Schwellenwert

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Function sigmoid(x As Double) As Double
      Return 1 / (1 + Math.Exp(-x))
   End Function

   Public Overrides Function ToString() As String
      Dim Text As String = String.Empty
      Try
         ' Eingangswerte zusammenstellen
         Dim EingangswerteString As String = "E: "
         If _Eingangswerte IsNot Nothing Then
            For Each Wert As Double In _Eingangswerte
               If EingangswerteString <> "E: " Then
                  EingangswerteString &= $", "
               End If
               EingangswerteString &= $"( {Format(Wert, "0.00")} )"
            Next
         End If

         ' Gewichtungen zusammenstellen
         Dim GewichtungenString As String = "G: "
         If _Gewichtungen IsNot Nothing Then
            For Each Wert As Double In _Gewichtungen
               If GewichtungenString <> "G: " Then
                  GewichtungenString &= $", "
               End If
               GewichtungenString &= $"( {Format(Wert, "0.00")} )"
            Next
         End If

         Text = $"{_Name}  (Sigmoid-Funk. mit Schwellenwert = {_Schwellenwert})" & vbNewLine &
                EingangswerteString & vbNewLine &
                GewichtungenString & vbNewLine &
                $"Ausgangswert = {Format(_Ausgangswert, "0.00")}"
      Catch ex As Exception
         Stop
      End Try
      Return Text
   End Function
End Class
