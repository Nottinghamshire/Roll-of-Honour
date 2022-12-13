using Ardalis.Specification;
using RollOfHonour.Core.Models;

namespace RollOfHonour.Core.BasicSearch;

public class TextSearchRecordedNameSpec : Specification<RecordedName, RecordedNameSearchResult>
{
  public TextSearchRecordedNameSpec(string searchString)
  {
    Query
      .Select(recordedName => new RecordedNameSearchResult()
      {
        Id = recordedName.Id,
        Name = recordedName.AsRecorded
      })
      .Where(recordedName =>
        recordedName.AsRecorded.Contains(searchString));
  }
}

