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

    public async Task<List<RegimentFilter>> GetRegimentFiltersForSearch(PersonQuery query)
    {
        return await _personRepository.GetRegimentFiltersForSearch(query);
    }

    public async Task<List<RegimentFilter>> GetRegimentFiltersForSearchByRegimentName(RegimentPersonQuery query)
    {
        return await _personRepository.GetRegimentFiltersForSearchByRegimentName(query);
    }

    public async Task<Result<PaginatedList<Memorial>>> MemorialSearch(MemorialQuery query, int pageNumber, int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError>
            {
                new()
                {
                    Identifier = nameof(query.SearchTerm),
                    ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<Memorial>>.Invalid(errors);
        }

        var memorialResults = await _memorialRepository.SearchMemorials(query.SearchTerm, pageNumber, pageSize);

        if (!memorialResults.Any())
        {
            return Result<PaginatedList<Memorial>>.NotFound();
        }

        return memorialResults;
    }

    public async Task<Result<PaginatedList<Person>>> PersonSearch(PersonQuery query, Filters filters, int pageNumber,
        int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError>
            {
                new()
                {
                    Identifier = nameof(query.SearchTerm),
                    ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<Person>>.Invalid(errors);
        }

        var peopleResults = await _personRepository.SearchPeople(query, filters, pageNumber, pageSize);

        if (!peopleResults.Any())
        {
            return Result<PaginatedList<Person>>.NotFound();
        }

        return peopleResults;
    }

    public async Task<Result<PaginatedList<Person>>> PersonSearchByRegimentName(RegimentPersonQuery query,
        Filters filters, int pageNumber, int pageSize)
    {
        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            var errors = new List<ValidationError>
            {
                new()
                {
                    Identifier = nameof(query.SearchTerm),
                    ErrorMessage = $"{nameof(query.SearchTerm)} is required."
                }
            };

            return Result<PaginatedList<Person>>.Invalid(errors);
        }

        var peopleResults = await _personRepository.SearchPeopleByRegimentName(query, filters, pageNumber, pageSize);

        if (!peopleResults.Any())
        {
            return Result<PaginatedList<Person>>.NotFound();
        }

        return peopleResults;
    }
}
