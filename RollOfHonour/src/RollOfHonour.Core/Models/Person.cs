namespace RollOfHonour.Core.Models;

public class Person
{
  public int Id { get; set; }
  public string FirstNames { get; set; } = string.Empty;
  public string Initials { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Rank { get; set; } = string.Empty;//TODO: This needs cleaning up - so many similar entries. Autopicker/Prompt perhaps
  public string ServiceNumber { get; set; } = string.Empty;

  public DateTime? DateOfBirth { get; set; }
  public DateTime? DateOfDeath { get; set; }
  public int? AgeAtDeath { get; set; }
  public string? Comments { get; set; }
  public bool Deleted { get; set; }

  public string? AddressAtEnlistment { get; set; }
  public int? Cwgc { get; set; }
  public string? PlaceOfBirth { get; set; }
  public string? EmploymentHobbies { get; set; }
  public string? FamilyHistory { get; set; }
  public string? MilitaryHistory { get; set; }
  public string? ExtraInfo { get; set; }

  public int? MainPhotoId { get; set; }

  public string Name
  {
    get
    {
      if (string.IsNullOrEmpty(FirstNames))
      {
        if (string.IsNullOrEmpty(Initials))
        {
          return $"{LastName}";
        }
        else
        {
          return $"{LastName}, {Initials}";
        }
      }
      return $"{LastName}, {FirstNames}";
    }
  }
}
