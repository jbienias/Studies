# Technologie .NET

Prowadzący - [dr Tomasz Borzyszkowski](https://github.com/tborzyszkowski)

-----------------------

## LIB_TEST (Simple Library to Unit Test)
```$xslt
1. Napisać projekt biblioteki (diagramy klas, przypadki użycia, opis poszczególnych funkcji)
2. Do projektu z punktu pierwszego stworzyć testy jednostkowe, które:
  - będą zawierały najmniej 3 różne klasy assercji
  - będą zawierały najmniej jeden Data-Driven Unit Test
  - wykorzystają Microsoft Fakes (stubs & shims)
3. Zaimplementować projekt tak by spełniał testy z punktu drugiego
```

-----------------------

## MVC (CRUD App + Unit Tests(w/ Mocks and Fakes) + CodedUI/Selenium)
### Aplikacja:
```$xslt
Modele:
- atrybuty dotyczące ograniczeń danych (własne walidatory)
- atrybuty dotyczące widoków
- minimum 2 tabele z relacją jeden-do-wiele

Kontrolery:
- powiązanie uprawnień zalogowanych userów z kontrolerami (atrybuty wykorzystujące role)
- logowanie i przynależność do ról
- LINQ

Widoki
- widoki pisane w Razor + przekazywanie danych do widoku
- umiejętność + przykład ręcznego rozszerzenia widoku
- widoki częściowe + komunikacja z widokiem podstawowym
- zastosowanie podstawowych helperów + definicja własnych
- zastosowanie layoutów
```

### Testy:
```$xslt
1. Zawierać projekt testowy z testami jednostkowymi dla modeli i kontrolerów.
2. Obsługa przez ASP MVC nieprzewidzianych wyjątków.
3. W testach kontrolerów należy zademonstrować metody izolacji kodu.
  - wykorzystanie Repository Test Double opartych na własnym interfejsie repozytorium i wykorzystaniu konstruktorów do wyspecyfikowania repozytorium.
  - wykorzystanie jednego z frameworków IoC dla ASP MVC (StructureMap,  RhinoMocks i NSubstitute  lub Moq).
4. Wykorzystanie Selenium WebDriver oraz Coded UI do testowania interfejsu użytkownika.
```

-----------------------

## MVVM_Xamarin CRUD App(UWP & Android)
```$xslt
1. Zastosowanie pełnego wzorca MVVM - binding wartości oraz "command" (25 %)
2. Adaptatywny interface użytkownika - dotyczy zarówno geometrii jak i systemu operacyjnego (15 %)
3. Wykorzystanie zdarzeń związanych z cyklem życia aplikacji - OnStart, OnSleep, OnResume (10%)
4. Wykorzystanie konstrukcji "DependencyService" do ujednolicenia asynchronicznego dostępu do plików (20 %)
6. Nawigacja stron z przekazywaniem wartości
7. Walidacja danych na formularzach
8. Usability
```