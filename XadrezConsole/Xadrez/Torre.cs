using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Torre no xadrez.
    /// Move-se qualquer número de casas em linha reta
    /// (horizontal ou vertical), bloqueada por peças no caminho.
    /// Também participa do roque junto ao Rei.
    /// </summary>
    internal class Torre : Peca
    {
        /// <summary>
        /// Construtor da Torre.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Torre(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        /// <summary>
        /// Verifica se a Torre pode ocupar a posição:
        /// vazia ou com peça adversária (captura).
        /// </summary>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis da Torre.
        /// Percorre as 4 direções retas (norte, leste, sul, oeste)
        /// casa a casa até encontrar obstáculo, borda ou peça adversária.
        /// </summary>
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

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
        /// Representação textual da Torre no tabuleiro.
        /// </summary>
        public override string ToString()
        {
            return "T";
        }
    }
}