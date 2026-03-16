using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace tabuleiro
{
    /// <summary>
    /// Classe base abstrata para todas as peças do xadrez.
    /// Define os atributos e comportamentos comuns a todas as peças:
    /// cor, posição, quantidade de movimentos e métodos de validação.
    /// </summary>
    public abstract class Peca
    {
        /// <summary>Cor da peça (Branca ou Preta).</summary>
        public Cor cor { get; set; }

        /// <summary>
        /// Quantidade de movimentos já realizados pela peça.
        /// Usado para validar roque, movimento duplo do peão e en passant.
        /// </summary>
        public int qtdMovimentos { get; protected set; }

        /// <summary>
        /// Posição atual da peça no tabuleiro.
        /// Null quando a peça está fora do tabuleiro (capturada).
        /// </summary>
        public Posicao posicao { get; set; }

        /// <summary>Referência ao tabuleiro onde a peça está posicionada.</summary>
        public Tabuleiro tabuleiro { get; protected set; }

        /// <summary>
        /// Construtor base para todas as peças.
        /// Inicializa a peça sem posição (null) e com zero movimentos.
        /// </summary>
        /// <param name="tab">Tabuleiro onde a peça será posicionada.</param>
        /// <param name="cor">Cor da peça (Branca ou Preta).</param>
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.cor = cor;
            this.posicao = null;
            this.tabuleiro = tab;
            this.qtdMovimentos = 0;
        }

        /// <summary>
        /// Método abstrato que cada peça deve implementar,
        /// retornando uma matriz booleana com todos os movimentos possíveis
        /// a partir da posição atual.
        /// </summary>
        /// <returns>
        /// Matriz booleana do tamanho do tabuleiro onde 'true' indica
        /// uma casa para a qual a peça pode se mover.
        /// </returns>
        public abstract bool[,] movimentosPossiveis();

        /// <summary>
        /// Verifica se existe ao menos um movimento possível para a peça.
        /// Percorre toda a matriz de movimentos procurando qualquer 'true'.
        /// </summary>
        /// <returns>True se houver ao menos um movimento disponível.</returns>
        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (mat[i, j]) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se a peça pode se mover para uma posição específica.
        /// </summary>
        /// <param name="pos">Posição de destino a verificar.</param>
        /// <returns>True se o movimento for válido.</returns>
        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        /// <summary>
        /// Incrementa o contador de movimentos da peça.
        /// Chamado sempre que a peça realiza um movimento.
        /// </summary>
        public void IncrementarQtdMovimentos()
        {
            qtdMovimentos++;
        }

        /// <summary>
        /// Decrementa o contador de movimentos da peça.
        /// Chamado ao desfazer um movimento (usado na simulação de xeque/xeque-mate).
        /// </summary>
        public void decrementarQtdMovimentos()
        {
            qtdMovimentos--;
        }
    }
}