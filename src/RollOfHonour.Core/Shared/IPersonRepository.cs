using RollOfHonour.Core.BasicSearch;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
  Task<Person?> GetById(int id);
  Task<IEnumerable<Person>> GetAll();
  Task<IEnumerable<Person>> DiedOnThisDay(DateTime date);
  Task<IEnumerable<PersonSearchResult>?> FindByName(string nameFragment);
}
