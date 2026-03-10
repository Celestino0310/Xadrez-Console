using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Torre:Peca
    {
        public Torre(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }


        public override string ToString()
        {
            return "T";
        }
    }
}
