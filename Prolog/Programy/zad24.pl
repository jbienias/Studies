%Proszę zdefiniować predykat delete(X,L1,L2), który jest spełniony, wtedy i tylko wtedy L2 jest listą L1 bez wszystkich wystąpień elementu X. 

delete(_,[],[]).
delete(X,[X|L1],L2) :- !, delete(X,L1,L2).
delete(X,[Y|L1],[Y|L2]) :- delete(X,L1,L2).