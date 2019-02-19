library("party")



#ZAD 1
myPredictRow <- function(sl, sw, pl, pw){
  if (pw < 0.9 ){
    return ("setosa")
  }else{
    if (pw< 1.8 ){
      return("versicolor")
    }else{
      return ("virginica")
    }
  }
}

myPredict <- function(){
  counter <- 0
  for (i in 1:nrow(iris)){
    x <- iris[i,]
    tmp <- myPredictRow(x[1],x[2],x[3],x[4])
    if (tmp == x[5]){
      counter <- counter + 1
    }
  }
  return (counter/nrow(iris))
}
#ZAD 2
set.seed(1234)
ind <- sample(2, nrow(iris), replace=TRUE, prob=c(0.67, 0.33))
iris.training <- iris[ind==1,1:5]
iris.test <-iris[ind==2,1:5]
iris.ctree <-ctree(Species ~ Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, data=iris.training)
print(iris.ctree)
plot(iris.ctree)
plot(iris.ctree, type="simple")

class(iris.test$Species)
class(predict(iris.ctree, iris.test[,1:4]))

predicted <- predict(iris.ctree, iris.test[,1:4])
real <- iris.test[,5]
t <- table(predicted,real)
t <- table(predicted,real)
accuracy <- sum(diag(t))/sum(t)

#ODPOWIEDZI
message(sprintf("Klasyfikacja za pomocą drzew: %f", accuracy))
message(sprintf("MyPredict() : %f", myPredict()))