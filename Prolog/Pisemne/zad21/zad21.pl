Używając dzrewa SLD proszę pokazać jak Prolog odpowiada na następujące pytania.

a)   p(x) :- a(x).
     p(x) :- b(x), c(x), !, d(x).
     p(x) :- f(x).                       
     a(1).     
     b(1).     
     c(1).
     b(2).     
     c(2).     
     d(2).
     f(3).

     ?- p(x). 

b)   p(1). 
     p(2) :- !.
     p(3).

     ?- p(x).       ?- p(x), p(y).    ?- p(x), !, p(y). 

