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
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            tabuleiroinicial();
;        }
       public void MovimentaPeca(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCaptured= Tab.retirarPeca(destino);

            Tab.colocarPeca(p,destino);

        }        
        
       

        public  void tabuleiroinicial()
        {
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('a', 1).ToPositionXadrez());
            Tab.colocarPeca(new Cavalo(Tab, Cor.Branca), new PosicaoXadrez('b', 1).ToPositionXadrez());
            Tab.colocarPeca(new Bispo(Tab, Cor.Branca), new PosicaoXadrez('c', 1).ToPositionXadrez());
            Tab.colocarPeca(new Dama(Tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPositionXadrez());
            Tab.colocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPositionXadrez());
            Tab.colocarPeca(new Bispo(Tab, Cor.Branca), new PosicaoXadrez('f', 1).ToPositionXadrez());
            Tab.colocarPeca(new Cavalo(Tab, Cor.Branca), new PosicaoXadrez('g', 1).ToPositionXadrez());
            Tab.colocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('h', 1).ToPositionXadrez());
            // brancas


            for (char c = 'a'; c <= 'h'; c++)
            {
                Tab.colocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez(c, 2).ToPositionXadrez());
            }

            // pretas
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('a', 8).ToPositionXadrez());
            Tab.colocarPeca(new Cavalo(Tab, Cor.Preta), new PosicaoXadrez('b', 8).ToPositionXadrez());
            Tab.colocarPeca(new Bispo(Tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPositionXadrez());
            Tab.colocarPeca(new Dama(Tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPositionXadrez());
            Tab.colocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPositionXadrez());
            Tab.colocarPeca(new Bispo(Tab, Cor.Preta), new PosicaoXadrez('f', 8).ToPositionXadrez());
            Tab.colocarPeca(new Cavalo(Tab, Cor.Preta), new PosicaoXadrez('g', 8).ToPositionXadrez());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('h', 8).ToPositionXadrez());



            for (char d = 'a'; d <= 'h'; d++)
            {
                Tab.colocarPeca(new Peao(Tab, Cor.Preta), new PosicaoXadrez(d, 7).ToPositionXadrez());
            }

        }
    }
}
