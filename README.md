# Opis protokołu 
Aplikacja jest własną implementacją protokołu obsługującego grę Makao. Podzielony jest na dwie części: serwer oraz klient.
 Wszystkie ważne zdarzenia są zapisywane w logach. Protokół obsługuje uproszczone zasady gry w Makao. [Zasady](https://github.com/mlesisz/Makao/blob/main/Zasady%20Makao.md)
## Serwer
Opiera się na architekturze wielowątkowej aby obsługiwać kilka klientów naraz oraz aby obsługiwać karzdy stół do gry na osobnym wątku.
Obłsuguje IPv4 i IPv6. Korzysta z bazy danych SqlLite która przechowuje informacje potrzebne do rejestracji i logowania.
## Klinet
Klient jest aplikacją stworzoną w Windows Forms. Działa na dwóch wątkach:
- Główny wątek który obsługuje formularz, wysyłanie wiadomości do serwera.
- Wątek w tle który odbiera i przetwarza wiadomości otrzymane od serwera.
### Odbiór /wysyłanie wiadomości
Wszystkie wiadomości wysyłane przez klienta mają nastepujący format: 

 `Action:Action\r\nToken:Token\r\nData:...\r\n\r\n`
 * Action - jedna z akcji np. Login. Wyjaśnione szczegółowo niżej.
 * Token - Wygenerowany przez serwer uniklany klucz uwierzytelniający używany do użytkownika przez serwer.
 * Data - (Wymagane dla niektórych akcji) Dane potrzebne do zapytania. 

Wszystkie wiadomości wysyłane przez serwer mają następujący format:

`Action:Action\r\nStatus:Status\r\nData:Data\r\nMessage:Message\r\n\r\n`
* Action - określa na jaką wiadomość odpowiada serwer lub jaką wysyła do klienta. 
* Status - informacja o powodzeniu wykonania akcji :
  * OK - akcja wykonana pomyślnie
  * WRONG - Akcja wykonana lecz nie spełniono warunków do pozytywnego wykonania akcji. Np. klient chce zagrać kartę której nie może zagrać.
  * ERROR - błąd podczas wykonania akcji.
* Data - (Opcjonalne) Dane będące wynikiem wykonania akcji.
* Message - (Opcjonalne) Wiadomość do klienta z opisem wykonania akcji.

# Akcje
### Register
### Login
### Logout
### Create
### List
### Join
### Leave
### Take
### Play
### End

## Jak uruchomić 
Wymagania:
* .net SDK > 5.0

* Klonujemy/pobieramy repozytorium
* Pobieramy dla swojej platformy .net 5.0 SDK https://dotnet.microsoft.com/download

Uruchomienie serwera
  * Przechodzimy do np. D:\Studia\Programowanie aplikacji sieciowych\Makao\Server\Server 
  * `dotnet run Program.cs`
  
Uruchomienie klienta
  * Przechodzimy do np. D:\Studia\Programowanie aplikacji sieciowych\Makao\ClientMakao\ClientMakao
  * `dotnet run Program.cs`



