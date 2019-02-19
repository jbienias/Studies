# LAB 8
library(arules)
library(arulesViz)
titanic.raw <- get(load('~/GitHub/IO/Lab\ 8/titanic.raw.rdata'))
str(titanic.raw)
rules <- apriori(titanic.raw)
inspect(rules)

rules <- apriori(titanic.raw,parameter = list(minlen=2, supp=0.005, conf=0.8),
                 appearance = list(rhs=c("Survived=No", "Survived=Yes"),default="lhs"),
                 control = list(verbose=F))
rules.sorted <- sort(rules, by="lift")
inspect(rules.sorted)

subset.matrix <- is.subset(rules.sorted, rules.sorted)
subset.matrix[lower.tri(subset.matrix, diag=T)] <- FALSE #bo NA nie weszło
redundant <- colSums(subset.matrix, na.rm=T) >= 1
which(redundant)
#remove redundant rules
rules.pruned <- rules.sorted[!redundant]
inspect(rules.pruned)

plot(rules.pruned)
plot(rules, method="graph", control=list(type="items"))
plot(rules, method="paracoord", control=list(reorder=TRUE))