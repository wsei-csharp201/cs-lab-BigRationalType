﻿---
html:
  embed_local_images: false
  embed_svg: true
  offline: false
  toc: false

print_background: false
---

## Krok 1. Podstawowa funkcjonalność

W kroku tym zdefiniujesz podstawową funkcjonalność projektowanego typu - zdefiniujesz wewnętrzną reprezentację danych ułamka (pola) zapewniając niezmienniczość tworzonych obiektów, zdefiniujesz konstruktory oraz tekstową reprezentację ułamka, określisz zasady dostępu do składników typu, utworzysz testy jednostkowe.

Wykonuj zadania w podanej kolejności.

Będziesz intensywnie korzystał z typu [`BigInteger`](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger) - zapoznaj się z jego dokumentacją.

### Zadania do wykonania - część 1

1. W projekcie typu _Class Library_ utwórz publiczną strukturę `BigRational` (w pliku `BigRational.cs`).

2. Zdefiniuj pola struktury (`numerator` - licznik, `denominator` - mianownik) jako wartości typu `BigInteger`.

3. Zapewnij odpowiedni poziom hermetyzacji (pola są `private`, wartości licznika i mianownika są udostępniane publicznie za pomocą gettersów `Numerator` oraz `Denominator`).

4. Pamiętaj, aby zapewnić niezmienniczość obiektów typu `BigRational`.

5. Dostarcz konstruktory obiektów typu `BigRational`. Rozważ różne sytuacje. Pamiętaj o `0` w mianowniku!
   Opracuj testy jednostkowe weryfikujące poprawność działania konstruktorów oraz _gettersów_.

6. Przyjmij, że tekstową reprezentacją ułamka jest postać:

    `[znak]<<licznik>>/<<mianownik>>`

    na przykład `-2/3` lub `-7/2`, ale nie `2/-3` oraz nie `1 1/2`.

    Opracuj odpowiednie przeciążenie metody `ToString()`.

    Opracuj testy jednostkowe weryfikujące poprawność reprezentacji tekstowej ułamka.

7. Zapewnij, aby ułamek zapamiętany był w postaci nieskracalnej (licznik i mianownik są względnie pierwsze) i zestandaryzowanej - znak ułamka jest znakiem licznika. Opracuj testy jednostkowe weryfikujące tę funkcjonalność.

---

#### Podpowiedzi - część 1

1. _Niezmienniczość_ obiektów zapewnisz słowem kluczowym [readonly](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly) (po to zresztą zostało wprowadzone do języka). Jeśli jednak ustanowi ono zbyt duże restrykcje, będziesz musiał zadbać o niewykonywanie żadnych zmian w zainicjowanych polach klasy i udostępniać je jedynie do odczytu. Od C# 7.2 można deklarować [readonly struct](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#readonly-struct) - wymusi ono na tobie określone działania.

2. Dla uproszczenia zapisu, tam gdzie nie jest to zbyt skomplikowane, wykorzystuj [notację lambda](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions).

3. Implementacja konstruktorów -- musisz utworzyć przeciążone konstruktory:

    ```csharp
    BigRational(BigInteger value) { ... }
    BigRational(BigInteger numerator, BigInteger denominator) { ... }
    BigRational(long numerator, long denominator) { ... }
    BigRational(ulong numerator, ulong denominator) { ... }
    ...
    //i być może dla wygody użytkownika
    // dla innych typów całkowitoliczbowych
    // w różnych kombinacjach
    ```

    oczywiście je łańcuchując w odpowiedni sposób. Możesz również skorzystać z mechanizmu [parametrów opcjonalnych](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments).

