using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Dama:Peca
    {
        public Dama(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

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

            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.definirValores(pos.Linha - 1, pos.Coluna - 1);
            }



            //sudeste
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
            }



            //sudoeste
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
            }


            //noroeste
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
            }
            //acima
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.Linha = pos.Linha - 1;
            }

            //a direita
            pos.definirValores(posicao.Linha, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.Coluna = pos.Coluna + 1;
            }

            //abaixo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {


                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.Linha = pos.Linha + 1;
            }

            //esquerda
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {

                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) { break; }
                pos.Coluna = pos.Coluna - 1;
            }


            return matriz;

        }
        public override string ToString()
        {
            return "D";
        }
    }
}
