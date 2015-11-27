
namespace TileProj
{
    public class Envelope : IEnvelope
    {
        public Envelope() { }

        public Envelope(double minx, double miny, double maxx, double maxy)
        {
            this.MinX = minx;
            this.MinY = miny;
            this.MaxX = maxx;
            this.MaxY = maxy;
        } 

        public Envelope(IPoint ll, IPoint ur)
        {
            this.MinX = ll.X;
            this.MinY = ll.Y;
            this.MaxX = ur.X;
            this.MaxY = ur.Y;
        }

        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public IPoint LL
        {
            get
            {
                return new Point(this.MinX, this.MinY);
            }
        }

        public IPoint UR
        {
            get
            {
                return new Point(this.MaxX, this.MaxY);
            }
        }

        public bool Intersects(IEnvelope other)
        {
            if (this.MinX <= other.MaxX &&
                this.MaxX >= other.MinX &&
                this.MinY <= other.MaxY &&
                this.MaxY >= other.MinY)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Envelope other = obj as Envelope;

            if (other != null &&
                this.MinX == other.MinX &&
                this.MaxX == other.MaxX &&
                this.MinY == other.MinY &&
                this.MaxY == other.MaxY)
            {
                return true;
            }
            return false;
        }
    }
}
