#include <stdio.h>
#include <stdlib.h>

//Jan Bienias
//238201
//Task: 1 (https://inf.ug.edu.pl/~mdziemia/aisd/zad-czerwono-czarne.txt)

//Sources :
//Thomas H. Cormen's book

typedef struct Tree {
  int number;
  char color; //B = black, R = red
  struct Tree *right;
  struct Tree *left;
  struct Tree *parent;
}Node;

Node* rootMain = NULL;

void WSTAW(Node **n, int val);
void WSTAW_BST(Node **n, Node *newNode);
void DRUKUJ(Node *n);
Node* SZUKAJ(Node *n, int val);
void LEFT_ROTATE(Node **n, Node *x);
void RIGHT_ROTATE(Node **n, Node *x);
void showMenu();


int main(void) {
    system("clear");
    //system ("cls");
    int exit = 1;
    while(exit) {
        int value;
        showMenu();
        int option;
        scanf("%d", &option);
        switch(option) {
        case 1:
            system("clear");
            //system("cls");
            printf("Enter value of node you want to insert > ");
            scanf("%d", &value);
            WSTAW(&rootMain, value);
            break;

        case 2:
            system("clear");
            //system("cls");
            printf("Enter value of node you want to find > ");
            scanf("%d", &value);
            SZUKAJ(rootMain, value);
            break;

        case 3:
            system("clear");
            //system("cls");
            printf("Printing red-black-tree in-order: \n");
            DRUKUJ(rootMain);
            break;

        case 4:
            system("clear");
            //system("cls");
            exit = 0;
            break;

        default:
            system("clear");
            //system("cls");
            printf("Unknown command!\n");
            break;

        }

    }
	return 0;
}

Node* createNode(int val) {
	Node* newNode = (Node*)malloc(sizeof(Node));
	newNode->number = val;
	newNode->left = NULL;
	newNode->right = NULL;
	newNode->parent = NULL;
	newNode->color = 'R';
	return newNode;
}

void WSTAW_BST(Node **n, Node *newNode) {
    if((*n) == NULL) {
		(*n) = newNode;
    } else {
    	if(newNode->number < (*n)->number) {
    		WSTAW_BST(&(*n)->left,newNode);
    		(*n)->left->parent = *n;
    	}
    	if(newNode->number >= (*n)->number) { //if we don't want duplicates of inserted value, change >= into >
    		WSTAW_BST(&(*n)->right,newNode);
    		(*n)->right->parent = *n;
    	}
    }
}

void WSTAW(Node **n, int val) {
	Node* newNode = createNode(val);
	WSTAW_BST(&rootMain, newNode);
	Node* grandparent = NULL; //grandparent of newNode
	Node* par = NULL; //parent of newNode
	//both variables created to make algorithm code
	//(especially in if statements) more clear
	while((newNode != (*n)) && (newNode->parent->color == 'R')) {
		grandparent = newNode->parent->parent;
		par = newNode->parent;
		if(par == grandparent->left) {
			Node* uncle = grandparent-> right;
			if(uncle != NULL && uncle->color == 'R') {
				par->color = 'B';
				uncle->color = 'B';
				grandparent->color = 'R';
				newNode = grandparent;
			}else {
                if(newNode == par->right) {
                    LEFT_ROTATE(n, par);
                    newNode = par;
                    par = newNode->parent;
                }
                par->color = 'B';
                grandparent->color = 'R';
                RIGHT_ROTATE(n, grandparent);
            }
		} else {
            Node* uncle = grandparent-> left;
            if(uncle != NULL && uncle->color == 'R') {
                par->color = 'B';
                uncle->color = 'B';
                grandparent->color = 'R';
                newNode = grandparent;
			} else {
			    if(newNode == par->left) {
                    RIGHT_ROTATE(n, par);
                    newNode = par;
                    par = newNode->parent;
                }
                par->color = 'B';
                grandparent->color = 'R';
                LEFT_ROTATE(n, grandparent);
            }
        }
	}
	(*n)->color = 'B';
}

void DRUKUJ(Node *n) //in-order printing
{
    if(n==NULL)
        return;
    DRUKUJ(n->left);
    if(n->color == 'R')
    	printf("[%d] \tRED\n", n->number);
    else if(n->color == 'B')
    	printf("[%d] \tBLACK\n", n->number);
    DRUKUJ(n->right);
}

Node* SZUKAJ(Node *n, int val) {

	if(n!=NULL)	{
        if(val == n->number) {
            printf("I've found [%d] node! Color: %c\n", n->number, n->color);
            return n;
        }
        else if(val > n->number)
            SZUKAJ(n->right, val);
        else if(val < n->number)
            SZUKAJ(n->left, val);
	} else
		printf("I didn't find [%d] node!\n", val);

	return NULL;
}

void LEFT_ROTATE(Node** n, Node *x) {
    Node *y = x->right;
    x->right = y->left;
    if(y->left != NULL) {
        y->left->parent = x;
    }
    y->parent = x->parent;
    if(x->parent == NULL) {
        (*n) = y;
    } else if(x == x->parent->left) {
        x->parent->left = y;
    } else {
        x->parent->right = y;
    }
    y->left = x;
    x->parent = y;
}

void RIGHT_ROTATE(Node** n, Node *x) {
    Node* y = x->left;
    x->left = y->right;
    if(y->right != NULL) {
        y->right->parent = x;
    }
    y->parent = x->parent;
    if(x->parent == NULL) {
        (*n) = y;
    } else if(x == x->parent->left) {
        x->parent->left = y;
    } else {
        x->parent->right = y;
    }
    y->right = x;
    x->parent=y;
}

void showMenu() {
    printf("\nRBTree Simulator 2017\n");
    printf("=========================\n");
    printf("1.INSERT NODE\n");
    printf("2.FIND NODE IF EXISTS\n");
    printf("3.PRINT TREE\n");
    printf("4.QUIT\n");
    printf("\nInput > ");
}
