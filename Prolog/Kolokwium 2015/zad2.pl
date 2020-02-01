a)
suffix(X, X).
suffix(X, [_|L] :- suffix(X, L).

%lub

suffix(L1, L2) :-
	append(_, L1, L2).
	
	
b)
reverse([], []).
reverse([X|L1], L2) :-
	reverse(L1, R),
	append(R, [X], L2).
	
palindrom(L) :-
	reverse(L, L).