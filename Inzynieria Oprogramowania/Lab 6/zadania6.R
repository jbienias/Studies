#LAB 6
#ZAD 1

library("class")
library("e1071")
          #1 najblizszego sasiada         #3 najblizszych sasiadow
#(2.7,6)  zielony                         czerwony
#(5,7)    niebieski                       niebieski
#(7,3.5)  niebieski                       zielony

normalize <- function(vector) {
  (vector - min(vector))/((max(vector))-min(vector))
}

iris.norm <- normalize(iris[1:4])
iris.norm <- cbind(iris.norm, iris[5])

set.seed(1234)
ind <- sample(2, nrow(iris.norm), replace=TRUE, prob=c(0.67, 0.33))
iris.norm.training <- iris[ind==1,1:5]
iris.norm.test <-iris[ind==2,1:5]

knn.3 <-knn(iris.norm.training[,1:4], iris.norm.test[,1:4], 
            cl=iris.norm.training[,5], k = 3, prob=FALSE)
predicted <- knn.3
real <- iris.norm.test[,5]
conf.matrix <- table(predicted,real)
accuracyKNN <- sum(diag(conf.matrix))/sum(conf.matrix)

#ZAD 2
#http://rischanlab.github.io/NaiveBayes.html
ind <- sample(2, nrow(iris.norm), replace=TRUE, prob=c(0.67, 0.33))
iris.training <- iris[ind==1,1:5]
iris.test <-iris[ind==2,1:5]
#iris.model.2 <- naiveBayes(x=iris[-5], y=iris$Species, data=iris.training)
iris.model <-naiveBayes(Species ~ Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, data=iris, subset=data.training)
print(iris.model)

class(iris.test$Species)
class(predict(iris.model, iris.test[,1:4]))

set.seed(1234)
predicted <- predict(iris.model, iris.test[,1:4])
real <- iris.test[,5]
t <- table(predicted,real)
accuracyNB <- sum(diag(t))/sum(t)
message(sprintf("Klasyfikator KNN: %f", accuracyKNN))
message(sprintf("Klasyfikator Naive Bayes: %f",accuracyNB))