using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Context;
using NetTopologySuite.Geometries;

using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using GeoUK.Projections;

namespace RollOfHonour.DataImport.WW2;

internal class DataUpdateService : IDataUpdateService
{
    private readonly RollOfHonourContext _context;

    public DataUpdateService(RollOfHonourContext context)
    {
        _context = context;
    }

    public async Task UpdateGeographicTypeFromEastingNorthing()
    {
        // Create a geometry point

        var connStr = _context.Database.GetConnectionString();

        var memorials = await _context.WarMemorials.Where(m => m.Easting != 0 && m.Northing != 0).ToListAsync();
        // foreach (var memorial in memorials)
        // {
        //     //Convert from Easting Northing to LatLong Point/Geographic type
        //
        //     Cartesian cartesian = GeoUK.Convert.ToCartesian(new Airy1830(), new BritishNationalGrid(),
        //         new EastingNorthing(memorial.Easting, memorial.Northing));
        //
        //     Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian);
        //
        //     LatitudeLongitude wgsLatLng = GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);
        //
        //     // Geometry point = new Point(wgsLatLng.Latitude, wgsLatLng.Longitude)
        //     // {
        //     //     SRID = 8307
        //     //     //SRID = 27700
        //     // };
        //
        //     var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(8307);
        //
        //     Geometry point = gf.CreatePoint(new Coordinate(wgsLatLng.Latitude, wgsLatLng.Longitude));
        //
        //     //Set property
        //     memorial.Location = point;
        // }

        foreach (var memorial in memorials)
        {
            memorial.Location = ConvertEastingNorthingToLatLong(new EastingNorthing(memorial.Easting, memorial.Northing));


        }

        try
        {
            //Save
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Point ConvertEastingNorthingToLatLong(EastingNorthing eastingNorthing)
    {
        // 1. Convert to Cartesian
        // Given an easting and northing in metres (see text)
        // const double easting = 651409.903;
        // const double northing = 313177.270;

        // Convert to Cartesian
        Cartesian cartesian = GeoUK.Convert.ToCartesian(new Wgs84(),
            new BritishNationalGrid(), eastingNorthing);

        // 2. Transform from OSBB36 datum to ETRS89 datum
        Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian); //ETRS89 is effectively WGS84

        // 3. Convert back to Latitude/Longitude
        LatitudeLongitude wgsLatLong = GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);
        var coord = new Coordinate(wgsLatLong.Latitude, wgsLatLong.Longitude);
        var newLatLong = new Point(coord) { SRID = 4326 };


        // var seattle = new Point(-122.333056, 47.609722) { SRID = 4326 };
        return newLatLong;
    }
}
