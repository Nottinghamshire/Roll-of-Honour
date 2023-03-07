using Ardalis.Result;
using NetTopologySuite.Index.HPRtree;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Core.Specifications;

namespace RollOfHonour.Core.BasicSearch;

public class SuperSearchService : ISuperSearchService
{
  private readonly IRepository<Memorial> _repository;

  public SuperSearchService(IRepository<Memorial> repository)
  {
    _repository = repository;
  }

  public async Task<Result<List<SearchResult>>> BasicSearch(string searchString)
  {
    List<SearchResult> results = new List<SearchResult>();

    if (string.IsNullOrEmpty(searchString))
    {
      var errors = new List<ValidationError>
      {
        new() { Identifier = nameof(searchString), ErrorMessage = $"{nameof(searchString)} is required." }
      };

      return Result<List<SearchResult>>.Invalid(errors);
    }

    var memorialsSpec = new TextSearchMemorialSpec(searchString);
    var memorialResults = await _repository.ListAsync();
    if (memorialResults != null)
      results.AddRange((IEnumerable<SearchResult>)memorialResults);


    var peopleSpec = new TextSearchPersonSpec(searchString);
    var peopleResults = await _repository.ListAsync();
    if (peopleResults != null)
      results.AddRange((IEnumerable<SearchResult>)peopleResults);


    var recordedNamesSpec = new TextSearchRecordedNameSpec(searchString);
    var recordedNamesResults = await _repository.ListAsync();
    if (recordedNamesResults != null)
      results.AddRange((IEnumerable<SearchResult>)recordedNamesResults);

    if (!results.Any())
    {
      return Result<List<SearchResult>>.NotFound();
    }

    return new Result<List<SearchResult>>(results);
  }

}
