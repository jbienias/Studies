#Projekt II - Zgłębianie danych
#Jan Bienias 238201

library("deducorrect")
library("party")
library("e1071")
library("MASS")
library("class")
library("arules")
library("arulesViz")
library("ggplot2")
library("scales")

data.location <- paste(dirname(rstudioapi::getSourceEditorContext()$path), "/wowbg.csv", sep="")
rules.location <- paste(dirname(rstudioapi::getSourceEditorContext()$path), "/wowbg_rules.txt", sep="")

normalize <- function(vector) {
  (vector - min(vector))/((max(vector))-min(vector))
}

#Zmienna przechowująca stałą wartość do set.seed
rng = 1337

#Przygotowanie danych
#Ładując dane: 
# - pomijam dwie pierwsze kolumny, gdyż nie interesują nas specyficzne numery porządkowe instancji battlegroundowych, z ktorych dane zostaly pobrane
# - pomijam wczytanie 12 kolumny o nazwie Lose, gdyż wywnioskujemy stan wygranej / przegranej za pomoca jednej kolumny - Win, zmieniajac jej puste wartości na 0
# - pomijam wczytanie ostatniej kolumny (14), Bonus Event, poniewaz jest jej malo + nie ma tak duzego wplywu (ok. 2% wiecej) na rozgrywke (otrzymywane pkty)
# - zmieniam kolejnosc kolumn, tak aby kolumna z "klasą" była na końcu - w tym przypadku kolumna Win (nazwe kolumny Win zmienię na Result z wartościami Win/Defeat)
data <- read.csv(data.location)[c(3,4,5,6,7,8,9,10,13,11)]
#Rozwijam skróty aby kolumny były bardziej zrozumiałe, np. : KB = Killing blows, etc
colnames(data) <- c("Faction", "Character_class", "Killing_blows", "Deaths", "Honorable_kills", "Damage_done", 
                    "Healing_done", "Honor", "Role", "Result")
#Zamiana typu danych w kolumnie Result na stringowe
data$Result <- sapply(data$Result, as.character)
#Następnie wartości NA zamieniam na Defeat, zaś 1 na Win
for(i in (1:nrow(data))) {
  if(is.na(data$Result[i])) {
    data$Result[i] <- "Defeat"
  } else {
    data$Result[i] <- "Win"
  }
}
#Kolumna Result musi byc factorem
data$Result <- sapply(data$Result, as.factor)
#Kopiuję dane bez correction rules (przyda się później do reguł asocjacyjnych)
data.raw <- data
#Tworzymy tymczasową kolumnę, w ktorej bedziemy przypisywac numer klasy (np. 0 = Warrior) - plik wowbg_rules.txt
data$Class_as_int <- sapply(data$Character_class, as.numeric)
#Tworzymy tymczasową kolumnę, w której będziemy przypisywać numer roli (1 = DPS, 0 = healer) - plik wowbg_rules.txt
data$Role_as_int <- sapply(data$Role, as.numeric)
#Tworzymy tymczasową kolumnę, w której będziemy przypisywać numer frakcji (1= Horda, 0 = Alliance) - plik wowbg_rules.txt
data$Faction_as_int <- sapply(data$Faction, as.numeric)
#Correction rules: 
# uzupelnia trzy tymczasowe kolumny odpowiednimi integerami
cr <-correctionRules(rules.location)
data <- correctWithRules(cr, data)$corrected
#Po poprawie danych (przypisywanie odpowiedniego inta do stringa), przepisujemy zawartosc z tymczasowych kolumn do kolumn właściwych - zamieniamy kolumny na liczbowe :)
data$Character_class <- data$Class_as_int
data$Role <- data$Role_as_int
data$Faction <- data$Faction_as_int
data <- data[,1:10]
###################################################################################################
#Podzial na dane testowe i treningowe
set.seed(rng)
ind <- sample(2, nrow(data), replace=TRUE, prob=c(0.67, 0.33))
#Zwykłe :
data.training <- data[ind==1,] 
data.test <-data[ind==2,]
#Znormalizowane :
data.norm <- normalize(data[1:9])
data.norm <- cbind(data.norm, data[10])
data.norm.training <- data.norm[ind==1,]
data.norm.test <- data.norm[ind==2,]
#Faktyczne wartości klas z danych testowych
real <- data.test[,10]
#nrow(data.training) 3595
#nrow(data.test) 1788
#nrow(data.)
###################################################################################################
#Klasyfikatory - testowanie : macierze błędów, dokładność
#a) Drzewo (paczka party)
data.ctree <-ctree(Result ~ ., data=data.training)
predicted_tree <- predict(data.ctree, data.test[,1:9])
cm_tree <- table(predicted_tree,real) #macierz błędów (confusion matrix) dla drzewa
accuracy_tree <- sum(diag(cm_tree))/sum(cm_tree) #dokładność (accuracy) dla drzewa

