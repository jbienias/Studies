using System;
using System.Collections.Generic;
using System.Linq;

namespace FPTAS
{
    public static class Helper
    {
        public static string ArrayToString<T>(T[] array)
        {
            string result = "(" + array.Length + ")[ ";
            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                    result += array[i];
                else
                    result += array[i] + ", ";
            }
            return result += " ]";
        }

        public static string ListToString<T>(List<T> list)
        {
            string result = "(" + list.Count + "){ ";
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                    result += list[i];
                else
                    result += list[i] + ", ";
            }
            return result += " }";
        }

        public static List<T> Merge<T>(List<T> a, List<T> b)
        {
            var c = new List<T>();
            for (int i = 0; i < a.Count; i++)
                c.Add(a[i]);
            for (int i = 0; i < b.Count; i++)
                c.Add(b[i]);
            return c.Distinct().ToList(); //!!!!!!! elementy unikalne !!!!!!!
        }

        public static List<int> AddValueToAll(List<int> list, int value)
        {
            for (int i = 0; i < list.Count; i++)
                list[i] += value;
            return list;
        }

        //0 -> sum of numbers generated
        //1 -> array
        public static object[] GenerateArray(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(1, size);
                sum += array[i];
            }

            return new object[] { sum, array };
        }
    }
}
