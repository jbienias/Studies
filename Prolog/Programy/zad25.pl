% Proszę zdefiniować predykat difference(A,B,C) 
% dla zbiorów A, B i C, który jest spełniony, wtedy i tylko wtedy 
% C = A \ B . 

% wersja A
diff_almost_built_in(A,B,C):- 
    findall(X, (member(X,A), not(member(X,B))), C).

% wersja B
diff([], _, []).
diff([X|A], B, C):- 
    member(X, B), !, 
    diff(A, B, C). 
diff([X|A], B, [X|C]):- 
    diff(A, B, C).


% wersja C
difference(_,[],_).
difference(A,[X|B], C) :- 
    select(X,A,C1),
    difference(C1,B,C).
