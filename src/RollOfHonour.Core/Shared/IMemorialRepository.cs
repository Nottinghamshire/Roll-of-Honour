using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Shared;

public interface IMemorialRepository
{
    Task<Memorial?> GetById(int id);
    int Count();
    Task<IEnumerable<Memorial>> GetAll();
    Task<IEnumerable<MemorialSearchResult>?> FindMemorialByName(string nameFragment);
    Task<PaginatedList<Memorial>> GetPageOfMemorials(int pageIndex, int pageSize);
}
