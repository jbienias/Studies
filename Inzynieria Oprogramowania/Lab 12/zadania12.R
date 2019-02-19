# Lab 2

library(tm)
library(ggplot2)
library(gridExtra)
library(dplyr)
library(reshape2)
library(pals)
library(colorRamps)
library(wordcloud)
library(stringr)

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


### DATA PROCESSING

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


# Next we will add another column with each characters race
# For this task we'll use the file data "races" that I've prepared
# beforehand

races <- read.csv(paste(dir, "races.csv", sep=""), sep = ";")

get_race <- function(x, y){
  race <- vector()
  for(i in 1:nrow(x)){
    if(x[i,"character"] %in% y$Character){
      race <- append(race, as.character(y$Race[match(x[i,"character"], y$Character)]))
    }
  }
  return(race)
}

char_race <- get_race(trilogy, races)
trilogy$race <- as.factor(char_race)

### Clean the text
cleaning <- function(x) {
  x = gsub("[[:punct:]]", " ", x)
  x = tolower(x)
  x = gsub("^ ", "", x)
  x = gsub(" $", "", x)
  x = gsub(" +", " ", x)
}

text <- cleaning(trilogy$dialogue)
trilogy$clean_dialogue <- as.character(text)

# To make things easier let's attach the trilogy dataset
attach(trilogy)

# We will also get another column with the amount of words by each line
by_words <- strsplit(clean_dialogue, " ")

amount_words <- vector()
for(i in 1:length(by_words)){
  amount_words <- append(amount_words, length(by_words[[i]]))  
}

trilogy$amount_words <- amount_words


### DATA MINING

### A) Global movie comparison

# Let's create a table with the amount of lines, words and characters so
# we can compare the three movies. For better perspective, we'll also
# include the duration of the movies, and get the variables refferring
# to text per minute.

Duration <- c(208, 223, 251) # in minutes
Characters_count <- c(length(unique(trilogy[movie=="The Fellowship of the Ring", "character"])),
                      length(unique(trilogy[movie=="The Two Towers", "character"])),
                      length(unique(trilogy[movie=="The Return of the King", "character"])))

Lines <- c(nrow(fellowship), nrow(twotowers), nrow(returnking))

Words <- c(sum(trilogy[movie=="The Fellowship of the Ring", "amount_words"]),
           sum(trilogy[movie=="The Two Towers", "amount_words"]),
           sum(trilogy[movie=="The Return of the King", "amount_words"]))

Lines_min <- round(Lines/Duration)
Words_min <- round(Words/Duration)

Movies <- as.data.frame(rbind("Duration (min)"= Duration, "Amount of characters"= Characters_count,
                              Lines, Words, "Lines/min"= Lines_min, "Words/min"= Words_min))


colnames(Movies) <- c("The Fellowship of the Ring", "The Two Towers",
                      "The Return of the King")

grid.table(Movies)

# As you can see even though The Fellowship of the Ring is the shorter
# and the one with less characters, it has he largest amount of dialogue


### B) Characters comparison
# Let's create a dataset with amount of lines, words and lines length by
# movie, race and character

lines_char <- as.data.frame.matrix(table(trilogy$character, trilogy$movie))
lines_char$character <- row.names(lines_char)
lines_char <- melt(lines_char, value.name = "lines",
                   variable.name = "movie",
                   varnames="character")
char_race <- get_race(lines_char, races)
lines_char$race <- as.factor(char_race)

words_char <- trilogy %>% group_by(movie, character, race) %>% 
  summarise(words = sum(amount_words))

full_info <- merge(lines_char, words_char)

# reorder columns
full_info <- full_info[, c(1,3,2,4,5)]

# add a column with the length of sentences (words/line)
full_info$line_length <- round(full_info$words/full_info$lines, 2)

full_info$character <- as.factor(character)
full_info$race <- as.factor(full_info$race)
full_info$movie <- factor(full_info$movie, 
                          levels= c("The Fellowship of the Ring",
                                    "The Two Towers",
                                    "The Return of the King"))
attach(full_info)

## Visualize data

# In order to create a color palette to viasualize 
# characters, I'll create a new column with the 
# names of the main characters (or somewhat important),
# and "Other" to secondary characters

main <- c("Frodo", "Sam", "Merry", "Pippin", "Gollum",
          "Sméagol", "Bilbo", "Gimli", "Gríma",
          "Aragorn", "Boromir", "Gandalf", "Saruman",
          "Arwen", "Celeborn", "Galadriel", "Legolas",
          "Elrond", "Haldir", "Éomer", "Éowyn", "Faramir",
          "KingoftheDead", "MouthofSauron", "Nazgul",
          "Ring", "Sauron", "Théoden", "Treebeard", "Tree",
          "WitchKing", "Uglúk", "Uruk")
maindt <- as.data.frame(main)

get_importance <- function(x, y){
  imp <- vector()
  for(i in 1:nrow(x)){
    if(x[i,"character"] %in% y$main){
      imp <- append(imp, as.character(y$main[match(x[i,"character"], y$main)]))
    }else{
      imp <- append(imp, "Other")
    }
  }
  return(imp)
}

importance <- get_importance(full_info, maindt)

