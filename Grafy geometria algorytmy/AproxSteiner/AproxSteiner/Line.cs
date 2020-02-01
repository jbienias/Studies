namespace AproxSteiner
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
    }
}
