% a)
leaves(drzewo(_, nil, nil), 1) :- !.
leaves(drzewo(_, L, nil), N) :-
    leaves(L, N).
leaves(drzewo(_, nil, R), N) :-
    leaves(R, N).
leaves(drzewo(_, L, R), N) :-
	leaves(L, N1),
	leaves(R, N2),
	N is N1 + N2.
	
% b)
ordered(D) :-
    inorder(D,L),
    check_sorted(L).

inorder(nil, []).
inorder(drzewo(X, L, R), T) :-
    inorder(L, T1),
    inorder(R, T2),
    append(T1, [X|T2], T).

check_sorted([]).
check_sorted([_]).
check_sorted([X,Y|L]) :-
    X < Y,
    check_sorted([Y|L]).

% c)
square(drzewo(X, nil, nil), drzewo(Y, nil, nil)) :- 
    Y is X*X.
square(drzewo(X, L, nil), drzewo(Y, L1, nil)) :-
	Y is X * X,
    square(L, L1).
square(drzewo(X, nil, R), drzewo(Y, nil, R1)) :-
    Y is X * X,
    square(R, R1).
square(drzewo(X, L, R), drzewo(Y, L1, R1)) :-
    Y is X * X,
    square(L, L1),
    square(R, R1).