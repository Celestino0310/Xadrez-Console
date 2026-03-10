using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    internal class Cavalo:Peca
    {
        public Cavalo(Tabuleiro Tab, Cor cor) : base(Tab, cor) { }


        public override string ToString()
        {
            return "C";
        }
    }
}
