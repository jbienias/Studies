%Proszę zdefiniować następujące predykaty dla list.

%a) nth(N,L,X), który jest spełniony, jeżeli X jest N-tym elementem listy L.
%b) ordered(L), który jest spełniony, jeżeli lista L jest posortowana.
%c) mergesort(L1,L2), który jest spełniony, jeżeli lista L2 jest wersją posortowaną listy L1. Predykat ma symulowac algorytm mergesort. 

%a)
nth(1, [X|_], X).
nth(N ,[_|L], X) :- nth(N1,L,X), N is N1 + 1.

%b)
ordered([_]).
ordered([X,Y|L]):- 
    X =< Y, ordered([Y|L]).
	
%c)

split3([], [], []).
split3([X|L], [X|L1], L2) :- split3(L, L2, L1).

merge3(L, [], L).
merge3([], L, L).
merge3([X|L1], [Y|L2], [X|L]) :-
    X =< Y, merge3(L1, [Y|L2], L).
merge3([X|L1], [Y|L2], [Y|L]) :-
    X > Y, merge3([X|L1], L2, L).

% % US - Unsorted 
% S - Sorted
% L1 - First half of US 
% L2 - Second part of US    
mergesort2([X], [X]).
mergesort2([X,Y|US], S) :-
    % US dzielimy na dwie części 
    split3([X,Y|US], L1, L2),
    % uruchamiamy algorytm dla pierwszej 
    mergesort2(L1, S1),
    % uruchamiamy algorytm dla drugiej
    mergesort2(L2, S2),
    % merge obu posortowanych części
    merge3(S1,S2,S).