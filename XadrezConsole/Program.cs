using System;
using tabuleiro;
using XadrezConsole.Xadrez;
using Xadrez;

namespace XadrezConsole
{
    /// <summary>
    /// Ponto de entrada do jogo de Xadrez Console.
    /// Gerencia o loop principal da partida, entrada do jogador,
    /// renderização do tabuleiro e efeitos sonoros.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Toca o jingle de fim de jogo estilo Pokémon Center.
        /// Chamado quando a partida termina em xeque-mate.
        /// </summary>
        private static void music()
        {
            Console.Beep(1047, 120); // Tu
            Console.Beep(1047, 120); // tu
            Console.Beep(1047, 120); // tu
            Thread.Sleep(60);        // Silêncio mínimo entre notas
            Console.Beep(1047, 180); // TÚ (nota longa)
            Thread.Sleep(60);
            Console.Beep(784, 120); // tu
            Console.Beep(880, 120); // ru
            Console.Beep(1047, 400); // ruuu (nota final)
            Console.WriteLine("END GAME");
        }

        /// <summary>
        /// Toca um som curto simulando o impacto de uma peça sendo colocada no tabuleiro.
        /// Usa frequências baixas para imitar um som abafado e pesado.
        /// Nota: Console.Beep só funciona no Windows.
        /// </summary>
        static void SomPecaColocada()
        {
            Console.Beep(50, 40);
        }

        /// <summary>
        /// Método principal — inicializa a partida e executa o loop do jogo.
        /// A cada turno:
        /// 1. Renderiza o tabuleiro e o estado da partida
        /// 2. Lê a posição de origem do jogador
        /// 3. Destaca os movimentos possíveis da peça selecionada
        /// 4. Lê a posição de destino
        /// 5. Executa a jogada e toca o som de peça
        /// O loop continua até ocorrer xeque-mate.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        // Mantém o tamanho fixo para evitar distorção das cores no redimensionamento
                        Console.SetWindowSize(50, 30);
                        Console.SetBufferSize(50, 30);

                        // Renderiza o estado atual da partida
                        Tela.imprimirPartida(partida);

                        // Lê e valida a posição de origem
                        Console.Write("posição de origem:");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPositionXadrez();
                        partida.validarPosicaoDeOrigem(origem);

                        // Destaca os movimentos possíveis da peça selecionada
                        bool[,] posicoesPossiveis = partida.Tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        // Lê e valida a posição de destino
                        Console.Write("posição destino:");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPositionXadrez();
                        partida.validarPosicaoDeDestino(origem, destino);

                        // Executa a jogada e toca o som de peça
                        partida.realizaJogada(origem, destino);
                        SomPecaColocada();
                    }
                    catch (TabuleiroException ex)
                    {
                        // Erros de regra (posição inválida, xeque, etc.) não encerram o jogo
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                // Fim de jogo — exibe o resultado e toca o jingle
                Console.WriteLine("XEQUE MATE!!");
                Console.WriteLine("PERDEDOR: Jogador de " + partida.JogadorAtual);
                music();
            }
            catch (TabuleiroException ex)
            {
                // Erro crítico inesperado fora do loop da partida
                Console.WriteLine(ex.Message);
            }
        }
    }
}