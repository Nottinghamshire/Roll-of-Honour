using System.Collections;
using Ardalis.Specification;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.Specifications;

public class MemorialsSpec : Specification<Memorial>
{
  public MemorialsSpec()
  {
    Query.OrderBy(memorial => memorial.Name);
  }
}

