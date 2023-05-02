using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using RollOfHonour.Data.Context;
using Dapper;

namespace RollOfHonour.DataImport.WW2;

public interface IWW2ImportRepository
{
    List<WW2Data> ProperWW2Data();
    List<WW2Data> CivilianWarDeadWW2Data();
}

public class WW2ImportRepository : IWW2ImportRepository
{
    private readonly RollOfHonourContext _context;

    public WW2ImportRepository(RollOfHonourContext context)
    {
        _context = context;
    }


    public List<WW2Data> ProperWW2Data()
    {
        var connStr = _context.Database.GetConnectionString();
        List<WW2Data> ww2Data = new List<WW2Data>();
        // using IDbConnection db = new SqlConnection(@"data source=db22-0029\DigitalTeamDev;Initial Catalog=RollOfHonour_WW2;Persist Security Info=True;Trusted_Connection=True;TrustServerCertificate=Yes;");
        using IDbConnection db = new SqlConnection(_context.Database.GetConnectionString());
        ww2Data = db.Query<WW2Data>(
            $"SELECT [FirstName] ,[Initials] ,[Last_Name] ,[Age_at_Death] ,[Date_of_Death] ,[Rank] ,[Regiment] ,[Sub_Unit] ,[Memorial_Location_Description] ,[Service_Number] ,[Memorial_Name] ,[DescriptiveLocation] ,[FamilyInfo] ,[MaybeCWGCRef] ,[OtherNotes] FROM [WW2Data] WHERE Regiment != 'Civilian War Dead'"
        ).ToList();

        return ww2Data;

    }

    public List<WW2Data> CivilianWarDeadWW2Data()
    {
        var connStr = _context.Database.GetConnectionString();
        List<WW2Data> ww2Data = new List<WW2Data>();
        // using IDbConnection db = new SqlConnection(@"data source=db22-0029\DigitalTeamDev;Initial Catalog=RollOfHonour_WW2;Persist Security Info=True;Trusted_Connection=True;TrustServerCertificate=Yes;");
        using IDbConnection db = new SqlConnection(_context.Database.GetConnectionString());
        ww2Data = db.Query<WW2Data>(
            $"SELECT [FirstName] ,[Initials] ,[Last_Name] ,[Age_at_Death] ,[Date_of_Death] ,[Rank] ,[Regiment] ,[Sub_Unit] ,[Memorial_Location_Description] ,[Service_Number] ,[Memorial_Name] ,[DescriptiveLocation] ,[FamilyInfo] ,[MaybeCWGCRef] ,[OtherNotes] FROM [WW2Data] WHERE Regiment = 'Civilian War Dead'"
        ).ToList();

        return ww2Data;
    }
}
