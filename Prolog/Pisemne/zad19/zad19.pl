Proszę dla następujących pytań podać drzewo SLD. Jakie odpowiedzi daje Prolog?

     append1([],L,L).
     append1([H|L1],L2,[H|L3]) :- append1(L1,L2,L3).

     append2([H|L1],L2,[H|L3]) :- append2(L1,L2,L3).
     append2([],L,L).                                   
	 
	 ?- appendi(X,[3,4],[2,3,4]).

