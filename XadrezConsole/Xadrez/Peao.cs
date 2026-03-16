using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using tabuleiro;
using Xadrez;

namespace XadrezConsole.Xadrez
{
    internal class Peao:Peca
    {

        private PartidaDeXadrez partida;
        public Peao(Tabuleiro Tab, Cor cor, PartidaDeXadrez pt) : base(Tab, cor) { partida = pt; }

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
               

                if( tabuleiro.peca(posicao).qtdMovimentos == 0)
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

                //#jogada especial Le'Passant

               
              
                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);

                    if (tabuleiro.posicaoValida(esquerda) && podeComer(esquerda) && tabuleiro.peca(esquerda) == partida.VulneravelEnPassant ) {
                        matriz[esquerda.Linha-1 ,esquerda.Coluna]=true;
                    
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && podeComer(direita) && tabuleiro.peca(direita) == partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha-1, direita.Coluna] = true;

                    }
                }  
                


            }
            else
            {  // frente
                if (tabuleiro.peca(posicao).qtdMovimentos == 0)
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

               
               
                if (posicao.Linha == 4) // Peão PRETO (chegou na linha 4 indo "para baixo")
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tabuleiro.posicaoValida(esquerda) && podeComer(esquerda) && tabuleiro.peca(esquerda) == partida.VulneravelEnPassant )
                    {
                        matriz[esquerda.Linha +1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && podeComer(direita) && tabuleiro.peca(direita) == partida.VulneravelEnPassant )
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
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
