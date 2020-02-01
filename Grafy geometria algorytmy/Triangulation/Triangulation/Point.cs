using System.Collections.Generic;
using System.Linq;

namespace Triangulation
{

    public enum ChainType
    {
        None,
        Upper,
        Lower
    }

    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public ChainType Chain { get; set; }
        public string Name { get; set; }

        public Point(double x, double y)
        {
            Chain = ChainType.None;
            X = x;
            Y = y;

        }

        //= 0 współliniowe -> zwraca 0
        //> 0 orientacja dodatnia (lewo) -> zwraca 1
        //< 0 orientacja ujemna (prawo) -> zwraca 2
        public static int Orientation(Point p, Point q, Point r)
        {
            var val = (p.X * (q.Y - r.Y)) - (p.Y * (q.X - r.X)) + (q.X * r.Y - q.Y * r.X);
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

        public string ToString(bool simple)
        {
            if (simple) return Name;
            else return ToString();
        }

        public string ToString(int i)
        {
            return Name + "[" + Chain + "](" + X + ", " + Y + ")";
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
