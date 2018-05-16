#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#define STRING 100

int lines_number(char *name);
void array_create(char *name, int arr[], int size);
void array_print(int arr[], int size);
void array_save(char *name, int arr[], int size);
void heapify (int arr[], int heap_size, int i);
void build(int arr[], int size);
void heapsort(int arr[], int size);

/*
Task(PL):
Zaimplementuj omawiany na wykładzie algorytm sortowania przez kopcowanie.
*/
//Jan Bienias


int main(int arg_count, char* arg[])
{
	if(arg_count == 3)
	{
		char name[STRING]; //nazwa pliku / sciezka do pliku z ktorego pobierzemy nasze dane (wierszowo)
		char name1[STRING]; //nazwa pliku na ktorym zapiszemy nasza posortowana tablice (rowniez wierszowo)
		sscanf(arg[1], "%s", name);
		sscanf(arg[2], "%s", name1);
		int n;
		n = lines_number(name);
		int tab[n];
		array_create(name, tab, n);
		printf("Wczytalem dane z pliku %s :\n", name);
		array_print(tab, n);
		heapsort(tab, n);
		printf("\nTwoja posortowana tablica to :\n");
		array_print(tab, n);
		printf("Zapisuje tablice na pliku %s.\n", name1);
		array_save(name1, tab, n);
		return 1;
	}

	else
	{
		printf("Niepoprawne wywolanie programu!\n");
		printf("Wywolanie program wymaga dwoch argumentow!\n");
		printf("./a.out arg1(plik-wejscie) arg2(plik-wyjscie)!\n");
		return 0;
	}
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


void array_create(char *name, int arr[], int size)
{
	FILE *file = fopen(name, "r");
	if (file == NULL)
	{
		printf("\nNie mozna otworzyc pliku %s!\n", name);
		exit(0);
	}
	int i;

	for(i=0; i < size; i++)
	{
		fscanf(file, "%d\n", &arr[i]);
	}
	fclose(file);
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


void array_save(char *name, int arr[], int size)
{
    int i;
    FILE *file = fopen(name, "w");

	for(i=0; i < size; i++)
	{
		fprintf(file, "%d\n", arr[i]);
	}
	fclose(file);
}


void heapify (int arr[], int heap_size, int i)
{
    int largest = i, temp;
    int l=i*2 + 1;
    int r=i*2 + 2;
    if (l<heap_size && arr[l]>arr[largest])
        largest=l;
    if (r<heap_size && arr[r]>arr[largest])
        largest=r;
    if (largest!=i)
    {
        temp=arr[largest];
        arr[largest]=arr[i];
        arr[i]=temp;
        heapify(arr,heap_size,largest);
    }
}


void build(int arr[], int size)
{
    int i;
    for (i=size/2-1;i>=0;i--)
        heapify(arr,size, i);
}


void heapsort(int arr[], int size)
{
    int temp;
    build(arr, size);
    int i;
    for (i=size-1;i>=0;i--)
    {
        temp=arr[i];
        arr[i]=arr[0];
        arr[0]=temp;
        heapify(arr,i,0);
    }
}
