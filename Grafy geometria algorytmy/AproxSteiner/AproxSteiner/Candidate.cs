using System;

namespace AproxSteiner
{
    public struct Candidate
    {
        public Point FirstParent { get; private set; }
        public Point SecondParent { get; private set; }
        public Point Point { get; private set; }

        public Candidate(Point firstParent, Point secondParent)
        {
            FirstParent = firstParent;
            SecondParent = secondParent;
            Point = new Point(Math.Min(FirstParent.X, SecondParent.X), Math.Min(FirstParent.Y, SecondParent.Y));
        }
    }
}
