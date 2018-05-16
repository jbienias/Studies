#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <time.h>
#define MDN 254
#define MLDD 1000000000.0 //10**10
/*
Task (PL):

X Nazwisko,

gdzie X jest liczbą typu int określającą popularność nazwiska Nazwisko. Posortuj te zapisy
alfabetycznie według nazwisk i wygeneruj nowy plik zawierający tak uporządkowane zapisy,
bez wartości określającej popularność nazwiska. Sortowanie wykonaj na dwa sposoby:
— sortowanie pozycyjne jak w zadaniu ALL.6.6 oraz
— sortowanie przez porównania: Quicksort lub Heapsort.
Porównaj rzeczywiste czasy obu sortowań (samych sortowań, bez czytania/zapisu do pliku)
dla różnej liczby wczytanych nazwisk (np. 500, 1000, 1500, 2000, . . . , 10000).

Uwagi. 

-Plik z nazwiskami można przekonwertować tak, aby pozbyć się polskich znaków
oraz dużych liter.
-Zakladam, ze nazwisko jest w postaci DUZA litera + reszta male litery
//(zatem RadixSort moze nie dzialac poprawnie dla nazwisk dwuczlonowych jak np. Kawa-Herbata)
*/
// Jan Bienias

// Kompilacja : -lrt -lm, tj. np. gcc a.c -lrt -lm
// Kod QuickSortaStringow : http://stackoverflow.com/questions/19612152/quicksort-string-array-in-c

char **A;
char **B;
char **Q;
char **TMP;

int max_len(char **arr, int ilosc_linii);
void same_len(char **A, int ilosc_linii, int max);
int lines_number(char *name);
void read_from_file(char **A, char *name, int ilosc_linii);
void save_to_file(char **A, char *name, int ilosc_linii);
//void small_strings(char **A, int ilosc_linii); //ew. funkcja ktora powoduje zmiane wszystkich charow / stringow na male litery
void CountingSort(char **A, char **B, int n, int p);
void RadixSort(char **A, char **B, int n, int max);
void swap_str_ptrs(char **arg1, char **arg2);
void QuickSort(char *args[], int len);

int main(int arg_count, char* arg[])
{
    if(arg_count == 2)
    {
        struct timespec tp0, tp1;
        double TnR, TnQ;
        char name[50]; //nazwa pliku / sciezka do pliku z ktorego pobierzemy nasze dane (wierszowo)
        sscanf(arg[1], "%s", name);
        int n;
        n = lines_number(name);
        A = (char**) malloc(n * sizeof(char*));
        B = (char**) malloc(n * sizeof(char*));
        TMP = (char**) malloc(n * sizeof(char*));
        Q = (char**) malloc(n * sizeof(char*));
        read_from_file(A,name, n);
        read_from_file(Q,name,n);
        int max = max_len(A, n);
		//small_strings(A, n);
		//small_strings(Q,n);
        //printf("Ilosc zeskanowanych linii : %d\n", n);
        //printf("Najdluzszy napis : %d\n", max);
        same_len(A, n, max);
        same_len(Q, n, max);
        clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp0);
        RadixSort(A, B, n, max);
        clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp1);
        TnR=(tp1.tv_sec+tp1.tv_nsec/MLDD)-(tp0.tv_sec+tp0.tv_nsec/MLDD);
        clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp0);
        QuickSort(Q, n);
        clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp1);
        TnQ=(tp1.tv_sec+tp1.tv_nsec/MLDD)-(tp0.tv_sec+tp0.tv_nsec/MLDD);
        save_to_file(A,"out.txt", n);
        save_to_file(Q,"outQ.txt", n);
		printf("\n");
        printf("Sortowania zakonczone!\nWyniki sortowan zapisano w plikach: 'out.txt' oraz 'outQ.txt'.\n\n");
		printf("----------------------------------\n");
        printf("|   Sortowanie   |      Czas     |\n");
        printf("----------------------------------\n");
        printf("|   Radix_sort   |%3.13lf|\n", TnR);
        printf("|   Quick_sort   |%3.13lf|\n", TnQ);
		printf("----------------------------------\n");
		printf("\n\n");

        return 1;
    }
	else
	{
		printf("Niepoprawne wywolanie programu!\n");
		printf("Wywolanie program wymaga jednego argumentu!\n");
		printf("./a.out arg1(plik-wejscie)!\n");
		return 0;
	}
}

