using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using Xadrez;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    public class Tela
    {   public static void imprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            Tela.imprimirTabuleiro(partida.Tab);

            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                
                Console.WriteLine("Esperando jogada do jogador " + partida.JogadorAtual);
                if (partida.Xeque == true) 
                { 
                    Console.WriteLine("O REI ESTÁ EM XEQUE!!!!");
                }
            }
            else { 
                Console.WriteLine("XEQUE MATE!!");
                Console.WriteLine("VENCEDOR JOGADOR :" + partida.JogadorAtual);
                Console.Beep(1047, 120); // Tu
                Console.Beep(1047, 120); // tu
                Console.Beep(1047, 120); // tu
                Thread.Sleep(60);
                Console.Beep(1047, 180); // TÚ (nota longa)
                Thread.Sleep(60);
                Console.Beep(784, 120); // tu
                Console.Beep(880, 120); // ru
                Console.Beep(1047, 400);


            }

        }
        public static void imprimirTabuleiro(Tabuleiro T)
        {
            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    ConsoleColor xua = Console.BackgroundColor;

                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    if (T.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(T.peca(i, j));
                    }

                    Console.BackgroundColor = xua;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro T, bool[,] posiveis)
        {
            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    ConsoleColor xua = Console.BackgroundColor;

                    if (posiveis[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    if (T.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(T.peca(i, j));
                    }

                    Console.BackgroundColor = xua;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

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
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x+" ");
            }
            Console.Write("]");

        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine().ToLower();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}