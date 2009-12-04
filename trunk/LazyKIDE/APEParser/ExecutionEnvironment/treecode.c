#include <stdio.h>
#include <stdlib.h>

typedef int Tipo;

typedef struct _no {
	Tipo info;
	struct _no *esq, *dir;
}
no, *ApontNo;

void inicializa(ApontNo *T) {
	T=NULL;
}

void insereDireita(ApontNo T, int i) {
	ApontNo novo = (ApontNo) malloc(sizeof(no));
	T->dir = novo;
	novo->info = i;
	novo->dir = NULL;
	novo->esq = NULL;
}


void insereEsquerda(ApontNo T, int i) {
	ApontNo novo = (ApontNo) malloc(sizeof(no));
	T->esq = novo;
	novo->info = i;
	novo->dir = NULL;
	novo->esq = NULL;
}

void localiza(int i,ApontNo T, ApontNo *posicao) {
	if (T == NULL){
		*posicao=NULL;
		return;
	}
	if(i==T->info){
		*posicao=T;
		return;
	}

	localiza(i,T->esq,posicao);
	if(*posicao==NULL)
		localiza(i,T->dir,posicao);
}

void preOrdem(ApontNo raiz) {
	if (raiz==NULL)
		return;
	printf("%d ",raiz->info);
	preOrdem(raiz->esq);
	preOrdem(raiz->dir);
}

void emOrdem(ApontNo raiz) {
	if (raiz==NULL)
		return;
	emOrdem(raiz->esq);
	printf("%d ",raiz->info);
	emOrdem(raiz->dir);
}

void posOrdem(ApontNo raiz) {
	if (raiz==NULL)
		return;
	posOrdem(raiz->esq);
	posOrdem(raiz->dir);
	printf("%d ",raiz->info);
}

int main() {
	int n,i,j;
	ApontNo raiz,aux;
	raiz = (ApontNo) malloc(sizeof(no));
	aux = raiz;
	printf("Digite n: ");
	scanf("%d",&n);
	aux->info=1;
	for(i=2;i<=n;i++) {
		localiza(i/2,raiz,&aux);
		j=aux->info;
		if(i%2==0)
			insereEsquerda(aux,i);
		else
			insereDireita(aux,i);
	}

	preOrdem(raiz);
	printf("\n");
	emOrdem(raiz);
	printf("\n");
	posOrdem(raiz);
	return 0;
}


