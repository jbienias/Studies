INC=/usr/include/postgresql
LIB=/usr/lib
CFLAGS=-I$(INC)
LDLIBS=-L$(LIB) -lpq
CC=gcc

main: main.c
	$(CC) $(CFLAGS) -o main main.c $(LDLIBS)

clean:
	@rm -f *.o *~ $(ALL)
