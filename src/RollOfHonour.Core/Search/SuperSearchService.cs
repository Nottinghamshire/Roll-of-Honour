using Ardalis.Result;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Search;

public class SuperSearchService : ISuperSearchService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMemorialRepository _memorialRepository;

    public SuperSearchService(IPersonRepository personRepository, IMemorialRepository memorialRepository)
    {
        _personRepository = personRepository;
        _memorialRepository = memorialRepository;
    }

    public Task<Result<List<ISearchResult>>> BasicSearch(string searchString)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<MemorialSearchResult>>> MemorialSearch(MemorialQuery query)
    {
        List<MemorialSearchResult> results = new List<MemorialSearchResult>();

        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError> { new()
                {
                Identifier = nameof(query.SearchTerm),
                ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<List<MemorialSearchResult>>.Invalid(errors);
        }

        var memorialResults = await _memorialRepository.FindMemorialByName(query.SearchTerm);

        if (memorialResults != null)
        {
            results.AddRange(memorialResults);
        }

        if (!results.Any())
        {
            return Result<List<MemorialSearchResult>>.NotFound();
        }

        return results;
    }

    public async Task<Result<List<PersonSearchResult>>> PersonSearch(PersonQuery query)
    {
        List<PersonSearchResult> results = new List<PersonSearchResult>();

        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError> { new()
                {
                Identifier = nameof(query.SearchTerm),
                ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<List<PersonSearchResult>>.Invalid(errors);
        }

        var peopleResults = await _personRepository.FindByName(query.SearchTerm);
        if (peopleResults != null)
        {
            results.AddRange(peopleResults);
        }

        if (!results.Any())
        {
            return Result<List<PersonSearchResult>>.NotFound();
        }

        return results;
    }
}
