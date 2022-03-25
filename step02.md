---
html:
  embed_local_images: false
  embed_svg: true
  offline: false
  toc: false

print_background: false
---

## Krok 2. Równość ułamków

Krok ten będzie miał niewiele zadań do wykonania (literalnie), ale jest prawdopodobnie najtrudniejszy.

Określisz w nim pojęcie "takie same", czyli kiedy dwa ułamki są sobie równe (implementacja `IEquatable`, `IEquatable<BigRational>`, generowanie _hashkodu_, przeciążenie operatorów `==` oraz `!=`), utworzysz testy jednostkowe.

Wykonuj zadania w podanej kolejności. Wykorzystaj technikę TDD (_Test Driven Development_) - najpier testy, potem kod.

### Zadania do wykonania

1. W projekcie typu _Class library_ dodaj nową klasę. Plik nazwij `BigRationalEquals.cs`.

2. Zmień nazwę klasy na `struct BigRational` (formalnie: powtórz nagłówek struktury `BigRational`). Dodaj słowo kluczowe `partial` przed `struct`. Korzystasz z funkcjonalności dzielenia kodu klasy lub struktury na wiele plików [`partial class`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods). Rozbudowę struktury `BigRational` w zakresie tego kroku przeprowadzisz w tym pliku. W przypadku dzielenia kodu na wiele plików słowo kluczowe `partial` musi być powtórzone w każdym nagłówku klasy/struktury w każdym pliku. W pliku `BigRational.cs` możesz dodać `global using System.Numerics;` (wprowadzone w C#10) - dzięki temu w całym projekcie biblioteki i we wszystkich jego plikach z kodem ta przestrzeń nazw będzie dostępna.

3. W projekcie z testami jednostkowymi utwórz plik o nazwie `BigRationalEqualsUnitTests.cs` typu _class_. Możesz to wykonać kolejno poleceniami: <kbd>Add > Class...</kbd>, a następnie - podglądając pierwszy plik z testami jednostkowymi - uzupełnij kod klasy testującej `BigRationalEqualsUnitTests` tak, aby całe _solution_ poprawnie się kompilowało. Testy jednostkowe związane z implementacją równości ułamków zapisz w tym pliku.

4. Opracuj odpowiednie przesłonięcie metody `Equals(Object)`. Równocześnie **MUSISZ** przesłonić `GetHashCode()` - w C#10 wykorzystaj klasę `HashCode` i metodę Combine()`.

5. Zaimplementuj interfejs `IEquatable<BigRational>` (wymaga implementacji _strongly typed_ `Equals<T>(T other)`).

6. Zaimplementuj statyczną wersję metody `Equals` o sygnaturze:

    ```csharp
    public static bool Equals(BigRational u1, BigRational u2)
    ```

7. Zaimplementuj przeciążenie operatora `==`. Będziesz **MUSIAŁ** równocześnie zaimplementować przeciążenie operatora `!=`.

8. Wszystkie powyższe metody muszą być wzajemnie spójne!

9. Opracuj testy jednostkowe.

10. Zwróć uwagę na sytuacje specjalne (`NaN`, `+Infinity`, `-Infinity`). W C# w typie zmiennoprzecinkowym jakakolwiek relacja z `NaN` daje w efekcie `false`!

### Podpowiedzi

1. Poczytaj o przesłanianiu `Equals` w C#:
  
   * <https://github.com/loganfranken/overriding-equals-in-c-sharp>

   * <https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/equality-operators>

   * <https://docs.microsoft.com/en-us/dotnet/api/system.object.equals>

   * <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type>

2. Poczytaj o implementowaniu interfejsu [`IEquatable<T>`](https://docs.microsoft.com/en-US/dotnet/api/system.iequatable), a w szczególności o metodzie [`IEquatable<T>.Equals(T)`](https://docs.microsoft.com/pl-pl/dotnet/api/system.iequatable).
  
3. _Równość_ jest [relacją równoważności](https://pl.wikipedia.org/wiki/Relacja_r%C3%B3wnowa%C5%BCno%C5%9Bci) (ściśle zdefiniowane pojęcie matematyczne), spełnia zatem następujące warunki:

    a. _zwrotność_: `x.Equals(x)` zwraca `true`

    b. _symetria_: `x.Equals(y)` zwraca tę samą wartość, co `y.Equals(x)`

    c. _przechodniość_: jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true`

    Wymagania dotyczące `Equals` uzupełnione są o kolejne warunki:

    d. dla niepustych (nie-`null`) referencji `x` oraz `y`, wielokrotne wywołanie `x.Equals(y)` konsekwentnie zwraca `true` lub konsekwentnie zwraca `false`, o ile żadna informacja związana z `x` oraz `y` nie uległy zmianie (nielosowość działania, niezależność od stanu aplikacji)

    e. `x.Equals(null)` zwraca `false`

    f. `x.Equals(y)` zwraca `false`, gdy `y != false`, jesli typ `x` oraz typ `y` są nieporównywalne

    g. `x.Equals(y)` zwraca `true`, gdy `y != false` oraz `x` i `y` są tego samego typu i są _semantycznie_ tożsame

    h. _Strongly typed_ `Equals<T>(T other)` musi działać tak samo jak `Equals(object)`

    i. `x.Equals(y)` zwraca `true` jesli oba `x` oraz `y` są `NaN` (dla typów liczbowych)

    j. `x.Equals(y)` zwraca `true` jesli oba `x` oraz `y` są tego samego rodzaju nieskończonością (dla typów liczbowych)

    k. `GetHashCode` powinien zwracać tę samą wartość, gdy jest wywołany dla obiektów tożsamych (smantycznie równych). UWAGA: nie ma wymogu działania przeciwnego, tzn. aby zwracane wartości były różne, gdy dwa obiekty nie są równe.

    l. Metoda statyczna `Equals(x, y)` oraz operator `==` są sobie równoważne
        * dla obu argumentów `null` zwraca `true` (w C# `null == null`)
        * zwraca `false`, jeśli jeden z argumentów jest `null` a drugi nie-`null`
        * dla obu argumentów nie-`null` zwraca taki sam wynik, co metoda instancji `Equals`

    m. Operator `!=` zwraca przeciwne wyniki niż operator `==`

    n. `Equals` nie zgłasza **żadnych** wyjątków

    Podane warunki przydadzą Ci się do opracowania testów jednostkowych Twojej implementacji równości ułamków.

   > 🛈: Dwa ułamki są sobie równe, jeżeli należą do tej samej _klasy równoważności_ względem relacji równości (też pojęcie _stricte_ matematyczne). Oznacza to, że np. 2/4 == 1/2. Ale tym problemem nie musimy się przejmować, ponieważ nasza implementacja przewiduje upraszczanie ułamków. Stąd, porównywanie ułamków może odbyć się _pole po polu_, jak domyślnie dla typów strukturalnych.

4. Prawa logiki przydają się przy budowaniu testów jednostkowych. Przykładowo, dla testu przechodniości relacji `Equals`:

    > jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true`

    W logice, zdanie ( (p ⇒ q) ⇔ (¬p ∨ q) ) jest tautologią - czyli zawsze prawdziwe. Zatem, zamiast używać implikacji, możemy ją zastapić odpowiednią alternatywą.

    W naszym przypadku, zdanie `(a ∧ b ⇒ c)`, gdzie `a` oznacza `x.Equals(y)`, `b` oznacza `y.Equals(z)`, zaś `c` oznacza `x.Equals(z)` zapiszemy w równoważny sposób, bez użycia implikacji: `¬(a ∧ b) ∨ c`.

    Korzystając z prawa de Morgana, przekształcimy je w `¬a ∨ ¬b ∨ c`.

    Pozostaje w teście sprawdzić, czy dla dowolnych danych testowych zdanie to jest zawsze prawdziwe. Jeśli nie - to powodem bedzie wadliwa implementacja `Equals`.

    ```csharp
    [DataTestMethod]
    [DataRow(1, 2, 1, 2, 1, 2)]
    [DataRow(1, 2, 2, 4, 3, 6)]
    [DataRow(-1, 2, 2, -4, -3, 6)]
    [DataRow(1, 2, -1, 2, 1, 2)]
    [DataRow(1, 2, 1, 3, 1, 3)]
    [DataRow(1, 2, 1, 2, 1, 3)]
    public void Equals_Przechodniosc_ZPrawLogiki_DowolneDane(int u1l, int u1m, int u2l, int u2m, int u3l, int u3m)
    {
        BigRational x = new (u1l, u1m);
        BigRational y = new (u2l, u2m);
        BigRational z = new (u3l, u3m);

        Assert.IsTrue( !x.Equals(y) || !y.Equals(z) || x.Equals(z) );
    }
    ```

5. Przeanalizuj i ustal różnice między trzema sposobami porównywania:

    * `object.ReferenceEquals(object objA, object objB)`
    * `objA.Equals(objB)`
    * `objA == objB`

6. Co to jest [`NullReferenceException`](https://docs.microsoft.com/pl-pl/dotnet/api/system.nullreferenceexception). Kiedy się może pojawić?

7. Przeanalizuj porównywanie do `null` (w odniesieniu do typów referencyjnych):
    * `if( x == null ) ...`
    * `if( x is null ) ...`
    * `if( x.Equals(null) ) ...`
    * `if( object.ReferenceEquals( x, null ) ...`

    Zwróć uwagę na ukryte niebezpieczeństwa. W naszym przypadku projektowany typ jest wartościowy i niedopuszcza wartości `null`, zatem powyższe sytuacje są hipotetyczne.

8. Porównywać możesz obiekty tego samego typu (czasami dopuszczalne dla podtypu). Jak sprawdzić, czy dwa obiekty są tego samego typu?

    Przeanalizuj poniższy kod:

    ```csharp
    //jakie są różnice (czy są)
    if (!(obj is Ulamek)) return false;
    if (obj.GetType() != typeof(Ulamek)) return false;
    if (this.GetType() != obj.GetType()) return false;
    if (!object.ReferenceEquals(this.GetType(), obj.GetType())) return false;
    ```


9. Aby wzbogacić funkcjonalnosc typu, powinniśmy również przewidzieć porównywanie ułamków do liczb innych typów (całkowitych czy zmiennoprzecinkowych). Całkowicie uzasadnionym jest np.
  
      ```csharp
      BigRational(1,2) == 0.5;
      BigRational(3,1) == 3;
      5L == BigRational(5,1);
      BigRational(1/0) == 1.0/0.0;
      ...
      ```
    Teoretycznie można to zrobić już teraz, wprowadzając przeciążone implementacje dla `Equals` oraz dla `==`, jednak po wdrożeniu mechanizmów konwersji jawnej i niejawnej otrzymamy tę samą funkcjonalność (a nawet większą) - wtedy należałoby refaktoryzować opracowany kod lub nawet go usuwać.

10. Dodatkowo (częste pytania egzaminacyjne):
  
    * jaka jest różnica między operatorem `is` oraz `as`
    * co zwraca operator `typeof`, a co metoda `GetType()`
    * czym jest `Type`

---

[Początek](README.md) | [Krok poprzedni](step01.md) | [Krok następny](step03.md)