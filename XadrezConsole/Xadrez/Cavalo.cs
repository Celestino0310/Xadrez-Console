using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Cavalo no xadrez.
    /// O Cavalo move-se em formato de "L":
    /// 2 casas em uma direção e 1 casa perpendicular (ou vice-versa).
    /// É a única peça capaz de saltar sobre outras peças.
    /// </summary>
    internal class Cavalo : Peca
    {
        /// <summary>
        /// Construtor do Cavalo.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Cavalo(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        /// <summary>
        /// Verifica se o Cavalo pode ocupar a posição:
        /// vazia ou com peça adversária (captura).
        /// O Cavalo ignora peças no caminho — só importa o destino.
        /// </summary>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis do Cavalo.
        /// Verifica os 8 destinos em formato "L" ao redor da posição atual.
        /// Cada movimento é independente — não há bloqueio por peças intermediárias.
        /// </summary>
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            // 2 para esquerda, 1 para cima
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para esquerda, 1 para baixo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para baixo, 1 para esquerda
            pos.definirValores(posicao.Linha + 2, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para baixo, 1 para direita
            pos.definirValores(posicao.Linha + 2, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para direita, 1 para baixo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para direita, 1 para cima
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 2);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para cima, 1 para direita
            pos.definirValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // 2 para cima, 1 para esquerda
            pos.definirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            return matriz;
        }

        /// <summary>
        /// Representação textual do Cavalo no tabuleiro.
        /// </summary>
        public override string ToString()
        {
            return "C";
        }
    }
}