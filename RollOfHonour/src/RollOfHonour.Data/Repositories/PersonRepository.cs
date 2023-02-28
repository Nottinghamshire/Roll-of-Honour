using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;
using System.Linq;

namespace RollOfHonour.Data.Repositories;

public class PersonRepository : IPersonRepository
{
  private RollOfHonourContext _dbContext { get; set; }

  public PersonRepository(RollOfHonourContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Core.Models.Person?> FindById(int id)
  {
    try
    {
      var dbPerson = await _dbContext.People.FirstAsync(p => p.Id == id);
      return dbPerson.ToDomainModel();
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }

  public async Task<IEnumerable<Core.Models.Person>> DiedOnThisDay(DateTime date)
  {
    var countOfPeople = _dbContext.People.Count();
    var random = new Random((int)date.Ticks);

    var dbPeople = new List<Data.Models.DB.Person>();

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
    IEnumerable<Core.Models.Person> people = dbPeople.Select(p => p.ToDomainModel());
    return people;
  }
}
