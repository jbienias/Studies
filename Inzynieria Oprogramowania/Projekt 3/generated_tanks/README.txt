Wygenerowane rodzaje czołgów ("najciekawsze(?)") :
a) krecacy.txt
   wygenerowany (losowo) neuralnet : jazda w koło + strzelanie
   przetrenowany neuralnet (1h) : krecenie sie w miejscu

b) przestraszony.txt
   wygenerowany (losowo) neuralnet : jazda w koło
   przetrenowany neuralnet (0.5h) : nagłe ruchy (zarówno samego czołgu jak i lufy) z dużymi interwałami (zespół Tourreta)
   
c) obserwator.txt
   wygenerowany (losowo) neuralnet : reagowanie na nadlatujący pocisk wroga - stara sie uciec od pocisku; podąża za nim lufą
   przetrenowany neuralnet (0.5h) : w 90% stanie w miejscu; czasami reaguje na zbliżający się pocisk przeciwnika; (bardzo rzadko) strzela mniej wiecej w kierunku wroga / pocisku wroga
   
d) snajper.txt
   wygenerowany (losowo) neuralnet : strzelanie gdy tylko to możliwe, bezruch
   przetrenowany neuralnet (0.2h) : bezczynność

e) czempion.txt <----- (SKRYPT DO TURNIEJU)
   wygenerowany (losowo) neuralnet : agresywny, jeżdżący chaotycznie, strzelający kiedy tylko to możliwe (faworyzuje rogi)
   przetrenowany neuralnet (0.015h) : =||=

... i wiele więcej (mniej "ciekawych")


Wnioski : Im więcej iteracji / czasu poświęconego na trening, tym bardziej czołg staje się "spokojny" - wykonuje mniej ruchów, staje się mało dynamiczny i co najgorsze - przestaje strzelać.
