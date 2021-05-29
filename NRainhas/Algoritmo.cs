using System;
using System.Collections.Generic;
using System.Text;

namespace NRainhas
{
    public class Algoritmo
    {
        private Random random = new();

        public byte[] EncontrarSolucao(byte tamanhoTabuleiro)
        {
            var vetorInicial = GerarPosicionamentoInicial(tamanhoTabuleiro);

            var totalConflitos = ObterTotalConflitos(vetorInicial);

            return vetorInicial;
        }

        private byte[] GerarPosicionamentoInicial(byte tamanhoTabuleiro)
        {
            var vetor = new byte[tamanhoTabuleiro];

            for (int i = 0; i < tamanhoTabuleiro; i++)
            {
                var numeroAleatorio = (byte)random.Next(0, tamanhoTabuleiro);
                vetor[i] = numeroAleatorio;
            }

            return vetor;
        }

        private int ObterTotalConflitos(byte[] vetor)
        {
            vetor = new byte[] { 3, 0, 2, 1 };
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

                    Console.WriteLine($"[{xDiagonalSecundaria}][{yDiagonalSecundaria}]");
                    xDiagonalSecundaria++;
                    yDiagonalSecundaria--;
                }
            }

            return conflitos;
        }
    }
}
