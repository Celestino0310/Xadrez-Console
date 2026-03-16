using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    /// <summary>
    /// Exceção personalizada para erros de regras e estados inválidos do tabuleiro.
    /// Lançada em situações como posição inválida, movimento ilegal ou estado inconsistente da partida.
    /// </summary>
    internal class TabuleiroException : Exception
    {
        /// <summary>
        /// Construtor da exceção do tabuleiro.
        /// </summary>
        /// <param name="msg">Mensagem descrevendo o erro ocorrido.</param>
        public TabuleiroException(string msg) : base(msg) { }
    }
}