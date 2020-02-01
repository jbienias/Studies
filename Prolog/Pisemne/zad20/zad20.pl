Proszę dla następującego pytania podać drzewo SLD. Jakie odpowiedzi daje Prolog?

is_list([]).
is_list([X|L]) :- is_list(L).                      

?- is_list(L).