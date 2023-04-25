using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Models.DB;

namespace RollOfHonour.DataImport.WW2;

interface IDataInsertService
{
    Task AddMissingData(List<WW2Data> results);
    // void AddMissingRegiments(List<WW2Data> allRows);
    // void AddMissingSubUnits(List<WW2Data> allRows);
    // Task AddMissingMemorials(List<WW2Data> allRows);
}

internal class DataInsertService : IDataInsertService
{
    private readonly RollOfHonourContext _context;

    public DataInsertService(RollOfHonourContext context)
    {
        _context = context;
    }

    public async Task AddMissingData(List<WW2Data> results)
    {
        await AddMissingRegiments(results);
        await AddMissingSubUnits(results);
        await AddMissingMemorials(results);
        //Add RecNames/People
        //TODO: await UpdateNamesCountOnMemorials
    }

    public async Task AddMissingRegiments(List<WW2Data> allRows)
    {
        HashSet<string> existingRegiments = _context.Regiments.Select(r => r.Name!).ToHashSet();
        HashSet<string> newRegiments = allRows.Select(r => r.Regiment!)
            .Distinct(StringComparer.CurrentCultureIgnoreCase).ToHashSet();
        List<string> missingRegiments = newRegiments.Where(n => !existingRegiments.Contains(n)).ToList();

        //Create new regiments
        var regimentsToCreate = missingRegiments.Select(x => new Regiment { Name = x }).ToList();

        await _context.Regiments.AddRangeAsync(regimentsToCreate);
        await _context.SaveChangesAsync();
    }

    public async Task AddMissingSubUnits(List<WW2Data> allRows)
    {
        var newSubUnitsForRegiments =
            allRows.Select(x =>
                new Tuple<string, string>(x.Regiment!, x.Sub_Unit!));

        foreach (var su in newSubUnitsForRegiments.Where(x => !string.IsNullOrWhiteSpace(x.Item2)))
        {
            // Get SU's for Regiment
            var existingRegiments = _context.Regiments.Include(r => r.SubUnits);
            Regiment? regiment = existingRegiments.FirstOrDefault(r => r.Name == su.Item1);

            //Does the Sub Unit already exist in the Regiment?
            var existingSu = regiment?.SubUnits.FirstOrDefault(s => s.Name == su.Item2);
            if (existingSu == null)
            {
                regiment?.SubUnits.Add(new SubUnit { Name = su.Item2 });
                await _context.SaveChangesAsync();
            }

        }
        //HACK: Could be made faster, but simplicity/delivery is the priority.
    }

    public async Task AddMissingMemorials(List<WW2Data> allRows)
    {
        var newWarMemorials = allRows
            .Select(r => new { Name = r.Memorial_Name, MemorialLocation = r.Memorial_Location_Description })
            .ToHashSet();

        foreach (var newWarMemorial in newWarMemorials)
        {
            // Does the Memorial already exist?
            var existingMemorial = _context.WarMemorials.FirstOrDefault(em => em.Name == newWarMemorial.Name);
            if (existingMemorial == null && !string.IsNullOrEmpty(newWarMemorial.Name))
            {
                _context.WarMemorials.Add(new WarMemorial
                {
                    Ukniwmref = null,
                    Name = newWarMemorial.Name!, 
                    Description = newWarMemorial.MemorialLocation,
                    Easting = 458060, Northing = 338044,
                    MainPhotoId = null,
                    NamesCount = -1,
                    District = null,
                    Postcode = null
                    
                });

                await _context.SaveChangesAsync();
            }
        }

    }

    private Task AddPersonRowAsync(WW2Data row)
    {
        throw new NotImplementedException();
        //Insert Person and Recorded Name
    }

    private async Task<PropertyValues?> AddAndSaveSubUnit(string? rowRegiment, string? rowSubUnit)
    {
        // Check we a new have SubUnit Name
        if (!rowSubUnit.IsNullOrEmpty())
        {
            var newSubUnit = new SubUnit { Name = rowSubUnit };
            await _context.SubUnits.AddAsync(newSubUnit);
            await _context.SaveChangesAsync();
            return await _context.Entry(newSubUnit).GetDatabaseValuesAsync();
        }

        return null;
    }


    // private Task<PropertyValues?> AddAndSaveRegimentWithSubUnit(string? rowRegiment, string? rowSubUnit)
    // {
    //     if (rowSubUnit.IsNullOrEmpty())
    //     {
    //         //Only add the regiment
    //         var newRegiment = new Regiment { Name = rowRegiment };
    //         _context.Regiments.AddAsync(newRegiment);
    //     }
    //     else
    //     {
    //         //Add the SubUnit too
    //         var newSubUnit = new SubUnit { Name = rowSubUnit, Regiment = new Regiment { Name = rowRegiment } };
    //         _context.SubUnits.AddAsync(newSubUnit);
    //     }
    //
    //     _context.SaveChangesAsync();
    //     return _context.Entry(newSubUnit).GetDatabaseValuesAsync();
    // }
}
