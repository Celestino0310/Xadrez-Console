using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
namespace XadrezConsole.Xadrez
{
    internal class PosicaoXadrez
    {
        public int Colunas { get; set; }
        public char Linhas { get; set; }

        PosicaoXadrez(char linhas,int colunas) {
        
            this.Colunas = colunas;
            this.Linhas = linhas;
        }
        public Posicao ToPositionXadrez() {

            return new Posicao(8- Colunas, Linhas-'a');
            
        }
    }
}
