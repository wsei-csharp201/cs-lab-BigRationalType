## Krok 3. Porównywanie ułamków

W tym etapie zdefiniujesz mechanizmy porównywania ułamków (czyli, dla danych dwóch ułamków, który jest większy, a który mniejszy, a może są równe). Formalnie, są to _operatory relacyjne_.

Wykonuj zadania w podanej kolejności.

### Zadania do wykonania

1. W projekcie _Class library_ dodaj nową klasę. Plik nazwij `BigRationalRelations.cs`.

2. Zmień nazwę klasy na `BigRationalRelations`. Dodaj słowo kluczowe `partial`, zmień `class` na `struct`. Korzystasz z funkcjonalności dzielenia klasy na wiele plików [`partial class`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods). Rozbudowę struktury `BigRational` w zakresie tego kroku przeprowadzisz w tym pliku.

3. W projekcie z testami jednostkowymi utwórz plik o nazwie `BigRationalRelationsUnitTests.cs`. Możesz to wykonać kolejno poleceniami: *Add > New Item .. > Basic Unit Test*. testy jednostkowe związane z implementacją równości ułamków zapisz w tym pliku.

4. Zaimplementuj interfejs [`IComparable`](https://docs.microsoft.com/en-Us/dotnet/api/system.icomparable). Jego implementacja powinna być spójna z `Equals`.

5. Zaimplementuj interfejs [`IComparable<Ulamek>`](https://docs.microsoft.com/en-Us/dotnet/api/system.icomparable-1?view=netstandard-2.0). Jego implementacja powinna być spójna z `Equals<Ulamek>` oraz poprzednim krokiem.

6. Zdefiniuj przeciążenie operatorów relacyjnych (`<`, `<=`, `>`, `>=`).

7. Napisz testy jednostkowe weryfikujące poprawność tych nowych funkcjonalności.

#### Podpowiedzi

1. Poczytaj na temat `IComparable` oraz `IComparer` (<https://www.c-sharpcorner.com/uploadfile/yougerthen/using-icomparable-and-icomparer-to-compare-objects/>). Implementacja interfejsu `IComparable<Ulamek>` wprowadza w zbiorze ułamków relację porządku liniowego - wiele algorytmów zewnętrznych z tej relacji korzysta domyślnie (np. sortowanie i wyszukiwanie binarne).

2. Implementując metodę [`CompareTo()`](https://docs.microsoft.com/pl-pl/dotnet/api/system.icomparable.compareto) musisz spełnić następujące warunki (relacja porządku liniowego):

    > For objects `A`, `B` and `C`, the following must be true:
    > * `A.CompareTo(A)` must return zero.
    > * If `A.CompareTo(B)` returns zero, then `B.CompareTo(A)` must return zero.
    > * If `A.CompareTo(B)` returns zero and `B.CompareTo(C)` returns zero, then `A.CompareTo(C)` must return zero.
    > * If `A.CompareTo(B)` returns a value other than zero, then `B.CompareTo(A)` must return a value of the opposite sign.
    > * If `A.CompareTo(B)` returns a value `x` not equal to zero, and `B.CompareTo(C)` returns a value `y` of the same sign as `x`, then `A.CompareTo(C)` must return a value of the same sign as `x` and `y`.

    Opracuj stosowne testy jednostkowe wg tych wytycznych.

3. Zwróć uwagę, iż implementując `operator <` musisz również zaimplementować operator komplementarny `operator >`. Przy implementacji operatorów `<=` oraz `>=` zadbaj, aby były zgodne z `Equals` i operatorem `==`.

4. Zbadaj - na przykładzie typu `double` - jak zachowuje się porównywanie do `double.NaN`. Zastosuj identyczną logikę w typie `BigRational`.

5. Zbadaj, jak w typie `double` działa `CompareTo()` dla `BigRational.PositiveInfinity' oraz `BigRational.NegativeInfinity'.


[Początek](README.md) | [Krok poprzedni](step02.md) | [Krok następny](step04.md)