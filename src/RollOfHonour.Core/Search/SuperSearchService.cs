using Ardalis.Result;
using RollOfHonour.Core.Models;
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

    public async Task<Result<PaginatedList<MemorialSearchResult>>> MemorialSearch(MemorialQuery query, int pageNumber, int pageSize)
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

            return Result<PaginatedList<MemorialSearchResult>>.Invalid(errors);
        }

        var memorialResults = await _memorialRepository.SearchMemorials(query.SearchTerm, pageNumber, pageSize);

        if (memorialResults != null)
        {
            results.AddRange(memorialResults);
        }

        if (!results.Any())
        {
            return Result<PaginatedList<MemorialSearchResult>>.NotFound();
        }

        return memorialResults;
    }

    public async Task<Result<PaginatedList<PersonSearchResult>>> PersonSearch(PersonQuery query, int pageNumber, int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError> { new()
                {
                Identifier = nameof(query.SearchTerm),
                ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<PersonSearchResult>>.Invalid(errors);
        }

        var peopleResults = await _personRepository.SearchPeople(query.SearchTerm, pageNumber, pageSize);

        if (!peopleResults.Any())
        {
            return Result<PaginatedList<PersonSearchResult>>.NotFound();
        }

        return peopleResults;
    }
}
