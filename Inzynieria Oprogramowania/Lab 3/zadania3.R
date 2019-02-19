#ZAD 1
library(genalg)
duzyProblemPlecakowy <- data.frame(wartosc = sample(10:100,30), waga = sample(10:100,30))
#FUNCTION
duzyLimit <- 600
fitnessFunc2 <- function(chr){
  calkowita_wartosc_chr<-chr%*%duzyProblemPlecakowy$wartosc
  calkowita_waga_chr<-chr%*%duzyProblemPlecakowy$waga
  if(calkowita_waga_chr > duzyLimit)
    return(0)
  else
    return(-calkowita_wartosc_chr)
}
#END OF FUNCTION

duzyPlecakGenAlg<-rbga.bin(size = 30, popSize = 200, iters = 50,
                          mutationChance = 0.03, elitism = T, evalFunc = fitnessFunc2)

chartData <- data.frame(srednia = -duzyPlecakGenAlg$mean, maksymalne = -duzyPlecakGenAlg$best)

plot(1:nrow(chartData), chartData$maksymalne, type="l", col="red", xlab="pokolenie", ylab="fitness (ocena)", main="Działanie Alg. Genetycznego")
lines(chartData$srednia, col="blue")
legend(x="bottomright", y=10, legend=c("maksymalne", "srednia"), lty=c(1, 1), lwd=c(2.5,2.5), col=c("red", "blue"))

#ZAD 2
library(genalg)
#FUNCTION
duzyLimit <- 600
fitnessFunc2 <- function(chr){
  calkowita_wartosc_chr<-chr%*%duzyProblemPlecakowy$wartosc
  calkowita_waga_chr<-chr%*%duzyProblemPlecakowy$waga
  if(calkowita_waga_chr > duzyLimit)
    return(0)
  else
    return(-calkowita_wartosc_chr)
}
#END OF FUNCTION

times <- data.frame(Rozmiar=character(),
                    Czas=character(),
                    stringsAsFactors=FALSE) 

for (s in c(30, 60, 120, 200)) {
  duzyProblemPlecakowy <- data.frame(wartosc = sample(10:100, s), waga = sample(10:100, s))
  
  times[nrow(times) + 1,] = c(s, system.time(rbga.bin(size = s, popSize = 200, iters = 100, mutationChance = 0.03, elitism = T, evalFunc = fitnessFunc2)))
}

plot(times$Rozmiar, times$Czas, type="l", col="red", xlab="Rozmiar", ylab="Średni czas wykonania", main="Wykres zad 2")