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
        var connStr = _context.Database.GetConnectionString();

        var memorials = await _context.WarMemorials.Where(m => m.Easting != 0 && m.Northing != 0).ToListAsync();


        foreach (var memorial in memorials)
        {
            // Create a geometry point
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
        // Convert to Cartesian
        Cartesian cartesian = GeoUK.Convert.ToCartesian(new Wgs84(),
            new BritishNationalGrid(), eastingNorthing);

        // 2. Transform from OSBB36 datum to ETRS89 datum
        Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian); //ETRS89 is effectively WGS84

        // 3. Convert back to Latitude/Longitude
        LatitudeLongitude wgsLatLong = GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);
        var coord = new Coordinate(wgsLatLong.Latitude, wgsLatLong.Longitude);
        var newLatLong = new Point(coord) { SRID = 4326 };

        return newLatLong;
    }
}
