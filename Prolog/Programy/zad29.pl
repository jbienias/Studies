%Proszę zdefiniować predykat variables(T,L), który jest spełniony, jeżeli L jest listą zmiennich w termie T.

check_list([], []).
check_list([X|L1], [X|L2]) :-
    check_list(L1, L2).

variables(T, L) :-
    T =.. [_|L1],
    check_list(L1, L).