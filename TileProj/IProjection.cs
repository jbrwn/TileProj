namespace TileProj
{
    public interface IProjection
    {
        IEnvelope CoordToEnvelope(ICoord coord);
        ICoord PointToCoord(IPoint point, double z);
        ICoord GetParent(ICoord coord);
        ICoordCollection GetChildren(ICoord coord);
    }
}
