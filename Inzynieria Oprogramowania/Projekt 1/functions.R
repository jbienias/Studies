#JAN BIENIAS 238201
#PROBLEM 3SAT
#ADDITIONAL FUNCTIONS

load_variables_locations_from_file <- function(file_name) {
  con <- file(file_name) 
  len <- length(readLines(con)) - 11  #plik cnf - góra 8 linii, dół 2 linie
  data <- read.table(con , skip=8, nrows=len)
  vector <- c()
  for (i in c(0:len) ) {
    for (j in c(1:3)) {
      vector <- c(vector, data[i, j])
    }
  }
  return(vector)
}

negate <- function(num) {
  ifelse(num==0, return(1), return(0))
}

extended_sample <- function(x, y, n, duplicates) {
  set.seed(as.integer((as.double(Sys.time())*1000+Sys.getpid()) %% 2^31))
  return(sample(x:y, n, replace=duplicates))
}

#stara funkcja generujaca losowe rozmieszczenie zmiennych z ew. powtorzeniami
generate_variable_locations <- function(ilosc_zmiennych, ilosc_pol) {
  rozstawienie_zmiennych <- extended_sample(1, ilosc_zmiennych, ilosc_zmiennych, F)
  if(ilosc_pol-ilosc_zmiennych > 0) {
    powtorki_zmiennych <- extended_sample(1, ilosc_zmiennych, ilosc_pol-ilosc_zmiennych, T)
    rozstawienie_zmiennych <- c(rozstawienie_zmiennych, powtorki_zmiennych)
    rozstawienie_zmiennych <- sample(rozstawienie_zmiennych)
  }
  return(rozstawienie_zmiennych)
}