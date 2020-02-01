a)
search(drzewo(X, _, _), X).
search(drzewo(_, L, _), X) :- search(L, X).
search(drzewo(_, _, R), X) :- search(R, X).

%lub
search(drzewo(X, _, _), X).
search(drzewo(_, L, R), X) :-
	search(L,X); search(R, X).

b)
prod(nil, 1).
prod(drzewo(X, L, R), P) :-
	prod(L, P1),
	prod(R, P2),
	P is X * P1 * P2.
	
c)
postorder(nil, []).
postorder(drzewo(X, L, R), T) :-
	postorder(L, T1),
	postorder(R, T2),
	append(T1, T2, T3),
	append(T3, [X], T).