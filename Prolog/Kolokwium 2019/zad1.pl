% a)
delete(_, [], []).
delete(X, [X|L1], L2) :-
    !, delete(X, L1, L2).
delete(X, [Y|L1], [Y|L2]) :-
    delete(X, L1, L2).
	
	
% b)
flatten([], []).
flatten([H|T], L) :-
    flatten(H, H1),
    flatten(T, T1),
    append(H1, T1, L).
flatten(L, [L]).

% c)
neighbours(X, Y, [X, Y|_]).
neighbours(X, Y, [_|L]) :-
    neighbours(X, Y, L).