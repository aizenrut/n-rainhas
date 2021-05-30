//Alunos: Igor Christofer Eisenhut e
//        Manoella Marcondes Junkes
using System;

namespace NRainhas
{
    public class Algoritmo
    {
        private const int MULTIPLICADOR_TENTATIVAS = 50_000;
        private const int MAXIMO_TENTATIVAS = 1_000_000;
        private Random random = new();

        public (byte[] cenarioInicial, byte[] solucao) EncontrarSolucao(byte tamanhoTabuleiro)
        {
            var vetorInicial = GerarPosicionamentoInicial(tamanhoTabuleiro);
            var vetor = new byte[tamanhoTabuleiro];
            vetorInicial.CopyTo(vetor, 0);

            var totalConflitos = int.MaxValue;
            var quantidadeMaximaTentativas = Math.Max(tamanhoTabuleiro * MULTIPLICADOR_TENTATIVAS, MAXIMO_TENTATIVAS);
            var quantidadeTentativas = 0;

            do
            {
                var xAleatorio = GerarPosicaoAleatoria(tamanhoTabuleiro);
                var yAleatorio = GerarPosicaoAleatoria(tamanhoTabuleiro);

                var vetorTeste = vetor;
                vetorTeste[xAleatorio] = yAleatorio;

                var totalConflitosTeste = ObterTotalConflitos(vetorTeste);

                if (totalConflitosTeste < totalConflitos)
                {
                    vetor = vetorTeste;
                    totalConflitos = totalConflitosTeste;
                }
            } while (totalConflitos != 0 && ++quantidadeTentativas < quantidadeMaximaTentativas);

            if (quantidadeTentativas == quantidadeMaximaTentativas)
            {
                throw new Exception("Não foi possível encontrar uma solução. Por favor, tente novamente ou reduza o tamanho do tabuleiro");
            }

            return (vetorInicial, vetor);
        }

        private byte[] GerarPosicionamentoInicial(byte tamanhoTabuleiro)
        {
            var vetor = new byte[tamanhoTabuleiro];

            for (int i = 0; i < tamanhoTabuleiro; i++)
            {
                var numeroAleatorio = GerarPosicaoAleatoria(tamanhoTabuleiro);
                vetor[i] = numeroAleatorio;
            }

            return vetor;
        }

        private byte GerarPosicaoAleatoria(byte tamanhoTabuleiro)
        {
            return (byte)random.Next(0, tamanhoTabuleiro);
        }

        private int ObterTotalConflitos(byte[] vetor)
        {
            var conflitos = 0;

            for (int i = 0; i < vetor.Length; i++)
            {
                for (int j = 0; j < vetor.Length; j++)
                {
                    if (j != i && vetor[j] == vetor[i])
                    {
                        conflitos++;
                    }
                }
            }

            for (int i = 0; i < vetor.Length; i++)
            {
                var menorCoordenada = Math.Min(i, vetor[i]);
                var xDiagonalPrincipal = i - menorCoordenada;
                var yDiagonalPrincipal = vetor[i] - menorCoordenada;

                while (xDiagonalPrincipal < vetor.Length && yDiagonalPrincipal < vetor.Length)
                {
                    if (xDiagonalPrincipal != i && vetor[xDiagonalPrincipal] == yDiagonalPrincipal)
                    {
                        conflitos++;
                    }

                    xDiagonalPrincipal++;
                    yDiagonalPrincipal++;
                }
            }

            for (int i = 0; i < vetor.Length; i++)
            {
                var menorCoordenada = Math.Min(i, vetor.Length - 1 - vetor[i]);
                var xDiagonalSecundaria = i - menorCoordenada;
                var yDiagonalSecundaria = vetor[i] + menorCoordenada;

                while (xDiagonalSecundaria < vetor.Length && yDiagonalSecundaria >= 0)
                {
                    if (xDiagonalSecundaria != i && vetor[xDiagonalSecundaria] == yDiagonalSecundaria)
                    {
                        conflitos++;
                    }

                    xDiagonalSecundaria++;
                    yDiagonalSecundaria--;
                }
            }

            return conflitos;
        }
    }
}
