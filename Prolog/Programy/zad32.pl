%Proszę zdefiniować predykat filter(P,L1,L2), który jest spełniony, jeżeli lista L2 zawiera dokładnie te elementy z listy L1, który spełniają predykat P.
%Przykład:

pos(X) :- X > 0.
neg(X) :- X < 0.

% ?- filter(pos,[1,2,0,5,-5,-6,8],L).
% L = [1,2,5,8]
% ?- filter(neg,[1,2,0,5,-5,-6,8],L).
% L = [-5,-6]

filter(_, [], []).
filter(P, [X|L1], [X|L2]) :-
	T =.. [P, X],
    call(T), ! ,
    filter(P, L1, L2).
filter(P, [_|L1], L2) :-
    filter(P, L1, L2).
    
    