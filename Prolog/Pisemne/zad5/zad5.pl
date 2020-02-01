% Niech będą dane następujące predykaty: 

 q1(X,Y) :- p(X,Y).                q2(X,Y) :- p(X,Z), q2(Z,Y).
 q1(X,Y) :- p(X,Z), q1(Z,Y).       q2(X,Y) :- p(X,Y).  

 q3(X,Y) :- p(X,Y).                q4(X,Y) :- q4(X,Z), p(Z,Y).  
 q3(X,Y) :- q3(X,Z), p(Z,Y).       q4(X,Y) :- p(X,Y).  

% oraz baza danych dla predykat p:

 p(pam,bob).       
 p(tom,bob).
 p(tom,liz).
 p(bob,ann).      
 p(bob,pat).     
 p(pat,jim). 
 
% Używając drzewo odpowiedzi proszę zilustrować, jak Prolog odpowiada na pytania
% ?-qi(tom,pat). oraz ?-qi(liz,jim).