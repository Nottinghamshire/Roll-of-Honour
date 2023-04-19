using Ardalis.Result;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Search;

public interface ISuperSearchService
{
  Task<Result<PaginatedList<Core.Models.Person>>> PersonSearch(PersonQuery searchQuery, Filters filters, int pageNumber, int pageSize);
  Task<Result<PaginatedList<Core.Models.Memorial>>> MemorialSearch(MemorialQuery searchQuery, int pageNumber, int pageSize);
  Task<List<RegimentFilter>> GetRegimentFiltersForSearch(PersonQuery query);
}