#b) Naive Bayes (paczka e1071)
data.model <-naiveBayes(Result ~ ., data=data.training)
predicted_nb <- predict(data.model, data.test[,1:9])
cm_nb <- table(predicted_nb,real) #macierz błędów (confusion matrix) dla Naive Bayes'a
accuracy_nb <- sum(diag(cm_nb))/sum(cm_nb) #dokładność (accuracy) dla Naive Bayes'a

#c) kNN (paczka class)
knn.3 <-knn(data.norm.training[,1:9], data.norm.test[,1:9], 
            cl=data.norm.training[,10], k = 3, prob=FALSE) #3 najblizszych sasiadow
predicted_knn <- knn.3
cm_knn <- table(predicted_knn,real) #macierz błędów (confusion matrix) dla kNN
accuracy_knn <- sum(diag(cm_knn))/sum(cm_knn) #dokładność (accuracy) dla kNN

#d) Inny wybrany klasyfikator : Linear Discriminant Analysis (LDA) (paczka MASS)
lda <- lda(Result ~ ., data=data.training)
predicted_lda <- predict(lda, data.test[,1:9])$class
cm_lda <- table(predicted_lda, real) #macierz błędów (confusion matrix) dla LDA
accuracy_lda <- sum(diag(cm_lda))/sum(cm_lda) #dokładność (accuracy) dla LDA

#Wykres słupkowy
accuracies <- c(accuracy_tree, accuracy_lda, accuracy_nb, accuracy_knn) * 100
accuracies_prcnt <- paste("~", paste(format(accuracies, digits=2, nsmall=2), "%"))
classification_names <- c("Drzewo", "LDA", "Naive Bayes", "kNN")
bp <- barplot(accuracies, main = "Dokładność (%) klasyfikatorów",
        names.arg = classification_names, 
        col = c("dodgerblue4", "dodgerblue3", "dodgerblue2", "dodgerblue1"), ylim = c(0, 100))
text(x = bp, y = accuracies - 15, label = accuracies_prcnt, pos = 3, cex = 1, col = "white")
###################################################################################################
#Funkcje pomocnicze: 
#https://en.wikipedia.org/wiki/Confusion_matrix
TP <- function(x) { #true positive - prawdziwie zaklasyfikowane jako pozytywne
  return(x[1,1])
}
FP <- function(x) { #false positive - fałszywie zaklasyfikowane jako pozytywne
  return(x[1,2])
}
TN <- function(x) { #true negative - prawdziwie zaklasyfikowane jako negatywne
  return(x[2,2])
}
FN <- function(x) { #false negative - fałszywie zaklasyfikowane jako negatywne
  return(x[2,1])
}
TPR <- function(x) { #odsetek prawdziwie pozytywnych (czułość)
  return (TP(x)/(TP(x)+FN(x)))
}
FPR <- function(x) { #odsetek fałszywie pozytywnych
  return (FP(x)/(FP(x)+TN(x)))
}
TNR <- function(x) { #odsetek prawdziwie negatywnych (swoistość)
  return (TN(x)/(TN(x)+FP(x)))
}
FNR <- function(x) { #odsetek fałszywie negatywnych
  return (FN(x)/(FN(x)+TP(x)))
}

