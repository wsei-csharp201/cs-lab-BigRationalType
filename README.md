# Ćwiczenie - implementacja ułamków w C#

* Autor: _Krzysztof Molenda_
* Wersja: 1.4 (2021.03.16)

Twoim zadaniem jest realizacja w C# typu `Rational`, reprezentującego matematyczną koncepcję liczby wymiernej. Realizacja typu powinna być spójna, kompletna, dobrze udokumentowana i przetestowana. Powinna dostarczać naturalnych dla użytkownika mechanizmów użycia (naturalnych, tzn. jak w matematyce oraz podobnych do tych, które stosowane są w analogicznych konstrukcjach języka C#).

## Cele

Zadanie to ma charakter edukacyjny. Jest ono realizowane na większości kursów programowania obiektowego, ze względu na odpowiednią złożoność oraz zakres tematyczny.

Realizując je:

1. Poznasz kolejne etapy projektowania typu.
2. Nauczysz się panować nad złożonym kodem oraz wieloma implementowanymi funkcjonalnościami. Dowiesz się, jak 'łańcuchować' kod.
3. Poznasz techniki tworzenia testów jednostkowych dla Twojego kodu. Dowiesz się, na czym polega technika TDD (_Test-Driven Development_).
4. Nauczysz się poprawnie dokumentować swój kod i generować dokumentację, np. w `html`.

Można jednak znaleźć inne uzasadnienia utworzenia takiego typu danych w C#.

Uruchom, w ramach eksperymentu, poniższy kod C#, który wyjaśni wszystko:

```csharp {dotnescript=csharp }
Console.WriteLine( 0.1 + 0.2 == 0.3 );
```

Błędny wynik spowodowany jest niedokładnością binarnej reprezentacji liczb zmiennoprzecinkowych. Częściowym rozwiązaniem tego problemu może byc praca na wartościach dokładnych - liczbach reprezentowanych w formie ułamków.

Platformy .NET dostarczają predefiniowane typy liczbowe:

* `sbyte`, `byte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong` - reprezentacja liczb całkowitych, bez utraty dokładności, w formie typu wartościowego

* `float`, `double` - zmiennoprzecinkowa reprezentacja liczb rzeczywistych, z potencjalną utratą dokładności, w formie typu wartościowego (odpowiednio 32-bitowa i 64-bitowa)

