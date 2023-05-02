using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Context;
using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using NetTopologySuite.Geometries;
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
        var connStr = _context.Database.GetConnectionString();


        var memorials = await _context.WarMemorials.Where(m => m.Easting != 0).ToListAsync();
        foreach (var memorial in memorials)
        {
            //Convert from Easting Northing to LatLong Point/Geographic type

            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Airy1830(), new BritishNationalGrid(),
                new EastingNorthing(memorial.Easting, memorial.Northing));

            Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian);

            LatitudeLongitude wgsLatLng = GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);

            // Geometry point = new Point(wgsLatLng.Latitude, wgsLatLng.Longitude)
            // {
            //     SRID = 8307
            //     //SRID = 27700
            // };

            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(8307);

            Geometry point = gf.CreatePoint(new Coordinate(wgsLatLng.Latitude, wgsLatLng.Longitude));

            //Set property
            memorial.Location = point;
        }

        //Save
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
