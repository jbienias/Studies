%Proszę zdefiniować predykat list_to_term(L,T), który jest spełniony, jeżeli L jest niepustą listą liczb naturalnych a T jest termem zbudowany z tych liczb przy pomocy operatora +.
%Przykłady:

% ?- list_to_term([1,2,3,4],T).
% T = 1 + (2 + (3 + 4))
% ?- list_to_term([7],T).
% T = 7


is_list_int([X]) :- integer(X).
is_list_int([X|L]) :- integer(X), is_list_int(L).

list_to_term([X],T) :- 
    is_list_int([X]), 
    T = X.
list_to_term([X|L], T) :-
    is_list_int([X|L]),
    T = X + T2,
    list_to_term(L, T2).
	
%lub

list_to_term([X], T) :- T =.. [X].

list_to_term([X|L], T) :- list_to_term(L, T1), T =.. [+, X, T1].