int max_len(char **A, int ilosc_linii)
{
    int i, max=0;
    for(i=0;i<ilosc_linii;i++)
    {
        if(strlen(A[i])>max)
        {
            max=strlen(A[i]);
        }
    }
    return max;
}

void same_len(char **A, int ilosc_linii, int max)
{
    int i, j;
    for (i=0; i<ilosc_linii; i++)
        for (j=strlen(A[i]); j<max; j++)
            A[i][j]=' ';
}

int lines_number(char *name)
{
	FILE *file = fopen(name, "r");
	if (file == NULL)
	{
		printf("\nNie mozna otworzyc pliku %s!\n", name);
		exit(0);
	}
	int counter=0;
	int ch;

	do
	{
		ch = fgetc(file);
		if(ch == '\n')
			counter++;
	} while (ch != EOF);

	if(ch != '\n' && counter != 0)
		counter++;

	fclose(file);
	return counter;
}

void read_from_file(char **A, char *name, int ilosc_linii)
{
    FILE *file;
    file = fopen(name, "r");
	if (file == NULL)
	{
		printf("\nNie mozna otworzyc pliku %s!\n", name);
		exit(0);
	}
    int i;
    for(i=0; i<ilosc_linii; i++)
    {
        int tmp;
        char slowo[MDN];
        fscanf(file, "%d", &tmp);
        fscanf(file, "%s", slowo);
        A[i] = (char*)malloc(MDN * sizeof(char));
        strcpy(A[i],slowo);
    }
    fclose(file);
}

void save_to_file(char **A, char *name, int ilosc_linii)
{
    FILE *file;
    file = fopen(name, "w");
    int i;
    for (i=0; i<ilosc_linii; i++)
    {
        fprintf(file, "%s\n", A[i]);
    }
    fclose(file);
}

/*
void small_strings(char **A, int ilosc_linii)
{
    int i, j;
    for (i=0; i<ilosc_linii; i++)
        for (j=0; j<strlen(A[i]); j++)
            A[i][j]=tolower(A[i][j]);
}
*/


void CountingSort(char **A, char **B, int n, int p)  //n -> ilosc, p -> pozycja
{
    //ASCII code : od 0 - 127 (128 elementow)
    //rownie dobrze mozna sobie darowac elementy od 0 - 32...
    //podobnie jak ostatni element 127 = DEL
    int i, j;
    int C[128];
    for (i=0; i<127; i++)
        C[i]=0;
    for (j=0; j<n; j++)
        C[(int)A[j][p]]++;
    for (i=0; i<127; i++)           //for (i = 1; i<127; i++)
    {                               //      C[i] += C[i-1];
        if(i==0)
        {
            C[i] = C[i];
        }
        else
            C[i]+=C[i-1];
    }
    for (j=n-1; j>=0; j--)
    {
        B[C[(int)A[j][p]]-1]=A[j];
        C[(int)A[j][p]]--;
    }
}

void RadixSort(char **A, char **B, int n, int max)
{
    int i;
    for(i=max-1; i>=0; i--)
    {
        CountingSort(A, B, n, i);
        TMP = A;
        A = B;
        B = TMP;
    }
}

void swap_str_ptrs(char **arg1, char **arg2)
{
    char *tmp = *arg1;
    *arg1 = *arg2;
    *arg2 = tmp;
}

void QuickSort(char *args[], int len)
{
    unsigned int i, pvt=0;
    if (len <= 1)
        return;
    swap_str_ptrs(args+((unsigned int)rand() % len), args+len-1);
    for (i=0;i<len-1;++i)
    {
        if (strcmp(args[i], args[len-1]) < 0)
            swap_str_ptrs(args+i, args+pvt++);
    }
    swap_str_ptrs(args+pvt, args+len-1);
    QuickSort(args, pvt++);
    QuickSort(args+pvt, len - pvt);
}
