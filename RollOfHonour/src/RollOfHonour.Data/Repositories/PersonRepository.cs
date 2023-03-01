using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
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
      var dbPerson = await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

      // TODO: Work out mapping
      var result = new Person
      {
        Id = dbPerson.Id,
        Comments = dbPerson.Comments,
        Cwgc = dbPerson.Cwgc,
        Deleted = dbPerson.Deleted,
        Initials = dbPerson.Initials ?? string.Empty,
        Rank = dbPerson.Rank ?? string.Empty,
        EmploymentHobbies = dbPerson.EmploymentHobbies,
        ExtraInfo = dbPerson.ExtraInfo,
        FamilyHistory = dbPerson.FamilyHistory,
        FirstNames = dbPerson.FirstNames ?? string.Empty,
        LastName = dbPerson.LastName ?? string.Empty,
        MilitaryHistory = dbPerson.MilitaryHistory,
        ServiceNumber = dbPerson.ServiceNumber ?? string.Empty,
        AddressAtEnlistment = dbPerson.AddressAtEnlistment,
        AgeAtDeath = dbPerson.AgeAtDeath,
        DateOfBirth = dbPerson.DateOfBirth,
        DateOfDeath = dbPerson.DateOfDeath,
        MainPhotoId = dbPerson.MainPhotoId,
        PlaceOfBirth = dbPerson.PlaceOfBirth
      };

      return dbPerson.ToDomainModel();
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }

  public async Task<IEnumerable<Person>> GetAll()
  {
    try
    {
      var people = await _dbContext.People.OrderByDescending(x => x.Id).Take(25).ToListAsync();

      return people.Select(p => p.ToDomainModel());
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
}
