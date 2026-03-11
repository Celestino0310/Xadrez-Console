using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Bispo:Peca
    {
        public Bispo(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);
            //nordeste
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }

            //sudeste
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }
            //sudoeste
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }
            //noroeste
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }
            return matriz;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
