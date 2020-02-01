using System;
using System.Collections.Generic;
using System.Linq;

namespace FPTAS
{
    public static class FPTAS
    {
        //------------------------------------------KNAPSACK------------------------------------------
        public static int KnapSack(int[] values, int[] sizes, int S, double eps)
        {
            if (values.Length != sizes.Length)
                throw new Exception("Values and Weights arrays must contain the same ammount of elements!");

            var n = values.Length;
            var P = values.Max();
            var K = (eps * P) / n;
            int[] valuesPrim = new int[n];

            for (int i = 0; i < n; i++)
                valuesPrim[i] = (int)(values[i] / K);

            return (int)(KnapSackDP(valuesPrim, sizes, S) * K);
        }

        public static int KnapSackDP(int[] values, int[] sizes, int S)
        {
            if (values.Length != sizes.Length)
                throw new Exception("Values and Weights arrays must contain the same ammount of elements!");

            var n = values.Length;
            int result = 0;

            int[,] K = new int[n + 1, S + 1];
            var list = new List<int>();

            for (int i = 0; i <= n; i++)
            {
                for (int s = 0; s <= S; s++)
                {
                    if (i == 0 || s == 0)
                        continue;
                    else if (sizes[i - 1] > s)
                        K[i, s] = K[i - 1, s];
                    else
                        K[i, s] = Math.Max(K[i - 1, s], K[i - 1, s - sizes[i - 1]] + values[i - 1]);

                }
            }
            result = K[n, S];
            return result;
        }

        //------------------------------------------SUBSETSUM------------------------------------------

        public static int SubsetSum(int[] array, int t, double epsilon)
        {
            Array.Sort(array);
            var n = array.Length;

            List<int> L = new List<int>();
            L.Add(0);

            for (int i = 0; i < n; i++)
            {
                L = Helper.Merge(L, Helper.AddValueToAll(new List<int>(L), array[i]));
                L = Trim(L, epsilon / L.Count);
                L.RemoveAll(x => x > t);
            }

            return L.Max();
        }

        private static List<int> Trim(List<int> L, double sigma)
        {
            var Lout = new List<int>();
            Lout.Add(L[0]);
            var tmp = L[0];

            for (int i = 1; i < L.Count; i++)
            {
                if (tmp < (1 - sigma) * L[i])
                {
                    Lout.Add(L[i]);
                    tmp = L[i];
                }
            }
            return Lout;
        }

        //------------------------------------------UNUSED------------------------------------------
        //https://www.geeksforgeeks.org/printing-items-01-knapsack/
        private static List<int> getItemIndexesListFromKnapsack(int[] values, int[] sizes, int S, int[,] K, int result)
        {
            var n = values.Length;
            var list = new List<int>();
            int s = S;
            for (int i = n; i > 0 && result > 0; i--)
            {
                if (result == K[i - 1, s])
                    continue;
                else
                {
                    list.Add(i - 1);
                    result = result - values[i - 1];
                    s = s - sizes[i - 1];
                }
            }
            return list.OrderBy(p => p).ToList();
        }

    }
}
