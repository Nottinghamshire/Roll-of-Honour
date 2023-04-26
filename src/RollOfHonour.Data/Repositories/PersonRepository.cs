using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Search;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private string settingBlobName = "ncc01sarollhonstdlrsdev";
    private string settingBlobImageContainerName = "images";

    private RollOfHonourContext _dbContext { get; set; }

    public PersonRepository(RollOfHonourContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Person?> GetById(int id)
    {
        try
        {
            var dbPerson = await _dbContext.People
              .Include(p => p.Photos)
              .Include(p => p.Decorations)
              .Include(p => p.RecordedNames).ThenInclude(rn => rn.WarMemorial)
              .Include(p => p.SubUnit).ThenInclude(unit => unit!.Regiment)
              .FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                // TODO: Is this necessary?
                return null;
            }
            return dbPerson.ToDomainModel(settingBlobName, settingBlobImageContainerName);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Person>> DiedOnThisDay(DateTime date)
    {
        var diedToday = _dbContext.People.Where(p => p.DateOfDeath != null && (p.DateOfDeath == date));
        var diedTodayCount = diedToday.Count();
        var dbPeople = new List<Models.DB.Person>();

        if (diedTodayCount < 4)
        {
            var countOfPeople = diedTodayCount;
            var totalIds = Count() - 1;
            var random = new Random((int)date.Ticks);

            while (countOfPeople < 4)
            {
                var randomId = random.Next(0, totalIds);
                var person = await _dbContext.People
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(p => p.Id == randomId);
                if (person is not null)
                {
                    dbPeople.Add(person);
                    countOfPeople++;
                }
            }
        }

        dbPeople.AddRange(diedToday.ToList());
        IEnumerable<Person> people = dbPeople.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
        return people;
    }

    public async Task<PaginatedList<Person>> SearchPeople(PersonQuery query, Filters filters, int pageIndex, int pageSize)
    {
        var dbPeople = GetPeopleByName(query);

        if (filters.IsFiltered)
        {
            dbPeople = FilterPeople(dbPeople, filters);
        }

        var resultCount = dbPeople.Count();
        if (resultCount == 0)
        {
            return new PaginatedList<Person>();
        }

        dbPeople = dbPeople
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).Distinct()
            .OrderBy(p => p.LastName)
            .AsNoTracking();

        var results = await dbPeople.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName)).ToListAsync();
        return new PaginatedList<Person>(results, resultCount, pageIndex, pageSize);
    }

    public async Task<List<RegimentFilter>> GetRegimentFiltersForSearch(PersonQuery query)
    {
        var dbPeople = GetPeopleByName(query);
        var regiments = await dbPeople
            .Where(p => p.SubUnit != null && p.SubUnit.RegimentId.HasValue && p.SubUnit.Regiment != null && !string.IsNullOrEmpty(p.SubUnit.Regiment.Name))
            .Select(p => new RegimentFilter((int)p.SubUnit!.RegimentId!, p.SubUnit!.Regiment!.Name!))
            .AsNoTracking()
            .Distinct()
            .ToListAsync();

        return regiments;
    }

    public async Task<PaginatedList<Person>> GetPageOfPeople(int pageIndex, int pageSize)
    {
        var dbPeople = await _dbContext.People
            .Include(p => p.Photos)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        if (!dbPeople.Any())
        {
            return new PaginatedList<Person>();
        }

        return new PaginatedList<Person>(dbPeople.Select(p =>
            p.ToDomainModel(settingBlobName, settingBlobImageContainerName)).ToList(), _dbContext.People.Count(), pageIndex, pageSize);
    }

    public int Count()
    {
        return _dbContext.People.Count();
    }

    private IQueryable<Models.DB.Person> FilterPeople(IQueryable<Models.DB.Person> people, Filters filters)
    {
        if (filters.DateRangeUsed)
        {
            people = DiedBefore(people, filters.DiedBefore);
            people = BornAfter(people, filters.BornAfter);
        }

        if (filters.HasRegiments)
        {
            people = ByRegiment(people, filters.Regiments);
        }

        return people;
    }

    private IQueryable<Models.DB.Person> ByRegiment(IQueryable<Models.DB.Person> people, HashSet<int> regimentIds)
    {
        return people.Where(p => p.SubUnit != null && p.SubUnit.RegimentId.HasValue && regimentIds.Contains((int)p.SubUnit.RegimentId));
    }

    private IQueryable<Models.DB.Person> DiedBefore(IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfDeath <= date);
    }

    private IQueryable<Models.DB.Person> BornAfter(IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfBirth >= date);
    }

    private IQueryable<Models.DB.Person> GetPeopleByName(PersonQuery query)
    {
        var dbPeople = _dbContext.People
              .Include(p => p.Photos)
              .Include(p => p.Decorations)
              .Include(p => p.RecordedNames)
              .ThenInclude(rn => rn.WarMemorial)
              .Include(p => p.SubUnit)
              .ThenInclude(unit => unit!.Regiment)
              .Where(p => p.Deleted == false && (p.FirstNames!.Contains(query.SearchTerm)
                || p.LastName!.Contains(query.SearchTerm)));

        return dbPeople;
    }
}
