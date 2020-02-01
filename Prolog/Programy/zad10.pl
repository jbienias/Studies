%Drzewo binarne D jest albo pusty (repezentowane przez nil) albo zawiera element X i dwa poddrzewa L i P (reprezentowane przez drzewo(X,L,P)).
%Proszę zdefiniować następujące predykaty dla drzew.

%a)    size(D,N), który jest spełniony, jeżeli N jest ilością elementów drzewa D.
%b)    search(D,N), który jest spełniony, jeżeli N jest elementem drzewa D.
%c)    max(D,N), który jest spełniony, jeżeli N jest maximum elementów w drzewie D.
%d)   times(N,D1,D2), który bierzy wszystkie wartości węzłów drzewa D1 razy N.
%e)    preorder(D,L), który jest spełniony, jeżeli lista L zawiera elementy drzewa D w kolejności prefiksowym.
%f)    subtree(D1,D2), który jest spełniony, jeżeli drzewo D1 jest poddrzewem drzewa D1.
%g)    subst(D1,D2,D3,D4), który jest spełniony, jeżeli drzewo D4 jest drzewem D2, w którym wszystkie poddrzewa D1 zostały zmienione na drzewo D4. 

%a)

size2(nil,0).
size2(drzewo(_,L,R),N) :- 
	size2(L,N1),
	size2(R,N2),
	N is N1 + N2 + 1.

%b)

search2(drzewo(X,_,_),X).
search2(drzewo(_,L,_),X) :- search2(L,X).
search2(drzewo(_,_,R),X) :- search2(R,X).

%c)

max2(drzewo(N, nil, nil), N).
max2(drzewo(X, L, nil), N) :-   
    max2(L, X1),
    N is max(X, X1).
max2(drzewo(X, nil, R), N) :- 
	max2(R, X1),
	N is max(X, X1).
max2(drzewo(X, L, R), N) :-
    max2(L, X1),
    max2(R, X2),
    X3 is max(X1, X2),
    N is max(X, X3).

%d) 

times(_,nil,nil).
times(N,drzewo(X1,L1,R1),drzewo(X2,L2,R2)) :- 
	X2 is N * X1,
	times(N,L1,L2),
	times(N,R1,R2).
	
%e)

preorder(nil, []).
preorder(drzewo(X,L,R), T) :-
    preorder(L, T1),
    preorder(R, T2),
    append([X|T1], T2, T).

inorder(nil, []).
inorder(drzewo(X,L,R), T) :-
    inorder(L, T1),
    inorder(R, T2),
    append(T1, [X|T2], T).


postorder(nil, []).
postorder(drzewo(X,L,R), T) :-
    postorder(L, T1),
    postorder(R, T2),
    append(T1,T2 ,T3),
    append(T3, [X], T).
	
%f)

subtree(nil,nil).
subtree(drzewo(X,L,R),drzewo(X,L,R)).
subtree(drzewo(_,L,_),L).
subtree(drzewo(_,_,R),R).
subtree(drzewo(_,L,_),D) :- subtree(L,D).
subtree(drzewo(_,_,R),D) :- subtree(R,D).

%g)

subst(D1,drzewo(X,D1,D1),D3,drzewo(X,D3,D3)).
subst(D1,drzewo(X,D1,R),D3,drzewo(X,D3,R)).
subst(D1,drzewo(X,L,D1),D3,drzewo(X,L,D3)).
subst(D1,drzewo(X,L1,R1),D3,drzewo(X,L2,R2)):-
	subst(D1,L1,D3,L2),
	subst(D1,R1,D3,R2).