using System;

namespace TileProj
{
    public class SphericalMercator : IProjection
    {
        private const double EARTH_RADIUS = 6378137.0;

        public SphericalMercator() {}

        public IEnvelope CoordToEnvelope(ICoord coord)
        {
            double z = coord.Z;
            double x = coord.X;
            double y = coord.Y;
            if (!coord.TMS)
            {
                y = (Math.Pow(2, z) - 1) - y;
            }
            double c = 2.0 * Math.PI * EARTH_RADIUS;
            double n = Math.Pow(2, z);
            double res = c / n;
            double shift = c / 2.0;
            return new Envelope()
            {
                MinX = x * res - shift,
                MaxX = (x + 1) * res - shift,
                MinY = y * res - shift,
                MaxY = (y + 1) * res - shift
            };
        }

        public ICoordCollection GetChildren(ICoord coord)
        {
            return new CoordCollection()
            {
                SouthWest = new Coord()
                {
                    Z = coord.Z + 1,
                    X = coord.X << 1,
                    Y = coord.TMS ? coord.Y << 1 : (coord.Y << 1) + 1,
                    TMS = coord.TMS
                },
                NorthWest = new Coord()
                {
                    Z = coord.Z + 1,
                    X = coord.X << 1,
                    Y = coord.TMS ? (coord.Y << 1) + 1 : coord.Y << 1,
                    TMS = coord.TMS
                },
                SouthEast = new Coord()
                {
                    Z = coord.Z + 1,
                    X = (coord.X << 1) + 1,
                    Y = coord.TMS ? coord.Y << 1 : (coord.Y << 1) + 1,
                    TMS = coord.TMS
                },
                NorthEast = new Coord()
                {
                    Z = coord.Z + 1,
                    X = (coord.X << 1) + 1,
                    Y = coord.TMS ? (coord.Y << 1) + 1 : coord.Y << 1,
                    TMS = coord.TMS
                }
            };
        }

        public ICoord GetParent(ICoord coord)
        {
            double z = 0;
            int x = 0;
            int y = 0;
            bool tms = coord.TMS;
            if (coord.Z > 1)
            {
                z = coord.Z - 1;
                x = coord.X >> 1;
                y = coord.Y >> 1;
            }
            return new Coord()
            {
                Z = z,
                X = x,
                Y = y,
                TMS = tms
            };
        }

        public ICoord PointToCoord(IPoint point, double z)
        {
            double c = 2.0 * Math.PI * EARTH_RADIUS;
            double n = Math.Pow(2, z);
            double res = c / n;
            double shift = c / 2.0;

            // flip y value to match zxy scheme
            double x = point.X;
            double y = point.Y * -1;

            return new Coord()
            {
                Z = z,
                X = (int)Math.Floor((x + shift) / res),
                Y = (int)Math.Floor((y + shift) / res),
                TMS = false
            };
        }

        public IPoint LongLatToXY(IPoint point)
        {
            return new Point()
            {
                X = (Math.PI / 180) * point.X * EARTH_RADIUS,
                Y = EARTH_RADIUS * Math.Log(Math.Tan(Math.PI / 4.0 + point.Y * (Math.PI / 180) / 2))

            };
        }

        public IPoint XYtoLongLat(IPoint point)
        {
            return new Point()
            {
                X = 180 / Math.PI * (point.X / EARTH_RADIUS),
                Y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(point.Y / EARTH_RADIUS)) - Math.PI / 2)

            };
        }

        public IEnvelope LongLatToXY(IEnvelope envelope)
        {
            return new Envelope(LongLatToXY(envelope.LL), LongLatToXY(envelope.UR));
        }

        public IEnvelope XYtoLongLat(IEnvelope envelope)
        {
            return new Envelope(XYtoLongLat(envelope.LL), XYtoLongLat(envelope.UR));
        }
    }
}
