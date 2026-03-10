using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using tabuleiro;
using Xadrez;
using XadrezConsole.Xadrez;
namespace XadrezConsole
{
    public class Tela
    {
       
        public static void imprimirTabuleiro(Tabuleiro T) { 
            for(int i = 0; i < T.Linhas;i++)
            {
                Console.Write(8-i+" ");
                for(int j=0; j<T.Colunas;j++){
                    ConsoleColor xua = Console.BackgroundColor;
                    if ((i + j) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    if (T.peca(i,j) == null)
                    {
                        Console.Write("- " );
                    }
                    else
                    {
                    imprimirPeca(T.peca(i,j));
                    }
                    Console.BackgroundColor = xua;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }
        public static void imprimirPeca(Peca peca)
        {   
            if (peca.cor == Cor.Branca)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;

            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;

            }

        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);

        }
    }
}
