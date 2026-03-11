using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Peao:Peca
    {
        public Peao(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null;
        }
        private bool podeComer(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p != null && p.cor != cor;
        }


        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);
            if (cor == Cor.Branca)
            {
                // frente Inicial - pula 2-> refatorar isso para que o peão não pule outros peões
                if( tabuleiro.peca(pos).qtdMovimentos == 0)
                {
                    pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                    if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                    {
                        matriz[pos.Linha, pos.Coluna] = true;
                    }

                }



                //frente        
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // nordeste
                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // noroeste
                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {  // frente
                if (tabuleiro.peca(pos).qtdMovimentos == 0)
                {
                    pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                    if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                    {
                        matriz[pos.Linha, pos.Coluna] = true;
                    }

                }

                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // nordeste
                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // noroeste
                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }


            }

            return matriz;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
