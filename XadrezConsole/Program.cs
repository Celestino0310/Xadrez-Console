using System;
using tabuleiro;
using XadrezConsole.Xadrez;
using Xadrez;
using System.Runtime.CompilerServices;

namespace XadrezConsole
{
    class Program
    {   
        private static void music()
        {
            Console.Beep(1047, 120); // Tu
            Console.Beep(1047, 120); // tu
            Console.Beep(1047, 120); // tu
            Thread.Sleep(60);
            Console.Beep(1047, 180); // TÚ (nota longa)
            Thread.Sleep(60);
            Console.Beep(784, 120); // tu
            Console.Beep(880, 120); // ru
            Console.Beep(1047, 400);
            Console.WriteLine("END GAME");
        }
        static void SomPecaColocada()
        {
            //Console.Beep(120, 25); pra deixa mais alto 
          
            Console.Beep(50, 40);
        }
        public static void Main(string[] args)
        {   //INSTANCIANDO AS CLASSES NECESSÁRIAS

            PartidaDeXadrez pt = new PartidaDeXadrez();
            Posicao P = new Posicao(2, 2);
            //pósition (horizontal, vertical)= (coluna,linha)
            //tabuleiro.peca(P.Linha, P.Coluna);

            //COMO TELA É UM OBJETO ESTÁTICO NÃO PRECISA SER INSTANCIADO,E SE VC INSTANCIAR DA ERRO!VOCE CHAMA PELA CLASSE MESMO...
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                //tentando adicionar som de peça Console.Beep(x,y)
                while (partida.Terminada != true)
                {
                    try
                    {
                        Tela.imprimirPartida(partida);
                        Console.Write("posição de origem:");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPositionXadrez();
                        partida.validarPosicaoDeOrigem(origem);
                        bool[,] posicoesPossiveis = partida.Tab.peca(origem).movimentosPossiveis();
                        Console.Clear(); 

                        Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.Write("posição destino:");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPositionXadrez();
                        partida.validarPosicaoDeDestino(origem,destino);
                        partida.realizaJogada(origem, destino);
                        SomPecaColocada();
                    }
                    catch (TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.WriteLine("XEQUE MATE!!");
                Console.WriteLine("PERDEDOR: Jogador de " + partida.JogadorAtual);// ta dando o jogador oposto pois ta passando o turno..tem que arruma isso aqui
                music();
            }catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}