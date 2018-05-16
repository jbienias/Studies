# Testowanie aplikacji JAVA 2017-2018
## Projekt 1 (JUnit, narzędzie Hamcrest oraz MAVEN) 

Projekt nr. 6

```java
mvn package
java -cp target/myProjectApp-1.0-SNAPSHOT.jar Projekt1.App
```


-----------------------
### REGUŁY GRY

1. Wybieramy **jedno** z poniższych zadań. Zadania różnią się poziomem trudności i są inaczej punkto-
wane. 

2. Każdy projekt ma być wykonany przy użyciu narzędzia Maven! Projekt **nie powinien zawierać pliku jar oraz folderu
target**.

3. Przesyłanie projektu będzie odbywało się przy pomocy utworzenia Issue w swoim repozytorium. Utworzenie Issue wiąże się z oddanym projektem. Wszelkie zmiany po Issue będą obcinane.

**TERMIN: 09.04.2018**

- **Spóźnienia** z terminem będą wiązały się z **mniejszą ilością punktów**.
- **Maksymalny deadline** to **13.04.2018** i wtedy obowiązuje **50%** punktów z projektu. A więc dzień zwłoki oznacza obniżenie progu o **10%**. Po tym terminie projekty liczone są na **0%**!
- Projekt, w którym nie będzie wykonywało się polecenie **mvn test** będzie liczony na **0%**!
- Ponadto pod ocenę będzie brany styl projektu: jak zapisane są testy i jak sprawdzane są asercje.
- Testy powinny wykorzystywać wiele różnych asercji (a nie tylko assertEquals)!
- Ponadto po sprawdzeniu projektu należy go obronić: będą to krótkie pytania i ewentualne drobne
zmiany w kodzie podane przez prowadzącego!

-----------------------

**PROJEKT 1** (8 pkt)

Napisz program, który będzie implementował stos, wraz z wszelkimi na nim operacjami, a mianowicie: 

- Odkładanie elementu na stos.
- Zdejmowanie elementu z wierzchu stosu.
- Sprawdzenie elementu z wierzchu stosu. 
- Sprawdzenie czy stos jest pusty.

Pod ocenę będą brane pod uwagę następujące elementy:
- (0.5 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (1 pkt) Uwzględnienie powyższych wymagań.
- (2 pkt) Przypadki testowe.
- (0.5 pkt) Użycie różnych asercji.
- (0.5 pkt) Uwzględnienie wyjątków.
- (0.5 pkt) Zastosowanie biblioteki Hamcrest.
- (0.5 pkt) Pokrycie kodu.
- (0.5 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Użycie typu generycznego.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)

-----------------------

**PROJEKT 2** (10 pkt) 

Dany jest interfejs (kontrakt), który jest częścią oprogramowania pewnej gry dla dzieci na liczbach naturalnych. Interfejs wygląda następująco: 
```
public Interface Psikus{
  Integer CyfroKrad(Integer liczba);
  Integer HultajChochla(Integer liczba) throws NieudanyPsikusException;
  Integer Nieksztaltek (Integer liczba);
  Integer Heheszki(Integer liczba);
  bool Titit(Integer liczba dziel);
}
```
Metody w klasie Kontrakt mają działać następująco: 

- Metoda CyfroKrad działa w ten sposób, że w liczbie przekazanej jako argument, usuwa
losowo jedną cyfrę. Gdy jednocyfrowa zwraca null.
- Metoda HultajChochla działa w ten sposób, że **losowo** zmienia wystąpienia dwóch cyfr w liczbie będącej argumentem.
- Metoda Nieksztaltek działa w ten sposób, że w zadanej liczbie dokonuje jednej losowej
zmiany cyfr według schematu:

- 3 → 8
- 7 → 1
- 6 → 9

Gdy powyższe liczby nie występują w liczbie będącej argumentem, metoda zwraca argument.
- Metoda Heheszki zamienia liczbę na dowolną całkowitą z przedziału <0,liczba).
- Metoda Titit sprawdza, czy liczba jest podzielna przez liczba dziel.

