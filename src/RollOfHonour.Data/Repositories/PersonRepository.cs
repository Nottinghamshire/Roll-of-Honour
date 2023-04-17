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
        .Include(p => p.SubUnit).ThenInclude(unit => unit.Regiment)
        .FirstOrDefaultAsync(p => p.Id == id);

        if (dbPerson is null)
            {
                // TODO: Is this necessary?
                return null;
            }
      return dbPerson.ToDomainModel(settingBlobName, settingBlobImageContainerName);
    }
    catch (InvalidOperationException ex)
    {
      return null;
    }
  }

  // TODO: Should this return a null rather than an empty enumerable?
  public async Task<IEnumerable<Person>> GetAll()
  {
    try
    {
      var people = await _dbContext.People
        .Include(p => p.Photos)
        .Take(25)
        .ToListAsync();

      return people.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
    }
    catch (Exception)
    {
        return Enumerable.Empty<Person>();
    }
  }

    public async Task<IEnumerable<Person>> DiedOnThisDay(DateTime date)
    {
        var countOfPeople = _dbContext.People.Count();
        var random = new Random((int)date.Ticks);

        var dbPeople = new List<Models.DB.Person>();

        for (var i = 0; i <= 2; i++)
        {
            var randomId = random.Next(0, countOfPeople - 1);
            var person = await _dbContext.People.FirstOrDefaultAsync(p => p.Id == randomId);
            if (person == null)
            {
                --i;
            }
            else
            {
                dbPeople.Add(person);
            }
        }

        IEnumerable<Person> people = dbPeople.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
        return people;
    }

    public async Task<PaginatedList<Core.Models.Person>> SearchPeople(PersonQuery query, Filters filters, int pageIndex, int pageSize)
    {
        var dbPeople = GetPeopleByName(query, pageIndex, pageSize);
        if (filters.IsFiltered)
        {
            dbPeople = FilterPeople(dbPeople, filters);
        }

        var resultCount = dbPeople.Count();
        if (resultCount == 0)
        {
            // return something else
            throw new NotImplementedException();
        }

        dbPeople = dbPeople.Skip((pageIndex - 1) * pageSize).Take(pageSize).Distinct().OrderBy(p => p.LastName).AsNoTracking();

        var results = await dbPeople.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName)).ToListAsync();
        var paginatedResults = new PaginatedList<Core.Models.Person>(results, resultCount, pageIndex, pageSize);
        return paginatedResults;
    }

    public async Task<PaginatedList<Person>> GetPageOfPeople(int pageIndex, int pageSize)
    {
        var dbPeople = await _dbContext.People.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

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

        return people;
    }

    private IQueryable<Models.DB.Person> DiedBefore(IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfDeath <= date);
    }

    private IQueryable<Models.DB.Person> BornAfter( IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfBirth >= date);
    }

    private IQueryable<Models.DB.Person> GetPeopleByName(PersonQuery query, int pageIndex, int pageSize)
    {

        var dbPeople = _dbContext.People.Include(p => p.Decorations)
              .Include(p => p.RecordedNames)
              .ThenInclude(rn => rn.WarMemorial)
              .Include(p => p.SubUnit)
              .ThenInclude(unit => unit.Regiment)
              .Where(p => p.FirstNames.Contains(query.SearchTerm)
                || p.LastName.Contains(query.SearchTerm));

        return dbPeople;
    }
}
