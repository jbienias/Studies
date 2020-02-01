using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellingSalesmanProblem
{
    public struct Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public int Id { get; private set; }


        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Id = 0;
        }

        public Point(double x, double y, int id)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public override string ToString()
        {
            return "p" + Id;
        }

        public string ToString2()
        {
            return "[p" + Id + "](" + X + ", " + Y + ")";
        }


        public static List<Point> Sort(List<Point> points, bool x)
        {
            if (x)
                return points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            else
                return points.OrderBy(p => p.Y).ToList();
        }


        public static double Distance(Point a, Point b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
