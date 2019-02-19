# Reading Data:
Sys.setenv(JAVA_HOME='C:\\Program Files\\Java\\jdk1.8.0_201')
library(rJava)

library('stringr')
library('readr')
library('wordcloud')
library('tm')
library('SnowballC')
library('RWeka')
library('RSentiment')
library('DT')

dir <- "C:/Users/janbi/Desktop/data/"
fellowship <- read.csv(paste(dir, "FellowshipOfTheRing.txt", sep=""), header = F, sep = "\n")
fellowship$movie <- "The Fellowship of the Ring"
twotowers <- read.csv(paste(dir,"TwoTowers.txt", sep=""), header = F, sep = "\n")
twotowers$movie <- "The Two Towers"
returnking <- read.csv(paste(dir, "ReturnOfTheKing.txt", sep=""), header = F, sep = "\n")
returnking$movie <- "The Return of the King"
trilogy <- rbind(fellowship, twotowers, returnking)

colnames(trilogy) <- c("dialogue", "movie")
trilogy$dialogue <- as.character(trilogy$dialogue)
trilogy$movie <- as.factor(trilogy$movie)

# Get each dialogue's character
characters <- vector()
for(i in 1:nrow(trilogy)){
  who <- gsub( ":.*$", "", trilogy[i,1])
  who <- gsub(" ", "", who)
  characters <- append(characters, who )
}

trilogy$character <- as.factor(characters)

# Remove character from dialogue
sentence <- vector()
for(i in 1:nrow(trilogy)){
  what <- gsub( ".*:", "", trilogy[i,1])
  what <- gsub("^ ", "", what)
  sentence <- append(sentence, what)
}

trilogy$dialogue <- as.character(sentence)

lotr <- trilogy$dialogue

# Preparing Data
set.seed(2137)
lotr_sample = sample(lotr, (length(lotr)))
corpus = Corpus(VectorSource(list(lotr_sample)))
corpus = tm_map(corpus, removePunctuation)
corpus = tm_map(corpus, content_transformer(tolower))
corpus = tm_map(corpus, removeNumbers) 
corpus = tm_map(corpus, stripWhitespace)
corpus = tm_map(corpus, removeWords, stopwords('english'))

dtm_lotr = DocumentTermMatrix(VCorpus(VectorSource(corpus[[1]]$content)))
freq_lotr <- colSums(as.matrix(dtm_lotr))


# Sentiments Lotr
sentiments_lotr = calculate_sentiment(names(freq_lotr))
sentiments_lotr = cbind(sentiments_lotr, as.data.frame(freq_lotr))

sent_pos_lotr = sentiments_lotr[sentiments_lotr$sentiment == 'Positive',]
sent_neg_lotr = sentiments_lotr[sentiments_lotr$sentiment == 'Negative',]

cat("We have negative Sentiments: ",sum(sent_neg_lotr$freq_lotr)," positive: ",sum(sent_pos_lotr$freq_lotr))

# Positive Sentiments
DT::datatable(sent_pos_lotr)

# Negative Sentiments
DT::datatable(sent_neg_lotr)

# Wordcloud
layout(matrix(c(1, 2), nrow=2), heights=c(1, 4))
par(mar=rep(0, 4))

plot.new()
text(x=0.5, y=0.5, "Lord of the Rings: Positive words")
set.seed(100)
wordcloud(sent_pos_lotr$text,sent_pos_lotr$freq, min.freq=10,colors=brewer.pal(6,"Dark2"))

plot.new()
text(x=0.5, y=0.5, "Lord of the Rings: Negative words")
set.seed(100)
wordcloud(sent_neg_lotr$text,sent_neg_lotr$freq, min.freq=20,colors=brewer.pal(6,"Dark2"))
