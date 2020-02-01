%Niech będzie dana baza danych zawierająca predykaty parent/2, female/1 i male/1.
%Proszę zdefiniować predykaty child/2, mother/2, sister/2, has_a_child/1, grandparent/2 oraz predecessor/2. 

female(ania).
female(barbara).
female(celina).
female(daria).
female(ewelina).
male(adam).
male(bogdan).
male(czarek).
male(damian).

%parent(parent, child)
parent(ania, barbara).
parent(adam, barbara).

parent(barbara, czarek).
parent(bogdan, czarek).

parent(czarek, celina).

parent(daria, damian).

%X is child of Y
child(X, Y) :-
    parent(Y, X).

%X is mother of Y
mother(X, Y) :-
    parent(X, Y),
    female(X).

%X is a sister of Y
sister(X, Y) :-
    X \= Y,
    female(X),
    parent(M, X),
    parent(M, Y).

has_a_child(X) :-
    parent(X, _).
    %or child(_, X).

grandparent(X, Y) :-
    parent(Z, Y),
    parent(X, Z).

%X is a predecessor of Y
predecessor(X, Y) :-
    parent(X, Y).

predecessor(X, Y) :-
    parent(Z, Y),
    predecessor(X, Z).
