using Ardalis.Result;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Search;

public interface ISuperSearchService
{
  Task<Result<List<ISearchResult>>> BasicSearch(string searchString);
  Task<Result<PaginatedList<Core.Models.Person>>> PersonSearch(PersonQuery searchQuery, int pageNumber, int pageSize);
  Task<Result<PaginatedList<Core.Models.Memorial>>> MemorialSearch(MemorialQuery searchQuery, int pageNumber, int pageSize);
}
