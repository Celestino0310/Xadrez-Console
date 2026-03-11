using tabuleiro;
namespace XadrezConsole.Xadrez
{
    internal class Rei:Peca
    {

        public Rei(Tabuleiro Tab,Cor cor): base(Tab,cor) { }


        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.peca(pos);
            return p==null||p.cor!=this.cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);
            //acima
            pos.definirValores(posicao.Linha-1,posicao.Coluna);
            if(tabuleiro.posicaoValida(pos)&& podeMover(pos)) {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }

            //nordeste
            pos.definirValores(posicao.Linha-1,posicao.Coluna+1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }
            //direita
            pos.definirValores(posicao.Linha,posicao.Coluna+1);
                if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                {
                    matriz[pos.Linha - 1, pos.Coluna] = true;
                }
            //sudeste
            pos.definirValores(posicao.Linha+1,posicao.Coluna+1);
                    if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                    {
                        matriz[pos.Linha - 1, pos.Coluna] = true;
                    }
            //abaixo
            pos.definirValores(posicao.Linha+1,posicao.Coluna);
                        if (tabuleiro.posicaoValida(pos) && podeMover(pos))
                        {
                            matriz[pos.Linha - 1, pos.Coluna] = true;
                        }  
            //sudoeste
            pos.definirValores(posicao.Linha+1,posicao.Coluna-1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                 matriz[pos.Linha - 1, pos.Coluna] = true;
            }
            //esquerda
            pos.definirValores(posicao.Linha,posicao.Coluna-1);
            if(tabuleiro.posicaoValida(pos)&& podeMover(pos)) {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }

            //noroeste
            pos.definirValores(posicao.Linha-1,posicao.Coluna-1);
            if(tabuleiro.posicaoValida(pos)&& podeMover(pos)) {
                matriz[pos.Linha - 1, pos.Coluna] = true;
            }

           return matriz;


        }
        public override string ToString()
        {
            return "R";
        }

    }
}
