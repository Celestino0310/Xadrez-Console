using System;
using tabuleiro;
using XadrezConsole.Xadrez;
using Xadrez;
namespace XadrezConsole
{
    class Program
    {
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
                //tentando adicionar som de peça
                while (partida.Terminada != true)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tab);

                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.Turno);
                        Console.WriteLine("Esperando jogada do jogador " + partida.JogadorAtual);
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
                    }
                    catch (TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}