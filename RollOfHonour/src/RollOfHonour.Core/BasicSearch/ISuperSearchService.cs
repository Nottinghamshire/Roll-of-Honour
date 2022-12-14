using Ardalis.Result;

namespace RollOfHonour.Core.BasicSearch;

public interface ISuperSearchService
{
  Task<Result<List<SearchResult>>> BasicSearch(string searchString);
}
