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
  
  public async Task<Core.Models.Person?> FindById(int id)
  {
    try
    {
      var dbPerson = await _dbContext.People.FirstAsync(p => p.Id == id);
      
      // TODO: Work out mapping
      var result = new Person()
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
    catch (InvalidOperationException)
    {
      return null;
    }
  }
}
