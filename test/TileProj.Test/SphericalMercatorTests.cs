using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TileProj.Test
{
    public class SphericalMercatorTests
    {
        [Fact]
        public void SphericalMercator_CoordToEnvelope()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(0, 0, 0);
            Envelope e = new Envelope(-20037508.342789244, -20037508.342789244, 20037508.342789244, 20037508.342789244);
            Assert.Equal(e, sm.CoordToEnvelope(c));

            Coord c1 = new Coord(1, 0, 1);
            Envelope e1 = new Envelope(-20037508.342789244, -20037508.342789244, 0, 0);
            Assert.Equal(e1, sm.CoordToEnvelope(c1));

            Coord c2 = new Coord(1, 0, 0, true);
            Envelope e2 = new Envelope(-20037508.342789244, -20037508.342789244, 0, 0);
            Assert.Equal(e1, sm.CoordToEnvelope(c1));
        }
        
        [Fact]
        public void SphericalMercator_GetChildren()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(9, 243, 166);
            ICoordCollection children = sm.GetChildren(c);
            Coord nw = new Coord(10, 486, 332);
            Coord sw = new Coord(10, 486, 333);
            Coord se = new Coord(10, 487, 333);
            Coord ne = new Coord(10, 487, 332);
            Assert.Equal(nw, children.NorthWest);
            Assert.Equal(sw, children.SouthWest);
            Assert.Equal(se, children.SouthEast);
            Assert.Equal(ne, children.NorthEast);
        }
        
        [Fact]
        public void SphericalMercator_GetChildren_TMS()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(9, 243, 166, true);
            ICoordCollection children = sm.GetChildren(c);
            Coord nw = new Coord(10, 486, 333, true);
            Coord sw = new Coord(10, 486, 332, true);
            Coord se = new Coord(10, 487, 332, true);
            Coord ne = new Coord(10, 487, 333, true);
            Assert.Equal(nw, children.NorthWest);
            Assert.Equal(sw, children.SouthWest);
            Assert.Equal(se, children.SouthEast);
            Assert.Equal(ne, children.NorthEast);
        }
        
        [Fact]
        public void SphericalMercator_GetParent_MinTile()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(0, 0, 0);
            Assert.Equal(c, sm.GetParent(c));
        }
        
        [Fact]
        public void SphericalMercator_GetParent()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(10, 486, 332);
            Coord e = new Coord(9, 243, 166);
            Assert.Equal(e, sm.GetParent(c));
        }
        
        [Fact]
        public void SphericalMercator_PointToCoord()
        {
            SphericalMercator sm = new SphericalMercator();
            Point p = new Point(-11131949, 4738603);
            Coord e = new Coord(12, 910, 1563);
            Assert.Equal(e, sm.PointToCoord(p, 12));
        }
        
        [Fact]
        public void SphericalMercator_PointToCoord_MaxExtent()
        {
            SphericalMercator sm = new SphericalMercator();
            Point p1 = new Point(-20037508.342789244, -20037508.342789244);
            Point p2 = new Point(20037508.342789244, 20037508.342789244);
            Coord e1 = new Coord(0, 0, 0);
            Assert.Equal(e1, sm.PointToCoord(p1, 0));
            Assert.Equal(e1, sm.PointToCoord(p2, 0));
            Coord e2 = new Coord(4, 0, 15);
            Assert.Equal(e2, sm.PointToCoord(p1, 4));
            Coord e3 = new Coord(4, 15, 0);
            Assert.Equal(e3, sm.PointToCoord(p2, 4));
        }
        
        [Fact]
        public void SphericalMercator_LongLatToXY()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-122.33517, 47.63752);
            Point merc = new Point(-13618288.8305, 6046761.54747);
            IPoint actual = sm.LongLatToXY(ll);
            Assert.Equal(merc.X, actual.X, 4);
            Assert.Equal(merc.Y, actual.Y, 4);
        }
        
        [Fact]
        public void SphericalMercator_LongLatToXY_MaxExtent()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-180, -90);
            Point merc = new Point(-20037508.342789244, -20037508.342789244);
            IPoint actual = sm.LongLatToXY(ll);
            Assert.Equal(merc.X, actual.X);
            Assert.Equal(merc.Y, actual.Y);
        }
        
        [Fact]
        public void SphericalMercator_XYToLongLat()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-122.33517, 47.63752);
            Point merc = new Point(-13618288.8305, 6046761.54747);
            IPoint actual = sm.XYtoLongLat(merc);
            Assert.Equal(ll.X, actual.X, 4);
            Assert.Equal(ll.Y, actual.Y, 4);
        }
    }
}
