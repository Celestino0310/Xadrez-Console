using XadrezConsole.Xadrez;
using System;
using System.Collections.Generic;
using tabuleiro;

namespace Xadrez
{
    /// <summary>
    /// Controla toda a lógica de uma partida de xadrez:
    /// turnos, movimentos, xeque e xeque-mate.
    /// </summary>
    public class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Xeque { get; private set; }
        public bool Terminada { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> Capituradas;

        /// <summary>
        /// Inicializa uma nova partida, criando o tabuleiro e posicionando as peças.
        /// </summary>
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
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
        /// Captura a peça adversária no destino, se houver.
        /// </summary>
        /// <returns>A peça capturada, ou null se nenhuma foi capturada.</returns>
        public Peca MovimentaPeca(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);

            if (pecaCapturada != null)
                Capituradas.Add(pecaCapturada);
            //especial move rock
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(OrigemT);
                T.IncrementarQtdMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }

            //especial move rock grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(OrigemT);
                T.IncrementarQtdMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }
            //especial en passant
            if (p is Peao)
            {

                if (pecaCapturada == null && origem.Coluna != destino.Coluna)
                {
                    Posicao PosP;
                    if (p.cor == Cor.Branca)
                    {

                        PosP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {

                        PosP = new Posicao(destino.Linha - 1, destino.Coluna);

                    }
                    pecaCapturada = Tab.retirarPeca(PosP);
                    Capituradas.Add(pecaCapturada);




                }


            }
            return pecaCapturada;
        }

        /// <summary>
        /// Desfaz um movimento, devolvendo as peças às posições originais.
        /// Usado para testar xeque/xeque-mate sem alterar o estado real da partida.
        /// </summary>
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
            //especial rock desfazer
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(DestinoT);
                T.decrementarQtdMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }

            //especial move rock grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(DestinoT);
                T.IncrementarQtdMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }
            //especial en passant
            if (p is Peao)
            {

                if (capturada == VulneravelEnPassant && origem.Coluna != destino.Coluna)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao PosP;
                    if (p.cor == Cor.Branca)
                    {

                        PosP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {

                        PosP = new Posicao(4, destino.Coluna);

                    }
                    Tab.colocarPeca(peao,PosP);
                    Capituradas.Remove(capturada);




                }
            }


        }

        /// <summary>
        /// Realiza a jogada completa: move a peça, verifica auto-xeque,
        /// verifica xeque/xeque-mate no adversário e passa o turno.
        /// </summary>
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = MovimentaPeca(origem, destino);

            // BUG CORRIGIDO: não pode deixar o próprio rei em xeque após mover,erro grande
            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("VOCÊ NÃO PODE DEIXAR SEU REI EM XEQUE!");
            }
            //PROMOÇÃO DE PEÃO
            Peca p = Tab.peca(destino);
            if(p is Peao)
            {
                if (p.cor == Cor.Branca &&  destino.Linha==0 || p.cor == Cor.Preta && destino.Linha==7)
                {
                    p = Tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Dama dama = new Dama(Tab,p.cor);
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

            p = Tab.peca(destino);
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;


            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        // ---------------------------------------------------------------
        // LÓGICA DE XEQUE
        // ---------------------------------------------------------------

        /// <summary>
        /// Retorna o Rei da cor informada, buscando entre as peças em jogo.
        /// Lança exceção se o Rei não for encontrado (estado inválido).
        /// </summary>
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                    return x;
            }
            throw new TabuleiroException("NÃO EXISTE REI DA COR " + cor + " NA PARTIDA!");
            // BUG CORRIGIDO: retornar null aqui ocultava erros graves de estado
        }

        /// <summary>
        /// Verifica se o Rei da cor informada está em xeque.
        /// Percorre todas as peças ADVERSÁRIAS e checa se alguma alcança o Rei.
        /// </summary>
        public bool estaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            // BUG CORRIGIDO: era PecasEmJogo(cor) — deve ser as peças ADVERSÁRIAS
            // que ameaçam o Rei, não as da mesma cor!
            foreach (Peca x in PecasEmJogo(corAdversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                    return true;
            }
            return false;
        }


        // Verifica se a cor informada está em xeque-mate:
        // está em xeque E nenhum movimento de nenhuma peça consegue sair do xeque.

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

                            // Guarde a origem ANTES de mover.
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca capturada = MovimentaPeca(origem, destino);
                            bool aindaEmXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, capturada);

                            // Se encontrou ao menos um movimento que sai do xeque, não é mate
                            if (!aindaEmXeque) return false;
                        }
                    }
                }
            }

            // Todos os movimentos testados mantiveram o xeque: é xeque-mate
            return true;
        }

        // ---------------------------------------------------------------
        // CONJUNTOS DE PEÇAS
        // ---------------------------------------------------------------


        public HashSet<Peca> CapturadasPorCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capituradas)
            {
                if (x.cor == cor)
                    aux.Add(x);
            }
            return aux;
        }

        /// <summary>
        /// Retorna todas as peças ainda em jogo (não capturadas) de uma determinada cor.
        /// </summary>
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                    aux.Add(x);
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
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPositionXadrez());
            pecas.Add(peca);
        }

        /// <summary>
        /// Posiciona todas as peças para uma partida completa de xadrez.
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
        /// </summary>
        public void tabuleiroTeste()
        {


            // Brancas
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));

            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));

            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 2, new Peao(Tab, Cor.Branca, this));

            // Pretas
            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));

            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));

            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            for (char c = 'a'; c <= 'h'; c++)
                colocarNovaPeca(c, 7, new Peao(Tab, Cor.Preta, this));
        }
    }
}
