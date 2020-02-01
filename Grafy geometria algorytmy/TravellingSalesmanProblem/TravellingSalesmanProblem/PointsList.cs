using System;
using System.Collections;
using System.Collections.Generic;

namespace TravellingSalesmanProblem
{
    public class PointsList : IEnumerable, IComparable
    {
        public List<Point> Points { get; private set; }

        public PointsList()
        {
            Points = new List<Point>();
        }

        public PointsList(List<Point> points)
        {
            Points = points;
        }

        public Point this[int i]
        {
            get { return Points[i]; }
            set { Points[i] = value; }
        }

        public int Count
        {
            get => Points.Count;
        }

        public static PointsList Copy(PointsList pl)
        {
            var result = new PointsList();
            foreach (var point in pl.Points)
                result.Points.Add(point);
            return result;
        }

        public static bool operator ==(PointsList a, PointsList b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(PointsList a, PointsList b)
        {
            return !(a.Equals(b));
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PointsList l = (PointsList)obj;
                if (l.Points.Count != Points.Count)
                    return false;
                for (int i = 0; i < Points.Count; i++)
                    if (!l[i].Equals(this[i]))
                        return false;
                return true;
            }
        }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (var p in Points)
                result += p.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return Points.GetEnumerator();
        }

        public int CompareTo(object obj)
        {
            PointsList l = (PointsList)obj;
            if (this.Count > l.Count)
                return 1;
            if (this.Count == l.Count)
                for (int i = 0; i < Count; i++)
                {
                    return this[i].Id.CompareTo(l[i].Id);
                }
            return -1;
        }
    }
}
