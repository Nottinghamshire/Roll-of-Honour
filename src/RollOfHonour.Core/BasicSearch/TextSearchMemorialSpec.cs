using Ardalis.Specification;
using RollOfHonour.Core.BasicSearch;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Specifications;

public class TextSearchMemorialSpec : Specification<Memorial, MemorialSearchResult>
{
  public TextSearchMemorialSpec(string searchString)
  {
    Query
      .Select(memorial => new MemorialSearchResult()
      {
        Id = memorial.Id,
        Name = memorial.Name,
        Description = memorial.Description
      })
      .Where(memorial =>
        memorial.Name.Contains(searchString) ||
        memorial.Description.Contains(searchString));
  }
}

