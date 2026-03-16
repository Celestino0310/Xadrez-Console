using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    /// <summary>
    /// Representa uma posição no formato de notação xadrez (ex: "e4", "a1").
    /// Faz a conversão entre a notação humana (coluna letra + linha número)
    /// e as coordenadas internas da matriz do tabuleiro (linha/coluna inteiras).
    /// </summary>
    public class PosicaoXadrez
    {
        /// <summary>
        /// Coluna em notação xadrez, representada por uma letra de 'a' a 'h'.
        /// Corresponde ao eixo horizontal do tabuleiro.
        /// </summary>
        public char Coluna { get; set; }

        /// <summary>
        /// Linha em notação xadrez, representada por um número de 1 a 8.
        /// Corresponde ao eixo vertical do tabuleiro.
        /// </summary>
        public int Linha { get; set; }

        /// <summary>
        /// Construtor da PosicaoXadrez.
        /// </summary>
        /// <param name="coluna">Coluna em notação xadrez ('a' a 'h').</param>
        /// <param name="linha">Linha em notação xadrez (1 a 8).</param>
        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        /// <summary>
        /// Converte a posição em notação xadrez para coordenadas internas da matriz.
        /// A linha é invertida pois o tabuleiro exibe linha 8 no topo (índice 0 da matriz)
        /// e linha 1 na base (índice 7 da matriz).
        /// A coluna 'a' corresponde ao índice 0, 'b' ao índice 1, e assim por diante.
        /// </summary>
        /// <returns>
        /// Um objeto <see cref="Posicao"/> com as coordenadas internas equivalentes.
        /// </returns>
        public Posicao ToPositionXadrez()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        /// <summary>
        /// Representação textual da posição em notação xadrez.
        /// Exemplo: coluna 'e' + linha 4 = "e4".
        /// </summary>
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}