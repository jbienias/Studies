using System;
using System.Collections.Generic;
using System.Linq;

namespace AproxSteiner
{
    public struct Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Manhatan { get; private set; } //od pkt 0,0

        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Manhatan = X + Y;
        }

        public static int Orientation(Point p, Point q, Point r)
        {
            var val = (p.X * q.Y * 1) + (p.Y * 1 * r.X) + (1 * q.X * r.Y) -
                (1 * q.Y * r.X) - (p.X * 1 * r.Y) - (p.Y * q.X * 1);
            return (val > 0) ? 1 : ((val == 0) ? 0 : 2);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public bool CompareByX(Point q)
        {
            return (this.X < q.X) || (this.X == q.X && this.Y < q.Y);
        }


        public static List<Point> Sort(List<Point> points, bool x)
        {
            if (x)
                return points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            else
                return points.OrderBy(p => p.Y).ToList();
        }


        public double Distance(Point b)
        {
            return Math.Sqrt(Math.Pow(this.X - b.X, 2) + Math.Pow(this.Y - b.Y, 2));
        }

        public bool CompareByY(Point q)
        {
            return this.Y < q.Y;
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
