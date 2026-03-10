using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _peca;

        public Tabuleiro(int linhas ,int coluna)
        {
            this.Linhas = linhas;
            this.Colunas = coluna;
            _peca = new Peca[linhas,coluna];
        }

        public Peca peca(int linha, int coluna) {
            return _peca[linha , coluna]; ;
        }
        public void colocarPeca(Peca p,Posicao pos)
        {
            _peca[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }
    }
}
