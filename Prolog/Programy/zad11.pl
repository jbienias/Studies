%IMO DO OLANIA STRASZNIE
%ZADANIE NIEDOKONCZONE!

%Proszę zdefiniować predykat route(Place1,Place2,Day,Route), który zrealizuje planowanie lotów na podstawie rozkładu jazdy.
%Rozkład jazdy jest podane przez predykat timetable(Place1,Place2,List_of_flights), gdzie List_of_Flights jest listą lotów od Place1 do Place2. Każdy lot jest listą [Departure_time,Arrival_time,Flight_number,List_of_days].
%Przykład:

     timetable( london, edinburgh,
                [ [ 9:40, 10:50, ba4733, alldays],
                  [19:40, 20:50, ba4833, [mo,tu,we,th,fr]] ]).
     

%Plan lotów Route jest listą lotów PlaceA-PlaceB:Flight_number:Departure_time, taka że

%    PlaceA w pierwszym elemencie w liście, to Place1.
%    PlaceB w ostatnim elemencie w liście, to Place2.
%    wszystkie loty odbędą się w tym samym dniu.
%    między lotami jest wystarczający czas na transfer. 


%Przykład:

%     ?- route(ljubljana,edinburgh,th,R).
%     R = [ljubljana-zurich:yu322:11:30, zurich-london:sr806:16:10, london-edinburgh:ba4822:18:40].

%ODP:

route(Place1,Place2,Day,[X|L]):-
    timetable(Place1,Place2,[[Departure,_,ID,Days]|_]),member(Day,Days),X=[Place1,Place2,ID,Departure],
    route(Place1,Place2,Day,L).
route(Place1,Place2,Day,[X|L]):-
    timetable(Place1,Place2,[[Departure,_,ID,alldays]|_]),X=[Place1,Place2,ID,Departure],
    route(Place1,Place2,Day,L).

     