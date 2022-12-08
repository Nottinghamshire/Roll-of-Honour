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
    public string AsRecorded { get; set; }
    //public string? FirstName { get; set; }
    //public string? Initials { get; set; }
    //public string? LastName { get; set; }
    //public string? Rank { get; set; }
    //public string? Sex { get; set; }
    public string? ServiceNumber { get; set; }

    public int? PersonId { get; set; }

    public void MatchToPerson(int personId)
    {
        Guard.Against.Null(personId, nameof(personId));
        PersonId = personId;
    }
}