crs <- data.frame("Drzewo", FPR(cm_tree), TPR(cm_tree)) #cr - classification ratings
names(crs) <- c("Name", "FPR", "TPR")
crs <- rbind(crs, data.frame("Name" = "Naive Bayes", "FPR" = FPR(cm_nb), "TPR" = TPR(cm_nb)))
crs <- rbind(crs, data.frame("Name" =  "kNN", "FPR" = FPR(cm_knn),"TPR" = TPR(cm_knn)))
crs <- rbind(crs, data.frame("Name" = "LDA", "FPR" = FPR(cm_lda), "TPR" = TPR(cm_lda)))
crs <- rbind(crs, data.frame("Name" = "Ideal", "FPR" = 0, "TPR" = 1))

colors <- c("red", "blue", "orange", "green", "black")
plot(x = crs$FPR, y = crs$TPR, col = colors[unique(crs$Name)], pch=19,
     xlab = "False Positive Rate", ylab = "True Positive Rate",ylim = c(0, 1), xlim=c(0,1), main = "ROC - ocena jakości klasyfikacji")
grid(nx = NULL, ny = NULL, col = "black", lty="dotted", lwd = par("lwd"), equilogs = TRUE)
legend(x="topright", legend = unique(crs$Name),col=colors[unique(crs$Name)], pch=19)

###################################################################################################
#Grupowanie metodą k-średnich
#data.log <- log(data[,1:9]) #- logarytm od 0? średnio ;_;
data.stand <- scale(data[,1:9], center=TRUE)
data.pca <- prcomp(data.stand)
data.kmeans <- predict(data.pca)
#Grupujemy wszystkie rekordy na 2,3 i 4 klastry :
km_2 <- kmeans(data.kmeans, 2, iter.max = 100, algorithm = c("Lloyd"), trace=FALSE)
km_3 <- kmeans(data.kmeans, 3, iter.max = 100, algorithm = c("Lloyd"), trace=FALSE)
km_4 <- kmeans(data.kmeans, 4, iter.max = 100, algorithm = c("Lloyd"), trace=FALSE)

#Funkcja pomocnicza, która wylicza ilość wystąpień rodzaju klasy w klastrach
clusterInfo <- function(cluster, data) {
  #Obliczamy ilosc klastrow
  n <- length(unique(cluster))
  #Tworzymy trzy wektory numeryczne o dlugosci odpowiadajacej ilosci klastrow
  count_of_elements <- vector(mode="numeric", length=n) #wszystkie elementy klastra
  win <- vector(mode="numeric", length=n) #ilosc elementow z wartoscia wygranej
  defeat <- vector(mode="numeric", length=n) #ilosc elementow z wartoscia przegranej
  #Dla kazdego wiersza danych
  for(i in 1:nrow(data)) {
    #Dla kazdego klastra
    for(j in 1:n) {
      #Jesli wiersz i jest w klastrze j
      if (cluster[i] == j) {
        #Dodaj +1 do licznika wszystkich elementow
        count_of_elements[j] <- count_of_elements[j] + 1
        #Dodaj +1 do j'tego miejsca w wektorze w win badz defeat, w zależności od wartośći klasy
        if(data$Result[i] == "Win") 
          win[j] <- win[j] + 1
        else 
          defeat[j] <-defeat[j] + 1
      }
    }
  }
  return (data.frame(count_of_elements,win,defeat))
}

#plot dla 2 klastrów
plot(data.kmeans, col=colors[km_2$cluster], main = "Grupowanie k-średnich (2 klastry)")
points(km_2$centers, pch=10, cex=3, lwd=3)
legend(x="bottomleft", legend = unique(km_2$cluster),col=colors[unique(km_2$cluster)], pch=1)
km_2_info <- clusterInfo(km_2$cluster, data)
#plot dla 3 klastrów
plot(data.kmeans, col=colors[km_3$cluster], main = "Grupowanie k-średnich (3 klastry)")
points(km_3$centers, pch=10, cex=3, lwd=3)
legend(x="bottomleft", legend = unique(km_3$cluster),col=colors[unique(km_3$cluster)], pch=1)
km_3_info <- clusterInfo(km_3$cluster, data)
#plot dla 4 klastrów
plot(data.kmeans, col=colors[km_4$cluster], main = "Grupowanie k-średnich (4 klastry)")
points(km_4$centers, pch=10, cex=3, lwd=3)
legend(x="bottomleft", legend = unique(km_4$cluster),col=colors[unique(km_4$cluster)], pch=1)
km_4_info <- clusterInfo(km_4$cluster, data)
#jak jest naprawdę
plot(data.kmeans, col=colors[data$Result], main = "Oryginalny podział danych")
legend(x="bottomleft", legend = unique(data$Result),col=colors[unique(data$Result)], pch=1)

