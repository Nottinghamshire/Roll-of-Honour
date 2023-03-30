using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.BasicSearch;
using RollOfHonour.Core.Models;
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

      return dbPerson.ToDomainModel(settingBlobName, settingBlobImageContainerName);
    }
    catch (InvalidOperationException ex)
    {
      return null;
    }
  }

  public async Task<IEnumerable<Person>> GetAll()
  {
    try
    {
      var people = await _dbContext.People
        .Include(p => p.Photos)
        .OrderByDescending(x => x.Id)
        .Take(25)
        .ToListAsync();

      return people.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
    }
    catch (Exception e)
    {
      return null;
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
      var person = await _dbContext.People.Include(p => p.Photos).FirstOrDefaultAsync(p => p.Id == randomId);
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

  public async Task<IEnumerable<PersonSearchResult>?> FindByName(string nameFragment)
  {
    var dbPeople = _dbContext.People.Where(p =>
      p.FirstNames.Contains(nameFragment) ||
      p.LastName.Contains(nameFragment));

    if (dbPeople.Count() == 0)
    {
      return null;
    }

    var results = await dbPeople
      .Select(p => new PersonSearchResult() { Id = p.Id, Name = $"{p.FirstNames} {p.LastName}" }).ToListAsync();

    return results;
  }
}
