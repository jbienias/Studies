using System;
using System.Collections.Generic;
using System.IO;

namespace PairOfClosestPoints
{
    public class Plain
    {
        public List<Point> Points { get; private set; }

        public Plain(string filePath)
        {
            if (File.Exists(filePath))
            {
                Points = new List<Point>();
                var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    var x = 0;
                    var y = 0;
                    string[] point = lines[i].Split(',');
                    Int32.TryParse(point[0], out x);
                    Int32.TryParse(point[1], out y);
                    Points.Add(new Point(x, y));
                }
            }
            else
                throw new FileNotFoundException("File " + filePath + " does not exist!");
        }

        public Point this[int i]
        {
            get { return Points[i]; }
            set { Points[i] = value; }
        }

        //index 0,1 -> Punkty, index 2 -> Dystans
        public Object[] naiveClosestPointDistance(List<Point> S)
        {
            if (S == null || S.Count == 0)
                throw new ArgumentNullException("Lista jest nullem lub jest pusta!");
            else if (S.Count == 1)
                return new object[] { S[0], S[0], 0.0 };
            Point p1 = default(Point);
            Point p2 = default(Point);
            double min = S[0].Distance(S[1]);
            for (int i = 0; i < S.Count; i++)
                for (int j = i + 1; j < S.Count; j++)
                    if (S[i].Distance(S[j]) < min)
                    {
                        min = S[i].Distance(S[j]);
                        p1 = S[i];
                        p2 = S[j];
                    }
            return new object[] { p1, p2, min };
        }

        //index 0,1 -> Punkty, index 2 -> Dystans
        public Object[] ClosestPointsDistance()
        {
            //Preprocessing
            List<Point> S = Points.GetRange(0, Points.Count);
            Helper.MergeSort(S, true);
            return closestPointsDistance(S);
        }

        //index 0,1 -> Punkty, index 2 -> Dystans
        private Object[] closestPointsDistance(List<Point> S)
        {
            //I
            if (S == null || S.Count == 0)
                throw new ArgumentNullException("Lista jest nullem lub jest pusta!");
            else if (S.Count == 1)
                return new object[] { S[0], S[0], 0.0 };

            if (S.Count < 4)
            {
                return naiveClosestPointDistance(S);
            }
            int middle = S.Count / 2;

            List<Point> S1 = S.GetRange(0, middle); //left
            List<Point> S2 = S.GetRange(middle, S.Count - (middle)); //right

            double l = S1[S1.Count - 1].X;

            //II
            double p = (double)closestPointsDistance(S1)[2];
            double q = (double)closestPointsDistance(S2)[2];
            double d = Math.Min(p, q);

            //III
            List<Point> Middle = new List<Point>();
            for (int i = 0; i < S.Count; i++)
                if (S[i].X >= l - d && S[i].X <= l + d)
                    Middle.Add(S[i]);

            //Helper.MergeSort(Middle, false);
            Point p1 = default(Point);
            Point p2 = default(Point);
            for (int i = 0; i < Middle.Count; i++)
                for (int j = i + 1; j < Middle.Count; j++)
                {
                    p1 = Middle[i];
                    p2 = Middle[j];

                    if (p1.Y - p2.Y >= d)
                        break;

                    if (p1.Distance(p2) < d)
                        d = p1.Distance(p2);
                }

            return new object[] { p1, p2, d };
        }
    }
}
