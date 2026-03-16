using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Dama no xadrez.
    /// A Dama combina os movimentos da Torre e do Bispo:
    /// move-se qualquer número de casas em linha reta ou diagonal,
    /// bloqueada por peças no caminho.
    /// </summary>
    internal class Dama : Peca
    {
        /// <summary>
        /// Construtor da Dama.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Dama(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        /// <summary>
        /// Verifica se a Dama pode ocupar a posição:
        /// vazia ou com peça adversária (captura).
        /// </summary>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis da Dama.
        /// Percorre as 4 diagonais e as 4 direções retas,
        /// parando ao encontrar obstáculo ou borda.
        /// Pode capturar a primeira peça adversária encontrada em cada direção.
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

            // Reta acima (norte — linha--)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.Linha = pos.Linha - 1;
            }

            // Reta direita (leste — coluna++)
            pos.definirValores(posicao.Linha, posicao.Coluna + 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.Coluna = pos.Coluna + 1;
            }

            // Reta abaixo (sul — linha++)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.Linha = pos.Linha + 1;
            }

            // Reta esquerda (oeste — coluna--)
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            while (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != cor) break;
                pos.Coluna = pos.Coluna - 1;
            }

            return matriz;
        }

        /// <summary>
        /// Representação textual da Dama no tabuleiro.
        /// </summary>
        public override string ToString()
        {
            return "D";
        }
    }
}