%Długość listy można w Prologu zdefiniować następująco:

%  length(0,[]).
%  length(N,[_|L]) :- length(M,L), N is M+1.

%Proszę zdefiniować predykaty member/2 oraz sil/2, fib/2 i nwd/3. 

member2([X|_], X).
member2([_|X], L) :- member2(X, L).

sil(0, 1).
sil(1, 1).
sil(N, R) :-
    N > 1,
    N1 is N - 1, sil(N1, R1),
    R is N * R1.
	
fibb(0, 0).
fibb(1, 1).
fibb(N, A) :-
    N > 1,
    N1 is N - 1, fibb(N1, A1),
    N2 is N - 2, fibb(N2, A2),
    A is A1 + A2.
	
nwd(A, 0, A).
nwd(0, A, A).
nwd(A, B, C) :-
   A \= 0,
   B \= 0,
   D is A mod B,
   nwd(B, D, C).