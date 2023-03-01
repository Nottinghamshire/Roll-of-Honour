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

      return result;
    }
    catch (InvalidOperationException ex)
    {
      return null;
    }
  }

  public async Task<List<Person>> GetAll()
  {
    try
    {
      var people = await _dbContext.People.OrderByDescending(x => x.Id).Take(25).ToListAsync();

      return people.Select(person => new Person
        {
          Id = person.Id,
          Comments = person.Comments,
          Cwgc = person.Cwgc,
          Deleted = person.Deleted,
          Initials = person.Initials ?? string.Empty,
          Rank = person.Rank ?? string.Empty,
          EmploymentHobbies = person.EmploymentHobbies,
          ExtraInfo = person.ExtraInfo,
          FamilyHistory = person.FamilyHistory,
          FirstNames = person.FirstNames ?? string.Empty,
          LastName = person.LastName ?? string.Empty,
          MilitaryHistory = person.MilitaryHistory,
          ServiceNumber = person.ServiceNumber ?? string.Empty,
          AddressAtEnlistment = person.AddressAtEnlistment,
          AgeAtDeath = person.AgeAtDeath,
          DateOfBirth = person.DateOfBirth,
          DateOfDeath = person.DateOfDeath,
          MainPhotoId = person.MainPhotoId,
          PlaceOfBirth = person.PlaceOfBirth
        })
        .ToList();
    }
    catch (Exception e)
    {
      //Console.WriteLine(e);
      //throw;
      return null;
    }
  }
}
