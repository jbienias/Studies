%Niech będzie dana lista właściwości, n.p. [animate, male, canine, feline] oraz klausuly używające te właściwoście, n.p.

animate(fred).
animate(alice).
male(fred).
canine(alice).
feline(fred).

%Proszę zdefiniować predykat checkprops(A,L), gdzie A jest dowolnym objektem a L listą właściwości. Predykat ma być spełniony, jeśli objekt A posiada wszystkie właściwoście w L, n.p.

% ?- checkprops(fred, [animate, male]). 
% %yes
% ?- checkprops(alice, [animate, feline]).
% no


checkprops(_, []).
checkprops(A, [F|L1]) :-
	T =.. [F, A],
	call(T),
	checkprops(A, L1).
	

%imo to jest bardziej poprawne :

checkprops(A, [P]) :-
    T =.. [P,A],
    call(T).

checkprops(A, [P|L]) :-
    T =.. [P, A],
    call(T),
    !,
    checkprops(A, L).