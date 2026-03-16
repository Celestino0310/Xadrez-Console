using XadrezConsole.Xadrez;
using System;
using System.Collections.Generic;
using tabuleiro;

namespace Xadrez
{
    /// <summary>
    /// Controla toda a lógica de uma partida de xadrez:
    /// turnos, movimentos, xeque, xeque-mate e jogadas especiais.
    /// </summary>
    public class PartidaDeXadrez
    {
        /// <summary>Tabuleiro da partida.</summary>
        public Tabuleiro Tab { get; private set; }

        /// <summary>Número do turno atual (inicia em 1).</summary>
        public int Turno { get; private set; }

        /// <summary>Cor do jogador que deve realizar a próxima jogada.</summary>
        public Cor JogadorAtual { get; private set; }

        /// <summary>Indica se o Rei do jogador atual está em xeque.</summary>
        public bool Xeque { get; private set; }

        /// <summary>Indica se a partida foi encerrada por xeque-mate.</summary>
        public bool Terminada { get; private set; }

        /// <summary>
        /// Peão vulnerável ao En Passant no turno atual.
        /// Definido quando um peão avança 2 casas — válido apenas no turno seguinte.
        /// </summary>
        public Peca VulneravelEnPassant { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> Capituradas;

        /// <summary>
        /// Inicializa uma nova partida, criando o tabuleiro 8x8
        /// e posicionando todas as peças na configuração inicial.
        /// </summary>
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            Capituradas = new HashSet<Peca>();
            tabuleiroinicial();
        }

        // ---------------------------------------------------------------
        // VALIDAÇÕES
        // ---------------------------------------------------------------

        /// <summary>
        /// Valida se a posição de origem escolhida pelo jogador é legal:
        /// deve existir uma peça, ser da cor do jogador atual e ter movimentos disponíveis.
        /// </summary>
        /// <param name="pos">Posição de origem a validar.</param>
        /// <exception cref="TabuleiroException">Lançada se a origem for inválida.</exception>
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
                throw new TabuleiroException("NÃO EXISTE PEÇA NA POSIÇÃO ESCOLHIDA");

            if (JogadorAtual != Tab.peca(pos).cor)
                throw new TabuleiroException("NÃO É A VEZ DESSA COR JOGAR");

            if (!Tab.peca(pos).existeMovimentosPossiveis())
                throw new TabuleiroException("NÃO EXISTE MOVIMENTOS POSSÍVEIS PARA A PEÇA DE ORIGEM");
        }

