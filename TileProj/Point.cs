namespace TileProj
{
    public class Point : IPoint
    {
        public Point() { }
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            Point other = obj as Point;

            if (other != null &&
                this.X == other.X &&
                this.Y == other.Y)
            {
                return true;
            }
            return false;
        }
    }
}
