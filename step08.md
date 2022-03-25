## Krok 8. Propozycja zadań i problemów do rozwiązania

1. Wykorzystując opracowaną strukturę spróbuj rozwiązać elementarnie proste problemy na SPOJ:
   * [SIMPLE - Make It Simple](https://www.spoj.com/problems/SIMPLE/)
   * [SUMFRAC - Sum of fractions](https://www.spoj.com/problems/SUMFRAC/)

2. W ramach nowego projektu, opracuj **strukturę** `BigRational` (wariant _immutable_). Twój kod będzie wymagał tylko kilku drobnych modyfikacji. Dowiesz się w praktyce, jaka jest różnica między strukturą a klasą.

3. W ramach nowego projektu opracuj strukturę `BigRational` (wariant _immutable_), ale bazującą na wewnętrznej reprezentacji ułamka w formie niedokładnej - jako wartość typu [`decimal`](https://docs.microsoft.com/en-us/dotnet/api/system.decimal).

   ```csharp
   class Ulamek
   {
      private readonly decimal value;
      public long Licznik => throw new NotImplementedException(); // get
      public long Mianownik => throw new NotImplementedException(); // get

      // konstruktory
      public Ulamek( long licznik, long mianownik )
      {
        ...
      }
      // ...
   }
   ```

   W zasadzie zadanie to polega na opakowaniu (ang. _wrapping_, _wrapper class_) typu `decimal` tak, aby przez odbiorcę widziany był jako typ ułamka (pary liczb: licznik i mianownik, o określonych właściwościach i operacjach). Oczywiście trzeba bezie wykonać pracę związaną z przeciążeniami operatorów i - w szczególności - poprawnym zdefiniowaniem `Equals`. Ponieważ reprezentacja jest niedokładna, prawdopodobnie wprowadzisz parametr określający dokładność reprezentacji (i dokładność obliczeń). Np. pytając się, czy `u == v` oczekujesz odpowiedzi, że są równe z dokładnością do np. 10 miejsc po przecinku. Zastosowanie `decimal` wobec `double` zdecydowanie poprawia dokładność reprezentacji i obliczeń.

4. Wyzwanie: Wiedząc, co to są ułamki, jak się je reprezentuje i jak wykonuje się na nich obliczenia, spróbuj rozwiązać klasyczne problemy na SPOJ:
   * [ENUMRTNL - Enumeration of rationals](https://www.spoj.com/problems/ENUMRTNL/)
   * [GNUM - Guess number!](https://www.spoj.com/problems/GNUM/)

[Początek](README.md) | [Krok poprzedni](step07.md) |
