%Proszę zdefiniować predykat rzym/2, który transformuje rzymskie liczby do liczb dziesiętnich.
%Liczby rzymskie można po prostu reprezentować jako listy, n.p. [x,l,v,i,i].

rzym([], 0).
rzym(i, 1).
rzym(v, 5).
rzym(x, 10).
rzym(l, 50).
rzym(c, 100).

rzym([X,X,X|L], W) :- rzym(X, XV), XW is XV*3, rzym(L, W1), W is XW + W1.
rzym([X,X|L], W) :- rzym(X, XV), XW is XV*2, rzym(L, W1), W is XW + W1.
rzym([X,Y|L], W) :- rzym(X, XV), rzym(Y, YV), XV < YV, XW is YV-XV, rzym(L,W1), W is XW + W1.
rzym([X,Y|L], W) :- rzym(X, XV), rzym(Y, YV), XV > YV, XW is XV+YV, rzym(L,W1), W is XW + W1.
rzym([X|L], W) :- rzym(X, XV), rzym(L, W1), W is XV + W1.