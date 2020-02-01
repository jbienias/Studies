a)
member(X, [X|_]).
member(X, [_|L]) :- member(X, L).

b)
member(X, [X|_]).
member(X, [L|_]) :- is_list(L), member(X, L).
member(X, [_|L]) :- member(X, L). 