using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
namespace XadrezConsole
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro T) { 
            for(int i = 0; i < T.Linhas;i++)
            {
                for(int j=0; j<T.Colunas;j++){
                    if (T.peca(i,j) == null)
                    {
                        Console.Write("- " );
                    }
                    else
                    {
                        Console.Write(T.peca(i,j)+ " ");
                    }
                
                }
                Console.WriteLine();
            } 
        }
    }
}
