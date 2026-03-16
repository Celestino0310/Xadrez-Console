using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    /// <summary>
    /// Define as cores disponíveis para as peças do jogo.
    /// Em uma partida padrão de xadrez apenas Branca e Preta são utilizadas.
    /// As demais cores estão disponíveis para expansões futuras,
    /// como partidas com mais de dois jogadores.
    /// </summary>
    public enum Cor
    {
        /// <summary>Cor preta — segundo jogador na partida padrão.</summary>
        Preta,

        /// <summary>Cor branca — primeiro jogador na partida padrão, sempre inicia.</summary>
        Branca,

        /// <summary>Cor amarela — reservada para expansão multiplayer.</summary>
        Amarela,

        /// <summary>Cor azul — reservada para expansão multiplayer.</summary>
        Azul,

        /// <summary>Cor laranja — reservada para expansão multiplayer.</summary>
        Laranja
    }
}