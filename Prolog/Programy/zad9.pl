%Proszę zdefiniować predykat permutation(L1,L2), który jest spełniony, jeżeli lista L2 jest permutacją listy L1.
%Przy użyciu ";" powiennien być możliwe wyliczać wszystkie permutacje listy L1. 

delete2(X,[X|L1],L1).
delete2(X,[Y|L1],[Y|L2]):-
	delete2(X,L1,L2).
permute([],[]).
permute(L,[X|P]):-
	delete2(X,L,L1),
	permute(L1,P).
	
%lub (imo lepiej)

takeout(X,[X|R],R).  
takeout(X,[F |R],[F|S]) :- takeout(X,R,S).

perm([],[]) :- !.
perm([X|Y],Z) :- perm(Y,W), takeout(X,Z,W).  