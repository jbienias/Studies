a)
split(_, [], [], []).
split(X, [H|L], [H|L1], L2) :-
	H =< X, !, split(X, L, L1, L2).
split(X, [H|L], L1, [H|L2]) :-
	H > X, !, split(X, L, L1, L2).
	
b)
split(_, [], [], []).
split(P, [H|L], [H|L1], L2) :-
	T = [P, H], call(T), !, split(P, L, L1, L2).
split(P, [H|L], L1, [H|L2]) :-
	split(P, L, L1, L2).