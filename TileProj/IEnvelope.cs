
namespace TileProj
{
    public interface IEnvelope
    {
        double MinX { get; set; }
        double MinY { get; set; }
        double MaxX { get; set; }
        double MaxY { get; set; }
        IPoint LL { get; }
        IPoint UR { get; }
        bool Intersects(IEnvelope other);
    }
}
