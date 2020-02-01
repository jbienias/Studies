% Proszę zdefiniować predykat count(A,L,N), który jest spełniony, jeżeli N jest liczbą wystąpienia termu A w termie T. Przykłady:

%  ?- count(a, f(a), N).
%  N = 1
%  ?- count(a, f(a, g(b,a)), N).
%  N = 2
%  ?- count(a, f(a,X)), N).
%  N = 1
%  ?- count(f(a), f(a,g(f(a),f(a))), N).
%  N = 2


count(X,X,1) :- atomic(X).
count(T,T,1).
count(X,T,N) :- 
	T =.. [_|L1], count_list(X, L1, N).
count_list(_, [], 0).
count_list(_, [L1|_], 0) :- 
	var(L1).
count_list(X, [L1|L2], N) :- 
	count(X, L1, N1), count_list(X,L2,N2), N is N1 + N2.
	
% imo lepiej działa (i wygląda) to :

count(A,A,1) :- !.
count(A,T,N) :- 
	T =.. [_|L], count_list(A, L, N).
count_list(_, [], 0) :- !.
count_list(A, [H|L], N) :- 
	count(A, H, N1), count_list(A,L,N2), N is N1 + N2.