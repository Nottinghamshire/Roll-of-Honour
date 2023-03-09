using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IMemorialRepository
{
  Task<Memorial?> GetById(int id);
  Task<IEnumerable<Memorial>> GetAll();
}
