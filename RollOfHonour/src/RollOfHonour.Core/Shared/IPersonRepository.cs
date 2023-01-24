using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
  Task<Person?> FindPersonById(int id);
}
