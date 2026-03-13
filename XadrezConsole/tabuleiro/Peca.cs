using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using tabuleiro;

namespace tabuleiro
{
    public  abstract class Peca
    {

        public  Cor cor{ get; set; }
        public int qtdMovimentos { get;protected set;}
        public Posicao posicao { get;  set; }//é normalmente protected set mas eu vejo segurança dps..
        public Tabuleiro tabuleiro { get; protected set; }


        public abstract bool[,] movimentosPossiveis();

        public  bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j=0; j < tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool movimentoPossivel(Posicao pos)
        {

            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.cor = cor;
            this.posicao = null;
            this.tabuleiro = tab;
            this.qtdMovimentos = 0;

        }
       
        public void IncrementarQtdMovimentos() { 
            qtdMovimentos++ ; 
        }
        public void decrementarQtdMovimentos() { 
            qtdMovimentos-- ; 
        }
    }
}
