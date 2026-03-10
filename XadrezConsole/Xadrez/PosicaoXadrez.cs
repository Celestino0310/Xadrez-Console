using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace XadrezConsole.Xadrez
{
    public class PosicaoXadrez
    {
        public int Colunas { get; set; }
        public char Linhas { get; set; }

        public PosicaoXadrez(char linhas, int colunas)
        {
            Colunas = colunas;
            Linhas = linhas;
        }

        public Posicao ToPositionXadrez()
        {
            return new Posicao(8 - Colunas, Linhas - 'a');
        }
    }
}