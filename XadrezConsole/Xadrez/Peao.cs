using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Peao:Peca
    {
        public Peao(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }


        public override string ToString()
        {
            return "P";
        }
    }
}
