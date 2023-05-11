using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
    Task<Person?> GetById(int id);
    Task<IEnumerable<Person>> DiedOnDay(DateTime date);
    int Count();
    Task<PaginatedList<Person>> SearchPeople(PersonQuery query, Filters filters, int pageIndex, int pageSize);
    Task<PaginatedList<Person>> SearchPeopleByRegimentName(RegimentPersonQuery query, Filters filters, int pageIndex, int pageSize);
    Task<PaginatedList<Person>> GetPageOfPeople(int pageIndex, int pageSize);
    Task<List<RegimentFilter>> GetRegimentFiltersForSearch(ISearchQuery query);
    Task<List<RegimentFilter>> GetRegimentFiltersForSearchByRegimentName(ISearchQuery query);
}
