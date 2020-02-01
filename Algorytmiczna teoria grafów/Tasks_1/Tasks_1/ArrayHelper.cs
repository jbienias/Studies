using System;
using System.Collections.Generic;

namespace Tasks_1
{
    public static class ArrayHelper
    {
        public static int[,] Multiply(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
                throw new ArrayTypeMismatchException("Matrices sizes are incorrect for operation : a * b!");

            int[,] c = new int[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < b.GetLength(0); k++)
                        c[i, j] += a[i, k] * b[k, j];
            return c;
        }

        public static int[,] Transpose(int[,] a)
        {
            int[,] b = new int[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    b[j, i] = a[i, j];
            return b;
        }

        public static string ToString(int[] a)
        {
            string s = "";
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                s += a[i] + " ";
            }
            s += "\n";
            return s;
        }

        public static string ToString(int[,] a)
        {
            string s = "";
            int n = a.GetLength(0);
            int m = a.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    s += a[i, j] + " ";
                }
                s += "\n";
            }
            return s;
        }

        public static string ToString(List<int> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
                s += list[i] + " ";
            return s;
        }

        public static List<List<int>> Copy(List<List<int>> original)
        {
            var copy = new List<List<int>>();
            for (int i = 0; i < original.Count; i++)
            {
                copy.Add(new List<int>());
                for (int j = 0; j < original[i].Count; j++)
                    copy[i].Add(original[i][j]);
            }
            return copy;
        }

        public static string ToString(List<List<int>> list, List<char> names)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += names[i] + " -> ";
                for (int j = 0; j < list[i].Count; j++)
                    s += names[list[i][j]] + " ";
                s += "\n";
            }
            return s;

        }

        public static string ToString(List<List<int>> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += i + " -> ";
                for (int j = 0; j < list[i].Count; j++)
                    s += list[i][j] + " ";
                s += "\n";
            }
            return s;

        }
    }

}
