namespace KDimensionalTree
{
    public struct Area
    {
        public double Left { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Top { get; set; }

        public Area(double left, double right, double bottom, double top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }

        public Area(Area area)
        {
            Left = area.Left;
            Right = area.Right;
            Bottom = area.Bottom;
            Top = area.Top;
        }

        public bool PointBelongs(Point point)
        {
            return Left <= point.X & point.X <= Right && Bottom <= point.Y && point.Y <= Top;
        }

        public bool Contains(Area B) //is B c this?
        {
            return this.Left <= B.Left &&
                B.Right <= this.Right &&
                this.Bottom <= B.Bottom &&
                B.Top <= this.Top;

        }

        public bool Intersects(Area area)
        {
            return (area.Left < this.Left && this.Left <= area.Right
                || area.Left < this.Right && this.Right <= area.Right
                || this.Left <= area.Left && area.Right <= this.Right)
                && (area.Bottom < this.Bottom && this.Bottom <= area.Top
                || area.Bottom < this.Top && this.Top <= area.Top
                || this.Bottom <= area.Bottom && area.Top <= this.Top);
        }

        public override string ToString()
        {
            return "V(x.left = " + Left + ", x.right = " + Right + ", y.bottom = " + Bottom + ", y.top = " + Top + ")";
        }
    }
}
