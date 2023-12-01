using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RollOfHonour.Core;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;
using Person = RollOfHonour.Core.Models.Person;

namespace RollOfHonour.Data.Repositories;

public class MemorialRepository : IMemorialRepository
{
    private readonly Storage _storage;
    public RollOfHonourContext _dbContext { get; set; }

    public MemorialRepository(RollOfHonourContext dbContext, IOptions<Storage> storageSettings)
    {
        _dbContext = dbContext;
        _storage = storageSettings.Value;
    }

    public async Task<Memorial?> GetById(int id)
    {
        try
        {
            var dbMemorial = await _dbContext.WarMemorials
                .Include(m => m.RecordedNames)
                .Include(m => m.Photos)
                .FirstAsync(p => p.Id == id);

            return dbMemorial.ToDomainModel(_storage.ImageUrlPrefix);
        }
        catch (InvalidOperationException)
        {
            // TODO: Understand if this is even possible 
            return null;
        }
    }

    public async Task<PaginatedList<Memorial>> SearchMemorials(string searchString, int pageIndex, int pageSize)
    {
        var dbMemorials = _dbContext.WarMemorials
            .Include(m => m.Photos)
            .Where(m =>
                m.Name.Contains(searchString));

        var resultCount = dbMemorials.Count();

        if (resultCount == 0)
        {
            return new PaginatedList<Memorial>();
        }

        dbMemorials = dbMemorials.Skip(
                (pageIndex - 1) * pageSize)
            .Take(pageSize);

        var results = await dbMemorials
            .Select(m => m.ToDomainModel(_storage.ImageUrlPrefix))
            .ToListAsync();

        return new PaginatedList<Memorial>(results, resultCount, pageIndex, pageSize);
    }

    public int Count()
    {
        var count = _dbContext.WarMemorials.Count();
        return count;
    }

    public async Task<PaginatedList<Memorial>> GetPageOfMemorials(int pageIndex, int pageSize)
    {
        var dbMemorials = await _dbContext.WarMemorials
            .Include(m => m.Photos)
            .Skip(
                (pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        if (!dbMemorials.Any())
        {
            return new PaginatedList<Memorial>();
        }

        return new PaginatedList<Memorial>(
            dbMemorials.Select(m => m.ToDomainModel(_storage.ImageUrlPrefix))
                .ToList(), _dbContext.WarMemorials.Count(), pageIndex, pageSize);
    }

    public async Task RemovePerson(int memorialId, int citizenId)
    {
        var dbMemorial = await _dbContext.WarMemorials
            .Include(m => m.RecordedNames)
            .Include(m => m.Photos)
            .FirstAsync(p => p.Id == memorialId);

        var citizenRecordedName = await _dbContext.RecordedNames.FirstAsync(_ => _.PersonId == citizenId);

        dbMemorial.RecordedNames.Remove(citizenRecordedName);
        dbMemorial.NamesCount = dbMemorial.RecordedNames.Count;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AddPerson(int memorialId, Person person)
    {
        var dbMemorial = await _dbContext.WarMemorials
            .Include(m => m.RecordedNames)
            .Include(m => m.Photos)
            .FirstAsync(p => p.Id == memorialId);

        try
        {
            //create basic person then add to memorial, should change to use auto mapper instead
            var newPersonRecord = new Models.DB.Person
            {
                FirstNames = person.FirstNames,
                LastName = person.LastName,
                Initials = person.Initials,
                ServiceNumber = person.ServiceNumber,
                Rank = person.Rank,
                FullName = $"{person.FirstNames} {person.LastName}",
                AddressAtEnlistment = person.AddressAtEnlistment,
                EmploymentHobbies = person.EmploymentHobbies,
                ExtraInfo = person.ExtraInfo,
                FamilyHistory = person.FamilyHistory,
                PlaceOfBirth = person.PlaceOfBirth,
                DateOfBirth = person.DateOfBirth,
                DateOfDeath = person.DateOfDeath,
                MilitaryHistory = person.MilitaryHistory,
            };

            // need to create new subunit if doesnt exist
            var subUnit = await _dbContext.SubUnits.SingleOrDefaultAsync(_ => _.Name != null && _.Name.ToLower() == person.Unit);
            if (subUnit is not null)
                newPersonRecord.SubUnit = subUnit;
            else
            {
                    var newSubUnitRecord = new Models.DB.SubUnit
                {
                    Name = person.Unit, 
                    Regiment = new Models.DB.Regiment { Name = person.Regiment }
                };
                _dbContext.SubUnits.Add(newSubUnitRecord);
                await _dbContext.SaveChangesAsync();
                
                newPersonRecord.SubUnit = newSubUnitRecord;
            }

            var personEntity = _dbContext.People.Add(newPersonRecord).Entity;
            await _dbContext.SaveChangesAsync();

            var newRecord = new Models.DB.RecordedName
            {
                PersonId = personEntity.Id,
                Person = personEntity,
                FirstName = personEntity.FirstNames,
                LastName = personEntity.LastName,
                Initials = personEntity.Initials,
                Rank = personEntity.Rank,
                WarMemorialId = memorialId,
                WarMemorial = dbMemorial,
                AsRecorded = $"{personEntity.FirstNames} {personEntity.LastName} {personEntity.Rank}",
                ServiceNumber = personEntity.ServiceNumber,
                SubUnit = personEntity.SubUnit
            };

            dbMemorial.RecordedNames.Add(newRecord);
            dbMemorial.NamesCount = dbMemorial.RecordedNames.Count;

            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }
}
