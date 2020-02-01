using System;
using System.Collections.Generic;
using System.Linq;

namespace KDimensionalTree
{
    public struct Point
    {
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public Point(int x, int y) : this(String.Empty, x, y) { }

        public double Distance(Point b)
        {
            return Math.Sqrt(Math.Pow(this.X - b.X, 2) + Math.Pow(this.Y - b.Y, 2));
        }

        public static int Orientation(Point p, Point q, Point r)
        {
            var val = (p.X * q.Y * 1) + (p.Y * 1 * r.X) + (1 * q.X * r.Y) -
                (1 * q.Y * r.X) - (p.X * 1 * r.Y) - (p.Y * q.X * 1);
            return (val > 0) ? 1 : ((val == 0) ? 0 : 2);
        }

        public static List<Point> Sort(List<Point> points, bool x)
        {
            if (x)
                return points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            else
                return points.OrderBy(p => p.Y).ToList();
        }

        public override string ToString()
        {
            return Name + "(" + X + ", " + Y + ")";
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
