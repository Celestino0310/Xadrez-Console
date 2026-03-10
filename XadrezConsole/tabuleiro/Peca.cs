using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using tabuleiro;

namespace tabuleiro
{
    public class Peca
    {

        public  Cor cor{ get; set; }
        public int qtdMovimentos { get;protected set;}
        public Posicao posicao { get;  set; }//é normalmente protected set mas eu vejo segurança dps..
        public Tabuleiro tabuleiro { get; protected set; }



        public Peca(Tabuleiro tab,Cor cor) { 
            this.cor = cor;
            this.posicao= null;
            this.tabuleiro = tab;
            this.qtdMovimentos = 0;
        
        }
    }
}
