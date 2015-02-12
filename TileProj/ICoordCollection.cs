
namespace TileProj
{
    public interface ICoordCollection
    {
        ICoord SouthWest { get; set; }
        ICoord NorthWest { get; set; }
        ICoord NorthEast { get; set; }
        ICoord SouthEast { get; set; }
    }
}
