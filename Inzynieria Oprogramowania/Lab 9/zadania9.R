#LAB 9
library(neuralnet)
normalize <- function(vector) {
  (vector - min(vector))/((max(vector))-min(vector))
}

irisClass3to1 <- function(x,y,z) {
  if( x == max(x,y,z)) {
    return ('virginica')
  }
  if( y == max(x,y,z)) {
    return ('versicolor')
  }
  else return('setosa')
}

#Zad 1
fct.act <- function(x) {
  return (1/(1+exp(-x)))
}

forwardPass <- function(wiek, waga, wzrost) {
  hidden1 <- fct.act((-0.46122 * wiek) + (0.97314 * waga) + (-0.39203 * wzrost) + 0.80109)
  hidden2 <- fct.act((0.78548 * wiek) + (2.10584 * waga) + (-0.57847 * wzrost) + 0.43529)
  output <- ((-0.81546 * hidden1) + (1.03775 * hidden2) - 0.2368)
  return (output)
}

forwardPass(23,75,176) #0.798528, wyszło 0.7985342 (błąd zaokrąglenia?)

#Zad 2
iris.norm <- normalize(iris[, 1:4])
virginica.offset <- c(rep("virginica", nrow(iris.norm)))
versicolor.offset <- c(rep("versicolor", nrow(iris.norm)))
setosa.offset <- c(rep("setosa", nrow(iris.norm)))

virginica.offset <- match(iris[,5], virginica.offset, 
                          nomatch = FALSE, incomparables = NULL)
virginica.offset <- matrix(virginica.offset)
colnames(virginica.offset)[1] <- "Virginica"

versicolor.offset <- match(iris[,5], versicolor.offset, 
                           nomatch = FALSE, incomparables = NULL)
versicolor.offset <- matrix(versicolor.offset)
colnames(versicolor.offset)[1] <- "Versicolor"

setosa.offset <- match(iris[,5], setosa.offset, 
                       nomatch = FALSE, incomparables = NULL)
setosa.offset <- matrix(setosa.offset)
colnames(setosa.offset)[1] <- "Setosa"

iris.types <- cbind(virginica.offset, versicolor.offset, setosa.offset)
iris.norm <- cbind(iris.norm, iris.types)

set.seed(1410)
ind <- sample(2, nrow(iris.norm), replace=TRUE, prob=c(0.67, 0.33))
iris.norm.training <- iris.norm[ind==1, 1:7]
iris.norm.test <- iris.norm[ind==2, 1:7]

iris.network <- neuralnet(Virginica + Versicolor + Setosa 
                            ~ Sepal.Length + Sepal.Width +Petal.Length + Petal.Width,
                            iris.norm.training, 
                            hidden=4,  
                            stepmax = 1000000) #hidden = 4, bo tak wynika z PDFa :P, stepmax było za małe 
iris.result <- compute(iris.network, iris.norm.test[1:4], rep = 1)$net.result

results <- c()
true_results <- c()
nrow(iris.result)
nrow(iris.norm.test)
for(i in (1:nrow(iris.result))) {
  results <- c(results, irisClass3to1(iris.result[i,1],iris.result[i,2],iris.result[i,3]))
  true_results <- c(true_results, irisClass3to1(iris.norm.test[i,5],iris.norm.test[i,6],iris.norm.test[i,7]))
}


t <- table(results,true_results)
accuracy <- sum(diag(t))/sum(t)

#zad 3