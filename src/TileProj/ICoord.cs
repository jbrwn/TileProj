
namespace TileProj
{
    public interface ICoord
    {
        int X { get; set; }
        int Y { get; set; }
        double Z { get; set; }

        bool TMS { get; set;  }
    }
}