###################################################################################################
#Reguły asocjacyjne :
#Przygotowanie: 
#Jako że do reguł asocjacyjnych nie mogą być wykorzystywane liczby, to:
# - korzystam z wcześniej przygotowanych danych, ale bez zamiany stringów na int (dotyczy to kolumn character_class, faction i role)
# - obliczam średnie wartości kolumn : Killing_blows, Deaths, Honorable_kills, Damage_done, healing_done, honor, następnie przypisuje
#   stringi Above average i Below average
# - dzielę klasy postaci na trzy typy : klasy walczące wręcz (melee), walczące zasięgowo (ranged) i takie, które są wszechstronne (versatile)
# - na koncu upewniam sie ze wszystkie kolumny sa factorami lub booleanami

#Funkcje pomocnicze:
belowOrAboveAverage <- function(x, avg) {
  if (x >= avg) {
    return ("Above average")
  } else {
    return("Below average")
  }
}

distinguishMeleeFromRanged <- function(x) {
  if (x %in% c("Warrior", "Paladin", "Rogue", "Monk", "Demon Hunter", "Death Knight")) {
    return ("Melee")
  } else if (x %in% c("Hunter", "Priest", "Mage", "Warlock")) {
    return ("Ranged")
  } else { #Shaman i Druid mogą walczyć zarówno wręcz jak i zasięgowo
    return ("Versatile")
  }
}

Killing_blows_avg <- mean(data.raw$Killing_blows)
Deaths_avg <- mean(data.raw$Deaths)
Honorable_kills_avg <- mean(data.raw$Honorable_kills)
Damage_done_avg <- mean(data.raw$Damage_done)
Healing_done_avg <- mean(data.raw$Healing_done)
Honor_avg <- mean(data.raw$Honor)


avg_names <- c("Killing_blows", "Deaths", "Honorable_kills", "Damage_done", "Healing_done", "Honor")
avg_values <- c(Killing_blows_avg, Deaths_avg, Honorable_kills_avg, Damage_done_avg, Healing_done_avg, Honor_avg)


data.raw$Killing_blows_tmp <- sapply(data.raw$Killing_blows, as.character)
data.raw$Deaths_tmp <- sapply(data.raw$Deaths, as.character)
data.raw$Honorable_kills_tmp <- sapply(data.raw$Honorable_kills, as.character)
data.raw$Damage_done_tmp <- sapply(data.raw$Damage_done, as.character)
data.raw$Healing_done_tmp <- sapply(data.raw$Healing_done, as.character)
data.raw$Honor_tmp <- sapply(data.raw$Honor, as.character)
data.raw$Character_class_tmp <- sapply(data.raw$Character_class, as.character)

for(i in 1:nrow(data.raw)) {
  data.raw$Killing_blows_tmp[i] <- belowOrAboveAverage(data.raw$Killing_blows[i], Killing_blows_avg)
  data.raw$Deaths_tmp[i] <- belowOrAboveAverage(data.raw$Deaths[i], Deaths_avg)
  data.raw$Honorable_kills_tmp[i] <- belowOrAboveAverage(data.raw$Honorable_kills[i], Honorable_kills_avg)
  data.raw$Damage_done_tmp[i] <- belowOrAboveAverage(data.raw$Damage_done[i], Damage_done_avg)
  data.raw$Healing_done_tmp[i] <- belowOrAboveAverage(data.raw$Healing_done[i], Healing_done_avg)
  data.raw$Honor_tmp[i] <- belowOrAboveAverage(data.raw$Honor[i], Honor_avg)
  data.raw$Character_class_tmp[i] <- distinguishMeleeFromRanged(data.raw$Character_class[i])
}

