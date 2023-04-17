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

    public async Task<Result<PaginatedList<Core.Models.Memorial>>> MemorialSearch(MemorialQuery query, int pageNumber, int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError> { new()
                {
                Identifier = nameof(query.SearchTerm),
                ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<Core.Models.Memorial>>.Invalid(errors);
        }

        var memorialResults = await _memorialRepository.SearchMemorials(query.SearchTerm, pageNumber, pageSize);

        if (!memorialResults.Any())
        {
            return Result<PaginatedList<Core.Models.Memorial>>.NotFound();
        }

        return memorialResults;
    }

    public async Task<Result<PaginatedList<Core.Models.Person>>> PersonSearch(PersonQuery query, Filters filters, int pageNumber, int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError> { new()
                {
                Identifier = nameof(query.SearchTerm),
                ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<Core.Models.Person>>.Invalid(errors);
        }

        var peopleResults = await _personRepository.SearchPeople(query, filters, pageNumber, pageSize);

        if (!peopleResults.Any())
        {
            return Result<PaginatedList<Core.Models.Person>>.NotFound();
        }

        return peopleResults;
    }
}
