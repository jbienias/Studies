using System;
using System.Collections.Generic;
using System.IO;

//Jan Bienias 238201

namespace SimplePolygonKernel
{
    public class Polygon
    {
        public List<Point> Points { get; private set; }
        public Point Max { get; private set; } //global Maxima
        public Point Min { get; private set; } //global Minima
        public List<Point> Maximas { get; private set; }
        public List<Point> Minimas { get; private set; }
        public bool KernelExists { get; private set; }

        public Polygon(string filePath)
        {
            //Zakładamy, że wczytywany plik z punktami:
            // - punkty podane przeciwnie do wskazowek zegara
            // - punkty całkowite
            // - nie ma powtarzających się punktów (wszystkie unikalne)
            // - punkty tworzą wielokąt prosty
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
            if (Points.Count < 3)
                throw new ArgumentException("Polygon requires three or more points! Instead, it had " + Points.Count + " points.");
            Maximas = new List<Point>();
            Minimas = new List<Point>();
            KernelExists = kernelExists();
        }

        public double calculateKernelPerimeter(bool printPerimeterPoints)
        {
            List<Point> perimeterPoints = kernelPoints();
            if (perimeterPoints == null || perimeterPoints.Count == 0 || !KernelExists)
                return 0.0;

            if (printPerimeterPoints)
            {
                Console.WriteLine("Perimeter points: ");
                foreach (var pp in perimeterPoints)
                    Console.WriteLine(pp);
            }

            double perimeter = 0.0;
            var n = perimeterPoints.Count;
            for (int i = 0; i < n; i++)
            {
                var a = perimeterPoints[i % n];
                var b = perimeterPoints[(i + 1) % n];
                perimeter += Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            }
            return perimeter;
        }

        private bool kernelExists()
        {
            var n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                var p = Points[i % n];
                var q = Points[(i + 1) % n];
                var r = Points[(i + 2) % n];
                var s = Points[(i + 3) % n];

                if (p.Y != q.Y && Point.Orientation(p, q, r) == 2)
                {
                    if (p.Y < q.Y && r.Y < q.Y)
                        Maximas.Add(q);
                    else if (p.Y > q.Y && r.Y > q.Y)
                        Minimas.Add(q);
                    else if (r.Y == q.Y && Point.Orientation(q, r, s) == 2)
                    {
                        if (s.Y < r.Y)
                            Maximas.Add(q);
                        else if (s.Y > r.Y)
                            Minimas.Add(q);
                    }
                }
            }

            if (Minimas.Count == 0)
                Min = findPointWithBiggestY(Points);
            else
                Min = findPointWithSmallestY(Minimas);

            if (Maximas.Count == 0)
                Max = findPointWithSmallestY(Points);
            else
                Max = findPointWithBiggestY(Maximas);

            return (Max.Y <= Min.Y);
        }

        private List<Point> kernelPoints()
        {
            var perimeterPoints = new List<Point>();
            int n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                var a = Points[i];
                var b = Points[(i + 1) % n];

                var a_above = a.Y > Min.Y; //above Minimum
                var b_above = b.Y > Min.Y;
                var a_below = a.Y < Max.Y; //below Maximum
                var b_below = b.Y < Max.Y;
                var a_inside = a.Y <= Min.Y && a.Y >= Max.Y;
                var b_inside = b.Y <= Min.Y && b.Y >= Max.Y;

                if (!((a_above && b_above) || (a_below && b_below)))
                {
                    var crossingPoints = crossingPointsWithExtrema(a, b);
                    var minCrossPoint = crossingPoints[0];
                    var maxCrossPoint = crossingPoints[1];

                    //I
                    if (a_inside && b_inside)
                    {
                        perimeterPoints.Add(new Point(a.X, a.Y));
                    }
                    //II
                    else if (a_above && b_below)
                    {
                        perimeterPoints.Add(minCrossPoint);
                        perimeterPoints.Add(maxCrossPoint);
                    }
                    //III
                    else if (a_above && b_inside)
                    {
                        perimeterPoints.Add(minCrossPoint);
                    }
                    //IV
                    else if (a_inside && b_below)
                    {
                        perimeterPoints.Add(new Point(a.X, a.Y));
                        if (!maxCrossPoint.Equals(a))
                            perimeterPoints.Add(maxCrossPoint);
                    }
                    //V
                    else if (a_below && b_above)
                    {
                        perimeterPoints.Add(maxCrossPoint);
                        perimeterPoints.Add(minCrossPoint);
                    }
                    //VI
                    else if (a_below && b_inside)
                    {
                        perimeterPoints.Add(maxCrossPoint);
                    }
                    //VII
                    else if (a_inside && b_above)
                    {
                        perimeterPoints.Add(new Point(a.X, a.Y));
                        if (!minCrossPoint.Equals(a))
                            perimeterPoints.Add(minCrossPoint);
                    }
                }
            }
            return perimeterPoints;
        }

        private double[] coefficients(Point a, Point b)
        {
            var A = (b.Y - a.Y) / (b.X - a.X);
            var B = a.Y - a.X * A;
            return new[] { A, B };
        }

        private Point[] crossingPointsWithExtrema(Point a, Point b) //both min and max
        {
            double minCrossPointX;
            double maxCrossPointX;
            if (a.X == b.X || a.Y == b.Y)
            {
                minCrossPointX = a.X;
                maxCrossPointX = a.X;
            }
            else
            {
                var coefficients = this.coefficients(a, b);
                var A = coefficients[0];
                var B = coefficients[1];
                minCrossPointX = (Min.Y - B) / A;
                maxCrossPointX = (Max.Y - B) / A;
            }
            return new Point[] { new Point(minCrossPointX, Min.Y), new Point(maxCrossPointX, Max.Y) };
        }


        private Point findPointWithBiggestY(List<Point> list)
        {
            if (list == null || list.Count == 0)
                return null;
            var biggest = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (biggest.Y < list[i].Y)
                    biggest = list[i];
            }
            return biggest;
        }

        private Point findPointWithSmallestY(List<Point> list)
        {
            if (list == null || list.Count == 0)
                return null;
            var smallest = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (smallest.Y > list[i].Y)
                    smallest = list[i];
            }
            return smallest;
        }

        public override string ToString()
        {
            var str = String.Empty;
            foreach (var point in Points)
            {
                str += point + "\n";
            }
            return str.Remove(str.Length - 1);
        }
    }
}