Pod ocenę będą brane pod uwagę następujące elementy:
- (0.5 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (1 pkt) Uwzględnienie powyższych wymagań.
- (2 pkt) Przypadki testowe.
- (1 pkt) Użycie różnych asercji.
- (1 pkt) Uwzględnienie wyjątków.
- (1 pkt) Zastosowanie biblioteki Hamcrest.
- (1 pkt) Pokrycie kodu.
- (0.5 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Użycie innych technologii nie pokazywanych na zajęciach.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)

-------------------------

**PROJEKT 3** (12 pkt)

Gra w kółko i krzyżyk. Prosta gra, którą wszyscy znają. Wymagania do tej gry są następujące:
- Symbol można umieścić na planszy o wymiarach 3x3.
- Sposób na określenie, który gracz powinien jako następny wykonać posunięcie.
- Gracz wygrywa, jeśli jako pierwszy doda takie same symbole od krawędzi do krawędzi planszy
lub po przekątnej.
- Wynik jest remisowy, jeśli wszystkie pola zostały zapełnione.

Pod ocenę będą brane pod uwagę następujące elementy:
- (1 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (1 pkt) Uwzględnienie powyższych wymagań.
- (3 pkt) Przypadki testowe.
- (1 pkt) Użycie różnych asercji.
- (1 pkt) Uwzględnienie wyjątków.
- (1 pkt) Zastosowanie biblioteki Hamcrest.
- (1 pkt) Pokrycie kodu.
- (1 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Zastosowanie planszy większej niż 3x3.
- (2 pkt) Użycie innych technologii nie pokazywanych na zajęciach.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)

-------------------------

**PROJEKT 4** (15 pkt) 

Napiszmy program, który pozwalać będzie na przemieszczanie się statkiem po morzu. Morze ma wyspy na które statek nie może wpłynąć.
Wymagania do tego programu są następujące:
- Określone są punkt początkowy(x,y) statku oraz jego kierunek (N -> północ, S -> południe, E -> wschód, W -> zachód).
- Dodaj obsługę poleceń przesuwających statek naprzód (n) i wstecz (w).
- Dodaj polecenia powodujące obrót statku w lewo (l) i prawo (p).
- Statek przyjmuje łańcuch znaków z poleceniami (literami: n,w,l,p oznaczającymi odpowiednio naprzód,
wstecz, lewo, prawo).
- Dodaj obsługę wykrywania lądu. Wykrywanie powinno odbywać się przed każdym ruchem na
nową pozycję. Jeśli polecenie wymaga wpłynięcia na ląd, statek ma anulować ruch, pozostać
na obecnej pozycji i zgłosić napotkane przeszkody.
- Dodaj możliwość zapisu mapy do pliku oraz odtworzenia mapy z pliku.

Pod ocenę będą brane pod uwagę następujące elementy:
- (1 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (2 pkt) Uwzględnienie powyższych wymagań.
- (5 pkt) Przypadki testowe.
- (1 pkt) Użycie różnych asercji.
- (1 pkt) Uwzględnienie wyjątków.
- (1 pkt) Zastosowanie biblioteki Hamcrest.
- (1 pkt) Pokrycie kodu.
- (1 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Zastosowanie dodatkowych różnych wymagań do gry.
- (2 pkt) Użycie innych technologii nie pokazywanych na zajęciach.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)

-------------------------

**PROJEKT 5** (18 pkt) 

Gra w czwórki. Czwórki to gra dla dwóch osób. Pierwszy gracz wybiera kolor, a następnie obie osoby na zmianę wrzucają kolorowe krążki w kolumny pionowej planszy o wymiarach 7 kolumn x 6 wierszy. Krążki spadają w dół i zajmują najniższe wolne miejsce w danej kolumnie. Celem gry jest umieszczenie czterech krążków w swoim kolorze w pionie, w poziomie lub
po przekątnej. Wygrywa osoba, która zrobi to pierwsza. [Tutaj jest link do gry](http://www.xlgry.pl/gry-board-card/czworki/index.htm).

Wymagania do tej gry są następujące: 
- Plansza składa się z siedmiu kolumn i sześciu wierszy. Początkowo wszystkie pola są puste.
- Gracze wrzucają od góry krążki w kolumny. Jeśli kolumna jest pusta, wrzucony krążek spada
w dół planszy. Krążki dodane do tej kolumny będą się znajdować nad tymi wcześniej wrzu-
conymi.
- Gra jest przeznaczona dla dwóch osób. Każdemu graczowi jest przypisany jeden kolor. Jedna
osoba używa krążków czerwonych, a druga zielonych. Gracze wykonują ruchy na zmianę i za
każdym razem wrzucają jeden krążek.
- Program ma informować o zdarzeniach i błędach w grze. Dane wyjściowe powinny przedsta-
wiać stan na planszy po każdym ruchu.
- Gdy nie można dodać kolejnych krążków. Gra kończy się remisem.
- Jeśli gracz dodał krążek i w ten sposób połączył więcej niż trzy krążki w swoim kolorze
przylegające do siebie w pionowej linii, wygrywa.
- To samo dzieje się, jeśli połączono krążki w linii poziomej.
- To samo dzieje się, jeśli połączono krążki po stosie.
- Tworzenie list rankingowych graczy: imię, punktacja, ilość wygranych itd 

Pod ocenę będą brane pod uwagę następujące elementy:
- (1 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (4 pkt) Uwzględnienie powyższych wymagań.
- (6 pkt) Przypadki testowe.
- (1 pkt) Użycie różnych asercji.
- (1 pkt) Uwzględnienie wyjątków.
- (1 pkt) Zastosowanie biblioteki Hamcrest.
- (1 pkt) Pokrycie kodu.
- (1 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Użycie planszy dowolnego wymiaru.
- (2 pkt) Użycie innych technologii nie pokazywanych na zajęciach.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)

-------------------------

**PROJEKT 6** (20 pkt)

Napiszmy program, który będzie realizował prostą wersję systemu rezerwacji (do wyboru: książek, hoteli, restauracji itd.). 

Program ma następujące wymagania: 
- Załadować bazę danych z pliku tekstowego do systemu.
- Ma zwracać listę zarezerwowanych rzeczy dla danego użytkownika (warto skorzystać tutaj z HashTable).
- Ma unikać podwójnego rezerwowania rzeczy w tym samym dniu i godzinie.
- Ma być odporny na wszelkie nieprawidłowe argumenty. 
- Powinien rezerwować o pełnych godzinach lub np: 10.30, 11.30 itd. 
- Powinien zapisywać identyfikator zamówienia (np. adres email).
- Powinien generować potwierdzenia zamówienia (zapis do pliku lub unikalny kod).
- Powinien obsługiwać wszelkie wyjątki (rezerwacja w złym dniu, o godzinach nieczynnej restauracji itd.) 

Plik tekstowy z bazą danych może być dowolnego formatu, ale powinien zawierać nazwę miejsca, godziny otwarcia, dni oraz godziny wolne do rezerwacji, opis dodatkowy. 

Pod ocenę będą brane pod uwagę następujące elementy:
- (1 pkt) Kompilacja i uruchomienie bezbłędne projektu.
- (5 pkt) Uwzględnienie powyższych wymagań.
- (6 pkt) Przypadki testowe.
- (1 pkt) Użycie różnych asercji.
- (2 pkt) Uwzględnienie wyjątków.
- (1 pkt) Zastosowanie biblioteki Hamcrest.
- (1 pkt) Pokrycie kodu.
- (1 pkt) Styl kodu.
- (1 pkt) Zastosowanie metodyki TDD.
- (1 pkt) Zastosowanie testów parametrycznych.

Ponadto dodatkowo pod uwagę będą brane następujące elementy: 

- (1 pkt) Użycie biblioteki AssertJ.
- (1 pkt) Skonfigurowanie TravisCI do automatycznego budowania projektu.
- (1 pkt) Zastosowanie testów parametrycznych przy użyciu plików testowych.
- (1 pkt) Zastosowanie JAVA 8 lub wyżej.
- (2 pkt) Użycie planszy dowolnego wymiaru.
- (2 pkt) Użycie innych technologii nie pokazywanych na zajęciach.

Ponadto pod ocenę jest brane również: (Brak tych elementów: -1 pkt za
podpunkt od obowiązkowej punktacji zadania!)
- Historia projektu w repozytorium.
- Ocena opisu commitów 
- Stan repozytorium (żeby nie był śmietnikiem!!!)
