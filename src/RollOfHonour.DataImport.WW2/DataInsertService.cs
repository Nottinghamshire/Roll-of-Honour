using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Models.DB;

namespace RollOfHonour.DataImport.WW2;

internal class DataInsertService : IDataInsertService
{
    private readonly RollOfHonourContext _context;
    private List<WW2Data> ErrorRows;

    public DataInsertService(RollOfHonourContext context)
    {
        _context = context;
        ErrorRows = new List<WW2Data>();
    }

    public async Task<List<WW2Data>> AddMissingData(List<WW2Data> results)
    {
        await AddMissingRegiments(results);
        await AddMissingSubUnits(results);
        await AddMissingMemorials(results);
        await AddPeopleAndRecordedNames(results);
        await UpdateNamesCountOnMemorials();
        return ErrorRows;
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
                new Tuple<string, string?>(x.Regiment!, x.Sub_Unit));

        IIncludableQueryable<Regiment, ICollection<SubUnit>> existingRegiments =
            _context.Regiments.Include(r => r.SubUnits);


        foreach (var su in newSubUnitsForRegiments
                 //.Where(x => !string.IsNullOrWhiteSpace(x.Item2))
                )
        {
            // Get SU's for Regiment
            Regiment? regiment = existingRegiments.FirstOrDefault(r => r.Name == su.Item1);

            //Does the Sub Unit already exist in the Regiment?
            var existingSu = regiment?.SubUnits.FirstOrDefault(s => s.Name == su.Item2);
            if (existingSu == null)
            {
                regiment?.SubUnits.Add(new SubUnit { Name = su.Item2 });
                await _context.SaveChangesAsync();
                existingRegiments = _context.Regiments.Include(r => r.SubUnits);
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
                    Easting = 458060,
                    Northing = 338044,
                    MainPhotoId = null,
                    NamesCount = -1,
                    District = null,
                    Postcode = null
                });

                await _context.SaveChangesAsync();
            }
        }
    }

    private async Task AddPeopleAndRecordedNames(List<WW2Data> results)
    {
        // Get Lists
        var memorials = _context.WarMemorials.AsNoTracking().ToHashSet();
        var regimentsWithSubUnits = _context.Regiments.Include(r => r.SubUnits).AsNoTracking().ToHashSet();

        /* Person, RecName, Link to Mem, Link to SubUnit */
        foreach (var row in results)
        {
            Person? person = await FindExistingPersonMatch(row);
            if (person == null)
            {
                ErrorRows.Add(row);
            }
            WarMemorial? memorial = FindMemorial(memorials, row);
            SubUnit? subUnit = FindSubUnit(regimentsWithSubUnits, row);
            if (person == null)
            {
                person = await AddPerson(row, subUnit?.Id, memorial);
            }
        }
    }

    private async Task<Person?> FindExistingPersonMatch(WW2Data row)
    {
         Person? existingPerson = null;
    
        // CWGC Grave Ref and Service ID
        if (existingPerson == null && !string.IsNullOrEmpty(row.MaybeCWGCRef) &&!string.IsNullOrEmpty(row.Service_Number))
        {
            existingPerson = await _context.People.FirstOrDefaultAsync(p => p.Cwgc.ToString() == row.MaybeCWGCRef && p.ServiceNumber == row.Service_Number);
        }

        //Name with Date of Death
        if (existingPerson == null &&
            !string.IsNullOrEmpty(row.Last_Name) && !string.IsNullOrEmpty(row.FirstName) &&
            !string.IsNullOrEmpty(row.Initials))
        {
            existingPerson = await _context.People.FirstOrDefaultAsync(p =>
                p.LastName == row.Last_Name &&
                p.FirstNames == row.FirstName &&
                p.Initials == row.Initials &&
                p.DateOfDeath == row.Date_of_Death
            );
        }

        return existingPerson;
    }


    private WarMemorial? FindMemorial(HashSet<WarMemorial> memorials, WW2Data row)
    {
        return memorials.FirstOrDefault(m =>
            m.Name == row.Memorial_Name &&
            m.Description == row.Memorial_Location_Description
        ) ?? null;
    }

    private SubUnit? FindSubUnit(HashSet<Regiment> regimentsWithSubUnits, WW2Data row)
    {
        SubUnit? existingSU = null;
        if (row.Regiment != null)
        {
            var regiment = regimentsWithSubUnits.FirstOrDefault(r => r.Name == row.Regiment);
            existingSU = regiment?.SubUnits.FirstOrDefault(su => su.Name == row.Sub_Unit);
        }

        return existingSU;
    }

    private async Task<Person?> AddPerson(WW2Data row, int? subUnitId, WarMemorial? warMemorial)
    {
        var sbAsRec = new StringBuilder();
        if (!string.IsNullOrEmpty(row.FirstName))
            sbAsRec.Append(row.FirstName);

        if (!string.IsNullOrEmpty(row.Initials))
            sbAsRec.AppendFormat($" {row.Initials}");

        if (!string.IsNullOrEmpty(row.Last_Name))
            sbAsRec.AppendFormat($" {row.Last_Name}");

        if (!string.IsNullOrEmpty(row.Rank))
            sbAsRec.AppendFormat($" {row.Rank}");

        if (warMemorial == null)
        {
            return null;
        }

        var recordedName = new RecordedName
        {
            AsRecorded = sbAsRec.ToString(),
            FirstName = row.FirstName,
            Initials = row.Initials,
            LastName = row.Last_Name,
            Rank = row.Rank,
            Sex = null,
            ServiceNumber = row.Service_Number,
            WarMemorialId = warMemorial.Id,
            WarId = 2,
            SubUnitId = subUnitId,
            ArmedServiceId = null
        };

        if (row.Date_of_Death == null)
        {
        }

        var person = new Person
        {
            WarId = 2,
            ServiceNumber = row.Service_Number,
            FirstNames = row.FirstName,
            LastName = row.Last_Name,
            Initials = row.Initials,
            AgeAtDeath = row.Age_at_Death == null ? null : int.Parse(row.Age_at_Death),
            DateOfDeath = row.Date_of_Death,
            Rank = row.Rank,
            SubUnitId = subUnitId,
            FamilyHistory = row.FamilyInfo,
            Cwgc = row.MaybeCWGCRef == null ? null : int.Parse(row.MaybeCWGCRef),
            Comments = row.OtherNotes,
            DateOfBirth = null,
            MainPhotoId = null,
            ArmedServiceId = null,
            Deleted = false,
            AddressAtEnlistment = null,
            PlaceOfBirth = null,
            EmploymentHobbies = null,
            MilitaryHistory = null,
            ExtraInfo = null,
            RecordedNames = { recordedName }
        };

        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();

        return person;
    }

    private async Task UpdateNamesCountOnMemorials()
    {
        // Where person not deleted!
        var memorialsToUpdate =
            await _context.WarMemorials
                .Include(m => m.RecordedNames).ThenInclude(rn => rn.Person)
                .Where(m => m.NamesCount < 1)
                .ToListAsync();

        foreach (var memorial in memorialsToUpdate)
        {
            int nameCount = memorial.RecordedNames.Count(rn => rn.Person!.Deleted == false);
            memorial.NamesCount = nameCount;
        }

        await _context.SaveChangesAsync();
    }
}
