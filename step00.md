## Krok 0. Przygotowania - implementacja `BigRational`

W tym kroku skonfigurujesz œrodowisko projektu - utworzysz _solution_ oraz 3 projekty: typu _Class Library_, typu _Console application_ oraz typu _Unit test_.

1. Utwórz projekt o nazwie `RationalLib` typu _Class Library_ oraz solution o nazwie `RationalType`.

    > W ramach Twojego _solution_ bêdziesz korzysta³ z wielu projektów - m.in. projektu testuj¹cego (_Unit Tests_) czy projektu z aplikacj¹ próbn¹. Zatem powinieneœ mieæ _solution_ z mo¿liwoœci¹ utworzenia wielu projektów.

2. Dodaj do _solution_ projekt typu _Unit Tests_. Nadaj mu nazwê `RationalUnitTests`. W klasach tego projektu tworzyæ bêdziesz kod testów jednostkowych dla projektu `RationalLib`, zaœ uruchamiaæ je w _Test Explorer_.
   > Wykorzystujemy domyœlne œrodowisko testów jednostkowych Microsoft MSTest v2.
   > Dla œrodowiska wybierz .NET6 lub wy¿szy.

   Dodaj referencjê do projektu `RationalLib`.

3. W _solution_ utwórz jeszcze jeden projekt typu _Console application_
   o nazwie `RationalConsoleAppDemo`. (Wybierz .NET6 lub wy¿szy). Uczyñ ten projekt aktywnym.

   W projekcie tym bêdziesz zapisywa³ proste fragmenty kodu weryfikuj¹cego niektóre z funkcjonalnoœci projektowanej klasy.

   Dodaj referencjê do projektu `RationalLib` oraz wpisz w wygenerowanym `Program.cs`:

   ````csharp
   using RationalLib;
   using static System.Console;
   ````

    > Dyrektywa `using static` pozwala na odwo³ania bezpoœrednie do sk³adników
    > wskazanego typu. W naszym przypadku zamiast pisaæ `Console.WriteLine("abc")`
    > bêdziemy mogli napisaæ `WriteLine("abc")`.

    > Zwróæ uwagê, w jakiej przestrzeni nazw zdefininiowana bêdzie klasa `BigRational` (domyœlnie nazwa projektu - czyli `RationalLib`).

   SprawdŸ poprawnoœæ konfiguracji, uruchamiaj¹c program (dopisuj¹c w `Main()` liniê `WriteLine("Hello")` (dziêki `using static System.Console;` mo¿esz w takiej skrótej postaci wyprowadzaæ napisy na konsolê).

---

* Plik: `RationalLib.csproj`

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
      </PropertyGroup>

    </Project>
   ```

* Plik: `RationalUnitTests.csproj`

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <Nullable>enable</Nullable>

        <IsPackable>false</IsPackable>
      </PropertyGroup>

      <ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
        <PackageReference Include="MSTest.TestAdapter" Version="2.2.7" />
        <PackageReference Include="MSTest.TestFramework" Version="2.2.7" />
        <PackageReference Include="coverlet.collector" Version="3.1.0" />
      </ItemGroup>

      <ItemGroup>
        <ProjectReference Include="..\RationalLib\RationalLib.csproj" />
      </ItemGroup>

    </Project>
   ```

* Plik: `RationalConsoleApp.csproj`

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
      </PropertyGroup>

      <ItemGroup>
        <ProjectReference Include="..\RationalLib\RationalLib.csproj" />
      </ItemGroup>

    </Project>
   ```

---

[Pocz¹tek](README.md) | [Krok nastêpny](step01.md)