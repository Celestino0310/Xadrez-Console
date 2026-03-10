using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _peca;

        public Tabuleiro(int linhas, int coluna)
        {
            this.Linhas = linhas;
            this.Colunas = coluna;
            _peca = new Peca[linhas, coluna];
        }

        public Peca peca(int linha, int coluna)
        {
            return _peca[linha, coluna]; ;
        }
        public Peca peca(Posicao pos)
        {
            return _peca[pos.Linha, pos.Coluna];
        }
        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos)) { throw new TabuleiroException("já existe uma peça nessa posição"); }

            _peca[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }
        public bool existePeca(Posicao pos)
        {
            posicaoValida(pos);
            return peca( pos)!=null;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.Coluna >= Colunas || pos.Coluna < 0 || pos.Linha <= Linhas || pos.Linha < 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos)) { throw new TabuleiroException("posicção invalida!"); }
        }
    }
}
