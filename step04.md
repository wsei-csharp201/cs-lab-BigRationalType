## Krok 4. Konwersje

W tym etapie zdefiniujesz mechanizmy konwersji ułamków do innych typów - konwersje automatyczne i rzutowanie.

Wykonuj zadania w podanej kolejności.

### Zadania do wykonania

1. W projekcie _Class library_ dodaj nową klasę. Plik nazwij `BigRationalConversions.cs`.

2. Zmień nazwę klasy na `BigRational`. Dodaj słowo kluczowe `partial`, zmień `class` na `struct`. Korzystasz z funkcjonalności dzielenia klasy na wiele plików [`partial class`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods). Rozbudowę struktury `BigRational` w zakresie tego kroku przeprowadzisz w tym pliku.

3. W projekcie z testami jednostkowymi utwórz plik o nazwie `BigRationalConversionsUnitTests.cs`. Możesz to wykonać kolejno poleceniami: *Add > New Item .. > Basic Unit Test*. Testy jednostkowe związane z implementacją równości ułamków zapisz w tym pliku.

4. Zaimplementuj konwersję automatyczną z typów liczbowych całkowitych (`byte`, ..., `uint`..., `long`) na `BigRational`. Zrealizujesz to za pomocą przeciążenia operatora odpowiedniego typu z użyciem słowa kluczowego `implicit`.

5. Zaimplementuj konwersję jawną z typu `BigRational` na pozostałe typy liczbowe z użyciem słowa kluczowego `explicit`. Będą to konwersje z utratą dokładności.

6. Uruchom wszystkie testy jednostkowe. Jest pewne prawdopodobieństwo, że wprowadzając do klasy mechanizmy domyślnych konwersji, niektóre z nich mogą teraz nie zostać zaliczone.

7. Nierozsądnym byłoby dostarczanie konwersji niejawnej z `double` na `BigRational`. Poprzednio zrealizowałeś to w [kroku 1](step01.md) za pomocą konstruktora `BigRational(double)` (w nomenklaturze C++ nazywa się go konstruktorem konwertującym). Zamiana liczby, np. `2.124267` na odpowiadający jej ułamek jest to procesem złożonym i czasami pracochłonnym. Wprowadzenie niejawnego działania konwersji spowoduje co prawda bardziej zwarty i ładniejszy zapis wyrażeń arytmetycznych (np. `BigRational.Half + 2.73415` ), ale kodujący może nie zwrócić uwagi na fakt długotrwałego obliczania takiego wyrażenia. Zatem, zdefiniuj jawną konwersję z `double` na `BigRational`. Kodujący świadomie będzie musiał rzutować.

8. Opracuj stosowne testy jednostkowe dla konwersji liczbowych.

9. Przenieś kod implementujący `BigRational Parse(string)` oraz `bool TryParse(string, out BigRational)` który realizowałeś w [kroku 1](step01.md) do tej części klasy. raczej tu jest jego miejsce.

10. Statyczna klasa [`System.Convert`](https://docs.microsoft.com/en-us/dotnet/api/system.convert) zbiera wszystkie metody konwertujące z jednego typu do innego. Na przykład [`Convert.ToInt64()`](https://docs.microsoft.com/en-us/dotnet/api/system.convert.toint64?view=netstandard-2.0). Załóżmy, że naszym celem jest dołączenie do tej listy jeszcze jednej pozycji: `Convert.ToInt64(BigRational)` (czyli konwersji z `BigRational` na `long`). Jest to możliwe za pomocą implementacji interfejsu `IConvertible`. Zaimplementuj tę konwersję (pozostałe pozostaw z `NotImplementedException`).

#### Podpowiedzi

1. Poczytaj [o konwersjach ogólnie](https://docs.microsoft.com/en-US/dotnet/csharp/programming-guide/statements-expressions-operators/using-conversion-operators) oraz o konwersjach [domyślnych](https://docs.microsoft.com/en-US/dotnet/csharp/language-reference/keywords/implicit) i [jawnych](https://docs.microsoft.com/en-US/dotnet/csharp/language-reference/keywords/explicit). Wprowadzenie konwersji nie wymaga implementowania jakiegokolwiek interfejsu.

2. Poczytaj o implementacji interfejsu [`IConvertible`](https://docs.microsoft.com/en-Us/dotnet/api/system.iconvertible). Dołączanie do klasy `Convert` metod konwersji własnych typów na typy tam predefiniowane wymaga zaimplementowania interfejsu `IConvertible` -  musimy napisać kod dla kilkunastu (kilkudziesięciu) metod. Zaletą takiej implementacji jest fakt, iż z naszej klasy można korzystać w standardowy sposób. Przeczytaj również: [Type conversion example in C# .NET using the IConvertible interface](https://dotnetcodr.com/2015/04/22/type-conversion-example-in-c-net-using-the-iconvertible-interface/).

3. Poczytaj o konwersjach w .NET oraz C#: [Type Conversion in the .NET Framework](https://docs.microsoft.com/en-us/dotnet/standard/base-types/type-conversion).

4. Raczej nie stosuje się konwersji jawnych i niejawnych dla `string` - od tego są dostarczane dedykowane metody (`Parse` oraz `TryParse`).

[Początek](README.md) | [Krok poprzedni](step03.md) | [Krok następny](step05.md)