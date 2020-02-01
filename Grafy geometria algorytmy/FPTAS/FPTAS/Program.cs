using System;
using System.Linq;

namespace FPTAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initial example: ");
            int[] a = new int[] { 2, 4, 6, 8, 10 };
            int t = 11;
            Console.WriteLine("a, S = 11"); //OPT = 10
            double exEps = 1;
            for (int i = 1; i <= 100000000; i *= 10)
            {
                double currentEps = exEps / i;
                Console.WriteLine("EPS = " + currentEps);
                Console.WriteLine("KnapSack " + FPTAS.KnapSack(a, a, t, currentEps));
                Console.WriteLine("SubsetSum " + FPTAS.SubsetSum(a, t, currentEps) + "\n");
            }
            Console.WriteLine();


            double eps = 0.01;
            int n = 100;
            int iterations = 50;

            int[] knapSackErrors = new int[iterations];
            int[] subsetSumErrors = new int[iterations];

            for (int size = 1; size <= n; size++)
            {
                for (int i = 0; i < iterations; i++)
                {
                    var generated = Helper.GenerateArray(size);
                    var arr = (int[])generated[1];
                    var sum = (int)generated[0];

                    var knapSackResult = FPTAS.KnapSack(arr, arr, sum, eps);

                    var subsetSumResult = FPTAS.SubsetSum(arr, sum, eps);

                    knapSackErrors[i] = sum - knapSackResult;
                    subsetSumErrors[i] = sum - subsetSumResult;
                }

                Console.WriteLine("n = " + size);
                Console.WriteLine("KnapSack Error Avg: " + knapSackErrors.Average());
                Console.WriteLine("SubsetSum Error Avg: " + subsetSumErrors.Average() + "\n");
            }
        }
    }
}
