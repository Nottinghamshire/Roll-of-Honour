using System.Collections;
using Ardalis.Specification;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Specifications;

public class MemorialByIdWithNamesSpec : Specification<Memorial>
{
  public MemorialByIdWithNamesSpec(int memorialId)
  {
    Query
        .Where(memorial => memorial.Id == memorialId)
        .Include(project => project.RecordedNames);
  }
}
