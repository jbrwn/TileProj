
namespace TileProj
{
    public class Coord : ICoord
    {
        public Coord() { }
        
        public Coord(double z, int x, int y, bool tms = false)
        {
            this.Z = z;
            this.X = x;
            this.Y = y;
            this.TMS = tms;
        }

        public double Z { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool TMS { get; set; }

        public override bool Equals(object obj)
        {
            Coord other = obj as Coord;

            if (other != null &&
                this.Z == other.Z &&
                this.X== other.X &&
                this.Y == other.Y &&
                this.TMS == other.TMS)
            {
                return true;
            }
            return false;
        }
    }
}
