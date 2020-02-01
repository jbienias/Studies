using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellingSalesmanProblem
{
    public class Helper
    {
        //https://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
        // Iterative, using 'i' as bitmask to choose each combo members
        public static List<List<T>> Combinations<T>(List<T> list)
        {
            int comboCount = (int)Math.Pow(2, list.Count) - 1;
            List<List<T>> result = new List<List<T>>();
            for (int i = 1; i < comboCount + 1; i++)
            {
                // make each combo here
                result.Add(new List<T>());
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                        result.Last().Add(list[j]);
                }
            }
            return result;
        }

        public static double[] CreateDoubleMaxValueArray(int size)
        {
            double[] arr = new double[size];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Double.MaxValue;
            return arr;
        }

        public static string ListToString<T>(List<T> list)
        {
            var result = "";
            foreach (var element in list)
                result += element + " ";
            return result;
        }

        public static string DoubleArrayToString(double[] arr)
        {
            var result = "";
            foreach (var element in arr)
            {
                if (element == Double.MaxValue)
                    result += "inf" + " ";
                else
                    result += element + " ";
            }
            return result;
        }

        public static string ListOfPointsListToString(List<PointsList> list)
        {
            var result = "";
            foreach (var sublist in list)
                result += ListToString(sublist.Points) + "\n";
            return result;
        }

        public static string DictionaryToString(Dictionary<PointsList, double[]> dic)
        {
            var result = "";
            foreach (var entry in dic)
            {
                result += "{key: [" + ListToString(entry.Key.Points) + "], " +
                    "value: [" + DoubleArrayToString(entry.Value) + "]}" + "\n";
            }
            return result;
        }
    }
}
