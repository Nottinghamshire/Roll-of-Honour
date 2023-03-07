using Ardalis.Specification;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.BasicSearch;

public class TextSearchPersonSpec : Specification<Person, PersonSearchResult>
{
  public TextSearchPersonSpec(string searchString)
  {
    Query
      .Select(person => new PersonSearchResult()
      {
        Id = person.Id,
        Name = person.Name
      })
      .Where(person =>
        person.FirstNames.Contains(searchString) ||
        person.LastName.Contains(searchString) ||
        person.Initials.Contains(searchString));
  }
}

