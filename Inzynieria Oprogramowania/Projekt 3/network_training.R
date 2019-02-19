#Proj 3
#https://www.analyticsvidhya.com/blog/2017/09/creating-visualizing-neural-network-in-r/
library(neuralnet)

setwd("C:/Users/janbi/Desktop/PD-3-TANKS")
# getwd()

args = commandArgs(trailingOnly=TRUE)

if (args[1] == "1") {
  data = read.csv('data_logs_brown.csv', header=T)
} else if (args[1] == "2") {
  data = read.csv('data_logs_green.csv', header=T)
  # data <- data[,c(29:56, 1:28, 57)]
  names(data) <- c("enemyTank.x", "enemyTank.y", 
                   "enemyTank.rotation", "enemyTank.cannonRotation", 
                   "enemyTank.velocityX", "enemyTank.velocityY",
                   "enemyTank.accelerationX", "enemyTank.accelerationY",
                   "enemyTank.shootCooldown", 
                   "enemyTank.controls.turnLeft", "enemyTank.controls.turnRight",
                   "enemyTank.controls.goForward", "enemyTank.controls.goBack",
                   "enemyTank.controls.shoot", "enemyTank.controls.cannonLeft", "enemyTank.controls.cannonRight",
                   "enemyBullet1.x", "enemyBullet1.y", "enemyBullet1.velocityX", "enemyBullet1.velocityY",
                   "enemyBullet2.x", "enemyBullet2.y", "enemyBullet2.velocityX", "enemyBullet2.velocityY", 
                   "enemyBullet3.x", "enemyBullet3.y", "enemyBullet3.velocityX", "enemyBullet3.velocityY",
                   "myTank.x", "myTank.y",
                   "myTank.rotation", "myTank.cannonRotation", 
                   "myTank.velocityX", "myTank.velocityY", 
                   "myTank.accelerationX", "myTank.accelerationY",
                   "myTank.shootCooldown", 
                   "myTank.controls.turnLeft", "myTank.controls.turnRight",
                   "myTank.controls.goForward", "myTank.controls.goBack", 
                   "myTank.controls.shoot", "myTank.controls.cannonLeft", "myTank.controls.cannonRight",
                   "myBullet1.x", "myBullet1.y", "myBullet1.velocityX", "myBullet1.velocityY",
                   "myBullet2.x", "myBullet2.y", "myBullet2.velocityX", "myBullet2.velocityY",
                   "myBullet3.x", "myBullet3.y", "myBullet3.velocityX", "myBullet3.velocityY",
                   "currentGameTime"
  )
}
# colnames(data)

# Tweek params
input_num <- 11
hidden_num <- c(4)
output_num <- 7

# NN$weights

loaded_weights = c()
loaded_weights = as.numeric(read.csv(file = "data_all_weights.csv", header = F, sep=",")[1,])

start_weights = c()

for (i in seq(1, hidden_num[1])) {
  start_weights = c(start_weights, loaded_weights[i])
  for (j in seq(1, input_num)) {
    start_weights = c(start_weights, loaded_weights[hidden_num[1] + (i - 1) * hidden_num[1] + j])
  }
}

offset_out_layer = hidden_num[1] + hidden_num[1] * input_num

for (i in seq(1, output_num)) {
  start_weights = c(start_weights, loaded_weights[offset_out_layer + i])
  for (j in seq(1, hidden_num[1])) {
    start_weights = c(start_weights, loaded_weights[offset_out_layer + output_num + (i - 1) * hidden_num[1] + j])
  }
}

#############
if(args[1] == "1") {
	samplesize = 0.1 * nrow(data)
} else if (args[1] == "2") {
	samplesize = 0.3 * nrow(data)
}
set.seed(2000)
index = sample(seq_len(nrow(data)), size = samplesize)

datatrain = data[index,]
datatest = data[-index,]

# max = apply(data, 2, max)
# min = apply(data, 2, min)
# scaled = as.data.frame(scale(data, center=min, scale=max-min))

# trainNN = scaled[index, ]
# testNN = scaled[-index, ]

trainNN = data[index, ]
testNN = data[-index, ]

set.seed(1997)

if (length(loaded_weights) != 0) {
  NN = neuralnet(myTank.controls.turnLeft + myTank.controls.turnRight 
                 + myTank.controls.goForward + myTank.controls.goBack 
                 + myTank.controls.shoot + myTank.controls.cannonLeft + myTank.controls.cannonRight 
                 ~ myTank.x + myTank.y 
                 + myTank.rotation + myTank.shootCooldown 
                 + enemyTank.x + enemyTank.y 
                 + enemyTank.cannonRotation 
                 + enemyBullet1.x + enemyBullet1.y 
                 + enemyBullet2.x + enemyBullet2.y, 
                 trainNN,
                 startweights = start_weights,
                 hidden = hidden_num,
                 algorithm = "rprop+",
                 act.fct = "logistic",
                 learningrate = c(0.8, 0.3),
                 stepmax = 10000,
                 threshold = 0.3,
                 linear.output = F)
} else {
  NN = neuralnet(myTank.controls.turnLeft + myTank.controls.turnRight 
                 + myTank.controls.goForward + myTank.controls.goBack 
                 + myTank.controls.shoot + myTank.controls.cannonLeft + myTank.controls.cannonRight 
                 ~ myTank.x + myTank.y 
                 + myTank.rotation + myTank.shootCooldown 
                 + enemyTank.x + enemyTank.y 
                 + enemyTank.cannonRotation 
                 + enemyBullet1.x + enemyBullet1.y 
                 + enemyBullet2.x + enemyBullet2.y, 
                 trainNN, 
                 hidden = hidden_num,
                 algorithm = "rprop+",
                 act.fct = "logistic",
                 learningrate = c(0.8, 0.3),
                 stepmax = 1000,
                 threshold = 0.3,
                 linear.output = F)
}

#name_file <- paste(paste('plots/nn_plot_iteration', args[2], sep=""), '.png', sep="")
#plot(NN)
#dev.copy(png, name_file)
#dev.off()

bias1 <- c()

for (i in seq(1, hidden_num[1])) {
  bias1 <- c(bias1, NN$weights[[1]][[1]][1, i])
}

weight1 <- c()

for (i in seq(1, hidden_num[1])) {
  for (j in seq(2, input_num + 1)) {
    weight1 <- c(weight1, NN$weights[[1]][[1]][j, i])
  }
}

bias2 <- c()

for (i in seq(1, output_num)) {
  bias2 <- c(bias2, NN$weights[[1]][[2]][1, i])
}

weight2 <- c()

for (i in seq(1, output_num)) {
  for (j in seq(2, hidden_num[1] + 1)) {
    weight2 <- c(weight2, NN$weights[[1]][[2]][j, i])
  }
}

write(bias1, file = "data_bias1.txt", sep=",")
write(weight1, file = "data_weight1.txt", sep=",")
write(bias2, file = "data_bias2.txt", sep=",")
write(weight2, file = "data_weight2.txt", sep=",")