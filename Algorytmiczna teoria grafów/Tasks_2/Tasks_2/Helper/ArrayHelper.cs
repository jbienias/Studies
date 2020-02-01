using System;
using System.Collections.Generic;

namespace Tasks_2.Helper
{
    public static class ArrayHelper
    {
        public static int[,] Multiply(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
                throw new ArrayTypeMismatchException("Rozmiary macierzy są niepoprawne aby wykonać mnożenie : a * b!");

            int[,] c = new int[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < b.GetLength(0); k++)
                        c[i, j] += a[i, k] * b[k, j];
            return c;
        }

        public static T[,] CopyArray<T>(T[,] array)
        {
            T[,] result = new T[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    result[i, j] = array[i, j];
            return result;
        }

        public static T[] CopyArray<T>(T[] array)
        {
            T[] result = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = array[i];
            return result;
        }

        public static T[] InitArray<T>(int size, T initValue = default(T))
        {
            T[] result = new T[size];
            for (int i = 0; i < size; i++)
                result[i] = initValue;
            return result;
        }

        public static T[,] Transpose<T>(T[,] a)
        {
            T[,] b = new T[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    b[j, i] = a[i, j];
            return b;
        }

        public static string ToString<T>(T[] a)
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

        public static string ToString<T>(T[,] a)
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

        public static string ToString<T>(List<T> list, bool newLine)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += list[i] + " ";
                if (newLine && i != list.Count - 1)
                    s += "\n";
            }
            s += "\n";

            return s;
        }

        public static string ToString<T>(List<T> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += list[i] + " ";
            }
            return s;
        }

        public static string ToString<T>(List<List<T>> list)
        {
            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                s += "(" + i + ") -> {";
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == list[i].Count - 1)
                        s += list[i][j];
                    else
                        s += list[i][j] + ", ";
                }
                s += "}\n";
            }
            return s;
        }
    }
}
