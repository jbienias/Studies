using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TravellingSalesmanProblem
{
    public class Plain
    {
        public List<Point> Points { get; private set; }
        public double[,] DistanceMatrix { get; private set; }

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
                    Points.Add(new Point(x, y, i));
                }
            }
            else
                throw new FileNotFoundException("File " + filePath + " does not exist!");

            //Create Distance Matrix n^2
            DistanceMatrix = new double[Points.Count, Points.Count];
            for (int i = 0; i < Points.Count; i++)
                for (int j = 0; j < Points.Count; j++)
                    DistanceMatrix[i, j] = Point.Distance(Points[i], Points[j]);
        }


        //https://en.wikipedia.org/wiki/Held%E2%80%93Karp_algorithm
        public double TSP(bool withPrint)
        {
            //tworze wszystkie mozliwe kombinacje punktow
            var combosTmp = Helper.Combinations(Points);

            //... i pakuje do klasy PointsList
            List<PointsList> combos = new List<PointsList>();
            foreach (var combo in combosTmp)
                combos.Add(new PointsList(combo));
            //sortuje po dlugosci kombinacji i indexach
            combos = combos.OrderBy(p => p).ToList();

            //tworze dictionary z pointLista i tablica odleglosci (double, +inf)
            var C = new Dictionary<PointsList, double[]>();
            foreach (var c in combos)
                C[c] = Helper.CreateDoubleMaxValueArray(Points.Count);

            //Zczytanie dystansow z macierzy dystansow do dictionary
            for (int i = 0; i < Points.Count; i++)
                C[combos[i]][i] = DistanceMatrix[0, i];

            for (int i = 2; i < Points.Count; i++)
            {
                var currentCombos = combos.FindAll(p => p.Count == i).ToList();

                foreach (var s in currentCombos)
                {
                    var currentMinList = new List<double>();
                    foreach (var k in s.Points)
                    {
                        foreach (var l in s.Points)
                        {
                            if (k.Id != l.Id)
                            {
                                var key = PointsList.Copy(s);
                                key.Points.Remove(k);
                                currentMinList.Add(C[key][l.Id] + DistanceMatrix[k.Id, l.Id]);
                            }
                        }
                        C[s][k.Id] = currentMinList.Min();
                    }
                }
            }

            var minList = new List<double>();
            var pointsListWOFirst = new PointsList(new List<Point>(Points.GetRange(1, Points.Count - 1)));

            for (int i = 1; i < Points.Count; i++)
                minList.Add(C[pointsListWOFirst][i] + DistanceMatrix[i, 0]);

            var optimalCost = minList.Min();

            if (withPrint)
                Console.WriteLine(optimalPathTSP(optimalCost, pointsListWOFirst, C));

            return optimalCost;
        }

        private string optimalPathTSP(double optimalCost, PointsList pointsListWOFirst, Dictionary<PointsList, double[]> C)
        {
            string optimalPath = "0-";
            string res = "";

            Point l = new Point(Double.MinValue, Double.MinValue, Int32.MinValue);
            foreach (var k in pointsListWOFirst.Points)
            {
                if (optimalCost == (C[pointsListWOFirst][k.Id] + DistanceMatrix[k.Id, 0]))
                {
                    l = k;
                    var cropPoints = PointsList.Copy(pointsListWOFirst);
                    cropPoints.Points.Remove(k);
                    res = backtrackTSP(k.Id, cropPoints, C);
                    break;
                }
            }
            optimalPath += l.Id + "-" + res;
            return optimalPath;
        }

        private string backtrackTSP(int key, PointsList pointsList, Dictionary<PointsList, double[]> C)
        {
            if (pointsList.Count == 0)
                return "0";
            else
            {
                var mins = new List<double>();
                Point l = new Point(Double.MinValue, Double.MinValue, Int32.MinValue);
                foreach (var k in pointsList.Points)
                    mins.Add(C[pointsList][k.Id] + DistanceMatrix[k.Id, key]);
                var min_cost = mins.Min();
                string res = "";
                foreach (var k in pointsList.Points)
                    if (min_cost == C[pointsList][k.Id] + DistanceMatrix[k.Id, key])
                    {
                        l = k;
                        var cropPoints = PointsList.Copy(pointsList);
                        cropPoints.Points.Remove(k);
                        res = backtrackTSP(k.Id, cropPoints, C);
                        break;
                    }
                return l.Id + "-" + res;
            }
        }
    }
}
