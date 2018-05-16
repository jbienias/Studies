#include<stdio.h>
#include<string.h>
#include<stdlib.h>

/*
Task (PL) :
W oparciu o wykład zaimplementuj strukturę listy dowiązaniowej (bez wartownika),
której elementami są słowa (ciągi znaków), oraz operacje WSTAW(s, L) (wstawia s na
początek listy L), DRUKUJ(L) (wypisuje elementu listy L), SZUKAJ(s, L) (zwraca
wskaźnik na element s, o ile taki element znajduje się na liście L, w przeciwnym
wypadku zwraca NULL), USUŃ(s, L) (usuwa element s z listy L, o ile znajduje się
on na liście L) oraz KASUJ(L) (kasuje wszystkie elementy z listy L).
• Operacja BEZPOWTÓRZEŃ(L) polega na otrzymaniu z listy L jej kopii, ale bez
powtarzających się elementów; lista L nie przestaje istnieć. Zaimplementuj operację
BEZPOWTÓRZEŃ(L) (zwraca wskaźnik na listę).
• Mając dwie rozłączne listy L1 i L2, operacja SCAL(L1, L2) polega na otrzymaniu listy
L3, której elementami są elementy z L1 ∪L2, tzn. s należy do L3 wtedy i tylko wtedy,
gdy s należy do L1 lub s należy do L2; listy L1 i L2 stają się puste. Zaimplementuj
operację SCAL(L1, L2) (zwraca wskaźnik na wynikową listę L3).
*/
// Jan Bienias

typedef struct List{
    char* WORD;
    struct List *next;
    struct List *prev;
}list;

list *L1 = NULL;
list *L2 = NULL;
list *L3 = NULL; //wynikowa scalania

void WSTAW(list **L, char* word)
{
    list *new_node=(list *)malloc(sizeof(list));
    new_node->WORD = word;
    new_node->next = *L;
    new_node->prev = NULL;
    *L = new_node;
}

void DRUKUJ(list *node)
{
    while(node!=NULL)
    {
        printf("[%s]", node->WORD) ;
        node=node->next;
    }
    printf("\n");
}

list* SZUKAJ(list* node, char* word)
{
    while(node!=NULL)
    {
        if(node->WORD == word)
        {
            //printf("[%s] znaleziono na liscie!\n", word);
            return node;
        }
        node=node->next;
    }
    //printf("[%s] NIE znaleziono na liscie!\n");
    return NULL;
}

void USUN(list** L, char* word)
{
    //list* pierwszy = *L;
    list* previous;
    list* tmp = *L;

    while(tmp != NULL)
    {
        if(tmp->WORD == word)
        {
            if(tmp == *L)
            {
                *L = tmp->next;
                return;
            }

            else
            {
                previous->next = tmp->next;
                free(tmp);
                return;
            }
        }

        else
        {
            previous=tmp;
            tmp = tmp->next;
        }
  }

}

void BEZPOWTORZEN(list* L)
{
    list *node1, *node2, *tmp;
    node1 = L;
    while(node1 != NULL && node1->next != NULL)
    {
        node2 = node1;
        while(node2->next != NULL)
        {
            if(node1->WORD == node2->next->WORD)
            {
                tmp = node2->next;
                node2->next = node2->next->next;
                free(tmp);
            }
            else
            {
                node2 = node2->next;
            }
        }
        node1 = node1->next;
    }
}

void SCAL(list** lis1, list** lis2)
{
    list* tmp = (list*)malloc(sizeof(list));
    tmp = *lis1;
    while(tmp != NULL)
    {
        WSTAW(&L3, tmp->WORD);
        tmp = tmp->next;
    }
    tmp = *lis2;
    while(tmp != NULL)
    {
        WSTAW(&L3, tmp->WORD);
        tmp = tmp->next;
    }

    *lis1 = NULL;
    *lis2 = NULL;
    //free(lis1);
    //free(lis2);
}

int main(void)
{
    //L1 -> przykladowe wezly
    WSTAW(&L1, "Asia");
    WSTAW(&L1, "Basia");
    WSTAW(&L1, "Asia");
    WSTAW(&L1, "Kasia");
    WSTAW(&L1, "Asia");
    printf("L1 : ");
    DRUKUJ(L1);
    printf("\n");

    //L2 -> przykladowe wezly
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    WSTAW(&L2, "Kasia");
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    printf("L2 : ");
    DRUKUJ(L2);
    printf("\n");

    //search -> test funkcji SZUKAJ, ktora zwraca wskaznik na element
    list* search;
    search=SZUKAJ(L2, "Kasia");
    printf("Search [Kasia]* : ");
    DRUKUJ(search);

    printf("\n");
    printf("Usuwamy po kolei wszystkie elementy z L1 : ...\n");
    DRUKUJ(L1);
    USUN(&L1, "Asia");
    DRUKUJ(L1);
    USUN(&L1, "Asia");
    DRUKUJ(L1);
    USUN(&L1, "Asia");
    DRUKUJ(L1);
    USUN(&L1, "Basia");
    DRUKUJ(L1);
    USUN(&L1, "Kasia");
    printf("[NULL]\n");

    //L2 + L2 -> test funkcji BEZPOWTORZEN
    printf("\n");
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    WSTAW(&L2, "Kasia");
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    printf("L2+L2 : ");
    DRUKUJ(L2);
    printf("\nL2 bez powtorzen : ");
    BEZPOWTORZEN(L2);
    DRUKUJ(L2);

    //L1 i L2 -> odrestaurowanie
    L1 = NULL;
    L2 = NULL;
    printf("\n");
    WSTAW(&L1, "Asia");
    WSTAW(&L1, "Basia");
    WSTAW(&L1, "Asia");
    WSTAW(&L1, "Kasia");
    WSTAW(&L1, "Asia");
    printf("Odnowione L1 : ");
    DRUKUJ(L1);
    printf("\n");
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    WSTAW(&L2, "Kasia");
    WSTAW(&L2, "Basia");
    WSTAW(&L2, "Asia");
    printf("Odnowione L2 : ");
    DRUKUJ(L2);

    printf("\nL3 (scalenie L1 i L2) : ");
    SCAL(&L1, &L2);
    DRUKUJ(L3);
    DRUKUJ(L1); //puste po scaleniu!
    DRUKUJ(L2); //puste po scaleniu!
    return 0;
}
