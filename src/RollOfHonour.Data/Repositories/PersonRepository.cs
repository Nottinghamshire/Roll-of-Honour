using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class PersonRepository : IPersonRepository
{
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
              .Include(p => p.Decorations)
              .Include(p => p.RecordedNames).ThenInclude(rn => rn.WarMemorial)
              .Include(p => p.SubUnit).ThenInclude(unit => unit.Regiment)
              .FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                return null;
            }

            return dbPerson.ToDomainModel();
        }
        catch (InvalidOperationException)
        {
            // TODO: Is this necessary?
            return null;
        }
    }

    // TODO: Should this return a null rather than an empty enumerable?
    public async Task<IEnumerable<Person>> GetAll()
    {
        try
        {
            var people = await _dbContext.People.OrderByDescending(x => x.Id).Take(25).ToListAsync();
            return people.Select(p => p.ToDomainModel());
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

        IEnumerable<Person> people = dbPeople.Select(p => p.ToDomainModel());
        return people;
    }

    public async Task<PaginatedList<PersonSearchResult>> SearchPeople(string searchString, int pageIndex, int pageSize)
    {
        var dbPeople = _dbContext.People.Where(p =>
          p.FirstNames.Contains(searchString) ||
          p.LastName.Contains(searchString));
          
        var resultCount = dbPeople.Count();
        if (resultCount == 0)
        {
            // return something else
            throw new NotImplementedException();
        }
          
        dbPeople = dbPeople.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking();

        //TODO: Add extra fields
        var results = await dbPeople.Select(p => new PersonSearchResult() { Id = p.Id, Name = $"{p.FirstNames} {p.LastName}" }).ToListAsync();
        var paginatedResults = new PaginatedList<PersonSearchResult>(results, resultCount, pageIndex, pageSize);
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
            p.ToDomainModel()).ToList(), _dbContext.People.Count(), pageIndex, pageSize);
    }

    public int Count()
    {
        return _dbContext.People.Count();
    }
}
