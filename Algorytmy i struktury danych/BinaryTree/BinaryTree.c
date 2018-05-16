#include<stdio.h>
#include<string.h>
#include<stdlib.h>

/*
Task (PL):
Zaimplementuj strukturę danych, wraz z operacjami WSTAW,
SZUKAJ, USUŃ, DRUKUJ (w porządku in-order), która realizuje koncepcję drzewa wyszukiwań
binarnych, którego węzły przechowują klucze odpowiadające małym literom alfabetu
łacińskiego (znaki ASCII). A zatem, jako że węzły przechowują różne klucze, w
przypadku wstawiania klucza, który jest już w drzewie, w odpowiednim węźle zliczamy
liczbę powtarzających się kluczy. Analogicznie, „fizyczne” usuwanie węzła o danym kluczu
ma miejsce dopiero wtedy, gdy liczba powtarzających się takich kluczy wynosi 1.
*/
// Jan Bienias


typedef struct Tree{
  char znak;
  int ilosc;
  struct Tree *right;
  struct Tree *left;
}TreeNode;

void DRUKUJ(TreeNode *n);
void WSTAW(TreeNode **n, char word);
TreeNode* SZUKAJ(TreeNode *n, char word);
TreeNode* MINIMUM(TreeNode *n);
TreeNode *USUN(TreeNode *n, char word);
TreeNode *USUNtotalnie(TreeNode *n, char word);

void DRUKUJ(TreeNode *n) //in-order
{
    if(n==NULL)
        return;
    DRUKUJ(n->left);
    printf("['%c' (%d)]\n", n->znak, n->ilosc);
    DRUKUJ(n->right);
}

void WSTAW(TreeNode **n, char word)
{
    if(word < 97 || word > 122)
    {
        printf("[ %c ] nie jest mala litera!\n", word);
        return;
    }
    TreeNode *tmp = NULL;
    if(*n == NULL)
    {
        tmp = (TreeNode *)malloc(sizeof(TreeNode));
        tmp->left = NULL;
        tmp->right = NULL;
        tmp->znak = word;
        tmp->ilosc = 1;
        *n = tmp;
        return;
    }

    if(word < (*n)->znak)
    {
        WSTAW(&(*n)->left, word);
    }
    else if(word > (*n)->znak)
    {
        WSTAW(&(*n)->right, word);
    }
    else
        (*n)->ilosc++;
}

TreeNode* SZUKAJ(TreeNode *n, char word)
{
    if(n!=NULL)
    {
        if(word == n->znak)
        {
            printf("Znalazlem [ %c ] w drzewie! Ilosc : (%d) \n", word, n->ilosc);
            return n;
        }
        else if(word > n->znak)
            SZUKAJ(n->right, word);
        else if(word < n->znak)
            SZUKAJ(n->left, word);
    }
    else
    {
        printf("Nie znalazlem [ %c ] w drzewie!\n", word);
        return NULL;
    }

}

TreeNode* MINIMUM(TreeNode *n)
{
	TreeNode* tmp = n;
	while (tmp->left != NULL)
		tmp = tmp->left;
	return tmp;
}


TreeNode *USUN(TreeNode *n, char word)
{
    if (n == NULL)
        return n;

	if (word < n->znak)
		n->left = USUN(n->left, word);
	else if (word > n->znak)
		n->right = USUN(n->right, word);
	else
    {
        if (n->ilosc > 1)
        {
            n->ilosc--;
            return n;
        }
        else if (n->ilosc == 1)
        {
            if (n->left == NULL)
            {
                TreeNode *tmp = n->right;
                free(n);
                return tmp;
            }
            else if (n->right == NULL)
            {
                TreeNode *tmp = n->left;
                free(n);
                return tmp;
            }
            TreeNode *tmp = MINIMUM(n->right);
            n->znak = tmp->znak;
            n->ilosc = tmp->ilosc;
            n->right = USUNtotalnie(n->right, tmp->znak);
        }
    }
	return n;
}

TreeNode *USUNtotalnie(TreeNode *n, char word) //usuwamy nie zwazajac na ilosc wystapien
{
    if (n == NULL)
    return n;

	if (word < n->znak)
		n->left = USUNtotalnie(n->left, word);
	else if (word > n->znak)
        n->right = USUNtotalnie(n->right, word);
    else
    {
        if (n->left == NULL)
        {
            TreeNode *tmp = n->right;
            free(n);
            return tmp;
        }
        else if (n->right == NULL)
        {
            TreeNode *tmp = n->left;
            free(n);
            return tmp;
        }
        TreeNode *tmp = MINIMUM(n->right);
        n->znak = tmp->znak;
        n->right = USUNtotalnie(n->right, tmp->znak);
    }
    return n;
}


int main()
{
    TreeNode *L = NULL;
    //WSTAW(&L, '`'); -> nie dodamy tego, gdyz program obsluguje jedynie male litery (UTF-8)
    //WSTAW(&L, '}');
    WSTAW(&L, 'g');
    WSTAW(&L, 'd');
    WSTAW(&L, 'j');
    WSTAW(&L, 'h');
    WSTAW(&L, 'k');
    WSTAW(&L, 'a');
    WSTAW(&L, 'e');
    WSTAW(&L, 'b');
    //DRUKUJ(L);
    //zwiekszamy ilosc w lisciu jesli wstawiamy ten sam znak
    WSTAW(&L, 'j');
    WSTAW(&L, 'j');
    WSTAW(&L, 'h');
    WSTAW(&L, 'e');
    WSTAW(&L, 'e');
    WSTAW(&L, 'e');
    WSTAW(&L, 'b');
    WSTAW(&L, 'b');
    WSTAW(&L, 'b');
    printf("===po wstawieniu===\n");
    DRUKUJ(L);
    printf("\n===brak synow (usuwamy h)===\n");
    USUN(L, 'h'); // <- usuniecie liscia bez synow
    DRUKUJ(L);
    printf("\n===jeden syn (usuwamy a)====\n");
    USUN(L, 'a'); // <-usuniecie liscia z jednym synem
    DRUKUJ(L);
    printf("\n===dwoch synow (usuwamy g)===\n");
    USUN(L, 'g'); // <- usuniecie czubka (zatem ma dwoch synow)
    DRUKUJ(L);

    /*
    printf("|====TEST SZUKANIA====|\n");
    TreeNode *T = NULL;
    T = SZUKAJ(L, 'b');
    DRUKUJ(T);
    printf("|=====================|\n");
    */
}
