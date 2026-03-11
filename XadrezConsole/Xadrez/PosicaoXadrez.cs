using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    public class PosicaoXadrez
    {
        public char Coluna { get; set; }   // letra: a-h
        public int Linha { get; set; }     // número: 1-8

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPositionXadrez()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}