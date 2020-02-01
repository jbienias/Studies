% Niech będzie dana następująca definicja.

% max(X,Y,X) :- X >= Y, !.
% max(X,Y,Y).

% Na kolejne pytanie Prolog odpowiada "błędnie":

% ?- max(5,2,2).
% yes

%Dlaczego Prolog tak odpowiada i jak można to zreperować (bez zmian cut'u i drugiej reguły)? 

max(X,Y,Z) :- X >= Y, !, Z is X.
max(_,Y,Y).