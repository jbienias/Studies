using System.Collections.Generic;

namespace PairOfClosestPoints
{
    public abstract class Helper
    {
        public static void MergeSort(List<Point> list, bool x) //x - true - compare by X, false - compare by Y
        {
            if (list.Count > 1)
            {
                int middle = list.Count / 2;
                List<Point> L = list.GetRange(0, middle);
                List<Point> R = list.GetRange(middle, list.Count - (middle));
                MergeSort(L, x);
                MergeSort(R, x);
                int i = 0, j = 0, k = 0;
                while (i < L.Count && j < R.Count)
                {
                    if ((x && L[i].CompareByX(R[j])) || (!x && L[i].CompareByY(R[j])))
                    {
                        list[k] = L[i];
                        i++;
                    }
                    else
                    {
                        list[k] = R[j];
                        j++;
                    }
                    k++;
                }
                while (i < L.Count)
                {
                    list[k] = L[i];
                    i++;
                    k++;
                }
                while (j < R.Count)
                {
                    list[k] = R[j];
                    j++;
                    k++;
                }
            }
        }
    }
}
