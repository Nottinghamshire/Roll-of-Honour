using Ardalis.GuardClauses;

namespace RollOfHonour.Core.Models;

public class RecordedName
{
  public RecordedName(string asRecorded)
  {
    AsRecorded = asRecorded;
  }

  public int Id { get; set; }
  public int? IWMNameRefNo { get; set; } // Imperial War Museum
  public string AsRecorded { get; private set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string Initials { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string? Rank { get; set; } = string.Empty;
  public string? Sex { get; set; } = string.Empty;
  public string? ServiceNumber { get; set; } = string.Empty;

  public int? PersonId { get; set; }

  public void MatchToPerson(int personId)
  {
    Guard.Against.Null(personId, nameof(personId));
    PersonId = personId;
  }
}
