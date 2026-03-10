using System;
using tabuleiro;

namespace XadrezConsole
{
    class Program
    {
        public static void Main(string[] args)
        {   //INSTANCIANDO AS CLASSES NECESSÁRIAS
            Tabuleiro tabuleiro = new Tabuleiro(8,8);
            Posicao P= new Posicao(2,2);
            tabuleiro.peca(P.Linha,P.Coluna);

            //COMO TELA É UM OBJETO ESTÁTICO NÃO PRECISA SER INSTANCIADO,E SE VC INSTANCIAR DA ERRO!VOCE CHAMA PELA CLASSE MESMO...
            try
            {
                Tela.imprimirTabuleiro(tabuleiro);
            }
            catch (Exception ex) { 
            
            }
        }
    }
}