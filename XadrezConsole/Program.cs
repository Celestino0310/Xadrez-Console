using System;
using tabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    class Program
    {
        public static void Main(string[] args)
        {   //INSTANCIANDO AS CLASSES NECESSÁRIAS
            Tabuleiro tabuleiro = new Tabuleiro(8, 8);
            Posicao P = new Posicao(2, 2);
            //pósition (horizontal, vertical)= (coluna,linha)
            tabuleiro.peca(P.Linha, P.Coluna);

            //COMO TELA É UM OBJETO ESTÁTICO NÃO PRECISA SER INSTANCIADO,E SE VC INSTANCIAR DA ERRO!VOCE CHAMA PELA CLASSE MESMO...
            try
            {

                tabuleiroinicial(tabuleiro);
                tabuleiro.colocarPeca(new Cavalo(tabuleiro,Cor.Branca), new Posicao(4, 4));
                Tela.imprimirTabuleiro(tabuleiro);
            }
            catch (TabuleiroException ex) {
                Console.WriteLine(ex.Message);
            }
        }
        public static void tabuleiroinicial(Tabuleiro tabuleiro){
            //brancas
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca),new Posicao(0,0));
            tabuleiro.colocarPeca(new Cavalo(tabuleiro,Cor.Branca),new Posicao(1,0));
            tabuleiro.colocarPeca(new Bispo(tabuleiro,Cor.Branca),new Posicao(2,0));
            tabuleiro.colocarPeca(new Dama(tabuleiro,Cor.Branca),new Posicao(3,0));
            tabuleiro.colocarPeca(new Rei(tabuleiro,Cor.Branca),new Posicao(4,0));
            tabuleiro.colocarPeca(new Bispo(tabuleiro,Cor.Branca),new Posicao(5,0));
            tabuleiro.colocarPeca(new Cavalo(tabuleiro,Cor.Branca),new Posicao(6,0));
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca),new Posicao(7,0));
            for(int i = 0; i < 8; i++)
            {
                tabuleiro.colocarPeca(new Peao(tabuleiro, Cor.Branca), new Posicao(i, 1));
            }
            //pretas
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 7));
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Preta), new Posicao(1, 7));
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Preta), new Posicao(2, 7));
            tabuleiro.colocarPeca(new Dama(tabuleiro, Cor.Preta), new Posicao(3, 7));
            tabuleiro.colocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(4, 7));
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Preta), new Posicao(5, 7));
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Preta), new Posicao(6, 7));
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(7, 7));

            // Peões Pretos na linha logo acima das principais (6)
            for (int i = 0; i < 8; i++)
            {
                tabuleiro.colocarPeca(new Peao(tabuleiro, Cor.Preta), new Posicao(i, 6));
            }
        }
            
    }
}