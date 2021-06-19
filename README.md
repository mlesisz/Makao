## Jak uruchomić (testowałem tylko na Windowsie)
Wymagania:
* .net SDK > 5.0

* Klonujemy/pobieramy repozytorium
* Pobieramy/aktualizujemy dla swojej platformy .net 5.0 SDK https://dotnet.microsoft.com/download

Uruchomienie serwera
  * Przechodzimy do np. D:\Studia\Programowanie aplikacji sieciowych\Makao\Server\Server 
  * `dotnet run Program.cs`
  
Uruchomienie klienta
  * Przechodzimy do np. D:\Studia\Programowanie aplikacji sieciowych\Makao\ClientMakao\ClientMakao
  * `dotnet run Program.cs`


# Opis protokołu 
Aplikacja jest własną implementacją protokołu obsługującego grę Makao. Podzielony jest na dwie części: serwer oraz klient.
 Wszystkie ważne zdarzenia są zapisywane w logach. Protokół obsługuje uproszczone zasady gry w Makao. [Zasady](https://github.com/mlesisz/Makao/blob/main/Zasady%20Makao.md)
## Serwer
Opiera się na architekturze wielowątkowej, aby obsługiwać kilka klientów naraz oraz aby obsługiwać każdy stół do gry na osobnym wątku. Obsługuje IPv4 i IPv6. Korzysta z bazy danych SqlLite, która przechowuje informacje potrzebne do rejestracji i logowania.
## Klinet
Klient jest aplikacją stworzoną w Windows Forms. Działa na dwóch wątkach:
- Główny wątek, który obsługuje formularz, wysyłanie wiadomości do serwera.
- Wątek w tle, który odbiera i przetwarza wiadomości otrzymane od serwera.
### Odbiór /wysyłanie wiadomości
Wszystkie wiadomości wysyłane przez klienta mają następujący format: 

 `Action:Action\r\nToken:Token\r\nData:...\r\n\r\n`
 * Action - Jedna z akcji np. Login. Wyjaśnione szczegółowo niżej.
 * Token - Wygenerowany przez serwer unikalny klucz uwierzytelniający używany do użytkownika przez serwer.
 * Data - (Wymagane dla niektórych akcji) Dane potrzebne do zapytania. 

Wszystkie wiadomości wysyłane przez serwer mają następujący format:

`Action:Action\r\nStatus:Status\r\nData:Data\r\nMessage:Message\r\n\r\n`
* Action - Określa, na jaką wiadomość odpowiada serwer lub jaką wysyła do klienta. 
* Status - Informacja o powodzeniu wykonania akcji:
  * OK - Akcja wykonana pomyślnie.
  * WRONG - Akcja wykonana, lecz nie spełniono warunków do pozytywnego wykonania akcji. Np. klient chce zagrać kartę, której nie może zagrać.
  * ERROR - Błąd podczas wykonania akcji.
* Data - (Opcjonalne) Dane będące wynikiem wykonania akcji.
* Message - (Opcjonalne) Wiadomość do klienta z opisem wykonania akcji.

# Akcje
### Register
Umożliwia zarejestrowanie się gracza.

Przykładowy request: `Action:Register\r\nData:Login:test2\r\nPassword:test2\r\n\r\n`

Przykładowy response: `Action:Register\r\nStatus:WRONG\r\nMessage:Podany login jest zajęty.\r\n\r\n`
### Login
Uwierzytelnienie użytkownika, daje dostęp do pozostałych akcji.


Przykładowy request: `Action:Login\r\nData:Login:test\r\nPassword:test\r\n\r\n`

Przykładowy response: `Action:Login\r\nStatus:OK\r\nData:Wc5q6ECsGUSihNP+DJ5BAg==\r\n\r\n`
### Logout
Wylogowuje użytkownika.

Przykładowy request: `Action:Logout\r\nToken:kzcMdBErDk6IUREd3icc+A==\r\n\r\n`

Przykładowy response: `Action:Logout\r\nStatus:OK\r\n\r\n`
### Create
Tworzy nowy stół do gry, do którego mogą się dosiąść inni użytkownicy.

Przykładowy request: `Action:Create\r\nToken:Wc5q6ECsGUSihNP+DJ5BAg==\r\nData:testowy\r\n\r\n`

Przykładowy response: `Action:Create\r\nStatus:OK\r\n\r\n`
### List
Zwraca listę stołów z wolnymi miejscami.

Przykładowy request: `Action:List\r\nToken:Hndc+2S3BEWugWbY7cRbGA==\r\n\r\n`

Przykładowy response: `Action:List\r\nStatus:OK\r\nData:Stół1;Stół2\r\n\r\n`
### Join
Dołącza użytkownika do wolnego stołu.

Przykładowy request: `Action:Join\r\nToken:Hndc+2S3BEWugWbY7cRbGA==\r\nData:Stół1\r\n\r\n`

Przykładowy response: `Action:Join\r\nStatus:OK\r\nMessage:Dołączyłeś do stolika: Stół1\r\n\r\n`
### Leave
Użytkownik odchodzi od stołu.

Przykładowy request: `Action:Leave\r\nToken:Wc5q6ECsGUSihNP+DJ5BAg==\r\n\r\n`

Przykładowy response: `Action:Leave\r\nStatus:OK\r\n\r\n`
### Take
Użytkownik pobiera kartę podczas gry.

Przykładowy request: `Action:Take\r\nToken:Hndc+2S3BEWugWbY7cRbGA==\r\n\r\n`

Przykładowy response: `Action:Take\r\nStatus:OK\r\nData:Król Trefl\r\n\r\n`
### Play
Użytkownik kładzie kartę na stół.

Przykładowy request: `Action:Play\r\nToken:Hndc+2S3BEWugWbY7cRbGA==\r\nData:Card:9 Karo\r\n\r\n`

Przykładowy response: `Action:Play\r\nStatus:WRONG\r\nData:9 Karo\r\nMessage:Nie możesz zagrać tej karty.\r\n\r\n`
### State
Serwer przesyła informacje o aktualnym stanie gry.

Przykładowa wiadomość od serwera: `Action:State\r\nMessage:Gracz test2 pobrał kartę.\r\n\r\n`
### End
Aplikacja klienta została wyłączona przez użytkownika i przesyła ostatnią wiadomość o rozłączeniu.

Przykładowy request: `Action:End\r\n\r\n`