4. ⚠️ Problemy z konstruktorem domyślnym (bezargumentowym). Ponieważ `BigRational` jest strukturą, konstruktor ten tworzony jest automatycznie i nie możesz go zmienić. Oznacza to, że ułamek utworzony za jego pomocą **zawsze** będzie miał pola zainicjowane domyślnymi wartościami typu `BigInteger` (czyli `numerator = denominator = 0`). Ten problem nie pojawiłby się, jeśli projektowany typ byłby klasą - mógłbyś samodzielnie określić działanie konstruktora bezargumentowego.

    > Problem powoduje określone konsekwencje i wybory w kontekście projektowanego typu:
    > 1. Ułamek jest liczbą oraz liczby w C# reprezentowane są jako struktury ⇨ `BigRational` też będzie reprezentowany jako struktura.
    > 2. Skoro ułamek jest liczbą, to liczba ta powinna być "kompatybilna" ze zmiennoprzecinkową reprezentacją (`float`, `double`, `decimal`). W szczególności, w typach tych zdefiniowano (w standardzie) takie sytuacje jak `NaN`, `PositiveInfinity` czy `NegativeInfinity` oraz arytmetykę rozszerzoną na nieskończonościach.
    > 3. W definicji typu musisz rozważyć:<br />
    > ◌ `0/0` nie ma matematycznego (i programistycznego) sensu, zatem taką wartość zaklasyfikujemy jako `NaN`,<br />
    > ◌ `1/0` możemy interpretować jako +∞ zaś `-1/0` jako -∞. Ogólnie każdy ułamek o mianowniku `0` i liczniku niezerowym jest nieskończonością, jeśli licznik jest dodatni jest to `PositiveInfinity`, jeśli ujemną to `NegativeInfinity`,<br />
    > ◌ Zaimplementuj metody statyczne: `bool IsNaN`, `bool IsInfinity`, `bool IsNegativeInfinity`, `bool IsPositiveInfinity`, `IsFinite` - wzorując się na typie `double` (patrz [Double.IsNaN(Double) Method](https://docs.microsoft.com/en-us/dotnet/api/system.double.isnan)).
    > 4. Ułamki `1/2`,  `2/4` czy `3/6` reprezentują **tę samą** wartość (formalnie mówimy o relacji równoważności w zbiorze ułamków: $\frac{a}{b} = \frac{c}{d} ⇔ ad = bc, \quad b ≠ 0, d ≠ 0$ i klasach abstrakcji). Możemy się umówić, że wszystkie ułamki "tego samego typu" mają jednego reprezentanta - odpowiadający im ułamek nieskracalny.

5. Upraszczając ułamki skorzystasz z algorytmu Euklidesa obliczania NWD (ang. _GCD_). Nie znajdziesz go w klasie [`System.Math`](https://msdn.microsoft.com/en-us/library/system.math). Zatem:

    * albo zaimplementujesz go samodzielnie, np. na podstawie informacji z [Wikibooks](https://pl.wikibooks.org/wiki/Kody_%C5%BAr%C3%B3d%C5%82owe/Algorytm_Euklidesa#C/C++,_C#,_Java)
        > UWAGA: przed użyciem, sprawdź poprawność działania tego algorytmu dla rozwiązania Twojego problemu → np. jak zachowuje się dla liczb o różnych znakach.

    * albo skorzystasz z tego, dostarczonego w klasie [`System.Numerics.BigInteger`](https://msdn.microsoft.com/en-us/library/system.numerics.biginteger.greatestcommondivisor(v=vs.110).aspx).

    Proces upraszczania należy umieścić w konstruktorach po to, by zapamiętany ułamek był już nieskracalny.

6. Upraszczanie jest działaniem potencjalnie pochłaniającym czas (patrz: [Algorytm Euklidesa](https://pl.wikipedia.org/wiki/Algorytm_Euklidesa)) - w przypadku dużych liczb budujących ułamek. Rozważ możliwość selektywnego włączania lub wyłączania tego procesu. Możesz to zrealizować, poprzez zdefiniowanie prywatnego konstruktora, np.:

    ```csharp
    private BigRational(BigInteger numerator, BigInteger denominator, bool normalization = true)
    {
        // ...
        if( normalization )
        {
            // ... wywołanie algorytmu standaryzującego znak ułamka
            // ... oraz sprowadzającego do postaci nieskracalnej
        }
        // ...
    }
    ```

    Z tego konstruktora zawsze będziesz korzystał budując ułamki wewnątrz projektowanej klasy, gdy będziesz pewien, że nie jest wymagana standaryzacja czy upraszczanie. Pozostałe konstruktory - z domyślnie włączonym upraszczaniem i standaryzacją - udostępnisz światowi zewnętrznemu.  

7. Ponieważ testów jednostkowy dla Twojej klasy będzie dużo, rozbij je na wiele klas i plików. Dla potrzeb testowania podstawowej funkcjonalności z tego kroku, zmień nazwę klasy testującej np. na `BigRationalCoreUnitTests`.

8. Aby usprawnić proces testowania, zamiast metody testującej z atrybutem `[TestMethod]` możesz stosować atrybut `[DataTestMethod]` z podaniem w kolejnych, niższych wierszach, przykładowych zestawów testowych:

    ````csharp
    [DataTestMethod]
    [DataRow(1, 3, 1, 3)]
    [DataRow(3, 1, 3, 1)]
    [DataRow(0, 2, 0, 1)]
    public void Konstruktor_PoprawneDaneBezUpraszczania_OK(int licznik, int mianownik, int expextedLicznik, int expectedMianownik)
    {
        // arrange - realizowane jako DataRow

        // act
        Rational32 u = new Rational32(licznik, mianownik);

        // assert
        Assert.AreEqual(u.Licznik, expextedLicznik);
        Assert.AreEqual(u.Mianownik, expectedMianownik);
    }
    ````

    Uwaga: W tym przypadku parametrami `DataRow()` muszą być literały całkowite (`int`) - zatem testowanie przeprowadzisz tylko dla tego typu danych.

---

### Zadania do wykonania - część 2

Funkcjonalności z tej części mogą być zrealizowane już teraz, ale w niektórych przypadkach łatwiej będzie je zdefiniować równolegle, w kolejnych krokach (np. po implementacji _równości ułamków_ czy operatorów rzutowania) - lub obecny kod później zrefaktoryzować.

1. Zaimplementuj konstruktor tworzący ułamek na podstawie tekstowej jego reprezentacji, tzn. `new BigInteger("-2/3") ⟶ numerator = -2, denominator = 3`. Konstruktor ten powinien być działaniem odwrotnym do metody `ToString()`, tzn. jeśli utworzysz ułamek, następnie wyeksportujesz go do postaci tekstowej i ponownie utworzysz ułamek na jej podstawie, to otrzymasz "taki sam" ułamek:

    ```csharp
    var u = new BigRational(1,2);
    var s = u.ToString();
    var v = new BigRational(s);
    // u oraz v są "takie same"
    ```

    Rozsądnym będzie wcześniejsze napisanie metod parsujących i wykorzystanie w konstruktorze.

2. Wzorując się na typie `int` (formalnie [`System.Int32`](https://docs.microsoft.com/en-us/dotnet/api/system.int32)) zaimplementuj metody `BigRational Parse(string)` oraz `bool TryParse(string, out BigRational)`, które przetwarzają poprawnie uformowany napis do ułamka.

    > Zastanów się i zaimplementuj zgłaszanie odpowiednich wyjątków.

3. Wzorując się na typie `int` dodaj do klasy stałe `MaxValue` oraz `MinValue`, ale dla `Rational32`.

4. Zaimplementuj stałe ułamki: `ZERO` (jako `0/1`), `ONE` (jako `1/1`) oraz `HALF` (jako `1/2`).

5. Zaimplementuj konwersję `Rational32` do typu `double` (`ToDouble()`), `float` (`ToSingle()`) oraz `decimal` (`ToDecimal()`).

6. Zaimplementuj konstruktor `Rational32(double)` oraz `Rational32(decimal)` tak, aby korespondował z wcześniej opracowanymi konwersjami do tych typów.

7. Zaimplementuj konwersję ułamka do typu `int` - z utratą informacji - będzie to wyznaczenie części całkowitej z dzielenia.

8. Utwórz stosowne testy jednostkowe weryfikujące poprawność opracowanych metod.

#### Podpowiedzi - część 2

1. W języku C# zwyczajowo, jeśli potrzebujemy tylko sygnatury metody (np. aby kod się poprawnie kompilował), a implementację pozostawiamy na później, zamiast kodu zgłaszamy wyjątek [`NotImplementedException`](https://docs.microsoft.com/pl-pl/dotnet/api/system.notimplementedexception?view=netstandard-2.0).

2. Do konwersji z `string` do `Rational32` będziesz musiał parsować napis. Rozważ zastosowanie metody [string.Split](https://docs.microsoft.com/pl-pl/dotnet/csharp/how-to/parse-strings-using-split). Możesz również zastosować [wyrażenia regularne (REGEX)](https://docs.microsoft.com/pl-pl/dotnet/standard/base-types/regular-expressions).

3. Zadania dotyczące konwersji na inne typy liczbowe powtórzysz przy implementacji operatorów konwersji jawnej (rzutowanie) i niejawnej, w kolejnych krokach. Teraz wykonaj te implementacje i opracuj testy jednostkowe. Później, gdy będziesz refaktoryzował kod, testy będą "pilnowały" jego poprawności.

4. W języku C# stałe definiowane są za pomocą słowa kluczowego [`const`](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const). Definiowana stała musi być jasno określona lub możliwa do ustalenia jeszcze w trakcie kompilacji. W naszym przypadku zasymulujesz działanie stałej zmienną tylko do odczytu (prawdopodobnie użyjesz `public static readonly`). Przeczytaj: [C# Const, ReadOnly & Static ReadOnly Differences](https://www.arungudelli.com/tutorial/c-sharp/10-differences-between-constant-vs-readonly-static-readonly-fields/).

    > Statyczne składniki klasy incjowane są w [statycznym konstruktorze](https://docs.microsoft.com/pl-pl/dotnet/csharp/programming-guide/classes-and-structs/static-constructors). Składniki zadeklarowane jako `static readonly` muszą być inicjowane albo w statycznym konstruktorze, albo jako część swojej deklaracji. Dokumentacja Microsoft zaleca, iż - jeśli nie ma potrzeby definiowania statycznego konstruktora w klasie - to składniki `static readonly` inicjujemy w ich deklaracji, ze względów wydajnościowych.

---

* Nagranie video - część 1, reprezentacja ułamka, konstruktor:
  
  [![](http://img.youtube.com/vi/PPgQLlP_ZEw/0.jpg)](http://www.youtube.com/watch?v=PPgQLlP_ZEw "")

* Nagranie video - część 2, testy jednostkowe dla reprezentacji ułamka i konstruktora:

  [![](http://img.youtube.com/vi/vIGrxzxhpVs/0.jpg)](http://www.youtube.com/watch?v=vIGrxzxhpVs "")

* Nagranie video - część 3, normalizacja ułamka (standaryzacja znaku, upraszczanie), testy jednostkowe, metody pomocnicze:

  [![](http://img.youtube.com/vi/nk5zFofIMho/0.jpg)](http://www.youtube.com/watch?v=nk5zFofIMho "")

* Pliki z kodem po wykonaniu kroku: [./step01-solution/solution_snapshot](./step01-solution/solution_snapshot)

---

[Początek](README.md) | [Krok poprzedni](step00.md) | [Krok następny](step02.md)