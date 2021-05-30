//Alunos: Igor Christofer Eisenhut e
//        Manoella Marcondes Junkes
using System;
using System.Text;

namespace NRainhas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var algoritmo = new Algoritmo();

            try
            {
                Console.WriteLine("PROBLEMA N-RAINHAS");
                Console.WriteLine();
                Console.Write($"Informe o tamanho do tabuleiro: ");
                var entrada = Console.ReadLine();

                byte tamanho = 0; 

                while (!byte.TryParse(entrada, out tamanho))
                {
                    Console.Write("Tamanho inválido, informe um número entre 0 e 255: ");
                    entrada = Console.ReadLine();
                }

                Console.WriteLine();
                Console.WriteLine("Processando...");
                Console.WriteLine();

                var (cenarioInicial, solucao) = algoritmo.EncontrarSolucao(tamanho);

                Console.WriteLine("Cenário inicial:");
                Console.WriteLine();
                ImprimirTabuleiro(cenarioInicial);

                Console.WriteLine();

                Console.WriteLine("Solução:");
                Console.WriteLine();
                ImprimirTabuleiro(solucao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void ImprimirTabuleiro(byte[] vetor)
        {
            for (int i = 0; i < vetor.Length; i++)
            {
                var sb = new StringBuilder();

                for (int j = 0; j < vetor.Length; j++)
                {
                    var celula = vetor[j] == i ? "X" : " ";
                    sb.Append($"  {celula}  |");
                }

                Console.WriteLine(new string('-', vetor.Length * 6));
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine(new string('-', vetor.Length * 6));
        }
    }
}
