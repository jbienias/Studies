namespace Tasks_2.Struct
{
    public struct Edge
    {
        //V -> U (pierwszy -> drugi)
        public int V { get; private set; }
        public int U { get; private set; }
        public int Weight { get; private set; }
        public int Capacity
        {
            get => Weight;
            private set { }
        }

        public Edge(int v, int u, int weight = 0)
        {
            V = v;
            U = u;
            Weight = weight;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;
            else
            {
                Edge e = (Edge)obj;
                return ((V == e.V) && (U == e.U)) || ((V == e.U) && (U == e.V));
            }
        }

        public static bool operator ==(Edge a, Edge b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Edge a, Edge b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (Weight == 0)
                return "(" + V + ") ----> (" + U + ")";
            return "(" + V + ") --{w = " + Weight + "}--> (" + U + ")";
        }
    }
}
