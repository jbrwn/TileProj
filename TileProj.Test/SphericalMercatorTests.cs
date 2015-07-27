using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TileProj.Test
{
    [TestClass]
    public class SphericalMercatorTests
    {
        [TestMethod]
        public void SphericalMercator_CoordToEnvelope()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(0, 0, 0);
            Envelope e = new Envelope(-20037508.342789244, -20037508.342789244, 20037508.342789244, 20037508.342789244);
            Assert.AreEqual(e, sm.CoordToEnvelope(c));

            Coord c1 = new Coord(1, 0, 1);
            Envelope e1 = new Envelope(-20037508.342789244, -20037508.342789244, 0, 0);
            Assert.AreEqual(e1, sm.CoordToEnvelope(c1));

            Coord c2 = new Coord(1, 0, 0, true);
            Envelope e2 = new Envelope(-20037508.342789244, -20037508.342789244, 0, 0);
            Assert.AreEqual(e1, sm.CoordToEnvelope(c1));
        }

        [TestMethod]
        public void SphericalMercator_GetChildren()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(9, 243, 166);
            ICoordCollection children = sm.GetChildren(c);
            Coord nw = new Coord(10, 486, 332);
            Coord sw = new Coord(10, 486, 333);
            Coord se = new Coord(10, 487, 333);
            Coord ne = new Coord(10, 487, 332);
            Assert.AreEqual(nw, children.NorthWest);
            Assert.AreEqual(sw, children.SouthWest);
            Assert.AreEqual(se, children.SouthEast);
            Assert.AreEqual(ne, children.NorthEast);
        }

        [TestMethod]
        public void SphericalMercator_GetChildren_TMS()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(9, 243, 166, true);
            ICoordCollection children = sm.GetChildren(c);
            Coord nw = new Coord(10, 486, 333, true);
            Coord sw = new Coord(10, 486, 332, true);
            Coord se = new Coord(10, 487, 332, true);
            Coord ne = new Coord(10, 487, 333, true);
            Assert.AreEqual(nw, children.NorthWest);
            Assert.AreEqual(sw, children.SouthWest);
            Assert.AreEqual(se, children.SouthEast);
            Assert.AreEqual(ne, children.NorthEast);
        }

        [TestMethod]
        public void SphericalMercator_GetParent_MinTile()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(0, 0, 0);
            Assert.AreEqual(c, sm.GetParent(c));
        }

        [TestMethod]
        public void SphericalMercator_GetParent()
        {
            SphericalMercator sm = new SphericalMercator();
            Coord c = new Coord(10, 486, 332);
            Coord e = new Coord(9, 243, 166);
            Assert.AreEqual(e, sm.GetParent(c));
        }

        [TestMethod]
        public void SphericalMercator_PointToCoord()
        {
            SphericalMercator sm = new SphericalMercator();
            Point p = new Point(-11131949, 4738603);
            Coord e = new Coord(12, 910, 1563);
            Assert.AreEqual(e, sm.PointToCoord(p, 12));
        }

        [TestMethod]
        public void SphericalMercator_PointToCoord_MaxExtent()
        {
            SphericalMercator sm = new SphericalMercator();
            Point p1 = new Point(-20037508.342789244, -20037508.342789244);
            Point p2 = new Point(20037508.342789244, 20037508.342789244);
            Coord e1 = new Coord(0, 0, 0);
            Assert.AreEqual(e1, sm.PointToCoord(p1, 0));
            Assert.AreEqual(e1, sm.PointToCoord(p2, 0));
            Coord e2 = new Coord(4, 0, 15);
            Assert.AreEqual(e2, sm.PointToCoord(p1, 4));
            Coord e3 = new Coord(4, 15, 0);
            Assert.AreEqual(e3, sm.PointToCoord(p2, 4));
        }

        [TestMethod]
        public void SphericalMercator_LongLatToXY()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-122.33517, 47.63752);
            Point merc = new Point(-13618288.8305, 6046761.54747);
            IPoint actual = sm.LongLatToXY(ll);
            Assert.AreEqual(merc.X, actual.X, .0001);
            Assert.AreEqual(merc.Y, actual.Y, .0001);
        }

        [TestMethod]
        public void SphericalMercator_LongLatToXY_MaxExtent()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-180, -90);
            Point merc = new Point(-20037508.342789244, -20037508.342789244);
            IPoint actual = sm.LongLatToXY(ll);
            Assert.AreEqual(merc.X, actual.X);
            Assert.AreEqual(merc.Y, actual.Y);
        }

        [TestMethod]
        public void SphericalMercator_XYToLongLat()
        {
            SphericalMercator sm = new SphericalMercator();
            Point ll = new Point(-122.33517, 47.63752);
            Point merc = new Point(-13618288.8305, 6046761.54747);
            IPoint actual = sm.XYtoLongLat(merc);
            Assert.AreEqual(ll.X, actual.X, .0001);
            Assert.AreEqual(ll.Y, actual.Y, .0001);
        }

    }
}
