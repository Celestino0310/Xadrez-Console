using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    /// <summary>
    /// Representa uma posição no tabuleiro através de linha e coluna.
    /// Utiliza coordenadas internas (0-7) onde [0,0] é o canto superior esquerdo.
    /// Para notação xadrez (a1, e4...) use <see cref="XadrezConsole.Xadrez.PosicaoXadrez"/>.
    /// </summary>
    public class Posicao
    {
        /// <summary>Índice da linha no tabuleiro (0 a 7).</summary>
        public int Linha { get; set; }

        /// <summary>Índice da coluna no tabuleiro (0 a 7).</summary>
        public int Coluna { get; set; }

        /// <summary>
        /// Construtor da Posicao.
        /// </summary>
        /// <param name="linha">Índice da linha (0 a 7).</param>
        /// <param name="coluna">Índice da coluna (0 a 7).</param>
        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        /// <summary>
        /// Atualiza os valores de linha e coluna da posição.
        /// Usado para reutilizar um objeto Posicao sem criar um novo,
        /// evitando alocações desnecessárias durante o cálculo de movimentos.
        /// </summary>
        /// <param name="linha">Novo índice de linha.</param>
        /// <param name="coluna">Novo índice de coluna.</param>
        public void definirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        /// <summary>
        /// Representação textual da posição no formato "linha,coluna".
        /// Exemplo: "3,4"
        /// </summary>
        public override string ToString()
        {
            return Linha + "," + Coluna;
        }
    }
}