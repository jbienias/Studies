// Jan Bienias 238201

namespace SimplePolygonKernel
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        //= 0 współliniowe -> zwraca 0
        //> 0 orientacja dodatnia (lewo) -> zwraca 1
        //< 0 orientacja ujemna (prawo) -> zwraca 2
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
