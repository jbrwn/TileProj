
namespace TileProj
{
    public class CoordCollection : ICoordCollection
    {
        public CoordCollection() { }

        public CoordCollection(ICoord northEast, ICoord northWest, ICoord southEast, ICoord southWest)
        {
            this.NorthEast = northEast;
            this.NorthWest = northWest;
            this.SouthEast = southEast;
            this.SouthWest = SouthWest;
        }

        public ICoord NorthEast { get; set; }

        public ICoord NorthWest { get; set; }

        public ICoord SouthEast { get; set; }

        public ICoord SouthWest { get; set; }
    }
}
