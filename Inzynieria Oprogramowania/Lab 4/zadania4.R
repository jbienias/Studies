#LAB 4

#install.packages("Hmisc")
#install.packages("editrules")
#install.packages("deducorrect")
#install.packages("VIM")

#ZAD 1
library(editrules)
library(deducorrect)
library(Hmisc)
library(VIM)

dirty.iris <- read.csv("/home/hergroth/IO/Lab 4/dirty_iris.csv", header=TRUE, sep=",")
dirty.iris
full.records <- nrow(subset(dirty.iris, is.finite(Sepal.Length) &
                              is.finite(Sepal.Width) &
                              is.finite(Petal.Length) & 
                              is.finite(Petal.Width)))
full.records

E <- editset(c("Sepal.Length <= 30",
               "Sepal.Length > 0",
               "Sepal.Length > Petal.Length",
               "Sepal.Width > 0",
               "Petal.Length > 0", 
               "Petal.Length >= 2 * Petal.Width",
               "Petal.Width > 0",
               "Species %in% c('setosa', 'versicolor', 'virginica')"))
E
ve <-violatedEdits(E, dirty.iris)
summary(ve)
#plot(ve)

#ZAD 2
R <- correctionRules("/home/hergroth/IO/Lab 4/rules.txt")
R
corrected.iris <- correctWithRules(R, dirty.iris)
corrected.iris

#ZAD 3
#a)
x <- data.frame(corrected.iris[1])
for(i in 1:4) {
  x[,i] <- impute(x[,i], mean)
}
clean.iris.mean <- x
clean.iris.mean
#b)
y <- data.frame(corrected.iris[1])
clean.iris.knn <- kNN(y) #???
y

#ZAD 4
tmp <- iris
for(i in 1:4) {
  tmp[,i] <- log(tmp[,i])
}
iris.log <- tmp
iris.log

for(i in 1:4) {
  tmp[,i] <- scale(tmp[,i])
}
iris.log.scale <- tmp
iris.log.scale

#ZAD 5
nazwy <- iris.log.scale[,5] #<- potrzebne do ostatniego podpunktu
iris.log.scale <- iris.log.scale[,-5]
iris.log.scale
iris.pca <- prcomp(iris.log.scale)
#Jakie odchylenia standardowe mają główne składowe PC1, PC2, PC3, PC4? Jeśli chcemy 
#zostawić tylko dwie główne składowe o największym odchyleniu standardowym, to które to 
#będą?
#ODP: Zostawiamy PC1 i PC2 <- najwieksze

#Co zwracają komendy iris.pca[1] i iris.pca[2]? Co zwróci komenda iris.pca[2]$rotation
iris.pca[1]
iris.pca[2]
iris.pca[2]$rotation
iris.predict <- predict(iris.pca)
iris.predict <- iris.predict[, c(-3, -4)] #usuwamy PC3 i PC4
iris.predict <- data.frame(iris.predict) #przerabiamy na data frame
iris.predict$nazwy <- nazwy #dodajemy kolumne z nazwami
x <- iris.predict[,1]
y <- iris.predict[,2]
n <- iris.predict[,3]
plot(x, y, col=c("red", "blue", "green")[n], pch=19, xlab="PC1", ylab="PC2")
legend(x="topright", legend=unique(n), pch=19, col=c("red", "blue", "green"))