full_info$importance <- as.factor(importance)

pal_1 <- c(Aragorn = "royalblue1", 
           Arwen = "springgreen2", 
           Bilbo = "tomato3",
           Boromir = "royalblue4",
           Celeborn = "olivedrab1",
           Elrond  = "seagreen3",
           Éomer = "aquamarine",
           Éowyn  = "aquamarine4",
           Faramir = "skyblue3",
           Frodo  = "chocolate3",
           Galadriel  = "olivedrab3",
           Gandalf  = "deeppink3",
           Gimli = "firebrick2",
           Gollum  = "salmon",
           Gríma = "darkcyan",
           Haldir  = "darkkhaki",
           KingoftheDead  = "mediumslateblue",
           Legolas = "green2",
           Merry = "lightgoldenrod",
           MouthofSauron   = "midnightblue",
           Nazgul  = "mediumslateblue", 
           Pippin  = "peru",
           Ring = "gold",
           Sam  = "darkorange",    
           Saruman = "orchid4",
           Sauron  = "darkred",
           Sméagol = "tan1",
           Théoden  = "slateblue1",
           Tree  = "darkgreen",   
           Treebeard  = "forestgreen",
           Uglúk  = "gray64",
           Uruk   = "gray50",
           Other = "gray28",     
           WitchKing = "slateblue4")


lines_plot <- ggplot(full_info, aes(x= reorder(race, lines), y = lines, fill=importance)) +
  geom_bar(stat="identity", show.legend = F) +
  labs(title= "Amount of lines") +
  coord_flip() +
  facet_wrap(~movie) +
  theme(plot.title = element_text(hjust = 0.5, size = 18, vjust = 5, face = "bold"), 
        axis.title = element_blank(),
        axis.text = element_text(size = 10, face = "bold"),
        strip.text.x = element_text(size = 12, face = "bold"),
        legend.position = "bottom",
        legend.title = element_blank(),
        legend.text = element_text(size = 10, face = "bold")) +
  scale_fill_manual(values = pal_1) +
  scale_x_discrete(limits = rev(levels(full_info$race)))

words_plot <- ggplot(full_info, aes(x=reorder(race, words), y = words, fill=importance)) +
  geom_bar(stat="identity", show.legend = F) +
  labs(title= "Amount of words") +
  coord_flip() +
  facet_wrap(~movie) +
  theme(plot.title = element_text(hjust = 0.5, size = 18, vjust = 5, face = "bold"), 
        axis.title = element_blank(),
        axis.text = element_text(size = 10, face = "bold"),
        strip.text.x = element_text(size = 12, face = "bold")) +
  scale_fill_manual(values = pal_1) +
  scale_x_discrete(limits = rev(levels(full_info$race)))

linelength_plot <- ggplot(full_info, aes(x= reorder(race, line_length), y = line_length, fill=importance)) +
  geom_bar(stat="identity", show.legend = F) +
  labs(title= "Lines length") +
  coord_flip() +
  facet_wrap(~movie) +
  theme(plot.title = element_text(hjust = 0.5, size = 18, vjust = 1, face = "bold"), 
        axis.title = element_blank(),
        axis.text = element_text(size = 10, face = "bold"),
        strip.text.x = element_text(size = 12, face = "bold")) +
  scale_fill_manual(values = pal_1) +
  scale_x_discrete(limits = rev(levels(full_info$race)))


#### *Get the legend (function taken from stackoverflow)
g_legend<-function(a.gplot){
  tmp <- ggplot_gtable(ggplot_build(a.gplot))
  leg <- which(sapply(tmp$grobs, function(x) x$name) == "guide-box")
  legend <- tmp$grobs[[leg]]
  return(legend)}

legend <- g_legend(lines_plot) # save one of the plots with a name
# and insert it here. Make sure the option "show.legend" is T.
# Then you can visualize just the legend by itself as an unique object.
# In this case I used the "lines_plot", in which you can see some
# legend edition arguments.

grid.arrange(lines_plot, words_plot, linelength_plot, legend,
             ncol = 2)

# * To visualize the same variables but without differentiating between 
# movies and getting an overall view just run the same code above except
# the "facet_wrap()" line


## Top characters with most words by movies
pal_2 <- c(Dwarf= "firebrick2", Elve = "chartreuse3",
           Hobbit = "darkorange", Men = "darkcyan",
           Wizard = "mediumorchid4", Ent = "forestgreen")

ggplot(full_info[words>=200, ], 
       aes(x=reorder(character, words), y = words, fill= race)) +
  geom_bar(stat="identity", show.legend = T) +
  facet_wrap(~movie) + 
  labs(title= "Top characters with more than 200 words by movie") +
  ylim(0, 2500) +
  coord_flip() +
  theme(plot.title = element_text(hjust = 0.5, size = 18, vjust = 1, face = "bold"), 
        axis.title = element_blank(),
        axis.text = element_text(size = 10, face = "bold"),
        strip.text.x = element_text(size = 12, face = "bold"),
        legend.position = "bottom",
        legend.title = element_blank(),
        legend.text = element_text(size = 10, face = "bold")) +
  scale_fill_manual(values = pal_2)