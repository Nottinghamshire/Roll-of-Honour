using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;

namespace RollOfHonour.Core.Shared;

public interface IPersonRepository
{
    Task<Person?> GetById(int id);
    Task<IEnumerable<Person>> GetAll();
    Task<IEnumerable<Person>> DiedOnThisDay(DateTime date);
    int Count();
    Task<IEnumerable<PersonSearchResult>?> FindByName(string nameFragment);
    Task<PaginatedList<Person>> GetPageOfPeople(int pageIndex, int pageSize);
}
