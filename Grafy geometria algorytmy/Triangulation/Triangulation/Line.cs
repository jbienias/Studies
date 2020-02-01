namespace Triangulation
{
    public struct Line
    {
        public Point A { get; set; }
        public Point B { get; set; }

        public Line(Point a, Point b)
        {
            A = a;
            B = b;
        }

        public override string ToString()
        {
            return "{ " + A + " --> " + B + " }";
        }

        public string ToString(bool simple)
        {
            if (simple) return "{ " + A.ToString(simple) + " --> " + B.ToString(simple) + " }";
            else return ToString();
        }
    }
}
