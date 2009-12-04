void fecha_escopo() {
    /* Verifica o conteudo da fila atual e tenta reduzir */
    tenta_reduzir(); /* filaAtual */
    empilha(filaAtual);
}


/* Cria uma fila nova empilha a velha */
void abre_escopo();

Fila* tenta_reduzir(){
    char c = fila->dequeue();
    switch(c){
        case 'S':
        case 's':
            if (fila->cont >= 3) { 
            /* Se tem mais que 3 parametros faz a reducao e joga o resultado em uma nova fila 
                e empilha atual
            */
                char x = fila->dequeue ();
                char y = fila->dequeue ();
                char z = fila->dequeue ();
                /* Faz a reducao S */
                abre_escopo();
                abre_escopo();
                fila->enqueue(x);
                fila->enqueue(z);
                fecha_escopo();
                abre_escopo();
                fila->enqueue(y);
                fila->enqueue(z);
                fecha_escopo();
                fecha_escopo();
                fila->enqueueBeginning(resultado); /* Enfila o resultado daquela reducao no começo da fila */
            }
            break;
        case 'k':
        case 'K':
            if (fila->cont >= 2) {
                char x = dequeue(fila);
                char y = dequeue(fila);
                /* faz a reducao em K */
                fila->enqueueBeginning(x);
                
            }
            break;
        case 'I':
            if (fila->cont >= 1){
                char x = fila->dequeue();
                /* faz a reducao I */
                fila->enqueueBeginning(x);
            }
            break;
    }
    return filaAtual;

}


