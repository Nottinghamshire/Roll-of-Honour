using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Shared;

public interface IMemorialRepository
{
    Task<Memorial?> GetById(int id);
    Task<IEnumerable<Memorial>> GetAll();
    Task<IEnumerable<MemorialSearchResult>?> FindMemorialByName(string nameFragment);
}
