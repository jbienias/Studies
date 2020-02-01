% a)
groundterm(T) :-
    T =.. [_|L],
    check_no_variables(L).
    
check_no_variables([]).
check_no_variables([X|L]) :-
    \+var(X), %lub not(var(X))
    check_no_variables(L).
	
% b)
pos(X) :- X > 0.
neg(X) :- X < 0.

take_while(_, [], []).
take_while(P, [X|L1], [X|L2]) :-
    T =..[P, X], call(T), !, take_while(P, L1, L2).
take_while(P, _, L2) :- 
	take_while(P, [], L2).
