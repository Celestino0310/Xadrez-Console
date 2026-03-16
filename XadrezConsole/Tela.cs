using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using Xadrez;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    /// <summary>
    /// Responsável por toda a renderização visual do jogo no terminal.
    /// Contém métodos estáticos para imprimir o tabuleiro, peças, 
    /// peças capturadas e informações da partida.
    /// </summary>
    public class Tela
    {
        /// <summary>
        /// Imprime o estado completo da partida: tabuleiro, peças capturadas,
        /// turno atual, jogador da vez, alertas de xeque e xeque-mate.
        /// Toca um jingle ao fim da partida.
        /// </summary>
        /// <param name="partida">A partida de xadrez em andamento.</param>
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            Tela.imprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.Turno);

            if (!partida.Terminada)
            {
                Console.WriteLine("Esperando jogada do jogador " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("O REI ESTÁ EM XEQUE!!!!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE MATE!!");
                Console.WriteLine("VENCEDOR JOGADOR :" + partida.JogadorAtual);

                // Jingle de vitória estilo Pokémon Center
                Console.Beep(1047, 120);
                Console.Beep(1047, 120);
                Console.Beep(1047, 120);
                Thread.Sleep(60);
                Console.Beep(1047, 180);
                Thread.Sleep(60);
                Console.Beep(784, 120);
                Console.Beep(880, 120);
                Console.Beep(1047, 400);
            }
        }

        /// <summary>
        /// Imprime o tabuleiro no terminal com cores alternadas (verde/cinza).
        /// Casas vazias são representadas por "-".
        /// </summary>
        /// <param name="T">O tabuleiro a ser impresso.</param>
        public static void imprimirTabuleiro(Tabuleiro T)
        {
            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    ConsoleColor xua = Console.BackgroundColor;

                    if ((i + j) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    if (T.peca(i, j) == null)
                        Console.Write("- ");
                    else
                        imprimirPeca(T.peca(i, j));

                    Console.BackgroundColor = xua;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        /// <summary>
        /// Imprime o tabuleiro destacando em azul os movimentos possíveis
        /// de uma peça selecionada.
        /// </summary>
        /// <param name="T">O tabuleiro a ser impresso.</param>
        /// <param name="posiveis">
        /// Matriz booleana onde 'true' indica casas alcançáveis pela peça selecionada.
        /// </param>
        public static void imprimirTabuleiro(Tabuleiro T, bool[,] posiveis)
        {
            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    ConsoleColor xua = Console.BackgroundColor;

                    if (posiveis[i, j])
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else if ((i + j) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                    if (T.peca(i, j) == null)
                        Console.Write("- ");
                    else
                        imprimirPeca(T.peca(i, j));

                    Console.BackgroundColor = xua;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        /// <summary>
        /// Imprime uma peça no terminal com a cor correta:
        /// peças brancas em branco, peças pretas em preto.
        /// </summary>
        /// <param name="peca">A peça a ser impressa.</param>
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("  ");
                return;
            }

            ConsoleColor fundoAtual = Console.BackgroundColor;

            if (peca.cor == Cor.Branca)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;
            }

            Console.BackgroundColor = fundoAtual;
        }

        /// <summary>
        /// Imprime as peças capturadas de ambos os jogadores.
        /// Peças brancas em cor padrão, peças pretas em amarelo para contraste.
        /// </summary>
        /// <param name="partida">A partida de xadrez em andamento.</param>
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.Write("Brancas:");
            imprimirConjunto(partida.CapturadasPorCor(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas :");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.CapturadasPorCor(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        /// <summary>
        /// Imprime um conjunto de peças no formato [P T B ...].
        /// </summary>
        /// <param name="conjunto">Conjunto de peças a ser impresso.</param>
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        /// <summary>
        /// Lê uma posição digitada pelo jogador no formato xadrez (ex: "e2")
        /// e converte para um objeto PosicaoXadrez.
        /// </summary>
        /// <returns>A posição lida convertida em PosicaoXadrez.</returns>
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine().ToLower();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}