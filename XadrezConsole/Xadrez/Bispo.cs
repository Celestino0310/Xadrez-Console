using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Bispo no xadrez.
    /// Move-se quantas casas quiser nas 4 diagonais,
    /// bloqueado por peças no caminho.
    /// </summary>
    internal class Bispo : Peca
    {
        /// <summary>
        /// Construtor do Bispo.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Bispo(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        /// <summary>
        /// Verifica se o Bispo pode ocupar a posição:
        /// vazia ou com peça adversária (captura).
        /// </summary>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis do Bispo.
        /// Percorre as 4 diagonais casa a casa até encontrar
        /// obstáculo, borda ou peça adversária (que pode capturar).
        /// </summary>
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            // Diagonal noroeste (linha--, coluna--)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.definirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            // Diagonal sudeste (linha++, coluna++)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            // Diagonal sudoeste (linha++, coluna--)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
            }

            // Diagonal nordeste (linha--, coluna++)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
            }

            return matriz;
        }

        /// <summary>
        /// Representação textual do Bispo no tabuleiro.
        /// </summary>
        public override string ToString()
        {
            return "B";
        }
    }
}