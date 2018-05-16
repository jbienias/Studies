#include <time.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#define MLDD 1000000000.0 //10**10
// kompilowac z opcjami -lrt -lm, tj. np. gcc a.c -lrt -lm

/*
Task (PL):
• Zaimplementuj algorytm sortowania szybkiego omówiony na wykładzie.
• Zmierz i porównaj czasy działania sortowania dla dwóch rodzajów danych: dane
losowe oraz dane skrajnie niekorzystne (np. liczby uprządkowane rosnąco).
Testy (pomiary czasu) powinny być wykonane dla różnych wielkości tablicy, np. 100
elementów, 500 elementów, 1000 elementów i 2500 elementów. Ponadto w przypadku
danych losowych należy wziąć średni czas (średnia arytmetyczna) z odpowiednio dużej
próbki (np. 100, 500, 1000, 2500 losowań, odpowiednio, dla tablicy 100-, 500-, 1000-
i 2500-elementowej).
*/
// Jan Bienias

void array_print(int arr[], int size);
void random_array(int arr[], int size);
void bad_array(int arr[], int size);
int partition(int arr[], int size, int p, int r);
void quicksort(int arr[], int size, int p,int r);

int main()
{
/*  ----- ew. TESTY
    int n = 10;
    int arr[n];
    random_array(arr, n);
	array_print(arr, n);
    quicksort(arr, n, 0, n-1);
    array_print(arr, n);
    printf("\nTest dzialania funckji :\n");
    printf("Bad array:\n");
    bad_array(arr, n);
    array_print(arr, n);
    quicksort(arr, n, 0, n-1);
    array_print(arr, n);
    printf("Random array:\n");
    random_array(arr, n);
    array_print(arr, n);
    quicksort(arr, n, 0, n-1);
    array_print(arr, n);
    printf("\n");
*/
    int n;
    struct timespec tp0, tp1;
    printf("alg bad => czas sortowania 'zlej' tablicy\n");
    printf("alg rand => czas sortowania 'randomowej' tablicy\n");
    printf("|  N  |   alg bad     |   alg rand    |\n");
    for(n=100; n<2501; n=n+100)
    {
            double Tn, TnTmp,TnTotal=0;
            int tab[n];
            bad_array(tab, n);
            clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp0);
            quicksort(tab, n, 0, n-1);
            clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp1);
            Tn=(tp1.tv_sec+tp1.tv_nsec/MLDD)-(tp0.tv_sec+tp0.tv_nsec/MLDD);
            int i;
            for(i=0; i<n; i++)
            {
                int tab1[n];
                random_array(tab1, n);
                clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp0);
                quicksort(tab1, n, 0, n-1);
                clock_gettime(CLOCK_PROCESS_CPUTIME_ID,&tp1);
                TnTmp = (tp1.tv_sec+tp1.tv_nsec/MLDD)-(tp0.tv_sec+tp0.tv_nsec/MLDD);
                TnTotal = TnTotal + TnTmp;
            }
            TnTotal = TnTotal/n;
            printf("|%4d |%3.13lf|%3.13lf|\n", n, Tn, TnTotal);

    }

    return 0;
}

void array_print(int arr[], int size)
{
    int i;
	for(i = 0; i<size; i++)
	{
		printf("%d ", arr[i]);
	}
	printf("\n");
}

void random_array(int arr[], int size)
{
    int i;
    srand(time(NULL));
    for(i = 0; i < size; i++)
    {
        arr[i] = rand()%100;
    }
}

void bad_array(int arr[], int size)
{
    int i;
    for(i = 0; i < size; i++)
    {
        arr[i] = size - i;
    }

}

int partition(int arr[], int size, int p, int r)
{
    int j, tmp;
    int x=arr[r];
    int i=p-1;
    for(j=p; j<=r; j++)
    {
        if (arr[j] <= x)
        {
            i++;
            tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
    if (i<r) return i;
    else
        return i-1;
}

void quicksort(int arr[], int size, int p, int r)
{
    int q;
    if (p<r)
    {
        q = partition(arr, size, p, r);
        quicksort(arr, size, p, q);
        quicksort(arr, size, q+1, r);
    }
}
