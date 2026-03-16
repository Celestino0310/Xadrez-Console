using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    /// <summary>
    /// Representa o tabuleiro de xadrez como uma matriz bidimensional de peças.
    /// Responsável por armazenar, mover e validar posições das peças.
    /// </summary>
    public class Tabuleiro
    {
        /// <summary>Número de linhas do tabuleiro (padrão: 8).</summary>
        public int Linhas { get; set; }

        /// <summary>Número de colunas do tabuleiro (padrão: 8).</summary>
        public int Colunas { get; set; }

        private Peca[,] _peca;

        /// <summary>
        /// Construtor do Tabuleiro. Inicializa a matriz de peças vazia.
        /// </summary>
        /// <param name="linhas">Número de linhas.</param>
        /// <param name="coluna">Número de colunas.</param>
        public Tabuleiro(int linhas, int coluna)
        {
            this.Linhas = linhas;
            this.Colunas = coluna;
            _peca = new Peca[linhas, coluna];
        }

        /// <summary>
        /// Retorna a peça na posição especificada por linha e coluna.
        /// </summary>
        public Peca peca(int linha, int coluna)
        {
            return _peca[linha, coluna];
        }

        /// <summary>
        /// Retorna a peça na posição especificada por um objeto Posicao.
        /// </summary>
        public Peca peca(Posicao pos)
        {
            return _peca[pos.Linha, pos.Coluna];
        }

        /// <summary>
        /// Coloca uma peça no tabuleiro na posição informada.
        /// Lança exceção se já existir uma peça nessa posição.
        /// </summary>
        /// <param name="p">A peça a ser colocada.</param>
        /// <param name="pos">A posição de destino.</param>
        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos))
                throw new TabuleiroException("já existe uma peça nessa posição");
            _peca[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }

        /// <summary>
        /// Remove e retorna a peça da posição informada.
        /// Retorna null se a posição estiver vazia.
        /// </summary>
        /// <param name="pos">A posição de onde retirar a peça.</param>
        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null) return null;

            Peca aux = peca(pos);
            aux.posicao = null;
            _peca[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        /// <summary>
        /// Verifica se existe uma peça na posição informada.
        /// Valida a posição antes de verificar.
        /// </summary>
        public bool existePeca(Posicao pos)
        {
            posicaoValida(pos);
            return peca(pos) != null;
        }

        /// <summary>
        /// Verifica se a posição está dentro dos limites do tabuleiro.
        /// </summary>
        /// <returns>True se válida, False se fora dos limites.</returns>
        public bool posicaoValida(Posicao pos)
        {
            return !(pos.Coluna >= Colunas || pos.Coluna < 0
                  || pos.Linha >= Linhas || pos.Linha < 0);
        }

        /// <summary>
        /// Valida a posição e lança exceção se estiver fora dos limites.
        /// </summary>
        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
                throw new TabuleiroException("posição inválida!");
        }
    }
}