        /// <summary>
        /// Valida se a posição de destino é alcançável pela peça de origem.
        /// </summary>
        /// <param name="origem">Posição da peça a mover.</param>
        /// <param name="destino">Posição de destino desejada.</param>
        /// <exception cref="TabuleiroException">Lançada se o destino for inválido.</exception>
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).movimentoPossivel(destino))
                throw new TabuleiroException("POSIÇÃO DE DESTINO INVÁLIDA!");
        }

        // ---------------------------------------------------------------
        // MOVIMENTAÇÃO
        // ---------------------------------------------------------------

        /// <summary>
        /// Executa fisicamente o movimento de uma peça no tabuleiro.
        /// Trata automaticamente as jogadas especiais:
        /// roque pequeno, roque grande e en passant.
        /// </summary>
        /// <param name="origem">Posição de origem da peça.</param>
        /// <param name="destino">Posição de destino da peça.</param>
        /// <returns>A peça capturada, ou null se nenhuma foi capturada.</returns>
        public Peca MovimentaPeca(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);

            if (pecaCapturada != null)
                Capituradas.Add(pecaCapturada);

            // Jogada especial: Roque Pequeno (Rei move 2 casas para direita)
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(OrigemT);
                T.IncrementarQtdMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }

            // Jogada especial: Roque Grande (Rei move 2 casas para esquerda)
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(OrigemT);
                T.IncrementarQtdMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }

            // Jogada especial: En Passant
            // Ocorre quando o peão move na diagonal para casa vazia
            if (p is Peao && pecaCapturada == null && origem.Coluna != destino.Coluna)
            {
                Posicao PosP = p.cor == Cor.Branca
                    ? new Posicao(destino.Linha + 1, destino.Coluna)
                    : new Posicao(destino.Linha - 1, destino.Coluna);

                pecaCapturada = Tab.retirarPeca(PosP);
                Capituradas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        /// <summary>
        /// Desfaz um movimento, devolvendo todas as peças às posições originais.
        /// Também desfaz corretamente as jogadas especiais (roque e en passant).
        /// Usado para simular jogadas durante a verificação de xeque/xeque-mate.
        /// </summary>
        /// <param name="origem">Posição de origem da peça movida.</param>
        /// <param name="destino">Posição de destino onde a peça foi colocada.</param>
        /// <param name="capturada">Peça que foi capturada no movimento (pode ser null).</param>
        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();

            if (capturada != null)
            {
                Tab.colocarPeca(capturada, destino);
                Capituradas.Remove(capturada);
            }

            Tab.colocarPeca(p, origem);

            // Desfaz Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(DestinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }

            // Desfaz Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(DestinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }

            // Desfaz En Passant: reposiciona o peão capturado na casa original
            if (p is Peao && capturada == VulneravelEnPassant && origem.Coluna != destino.Coluna)
            {
                Peca peao = Tab.retirarPeca(destino);
                Posicao PosP = p.cor == Cor.Branca
                    ? new Posicao(3, destino.Coluna)
                    : new Posicao(4, destino.Coluna);

                Tab.colocarPeca(peao, PosP);
                Capituradas.Remove(capturada);
            }
        }

        /// <summary>
        /// Realiza a jogada completa: move a peça, verifica auto-xeque,
        /// trata promoção de peão, verifica xeque/xeque-mate no adversário,
        /// atualiza o VulneravelEnPassant e passa o turno.
        /// </summary>
        /// <param name="origem">Posição de origem da peça.</param>
        /// <param name="destino">Posição de destino da peça.</param>
        /// <exception cref="TabuleiroException">Lançada se o movimento deixar o próprio Rei em xeque.</exception>
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = MovimentaPeca(origem, destino);

            // Não é permitido deixar o próprio Rei em xeque
            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("VOCÊ NÃO PODE DEIXAR SEU REI EM XEQUE!");
            }

            // Promoção de Peão: ao atingir a última fileira, promove automaticamente para Dama
            Peca p = Tab.peca(destino);
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.Linha == 0) ||
                    (p.cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Dama dama = new Dama(Tab, p.cor);
                    Tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            // Verifica se o adversário ficou em xeque ou xeque-mate
            Xeque = estaEmXeque(corAdversaria(JogadorAtual));

            if (XequeMate(corAdversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // Atualiza o peão vulnerável ao En Passant (válido apenas por 1 turno)
            p = Tab.peca(destino);
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = p;
            else
                VulneravelEnPassant = null;
        }

        // ---------------------------------------------------------------
        // LÓGICA DE XEQUE
        // ---------------------------------------------------------------

        /// <summary>
        /// Retorna o Rei da cor informada buscando entre as peças em jogo.
        /// </summary>
        /// <param name="cor">Cor do Rei a localizar.</param>
        /// <returns>A peça Rei da cor informada.</returns>
        /// <exception cref="TabuleiroException">Lançada se o Rei não existir (estado inválido).</exception>
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei) return x;
            }
            throw new TabuleiroException("NÃO EXISTE REI DA COR " + cor + " NA PARTIDA!");
        }

        /// <summary>
        /// Verifica se o Rei da cor informada está em xeque.
        /// Percorre todas as peças adversárias e checa se alguma alcança o Rei.
        /// </summary>
        /// <param name="cor">Cor do Rei a verificar.</param>
        /// <returns>True se o Rei estiver em xeque.</returns>
        public bool estaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            foreach (Peca x in PecasEmJogo(corAdversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica se a cor informada está em xeque-mate:
        /// está em xeque E nenhum movimento de nenhuma peça consegue sair do xeque.
        /// </summary>
        /// <param name="cor">Cor a verificar.</param>
        /// <returns>True se for xeque-mate.</returns>
        public bool XequeMate(Cor cor)
        {
            if (!estaEmXeque(cor)) return false;

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca capturada = MovimentaPeca(origem, destino);
                            bool aindaEmXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, capturada);
                            if (!aindaEmXeque) return false;
                        }
                    }
                }
            }
            return true;
        }

        // ---------------------------------------------------------------
        // CONJUNTOS DE PEÇAS
        // ---------------------------------------------------------------

        /// <summary>
        /// Retorna todas as peças capturadas de uma determinada cor.
        /// </summary>
        /// <param name="cor">Cor das peças capturadas a filtrar.</param>
        public HashSet<Peca> CapturadasPorCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capituradas)
            {
                if (x.cor == cor) aux.Add(x);
            }
            return aux;
        }

        /// <summary>
        /// Retorna todas as peças ainda em jogo (não capturadas) de uma determinada cor.
        /// </summary>
        /// <param name="cor">Cor das peças a filtrar.</param>
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor) aux.Add(x);
            }
            aux.ExceptWith(CapturadasPorCor(cor));
            return aux;
        }

        // ---------------------------------------------------------------
        // AUXILIARES PRIVADOS
        // ---------------------------------------------------------------

        /// <summary>
        /// Retorna a cor adversária da cor informada.
        /// </summary>
        private Cor corAdversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        /// <summary>
        /// Alterna o jogador atual entre Branco e Preto ao fim de cada turno.
        /// </summary>
        private void MudaJogador()
        {
            JogadorAtual = (JogadorAtual == Cor.Branca) ? Cor.Preta : Cor.Branca;
        }

        // ---------------------------------------------------------------
        // POSICIONAMENTO DE PEÇAS
        // ---------------------------------------------------------------

        /// <summary>
        /// Coloca uma nova peça no tabuleiro usando notação xadrez (ex: 'e', 1).
        /// Registra a peça no conjunto geral de peças da partida.
        /// </summary>
        /// <param name="coluna">Coluna em notação xadrez ('a' a 'h').</param>
        /// <param name="linha">Linha em notação xadrez (1 a 8).</param>
        /// <param name="peca">Peça a ser colocada.</param>
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPositionXadrez());
            pecas.Add(peca);
        }

        /// <summary>
        /// Posiciona todas as peças para uma partida completa de xadrez
        /// na configuração oficial inicial.
        /// </summary>
        public void tabuleiroinicial()
        {
            // Brancas
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 2, new Peao(Tab, Cor.Branca, this));

            // Pretas
            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 7, new Peao(Tab, Cor.Preta, this));
        }

        /// <summary>
        /// Posicionamento reduzido para testes rápidos de lógica.
        /// Inclui apenas Torres, Reis e Peões para ambos os lados.
        /// </summary>
        public void tabuleiroTeste()
        {
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 2, new Peao(Tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 7, new Peao(Tab, Cor.Preta, this));
        }
    }
}