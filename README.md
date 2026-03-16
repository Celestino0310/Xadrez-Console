# ♟️ Xadrez Console

Jogo de Xadrez completo rodando direto no terminal, desenvolvido em **C# / .NET** com todas as regras oficiais implementadas.

---

## 🎮 Como Jogar

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download) ou superior
- Windows (o som via `Console.Beep` só funciona no Windows)

### Rodando o projeto
```bash
git clone https://github.com/Celestino0310/Xadrez-Console.git
cd Xadrez-Console
dotnet run --project XadrezConsole
```

### Notação de entrada
O jogo usa notação xadrez padrão com letras e números:
```
posição de origem: e2
posição de destino: e4
```
- Colunas: `a` até `h`
- Linhas: `1` até `8`

---

## ✅ Funcionalidades Implementadas

| Funcionalidade | Status |
|---|---|
| Movimentação de todas as peças | ✅ |
| Captura de peças | ✅ |
| Detecção de Xeque | ✅ |
| Detecção de Xeque-Mate | ✅ |
| Roque Pequeno | ✅ |
| Roque Grande | ✅ |
| En Passant | ✅ |
| Promoção de Peão | ✅ |
| Validação de jogadas ilegais | ✅ |
| Destaque de movimentos possíveis | ✅ |
| Placar de peças capturadas | ✅ |

---

## 🏗️ Arquitetura do Projeto

```
XadrezConsole/
├── tabuleiro/
│   ├── Tabuleiro.cs         # Lógica do tabuleiro (matriz 8x8)
│   ├── Peca.cs              # Classe base abstrata para peças
│   ├── Posicao.cs           # Posição genérica (linha/coluna)
│   └── TabuleiroException.cs
│
├── Xadrez/
│   ├── PartidaDeXadrez.cs   # Controller principal da partida
│   ├── PosicaoXadrez.cs     # Conversão notação xadrez ↔ matriz
│   ├── Rei.cs               # Rei (movimentos + roque)
│   ├── Torre.cs             # Torre
│   ├── Bispo.cs             # Bispo
│   ├── Dama.cs              # Dama
│   ├── Cavalo.cs            # Cavalo
│   └── Peao.cs              # Peão (en passant + promoção)
│
├── Tela.cs                  # Renderização do tabuleiro no console
└── Program.cs               # Entry point
```

---

## 📸 Preview

```
8  T - B - R B C T
7  P P P P P P P P
6  - - - - - - - -
5  - - - - - - - -
4  - - - - - - - -
3  - - - - - - - -
2  P P P P P P P P
1  T - B D R B C T
   a b c d e f g h

Brancas:[]
Pretas :[]
Turno: 1
Esperando jogada do jogador Branca
posição de origem:
```

### Legenda das peças
| Símbolo | Peça |
|---|---|
| `R` | Rei |
| `D` | Dama |
| `T` | Torre |
| `B` | Bispo |
| `C` | Cavalo |
| `P` | Peão |

---

## 🧠 Regras Especiais

### Roque
O roque é executado movendo o Rei duas casas em direção à Torre. O jogo detecta automaticamente se o roque pequeno (lado do rei) ou grande (lado da rainha) é possível, verificando:
- Rei e Torre não se moveram
- Casas entre eles estão vazias
- Rei não está em xeque

### En Passant
Captura especial do peão disponível por apenas **um turno** após o adversário avançar dois quadrados. O jogo controla automaticamente a janela de oportunidade.

### Promoção
Ao chegar na última fileira, o peão é promovido automaticamente.

---

## 🛠️ Tecnologias

- **Linguagem:** C# 
- **Plataforma:** .NET 10
- **Interface:** Console (terminal)
- **IDE recomendada:** Visual Studio 2022

---

## 👤 Autor

**Celestino**  
[github.com/Celestino0310](https://github.com/Celestino0310)
