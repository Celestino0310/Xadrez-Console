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
        public Posicao posicao { get; protected set; }
        public Tabuleiro tabuleiro { get; protected set; }



        public Peca(Cor cor,Posicao posicao,Tabuleiro tab) { 
            this.cor = cor;
            this.posicao= posicao;
            this.tabuleiro = tab;
            this.qtdMovimentos = 0;
        
        }
    }
}
