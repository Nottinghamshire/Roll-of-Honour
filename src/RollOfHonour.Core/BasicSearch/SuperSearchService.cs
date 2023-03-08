using Ardalis.Result;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.BasicSearch;

public class SuperSearchService : ISuperSearchService
{
  private readonly IPersonRepository _personRepository;

  private readonly IMemorialRepository _memorialRepository;
  //private readonly IRecordedNameRepository _recordedNameRepository;

  public SuperSearchService(IPersonRepository personRepository, IMemorialRepository memorialRepository)
  {
    _personRepository = personRepository;
    _memorialRepository = memorialRepository;
  }

  public Task<Result<List<SearchResult>>> BasicSearch(string searchString)
  {
    throw new NotImplementedException();
  }

  public async Task<Result<List<SearchResult>>> AdvancedSearch(SearchQuery searchQuery)
  {
    List<SearchResult> results = new List<SearchResult>();

    if (string.IsNullOrEmpty(searchQuery.SearchTerm))
    {
      var errors = new List<ValidationError>
      {
        new()
        {
          Identifier = nameof(searchQuery.SearchTerm),
          ErrorMessage = $"{nameof(searchQuery.SearchTerm)} is required."
        }
      };

      return Result<List<SearchResult>>.Invalid(errors);
    }

    // var memorialsSpec = new TextSearchMemorialSpec(searchString);
    // var memorialResults = await _memorialRepository.ListAsync();
    // if (memorialResults != null)
    //   results.AddRange((IEnumerable<SearchResult>)memorialResults);


    var peopleResults = await pe.ListAsync();
    if (peopleResults != null)
      results.AddRange((IEnumerable<SearchResult>)peopleResults);

    Query
      .Select(person => new PersonSearchResult() { Id = person.Id, Name = person.Name })
      .Where(person =>
        person.FirstNames.Contains(searchString) ||
        person.LastName.Contains(searchString) ||
        person.Initials.Contains(searchString));

    //
    // var recordedNamesSpec = new TextSearchRecordedNameSpec(searchString);
    // var recordedNamesResults = await _memorialRepository.ListAsync();
    // if (recordedNamesResults != null)
    //   results.AddRange((IEnumerable<SearchResult>)recordedNamesResults);

    if (!results.Any())
    {
      return Result<List<SearchResult>>.NotFound();
    }

    return new Result<List<SearchResult>>(results);
  }
}
