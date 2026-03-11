using tabuleiro;
namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Rei no jogo de xadrez.
    /// O Rei pode se mover uma casa em qualquer direção (horizontal, vertical ou diagonal).
    /// </summary>
    internal class Rei : Peca
    {
        /// <summary>
        /// Construtor do Rei.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Rei(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }

        /// <summary>
        /// Verifica se o Rei pode se mover para uma determinada posição.
        /// O movimento é válido se a posição estiver vazia ou ocupada por peça adversária.
        /// </summary>
        /// <param name="pos">Posição de destino a verificar.</param>
        /// <returns>True se o movimento for permitido, False caso contrário.</returns>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null || p.cor != this.cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis do Rei a partir da sua posição atual.
        /// Verifica as 8 direções: cima, nordeste, direita, sudeste,
        /// baixo, sudoeste, esquerda e noroeste.
        /// </summary>
        /// <returns>
        /// Matriz booleana do tamanho do tabuleiro onde 'true' indica
        /// uma casa para a qual o Rei pode se mover.
        /// </returns>
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            // Acima (norte)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Diagonal superior direita (nordeste)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Direita (leste)
            pos.definirValores(posicao.Linha, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Diagonal inferior direita (sudeste)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Abaixo (sul)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Diagonal inferior esquerda (sudoeste)
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Esquerda (oeste)
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            // Diagonal superior esquerda (noroeste)
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true; // BUG CORRIGIDO: era pos.Linha - 1
            }

            return matriz;
        }

        /// <summary>
        /// Representação textual da peça no tabuleiro.
        /// </summary>
        /// <returns>A letra "R" representando o Rei.</returns>
        public override string ToString()
        {
            return "R";
        }
    }
}