%Proszę zdefiniować predykat if-then-else(W,C,A), który realizuje operator "if W then C else A".

%1 wersja

if-then-else(W,C,A) :-
    (W -> C ; A).
	
	
%2 wersja
if-then-else(W,C,_) :-
	W,!,C.
	
if-then-else(_,_,A) :-
	A.