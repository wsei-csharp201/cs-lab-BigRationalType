## Krok 5. Operacje arytmetyczne

W kroku tym zdefiniujesz podstawowe działania arytmetyczne i operacje matematyczne na ułamkach.

Wykonuj zadania w podanej kolejności.

### Zadania do wykonania

1. W projekcie _Class library_ dodaj nową klasę. Plik nazwij `BigRationalArithmetic.cs`. 

2. Zmień nazwę klasy na `BigRational`. Dodaj słowo kluczowe `partial`, zmień `class` na `struct`. Korzystasz z funkcjonalności dzielenia klasy na wiele plików [`partial class`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods). Rozbudowę struktury `BigRational` w zakresie tego kroku przeprowadzisz w tym pliku.

3. W projekcie z testami jednostkowymi utwórz plik o nazwie `BigRationalArithmeticUnitTests.cs`. Możesz to wykonać kolejno poleceniami: *Add > New Item .. > Basic Unit Test*. testy jednostkowe związane z implementacją równości ułamków zapisz w tym pliku.

4. Utwórz publiczną metodę o sygnaturze

    ```csharp
    public BigRational Plus(BigRational other)
    ```

    realizującą operację dodawania ułamków.

    Rozważ sytuacje specjalne z `NaN` oraz `PositiveInfinity` czy `NegativeInfinity`.

5. Utwórz publiczną statyczną metodę o sygnaturze

    ```csharp
    public static BigRational Sum(BigRational u1, BigRational u2)
    ```

    realizującą operację obliczania sumy dwóch ułamków.


6. Zmodyfikuj metodę `Sum` tak, aby mogła przyjąć wiele argumentów (ale co najmniej 2).

7. Zdefiniuj przeciążenie operatora `+`.

8. Zrób to samo dla pozostałych operatorów arytmetycznych dwuargumentowych (`-`, `*`, `/`) oraz jednoargumentowego `-` (znak przeciwny), inkrementacji (`++`) i dekrementacji (`--`).

9. Zaimplementuj dla ułamka wybrane metody z [`System.Math`]((https://docs.microsoft.com/en-us/dotnet/api/system.math):
    * `Abs`
    * `Sign`
    * `Floor`
    * `Ceiling`
    * `Max`
    * `Pow`

10. Oczywiście opracuj stosowne testy jednostkowe dla tych nowych funkcjonalności.

### Podpowiedzi

1. W języku Java nie ma możliwości przeciążania operatorów. Zatem opracowując klasę `BigRational` w tym języku, operacje arytmetyczne definiowałbyś tak, jak w zadaniu `Plus` i `Sum`. Później budowałbyś wyrażenia arytmetyczne dla ułamków w trochę nieintuicyjny sposób:

   ```java
   BigRational u = ((new BigRational(1,2)).Plus(new BigRational(1,3))).Multiply(new BigRational(2,3)); // (1/2 + 1/3) * 2/3
   ```

    W C# oraz C++ przeciążanie operatorów zwiększa czytelność i zmniejsza ilość kodu, natomiast błędnie (niedokładnie) wykonane powoduje pojawienie się błędów trudnych do zidentyfikowania (należy zwrócić szczególną uwagę na konstruktory oraz na konwersje w powiązaniu z operacjami arytmetycznymi).

2. Aby przekazać do metody wiele argumentów użyj słowa kluczowego [params](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params).

3. Przeczytaj [dokumentację](https://docs.microsoft.com/en-US/dotnet/csharp/programming-guide/statements-expressions-operators/overloadable-operators) na temat przeciążania operatorów.

4. Jak opracować testy jednostkowe dla dodawania? Musisz zweryfikować prawa dodawania dla liczb wymiernych (formalnie zbiór liczb wymiernych z operacją `+` jest [grupą przemienną](https://pl.wikipedia.org/wiki/Grupa_przemienna)). Zatem:
   * poprawność dodawania ułamków: ( \( \frac{a}{b} + \frac{c}{d} = \frac{ad+bc}{bd} \) )
   * `Zero` jest elementem neutralnym: `u + 0 == 0 + u == u`
   * dodawanie jest operacją przemienną: `u + v == v + u`
   * dodawanie jest operacją łączną: `u + (v + w) == (u + v) + w`
   * dla dowolnego niezerowego elementu `u` istnieje element przeciwny `-u`, tj. taki, że `u + (-u) == (-u) + u == 0`
   * z poprzedniego punktu wynika, że odejmowanie jest operacją przeciwną do dodawania.

5. Jak opracować testy jednostkowe dla mnożenia? Analogicznie, weryfikujesz prawa matematyczne:
   * poprawność mnożenia ( \( \frac{a}{b} \cdot \frac{c}{d} = \frac{ac}{bd} \) )
   * `One` jest elementem neutralnym  `u * 1 == 1 * u == u`
   * mnożenie jest przemienne: `u * v == v * u`
   * dla dowolnego niezerowego elementu `u` istnieje element odwrotny `v`, tj. taki, że `u * v == v * u == 1`. Element ten nazywany jest _odwrotnością_
   * z poprzedniego punktu wynika, że dzielenie jest operacją odwrotną do mnożenia.

6. Formalnie, aby dostarczyć (na tym etapie) pełnego wsparcia dla działań arytmetycznych na ułamkach, musisz wielokrotnie przeciążyć operatory arytmetyczne. W końcu ułamki są liczbami i mają "współgrać" z innymi typami liczbowymi. Zatem, przykładowo, musisz rozważyć następujące przeciążenia metody `Plus()` oraz operatora `+`:

    ```csharp
    BigRational Plus(BigRational u);
    BigRational Plus(int x);
    BigRational Plus(long y);
    //...
    BigRational Plus(float f);
    BigRational Plus(double d);
    //... i.t.d. dla wszystkich typów liczbowych

    BigRational operator+(Ulamek u, Ulamek v);
    BigRational operator+(int x, Ulamek u);
    BigRational operator+(Ulamek u, int x);
    //... i.t.d. dla wszystkich typów liczbowych
    ```

    * Jednak niekoniecznie wszystkie. Jeśli np. zrealizujesz przeciążenie `BigRational Plus(long)`, to nie musisz realizować `BigRational Plus(int)`. W przypadku podania argumentu typu `int` nastąpi automatyczna jego konwersja do `long` i wywołana zostanie pasująca metoda `BigRational Plus(long)`. Podobnie będzie z `float` i `double` oraz z przeciążeniami operatora `+`.

    * Jeśli w projekcie poprawnie zdefiniujesz konwersje (jawne i domyślne), nie będziesz się musiał przejmować tymi przeciążeniami.

[Początek](README.md) | [Krok poprzedni](step04.md) | [Krok następny](step06.md)
