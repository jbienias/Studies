#JAN BIENIAS 238201
#PROBLEM 3SAT
#MAIN

library(genalg)
file_3SAT <- "./samples/"

#FUNKCJA FITNESS

var_location_array <- c()

fitness3SAT <- function(chromosome){
  counter <- 0
  for(i in seq(1, length(var_location_array), by = 3)) {
    trio <- c(0,0,0)
    for(j in 0:2) {
      if (var_location_array[i+j] < 0) {
        trio[j+1] <- negate(chromosome[abs(var_location_array[i+j])])
      } else {
        trio[j+1] <- chromosome[abs(var_location_array[i+j])]
      }
    }
    if(1 %in% trio) {
      counter <- counter+1
    }
  }
  return(-counter)
}

#PKT I - Badanie konkretnej instancji (zmiana parametrów a czas i poprawność wyników)
chosen_instance_file <- "./samples/91/2.cnf"
var_location_array <- load_variables_locations_from_file(chosen_instance_file)

data <- data.frame(Wynik_funkcji_fitness=numeric(),Indeks_wyniku=numeric(),Czas=numeric(),Szansa_na_mutacje=numeric(),
                   Populacja=numeric(),Elityzm=numeric(),stringsAsFactors=FALSE)

for(i in seq(0.00, 1, by = 0.05)) {
  for(j in seq(10,200, by=10)) {
    for(e in c(T,F)) {
      threeSATGenAlg <- 0
      time <- system.time(threeSATGenAlg <- rbga.bin(size = length(unique(abs(var_location_array))), popSize = j, iters = 100,
                                                     mutationChance = i, elitism = e, evalFunc = fitness3SAT))[1]
      best_score <- max(abs(threeSATGenAlg$best))
      best_score_index <- which.min(threeSATGenAlg$best)
      data[nrow(data) + 1,] = c(best_score, best_score_index, time, i, j, e)
      message(sprintf("Wykonano i: %f, j: %d, e: %s", i, j, e))
    }
  }
}

data
data[order(data$Wynik_funkcji_fitness, decreasing=TRUE), ]
data[order(data$Czas), ]
write.csv(data, "data.csv")
write.csv(data[order(data$Wynik_funkcji_fitness, decreasing=TRUE), ], "data_wynikiFitnessMalejaco.csv")
write.csv(data[order(data$Czas), ], "data_czasyRosnaco.csv")

#PKT II - Porównanie czasów wykonywania
mean_time_vector <- c()
size_vector <- c(91, 218, 325, 430, 538)

for(i in size_vector) {
  time <- 0
  for(j in c(1,2,3)) {
    current_file <- paste(file_3SAT,paste(i, paste("/", paste(j, ".cnf", sep=""), sep=""), sep=""), sep="")
    message(sprintf("Wczytuje plik %s", current_file))
    var_location_array <- load_variables_locations_from_file(current_file)
    threeSATGenAlg <- 0
    time <- time + system.time(threeSATGenAlg <- rbga.bin(size = length(unique(abs(var_location_array))), popSize = 200, iters = 100,
                                 mutationChance = 0.05, elitism = T, evalFunc = fitness3SAT))[1] #uzyskanie czasu "zegarowego"
    message(sprintf("Najlepszy wynik: %d/%d, znaleziony w iteracji: ", abs(min(threeSATGenAlg$best)), i))
  }
  mean_time = time / 3.0
  mean_time_vector <- c(mean_time_vector, mean_time)
}
write.csv(data.frame(c(size_vector, mean_time_vector)), "dokladne_czasy.csv")
plot(size_vector, mean_time_vector, type="o", pch=19,col="blue", xlab="Ilość klauzul", ylab="Średni czas wykonania (sek)", main="Czasy wykonywania alg. genetycznego")
