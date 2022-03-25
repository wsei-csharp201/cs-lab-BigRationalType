## Krok 6. Rozszerzenia klasy `BigRational`

W kroku tym poznasz mechanizmy rozszerzania funkcjonalności wcześniej zrealizowanej (i zamkniętej) klasy.

Wykonuj zadania w podanej kolejności.

### Zadania do wykonania

1. Zastanów się nad możliwością rozszerzenia typu `BigRational` na typ `BigRationalPro` przez dziedziczenie, wprowadzając dodatkową funkcjonalność:

    > mieszana reprezentacja tekstowa ułamka (znak, część całkowita, spacja, część ułamkowa zapisana w formie ułamka właściwego)
    > ```[znak][część całkowita] «licznik»/«mianownik»```
    > np. `-2 3/4` dla ułamka niewłaściwego, ale dla ułamka właściwego `3/4`.

    Spróbuj wykonać to zadanie, ale w innym projekcie typu _class library_.

2. Zamknij klasę `BigRational` uniemożliwiając jej dziedziczenie. UWAGA: zamykając klasę projekt z poprzedniego punktu nie będzie się kompilował.

3. Rozszerz klasę `String` o metodę `ToBigRational()`, konwertującą obiekt typu `string` na obiekt typu `BigRational`. Wykorzystaj stosowną metodę zaimplementowaną w klasie `BigRational`.

4. W projekcie _Console App_ utwórz klasę `BigRationalExtensions` w pliku `BigRationalExtensions.cs`. Zaimplementuj w niej metodę rozszerzającą typ `BigRational` odwracającą ułamek (tworzenie nowego ułamka, w którym licznik zamienia się z mianownikiem). Przykład użycia: `(new BigRational("2/3")).Reverse()` zwróci ułamek `3/2`.

5. Zaimplementuj metodę rozszerzającą `Pow` podnoszącą ułamek do podanej potęgi. Przykład użycia: `(new BigRational("2/3")).Pow(2)` zwróci ułamek `4/9`.

6. W klasie `BigRationalExtensions` dodaj metodę umożliwiającą obliczanie średniej arytmetycznej dla potencjalnie wielu argumentów typu `BigRational`. Dodaj również metodę `List<BigRational> Range(BigRational lowerBound, BigRational upperBound, BigRational step)` generującą ciąg arytmetyczny ułamków począwszy od `lowerBound` z krokiem `step` tak, aby nie przekroczyć `upperBound`. Musisz przewidzieć różne sytuacje (lista pusta, ciąg rosnący lub malejący, zapętlanie się procesu generowania ciągu, ...).


#### Podpowiedzi

1. Poczytaj o [zamykaniu klasy](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed).

1. Poczytaj o [metodach rozszerzających](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods).



[Początek](README.md) | [Krok poprzedni](step05.md) | [Krok następny](step07.md)