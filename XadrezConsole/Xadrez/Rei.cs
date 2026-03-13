using tabuleiro;
using Xadrez;
namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Rei no jogo de xadrez.
    /// O Rei pode se mover uma casa em qualquer direção (horizontal, vertical ou diagonal).
    /// </summary>
    internal class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro Tab, Cor cor,PartidaDeXadrez pt) : base(Tab, cor) { partida = pt; }

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
        private bool testeTorreParaRock(Posicao pos)
        {
            if (!tabuleiro.posicaoValida(pos)) return false;
            Peca p= tabuleiro.peca(pos);
            return p!=null && p.qtdMovimentos==0 && p.cor==cor && p is Torre; 
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

            // #JOGADAS ESPECIAIS - ROQUE
            if (qtdMovimentos == 0 && !partida.Xeque)
            {
                // BUG CORRIGIDO: era pos.Linha/pos.Coluna (posição do último movimento calculado = noroeste)
                // deve ser posicao.Linha/posicao.Coluna (posição REAL do Rei no tabuleiro)
                Posicao posT1 = new Posicao(posicao.Linha, posicao.Coluna + 3); // Torre lado rei
                Posicao posT2 = new Posicao(posicao.Linha, posicao.Coluna - 4); // Torre lado rainha

                // Roque pequeno
                if (testeTorreParaRock(posT1))
                {
                    Posicao p1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao p2 = new Posicao(posicao.Linha, posicao.Coluna + 2);
                    if (tabuleiro.peca(p1) == null && tabuleiro.peca(p2) == null)
                    {
                        matriz[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }

                // Roque grande
                if (testeTorreParaRock(posT2))
                {
                    Posicao q1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    Posicao q2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                    Posicao q3 = new Posicao(posicao.Linha, posicao.Coluna - 3);
                    if (tabuleiro.peca(q1) == null && tabuleiro.peca(q2) == null && tabuleiro.peca(q3) == null)
                    {
                        matriz[posicao.Linha, posicao.Coluna - 2] = true;
                    }
                }
            }
            // #JOGADAS ESPECIAIS- ROQUE
            if (qtdMovimentos==0 && !partida.Xeque)
            {
                Posicao posT1 = new Posicao(posicao.Linha,posicao.Coluna+3);
                Posicao posT2 = new Posicao(posicao.Linha,posicao.Coluna-4);
                //roque pequeno
                if (testeTorreParaRock(posT1)==true)
                {
                    Posicao p1 = new Posicao(posicao.Linha,posicao.Coluna+1);
                    Posicao p2 = new Posicao(posicao.Linha,posicao.Coluna+2);
                    if(tabuleiro.peca(p1)==null && tabuleiro.peca(p2) == null)
                    {
                        matriz[posicao.Linha,posicao.Coluna+2]= true;
                    }

                }
                //rock grande
                if (testeTorreParaRock(posT2)==true)
                {
                    Posicao q1 = new Posicao(posicao.Linha,posicao.Coluna-1);
                    Posicao q2 = new Posicao(posicao.Linha,posicao.Coluna-2);
                    Posicao q3 = new Posicao(posicao.Linha,posicao.Coluna-3);
                    if(tabuleiro.peca(q1)==null && tabuleiro.peca(q2) == null && tabuleiro.peca(q3)==null)
                    {
                        matriz[posicao.Linha,posicao.Coluna-2]= true;
                    }

                }

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