using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using Xadrez;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa a peça Peão no jogo de xadrez.
    /// O Peão possui movimentação assimétrica dependendo da cor:
    /// Branco avança decrementando a linha, Preto incrementando.
    /// </summary>
    internal class Peao : Peca
    {
        private PartidaDeXadrez partida;

        /// <summary>
        /// Construtor do Peão.
        /// </summary>
        /// <param name="Tab">Tabuleiro onde a peça está posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        /// <param name="pt">Referência à partida, necessária para verificar En Passant.</param>
        public Peao(Tabuleiro Tab, Cor cor, PartidaDeXadrez pt) : base(Tab, cor) { partida = pt; }

        /// <summary>
        /// Verifica se o Peão pode avançar para a posição (casa vazia).
        /// O Peão NÃO pode capturar para frente, apenas avançar em casa vazia.
        /// </summary>
        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p == null;
        }

        /// <summary>
        /// Verifica se o Peão pode capturar na posição informada.
        /// Captura válida: casa ocupada por peça adversária.
        /// </summary>
        private bool podeComer(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p != null && p.cor != cor;
        }

        /// <summary>
        /// Calcula todos os movimentos possíveis do Peão a partir da sua posição atual.
        /// Inclui:
        /// - Avanço de 1 casa
        /// - Avanço inicial de 2 casas (somente se a intermediária estiver livre)
        /// - Captura diagonal (nordeste e noroeste)
        /// - En Passant (jogada especial)
        /// </summary>
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                // Movimento duplo inicial — só permitido se a casa intermediária estiver livre
                if (tabuleiro.peca(posicao).qtdMovimentos == 0)
                {
                    Posicao intermediaria = new Posicao(posicao.Linha - 1, posicao.Coluna);
                    if (tabuleiro.posicaoValida(intermediaria) && podeMover(intermediaria))
                    {
                        pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                        if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                        {
                            matriz[pos.Linha, pos.Coluna] = true;
                        }
                    }
                }

                // Avanço normal de 1 casa para frente
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Captura diagonal nordeste
                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Captura diagonal noroeste
                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Jogada especial: En Passant (Peão Branco na linha 3)
                // O peão adversário vulnerável deve estar na coluna adjacente
                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tabuleiro.posicaoValida(esquerda) && podeComer(esquerda)
                        && tabuleiro.peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        // Destino é a diagonal, não a casa lateral
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && podeComer(direita)
                        && tabuleiro.peca(direita) == partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                // Movimento duplo inicial — só permitido se a casa intermediária estiver livre
                if (tabuleiro.peca(posicao).qtdMovimentos == 0)
                {
                    Posicao intermediaria = new Posicao(posicao.Linha + 1, posicao.Coluna);
                    if (tabuleiro.posicaoValida(intermediaria) && podeMover(intermediaria))
                    {
                        pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                        if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                        {
                            matriz[pos.Linha, pos.Coluna] = true;
                        }
                    }
                }

                // Avanço normal de 1 casa para frente
                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Captura diagonal sudeste
                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Captura diagonal sudoeste
                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tabuleiro.posicaoValida(pos) && podeComer(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // Jogada especial: En Passant (Peão Preto na linha 4)
                // O peão adversário vulnerável deve estar na coluna adjacente
                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tabuleiro.posicaoValida(esquerda) && podeComer(esquerda)
                        && tabuleiro.peca(esquerda) == partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tabuleiro.posicaoValida(direita) && podeComer(direita)
                        && tabuleiro.peca(direita) == partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }

        /// <summary>
        /// Representação textual do Peão no tabuleiro.
        /// </summary>
        public override string ToString()
        {
            return "P";
        }
    }
}