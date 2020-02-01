%Proszę zdefiniować następujące predykaty dla list.

%a)   last(X,L), który jest spełniony, jeżeli X jest ostatnim elementem listy L.
%b)   delete(X,L1,L2), który jest spełniony, jeżeli L2 równa się L1 bez elementu X.
%c)   delete(L1,L2), który jest spełniony, jeżeli L2 równa się L1 bez ostatnich trzech elementów.
%d)   reverse(L1,L2), który jest spełniony, jeżeli L2 jest listą L1 w odwrotnej kolejności.
%e)   evenlength(L) oraz oddlength(L), które są spełnione, jeżeli długość listy L jest parzysta oraz nieparzysta.
%f)   shift(L1,L2), który jest spełniony, jeżeli L2 równa się L1 po jednej rotacji do lewej.
%   Przykład:

%    ?- shift([1,2,3,4,5],L).
%    L = [2,3,4,5,1] 

%g)   quadrat(L1,L2), który jest spełniony, jeżeli L2 zawiera quadraty elementów listy L1.
%   Przykład:

%    ?- quadrat([1,2,3,4,5],L).
%    L = [1,4,9,16,25] 

%h)   combine(L1,L2,L3), który jest spełniony, jeżeli L3 zawiera pary elementów z list L1 i L2.
%   Przykład:

%    ?- combine([1,2,3,4],[a,b,c,d],L).
%    L = [[1,a],[2,b],[3,c],[4,d]] 

%i)   palindrom(L), który jest spełniony, jeżeli lista L zawiera palindrom.
%   Przykłady:

%    ?- palindrom([a,b,c]).
%    no
%    ?- palindrom([a,b,c,d,c,b,a]).
%    yes 

%j)   p(X,L,Y,Z), który jest spełniony, jeżeli Y jest poprzednikiem elementu X w liście L a Z następcą elementu X w liście L.
%   Przykład:

%    ?- p(3,[1,2,3,4,5],Y,Z).
%    Y = 2, Z = 4 

%k)   q(X,L1,L2), który jest spełniony, jeżeli L2 równa się początku listy L1 do podwójnego wystąpienia elementu X.
%   Przykład:

%    ?- q(3,[1,2,3,3,1,2,4],Z).
%    Z = [1,2,3,3] 

%a)

last2(X,[X]).

last2(X,[_|T]) :- 
    last2(X,T).
	
%b)

delete2(_, [], []).

delete2(X, [X|L], L).

delete2(X, [_|L1], [_|L2]) :-
    delete2(X,L1,L2).

%c)

delete_last_3([_,_,_], []).

delete_last_3([X|L1], [X|L2]) :-
    delete_last_3(L1, L2).

%d)

reverse2([],[]).

reverse2([H|T],L):-
    reverse2(T,R),
    append(R,[H],L).

%e)

evenlength([]).

evenlength2([_,_]).

evenlength2([_,_|T]) :-
    evenlength2(T).

oddlength2([_]).

oddlength2([_,_|T]) :-
    oddlength2(T).

%f)

shift2([],[]).

shift2([X|T], L) :-
  append(T, [X], L).

%g)

quadrat2([],[]).

quadrat2([X], [Y]) :-
    Y is X^2.

quadrat2([X|Tx],[Y|Ty]) :-
    Y is X^2,
    quadrat2(Tx, Ty).

%h)

combine([], [], []).

combine([X|L1],[Y|L2], [[X,Y]|L3]):-
    combine(L1, L2, L3).

% lub tak : (jesli bedziemy mogli uzyc 2 argumentow)
combine2([],[]).
combine2([X|L1],[Y|L2], [H|L3]):-
	H = [X,Y], combine2(L1, L2, L3).

%i)

palindrom2(X) :-
    reverse2(X,X).

%j)

p(X,[Y,X,Z|_],Y,Z).

p(X,[_|L],Y,Z):-
    p(X,L,Y,Z).

%k)

q(X,[X,X|_],[X,X|[]]).

q(X,[Y|L1],[Y|L2]):-
    q(X,L1,L2).