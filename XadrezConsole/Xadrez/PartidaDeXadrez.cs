using XadrezConsole.Xadrez;

using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
namespace Xadrez
{
    internal class PartidaDeXadrez
    {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }

        public bool Terminada { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> Capituradas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            Capituradas = new HashSet<Peca>();
            tabuleiroinicial();
            ;
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("NÃO EXISTE PEÇA NA POSIÇÃO ESCOLHIDA");
            }

            if (JogadorAtual != Tab.peca(pos).cor)
            {
                throw new TabuleiroException("NÃO É A VEZ DESSA COR JOGAR");
            }

            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("NÃO EXISTE movimentos possiveis para a peça de origem ");
            }

        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("POSIÇÃO DE DESTINO INVALIDA! ");

            }

        }
        public void MovimentaPeca(Posicao origem, Posicao destino)
        {


            Peca p = Tab.retirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCaptured = Tab.retirarPeca(destino);

            Tab.colocarPeca(p, destino);
            if (pecaCaptured != null)
            {
                Capituradas.Add(pecaCaptured);
            }

        }

        public HashSet<Peca> CapturadasPorCor(Cor cor)
        {

            HashSet<Peca> AUX = new HashSet<Peca>();
            foreach (Peca x in Capituradas)
            {
                if (x.cor == cor)
                {
                    AUX.Add(x);
                }
            }
            return AUX;
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            MovimentaPeca(origem, destino);
            Turno++;
            MudaJogador();
        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPositionXadrez());
            pecas.Add(peca);
        }
        public void tabuleiroinicial()
        {
            // brancas
            colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            for (char c = 'a'; c <= 'h'; c++)
            {
                colocarNovaPeca(c, 2, new Peao(Tab, Cor.Branca));
            }

            // pretas
            colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            for (char c = 'a'; c <= 'h'; c++)
            {
                colocarNovaPeca(c, 7, new Peao(Tab, Cor.Preta));
            }
        }
    }
}
