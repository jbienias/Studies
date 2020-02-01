using System;
using System.Collections.Generic;
using System.IO;

namespace Triangulation
{
    public class Polygon
    {
        public List<Point> Points { get; private set; }

        public Polygon(string filePath)
        {
            //Założenie:
            //Punkty podane w kolejności counterclockwise!

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
                    Points.Add(new Point(x, y) { Name = "p" + (i + 1) });
                }
            }
            else
                throw new FileNotFoundException("File " + filePath + " does not exist!");
            if (Points.Count < 3)
                throw new ArgumentException("Polygon requires three or more points! Instead, it had " + Points.Count + " points.");
            PreparePointsAndLines();
        }

        //Set chain boolean in points
        //Sort points by x
        //Set lines
        private void PreparePointsAndLines() //Preprocessing
        {
            var n = Points.Count;
            var largestX = FindPointWithMaxX().X;
            var chainType = ChainType.Lower;

            for (int i = 1; i < n; i++)
            {
                if (Points[i].X == largestX)
                {
                    chainType = ChainType.Upper;
                    continue;
                }
                Points[i].Chain = chainType;
            }

            //Points = Point.Sort(Points, true);
            var upperChainPoints = new List<Point>();
            var lowerChainPoints = new List<Point>();
            int j = 0;
            while (true)
            {
                if (Points[j].Chain == ChainType.None && Points[j].X == largestX)
                    break;
                lowerChainPoints.Add(Points[j]);
                j++;
            }
            j = n - 1;
            while (true)
            {
                if (Points[j].Chain == ChainType.None && Points[j].X == largestX)
                {
                    upperChainPoints.Add(Points[j]);
                    break;
                }
                upperChainPoints.Add(Points[j]);
                j--;
            }

            Points = mergeSortedLists(upperChainPoints, lowerChainPoints);
            return;
        }


        public List<Line> Triangulation()
        {
            var reportedLines = new List<Line>();
            var stack = new List<Point>();
            stack.Add(Points[0]);
            stack.Add(Points[1]);
            for (int i = 2; i < Points.Count - 1; i++)
            {
                if (Points[i].Chain != stack[stack.Count - 1].Chain)
                {
                    var top = stack[stack.Count - 1];
                    while (stack.Count != 0)
                    {
                        if (stack.Count > 1)
                            reportedLines.Add(new Line(Points[i], stack[stack.Count - 1]));
                        stack.RemoveAt(stack.Count - 1);
                    }
                    stack.Add(top);
                    stack.Add(Points[i]);
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        var top = stack[stack.Count - 1];
                        stack.RemoveAt(stack.Count - 1);
                        while (stack.Count != 0)
                        {
                            var orientation = Point.Orientation(stack[stack.Count - 1], top, Points[i]);
                            if (Points[i].Chain == ChainType.Lower)
                            {
                                if (orientation == 1)
                                {
                                    reportedLines.Add(new Line(Points[i], stack[stack.Count - 1]));
                                    top = stack[stack.Count - 1];
                                    stack.RemoveAt(stack.Count - 1);
                                }
                                else
                                    break;
                            }
                            else if (Points[i].Chain == ChainType.Upper)
                            {
                                if (orientation == 2)
                                {
                                    reportedLines.Add(new Line(Points[i], stack[stack.Count - 1]));
                                    top = stack[stack.Count - 1];
                                    stack.RemoveAt(stack.Count - 1);
                                }
                                else
                                    break;
                            }
                        }
                        stack.Add(top);
                        stack.Add(Points[i]);
                    }
                }
            }
            stack.RemoveAt(stack.Count - 1);
            while (stack.Count != 0)
            {
                if (stack.Count != 1)
                    reportedLines.Add(new Line(Points[Points.Count - 1], stack[stack.Count - 1]));
                stack.RemoveAt(stack.Count - 1);
            }
            return reportedLines;
        }


        private List<Point> mergeSortedLists(List<Point> A, List<Point> B)
        {
            var result = new List<Point>();
            for (int o = 0; o < A.Count + B.Count; o++)
            {
                result.Add(default(Point));
            }
            var i = 0; //index A
            var j = 0; //index B
            var k = 0; //index resulta
            while (i < A.Count && j < B.Count)
            {
                if (A[i].X < B[j].X)
                {
                    result[k] = A[i];
                    i += 1;
                    k += 1;
                }
                else
                {
                    result[k] = B[j];
                    j += 1;
                    k += 1;
                }
            }
            while (i < A.Count)
            {
                result[k] = A[i];
                i += 1;
                k += 1;
            }
            while (j < B.Count)
            {
                result[k] = B[j];
                j += 1;
                k += 1;
            }
            return result;
        }

        private Point FindPointWithMaxX()
        {
            var point = Points[0];
            for (int i = 1; i < Points.Count; i++)
            {
                if (point.X < Points[i].X)
                    point = Points[i];
            }
            return point;
        }

        private Point FindPointWithMinX()
        {
            var point = Points[0];
            for (int i = 1; i < Points.Count; i++)
            {
                if (point.X > Points[i].X)
                    point = Points[i];
            }
            return point;
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
