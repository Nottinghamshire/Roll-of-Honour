using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Shared;

public interface IMemorialRepository
{
  Task<Memorial?> FindById(int id);
}
