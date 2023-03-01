using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
  Task<Person?> GetById(int id);
  Task<List<Person>> GetAll();
}
