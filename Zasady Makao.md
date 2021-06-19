# Zasady

W aplikacji serwera zastosowałem uproszczone zasady gry w Makao.
Serwer po dołączeniu 3 graczy rozpoczyna grę i losuje każdemu po 5 kart. Z pozostałych kart wykłada się pierwszą w sposób widoczny dla wszystkich graczy,
lecz nie może być to karta funkcyjna. Zwykle odkrywa się karty aż do wyłożenia niefunkcyjnej. Kolejność i wybór pierwszego gracza ustala serwer.
Gra kończy się, wtedy kiedy jeden z graczy pozbędzie się wszystkich kart z ręki. 

## Karty niefunkcyjne
Są to karty: 5, 6, 7, 8, 9, 10 oraz Król Karo i Król Trefl.

## Karty funkcyjne
* 2 - Wymusza na następnym graczu dobranie 2 kart. (Jeśli można tyle pobrać).
* 3 - Wymusza na następnym graczu dobranie 3 kart. (Jeśli można tyle pobrać).
* 4 - Wymusza na następnym graczu dobranie 4 kart. (Jeśli można tyle pobrać).
* Król Pik i Król Kier - Wymuszają na następnym graczu dobranie 5 kart. (Jeśli można tyle pobrać).
* Walet - Wymusza na wszystkich graczach zagranie karty którą wybiera ( od 5 do 10) lub dobraniu karty.
* Dama - Dama na wszystko, wszystko na damę.
* As - Wymusza zmianę koloru.

Efekty kart funkcyjnych nie mogą się sumować i przechodzić na następnego gracza. 