data.raw$Killing_blows <- sapply(data.raw$Killing_blows_tmp, as.factor)
data.raw$Deaths <- sapply(data.raw$Deaths_tmp, as.factor)
data.raw$Honorable_kills <- sapply(data.raw$Honorable_kills_tmp, as.factor)
data.raw$Damage_done <- sapply(data.raw$Damage_done_tmp, as.factor)
data.raw$Healing_done <- sapply(data.raw$Healing_done_tmp, as.factor)
data.raw$Honor <- sapply(data.raw$Honor_tmp, as.factor)
data.raw$Character_class <- sapply(data.raw$Character_class_tmp, as.factor)
data.raw$Faction <- sapply(data.raw$Faction, as.factor)
data.raw$Result <- sapply(data.raw$Result, as.factor)
data.raw <- data.raw[,1:10]

#Wyliczenie:
#Czasami pojawia sie blad : Error in length(obj)...
#Trzeba zresetować paczki :
detach("package:arulesViz", unload=TRUE)
detach("package:arules", unload=TRUE)
library("arules")
library("arulesViz")
rules <- apriori(data.raw ,parameter = list(minlen=2, supp=0.005, conf=0.8),
                 appearance = list(rhs=c("Result=Win", "Result=Defeat"),default="lhs"),
                 control = list(verbose=F))
rules.sorted <- sort(rules, by="lift")
#Reguły mogą się powtarzać, lub niektóre mogą być zbędne - dlatego przycinamy je, eliminując redundancję
subset.matrix <- is.subset(rules.sorted, rules.sorted)
subset.matrix[lower.tri(subset.matrix, diag=T)] <- FALSE
redundant <- colSums(subset.matrix, na.rm=T) >= 1
which(redundant)
rules.pruned <- rules.sorted[!redundant]
rules.pruned <- sort(rules.pruned, by="lift")
inspect(rules.pruned)
#Wybranie reguł implikującyh zwycięstwo
rules.win <- subset(rules.pruned, (rhs %in% paste0("Result=Win")))
inspect(rules.win)
#write(rules.win, file = "association_rules_win.csv", sep = ",", quote = TRUE, row.names = FALSE)
#write(rules.pruned, file = "association_rules.csv", sep = ",", quote = TRUE, row.names = FALSE)

rules.horde <- subset(rules.win, (lhs %in% paste0("Faction=Horde")))
inspect(rules.horde)
rules.alliance <- subset(rules.win, (lhs %in% paste0("Faction=Alliance")))
inspect(rules.alliance)

rules.meele <- subset(rules.win, (lhs %in% paste0("Character_class=Melee")))
rules.ranged <- subset(rules.win, (lhs %in% paste0("Character_class=Ranged")))
rules.versatile <- subset(rules.win, (lhs %in% paste0("Character_class=Versatile")))

rules.DPS <- subset(rules.win, (lhs %in% paste0("Role=dps")))
rules.DPS.melee <- subset(rules.DPS, (lhs %in% paste0("Character_class=Melee")))
rules.DPS.ranged <- subset(rules.DPS, (lhs %in% paste0("Character_class=Ranged")))
rules.DPS.versatile <- subset(rules.DPS, (lhs %in% paste0("Character_class=Versatile")))

rules.Healer <- subset(rules.win, (lhs %in% paste0("Role=heal")))
rules.Healer.melee <- subset(rules.Healer, (lhs %in% paste0("Character_class=Melee")))
rules.Healer.ranged <- subset(rules.Healer, (lhs %in% paste0("Character_class=Ranged")))
rules.Healer.versatile <- subset(rules.Healer, (lhs %in% paste0("Character_class=Versatile")))

plot(rules.pruned)
#Ten plot dla takiej ilości rules (max 100) jest mega nieczytelny :/
#plot(rules, method="graph", control=list(type="items"))