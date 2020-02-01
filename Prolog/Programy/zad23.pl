%Proszę zoptimalizować następucący program używając green cuts.

% class(Number, positive) :- Number > 0.
% class(0,zero).
% class(Number, negative) :- Number < 0. 

%Green :
class(Number,positive) :- Number > 0,!.
class(0, zero) :- !.
class(Number, negative) :- Number < 0.

%Red :
class(Number,positive) :- Number > 0, !.
class(0, zero) :- !.
class(Number,negative).