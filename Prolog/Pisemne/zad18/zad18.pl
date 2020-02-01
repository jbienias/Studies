Proszę dla następujących pytań podać drzewo SLD. Jakie odpowiedzi daje Prolog?

a)   s(X) :- p(X), r(X).
     s(X) :- q(X).
     p(a).   q(a).   r(a).
     p(b).   q(c).   r(b).                        ?- s(X).

b)   q(X) :- p(X), r(X).
     p(a).
     r(a).
     r(f(Y)) :- r(Y).                             ?- r(Z).  oraz  ?- q(Z).

c)   p(X,Z) :- q(X,Y), p(Y,Z).
     p(X,X).
     q(a,b).                                      ?- p(X,b). 

