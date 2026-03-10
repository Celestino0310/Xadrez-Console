using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Dama:Peca
    {
        public Dama(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }


        public override string ToString()
        {
            return "D";
        }
    }
}
