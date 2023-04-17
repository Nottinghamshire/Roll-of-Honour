using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Shared;

public interface IMemorialRepository
{
    Task<Memorial?> GetById(int id);
    int Count();
    Task<IEnumerable<Memorial>> GetAll();
    Task<PaginatedList<Memorial>> SearchMemorials(string searchString, int pageIndex, int pageSize);
    Task<PaginatedList<Memorial>> GetPageOfMemorials(int pageIndex, int pageSize);
}
