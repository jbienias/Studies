%Niech będzie dana następująca definicja predykatu memberc(X,L)

member(X,[X|_]).
member(X,[_|L]) :- member(X,L).

memberc(X,[X|_]) :- !.
memberc(X,[_|L]) :- memberc(X,L).