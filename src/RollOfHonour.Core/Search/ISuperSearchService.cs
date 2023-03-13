using Ardalis.Result;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Search;

public interface ISuperSearchService
{
  Task<Result<List<ISearchResult>>> BasicSearch(string searchString);
  Task<Result<List<PersonSearchResult>>> PersonSearch(SearchQuery searchQuery);
  Task<Result<List<MemorialSearchResult>>> MemorialSearch(SearchQuery searchQuery);
}
