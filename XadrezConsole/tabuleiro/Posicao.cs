using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    public class Posicao
    
    {
        //criando as propriedades da classe Posição
        public int Linha { get; set; }
        public int Coluna { get; set; }

        //criando construtor com duas entradas de dados
        public Posicao(int linha,int coluna)
        {
            Coluna = coluna;
            Linha =linha;

        }

        public void definirValores(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;

        }
        //to string para conseguir
        public override string ToString()
        {
            return Linha+","+ Coluna;
        }

    }
}
