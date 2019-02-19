normalize <- function(vector) {
  (vector - min(vector))/((max(vector))-min(vector))
}

standarize <- function(vector){
  (vector - mean(vector))/sd(vector)
}