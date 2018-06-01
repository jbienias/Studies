# Testowanie aplikacji JAVA 2017-2018
## Projekt 3 (JBehave + SeleniumWebDriver) 


-----------------------
### REGUŁY GRY

1. Każdy projekt ma być wykonany przy użyciu narzędzia Maven! Projekt **nie powinien zawierać pliku jar oraz folderu
target**.

2. Przesyłanie projektu będzie odbywało się przy pomocy utworzenia Issue w swoim repozytorium. Utworzenie Issue wiąże się z oddanym projektem. Wszelkie zmiany po Issue będą obcinane.

**TERMIN: 03.06.2018**

- **Spóźnienia** z terminem będą wiązały się z **mniejszą ilością punktów**.
- **Maksymalny deadline** to **08.06.2018** i wtedy obowiązuje **50%** punktów z projektu. A więc dzień zwłoki oznacza obniżenie progu o **10%**. Po tym terminie projekty liczone są na **0%**!
- Projekt, w którym nie będzie wykonywało się polecenie **mvn integration-test** będzie liczony na **0%**!
- Ponadto pod ocenę będzie brany styl projektu: jak zapisane są testy i jak sprawdzane są asercje.
- Testy powinny wykorzystywać wiele różnych asercji (a nie tylko assertEquals)!
- Ponadto po sprawdzeniu projektu należy go obronić: będą to krótkie pytania i ewentualne drobne
zmiany w kodzie podane przez prowadzącego!

-----------------------

**Projekt 1** (25 pkt)

Przetestować dowolną stronę internetową przy użyciu technologii Selenium WebDriver w języku JAVA.
Pod ocenę będą brane następujące aspekty:

- (0.5 pkt) Kompilacja i bezbłędne uruchomienie projektu! Nie działanie implikuje wynikiem zero punktów!
- (1 pkt) Testy w panelu logującym użytkownika
- (1 pkt) Testy w panelu wyszukiwania
- (2 pkt) Testy w operacjach CRUD (dodawanie, edytowanie, usuwanie)
- (3 pkt) Testy jakiegokolwiek formularza (więcej punktów im bardziej rozbudowany formularz)
- (3 pkt) Inne ciekawe testy strony nie wymienione powyżej
- (1 pkt) Wykorzystanie modelu PageObject (połowa testów)
- (1 pkt) Wyszukiwanie elementów różnymi metodami (metoda By...)
- (1.5 pkt) Użycie Implicity/explicit/fluent wait (mniej więcej równa ilość w testach)
- (2 pkt) Testowanie wykorzystując różne przeglądarki interfejsowe (max punktów za 4 prze-
glądarki)
- (2 pkt) Testowanie wykorzystując przeglądarki bezinterfejsowych (co najmniej dwa)
- (4 pkt) Napisanie testów akceptacyjnych (co najmniej 6 scenariuszy w tym jeden przy użyciu słowa kluczowego Examples oraz jeden przy użyciu zmiennej typu ExampleTable).
- (1 pkt) Wykorzystanie PatternVariants, aliasów, typów scenariuszy w JBehave.
- (1 pkt) Wykorzystanie JUnit w wersji 5 (1 pkt - cały projekt w JUnit5).
- (1 pkt) Styl kodu.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za podpunkt od obowiązkowej
punktacji zadania!)
- Historia projektu w repozytorium (co najmniej 20 commitów).
- Różnorodne asercje (co najmniej 5 różnych).
- Struktura projektu, jego podział na klasy.
- Mieszanie różnych technologii ze sobą (dotyczy oczekiwania jawnego i niejawnego).
- Nie potrzebne stosowanie klauzul: try ... catch, Exception.

Ponadto dodatkowo będzie brane pod ocenę:
- (2 pkt) Wykorzystanie przeglądarek chmurowych (za dwie - 2 pkt).
- (0.5 pkt) Obsługa alertów/okien.
- (1 pkt) Napisanie własnego Matcher’a.
- (1.5 pkt) Nacjonalizacja scenariuszy w JBehave.
- (2 pkt) Uruchamianie testów zgodnie z systemem operacyjnym (dotyczy dostępności różnych przeglądarek).
- (1 pkt) Integracja z serwisem TravisCI.
- (3 pkt*) Inne ciekawe nietypowe rozwiązania nie podawane na zajęciach (punktacja zależna
od wykorzystania narzędzia).
