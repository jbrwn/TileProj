# TileProj
A .NET projection API for map tiles

[![Build status](https://ci.appveyor.com/api/projects/status/ybp80fv404dyorq5?svg=true)](https://ci.appveyor.com/project/jbrwn/tileproj)

## Installation
``` PM> Install-Package TileProj``` 
## Getting Started
TileProj comes out of the box with a [Spherical Mercator](http://wiki.openstreetmap.org/wiki/Mercator#Spherical_Mercator) projection.

Check out the [tests](https://github.com/jbrwn/TileProj/blob/master/TileProj.Test/SphericalMercatorTests.cs) for example usage

##API
Projections implment the IProjection interface:
public interface IProjection
```c#
    public interface IProjection
    {
        /// This method takes a zxy tile coordinate parameter and 
        /// returns a bbox envelope in the native units of the projection
        IEnvelope CoordToEnvelope(ICoord coord);
        
        /// This method takes a point and z-index parameter and returns
        /// the zxy tile coordinate
        ICoord PointToCoord(IPoint point, double z);
        
        /// This method takes a zxy tile coordinate paramater and
        /// returns its parent zxy tile coordinate
        ICoord GetParent(ICoord coord);
        
        /// This method take a zxy tile coordinate parameter and
        /// returns a collection of its child zxy tile coordinates
        ICoordCollection GetChildren(ICoord coord);
    }
```
