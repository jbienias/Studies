using System;

namespace PairOfClosestPoints
{
    public struct Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
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
