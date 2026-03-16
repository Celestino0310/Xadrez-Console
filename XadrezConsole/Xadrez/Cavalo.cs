using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Cavalo:Peca
    {
        public Cavalo(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            // Acima (norte)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna-2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // esquerda
            }
            // Acima (norte)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna-2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // Direita
            }

            

            // Direita (leste)
            pos.definirValores(posicao.Linha+2, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // cima
            }
            pos.definirValores(posicao.Linha+2, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // baixo
            }


            // Abaixo (sul)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; //direita
            }
            pos.definirValores(posicao.Linha - 1, posicao.Coluna +2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; //esquerda
            }


            // Esquerda (oeste)
            pos.definirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // cima
            }
            pos.definirValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // baixo
            }


            return matriz;
            
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
