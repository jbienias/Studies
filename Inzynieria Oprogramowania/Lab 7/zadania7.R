#LAB 7

#ZAD 2
iris.log <- log(iris[,1:4])
iris.stand <- scale(iris.log, center=TRUE)
iris.pca <- prcomp(iris.stand)
iris.final <- predict(iris.pca)[,1:2]

data <- iris.final
km<-kmeans(data, 3, iter.max = 10, nstart = 3,
           algorithm = c("Lloyd"), trace=FALSE)
colors = c("red", "blue", "green")
plot(data[,1:2], col=colors[km$cluster])
points(data[,1:2], col=colors[iris$Species], cex=2)
points(km$centers, pch=10, cex=3, lwd=3)

