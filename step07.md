## Krok 7. Refaktoryzacja i dokumentacja kodu

W ramach tego kroku uporządkujesz kod, dodasz komentarze, wygenerujesz dokumentację kodu

### Zadania do wykonania

1. Klasa `BigRational` zapisana jest w wielu plikach (klasa częściowa). Przenieś powiązany tematycznie kod metod do odpowiednich plików opisujących dane funkcjonalności. Przykład: parsowanie napisów oraz konwersje do innych typów liczbowych.

2. Skoryguj kod stosując technikę łańcuchowania. Dana funkcjonalność powinna być zaprogramowana w jednym miejscu, zaś w wielu innych udostępniasz ją w określony, zestandaryzowany sposób. Przykładem jest przesłonięcie metody `Equals`, do której wielokrotnie odwołujesz się, np. przeciążając operatory `==`, `!=` czy metody, np. `CompareTo()`.

3. Zastanów się nad optymalizacją kodu i zastosowanych w nim algorytmów. 

4. Skoryguj stosowane przez Ciebie nazwy w kodzie. Stosuj wytyczne Microsoft [C# Coding Conventions (C# Programming Guide)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions). Pamiętaj, że w C# nazwy parametrów metod **mają znaczenie** (w przeciwieństwie do innych języków programowania). Użytkownik, korzystający z Twojej klasy i publicznych metod może skorzystać z funkcjonalności argumentów nazwanych [Named and Optional Arguments (C# Programming Guide)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments). Zamiast zapisać np. `public BigRational(long num, long denom)` lepiej podać: `public BigRational(long numerator, long denominator)`.

5. Skomentuj wszystkie publiczne składniki Twojej klasy, stosując domyślny system komentowania dla C# [Documenting your code with XML comments](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc).

6. Wygeneruj dokumentację klasy (formalnie projektu typu _class library_) za pomocą dodatkowego narzędzia - Microsoft proponuje [DocFX](https://dotnet.github.io/docfx/). Możesz go użyć w wariancie _standalone_ albo jako dodatek NuGet - [integrując go z Twoim projektem typu _class library_](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html#3-use-docfx-integrated-with-visual-studio). Każdorazowy _Build_ projektu uruchamia proces generowania dokumentacji (przechowywanej w folderze `_site` projektu), dokumentacja klasy w html znajduje się w `_site/api`.

    > UWAGA: proces generowania dokumentacji może długo trwać! Weź pod uwagę nagły wzrost zapotrzebowania na pamięć dyskową (nawet w GB) - powstanie wiele plików, również dużych.


[Początek](README.md) | [Krok poprzedni](step06.md) | [Krok następny](step08.md)
