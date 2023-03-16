using Ardalis.Result;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Search;

public interface ISuperSearchService
{
  Task<Result<List<ISearchResult>>> BasicSearch(string searchString);
  Task<Result<List<PersonSearchResult>>> PersonSearch(PersonQuery searchQuery);
  Task<Result<List<MemorialSearchResult>>> MemorialSearch(MemorialQuery searchQuery);
}