* Ciekawostka: w .Net 5 zaimplementowano typ `System.Half` - 16-bitowa zmiennoprzecinkowa reprezentacja liczby rzeczywistej (<https://docs.microsoft.com/en-us/dotnet/api/system.half>), w formie typu wartościowego

* `decimal` - reprezentacja liczb rzeczywistych, ze zwiększoną dokładnością, ale pamięciożerna (128 bitów), w formie typu wartościowego

* `BigInteger` - reprezentacja liczby całkowitej o dowolnym (praktycznie nieograniczonym) zakresie (potrzebne w kryptografii) - w przestrzeni nazw `System.Numerics`

* `Complex` - reprezentacja liczby zespolonej - w przestrzeni nazw `System.Numerics`

W standardowych bibliotekach .NET (C#) jednak nie ma typu realizującego koncepcję ułamka (`Rational`).  Jej implementacja jest za to w pakiecie [Microsoft Solver Foundation](https://msdn.microsoft.com/en-us/library/microsoft.solverfoundation.common.rational(v=vs.93).aspx), zrealizowana w formie struktury, w dość specyficzny sposób i z bardzo ubogą dokumentacją (prawdopodobnie tylko dla potrzeb narzędzia *Solver*). Implementacja ta nie jest rozwijana/utrzymywana.

Przy tworzeniu specjalistycznego oprogramowania może okazać się potrzebną realizacja typu `Rational`, np. do prezentacji liczb w formie ułamkowej, do wykonania obliczeń dokładnych na ułamkach.

Zadanie to można zrealizować w 3 wariantach:

1. Reprezentacja wewnętrzna ułamka (`Rational32`) bazująca na typie `int` (dla licznika i mianownika).
2. Reprezentacja wewnętrzna ułamka (`Rational64`) bazująca na typie `ulong` (dla licznika i mianownika), wymagane będzie zapamiętanie znaku ułamka.
3. Reprezentacja wewnętrzna ułamka (`BigRational`) bazująca na typie `BigInteger` (dla licznika i mianownika).

## Założenia ogólne

Założenia ogólne dotyczące implementacji typu `Rational`:

1. Implementowany typ realizuje matematyczną koncepcję liczby wymiernej (<https://en.wikipedia.org/wiki/Rational_number>).

2. Realizacja typu *w formie struktury* języka C#.

3. Instancje typu są niezmiennicze (_immutable_).

4. _Licznik_ i _mianownik_ widziane są przez użytkownika końcowego jako wartości typu `BigInteger`.

5. Typ `Rational` reprezentuje koncepcję liczby, zatem jego instancje "współdziałają" z liczbami reprezentowanymi przez inne typy (`int`, ..., `double`, ...). Współdziałanie to obejmuje konwersje (jawne i niejawne), porównywanie (operatory relacyjne) oraz działania arytmetyczne.

6. **Wszystkie** publiczne składniki klasy są przetestowane (odpowiednie testy jednostkowe).

7. Klasa oraz jej składniki publiczne są udokumentowane (dokumentacja XML w kodzie).

8. Dokumentacja API w `html`.

9. W miarę możliwości należy wykorzystać nowe możliwości C# (C#8, ...).

## Etapy realizacji

W poniższym opracowaniu przedstawione są etapy realizacji typu w wariancie `BigRational`. Po zakończeniu ćwiczenia sugerowane jest - dla utrwalenia wiedzy i umiejętności - realizacja typów w wariantach `Rational32` oraz `Rational32`.

Zadanie zrealizuj w Visual Studio 2022 i .Net6. Możesz również wykorzystać lekkie środowisko VS Code lub JetBrains Rider.

Każdy z podanych poniżej kroków przedstawia:

1. instrukcje do wykonania
2. podpowiedzi
3. gotowe pełne lub częściowe rozwiązanie

Sugeruję próbę samodzielnego rozwiązania w oparciu o instrukcje i podpowiedzi, zaś później weryfikację w oparciu o przedstawioną propozycję rozwiązania!

> ⚠️ Kolejne kroki realizowane są w trybie przyrastającej wiedzy i umiejętności. Na początku podawane są zagadnienia i techniki stosunkowo proste, dokładnie omówione, z biegiem czasu prezentowane są zagadnienia trudniejsze, omawiane szybciej i pobieżnie. Zatem, wykonuj wszystkie polecenia w podanej kolejności, stosując zasadę: **najpierw spróbuj sam, później zobacz, jak robię to ja**.

- - -

* [Krok 0. Konfiguracja środowiska projektu](step00.md)
    > utworzysz _solution_ oraz 3 projekty: typu _Class Library_, _Console application_ oraz _Unit test_.

* [Krok 1. Podstawowa funkcjonalność](step01.md)
    > zdefiniujesz wewnętrzną reprezentację danych ułamka (pola) zapewniając niezmienniczość tworzonych obiektów, zdefiniujesz konstruktory oraz tekstową reprezentację ułamka, utworzysz testy jednostkowe

* [Krok 2. Równość/tożsamość ułamków](step02.md)
    > określisz, kiedy dwa ułamki są sobie równe - tożsame, "takie same" (implementacja `IEquatable`, `IEquatable<Rational>`, generowanie _hashkodu_, przeciążenie operatorów `==` oraz `!=`), utworzysz testy jednostkowe

* [Krok 3. Operacje arytmetyczne](step03.md)
    > zdefiniujesz podstawowe działania arytmetyczne na ułamkach (przeciążenie operatorów arytmetycznych `+`, `*`, ... ) oraz wybrane funkcje użytkowe (`Min`, `Max`, `Pow`, ...) wzorując się na wbudowanych typach liczbowych, utworzysz testy jednostkowe

* [Krok 4. Porównywanie ułamków](step04.md)
    > zdefiniujesz podstawowe operatory relacyjne (implementacja `IComparable<Rational>`, przeciążenie operatorów `<`, `>`), utworzysz testy jednostkowe

* [Krok 5. Konwersje](step05.md)
    > zdefiniujesz mechanizmy konwersji (jawnej, niejawnej) z i do ułamka, utworzysz testy jednostkowe

* [Krok 6. Rozszerzenia klasy `Rational`](step06.md)
    > poznasz mechanizmy rozszerzania funkcjonalności wcześniej zaprojektowanej klasy

* Krok 7. Refaktoryzacja i dokumentacja kodu
    > uporządkujesz kod czyniąc go bardziej czytelnym i łatwiejszym do zarządzania, wprowadzisz optymalizacje, opracujesz/uzupełnisz dokumentację, wygenerujesz dokumentację w `html`

* Krok ostatni. Zadania i wyzwania
    > na bazie zrealizowanego projektu rozwiążesz zadania o podobnym charakterze