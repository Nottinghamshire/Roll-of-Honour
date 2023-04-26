using System.Collections.ObjectModel;

namespace RollOfHonour.Core.Models;

public class Person
{
    public Person(int? mainPhotoId)
    {
        MainPhotoId = mainPhotoId;
    }

    public int Id { get; set; }
    public string FirstNames { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Rank { get; set; } =
        string.Empty; //TODO: This needs cleaning up - so many similar entries. Autopicker/Prompt perhaps

    public string ServiceNumber { get; set; } = "Unknown";
    public string ServiceNumberString => !string.IsNullOrEmpty(ServiceNumber) ? ServiceNumber : "Unknown";

    public string Unit { get; set; } = string.Empty;
    public int? UnitId { get; set; }
    public string Regiment { get; set; } = string.Empty;
    public int? RegimentId { get; set; }

    public string UnitRegimentString
    {
        get
        {
            if (!string.IsNullOrEmpty(Unit) && !string.IsNullOrEmpty(Regiment))
            {
                return $"{Unit} {Regiment}";
            }

            if (!string.IsNullOrEmpty(Unit))
            {
                return Unit;
            }

            return !string.IsNullOrEmpty(Regiment) ? Regiment : string.Empty;
        }
    }

    public DateTime? DateOfBirth { get; set; }
    public string DateOfBirthString => DateOfBirth.HasValue ? DateOfBirth.Value.ToString("dd MMM yyyy") : "Unknown";
    public DateTime? DateOfDeath { get; set; }
    public string DateOfDeathString => DateOfDeath.HasValue ? DateOfDeath.Value.ToString("dd MMM yyyy") : "Unknown";
    public int? AgeAtDeath { get; set; }

    public string AgeAtDeathString
    {
        get
        {
            if (AgeAtDeath.HasValue)
            {
                return $"{AgeAtDeath.Value.ToString()} Years Old";
            }

            if (DateOfBirth.HasValue && DateOfDeath.HasValue)
            {
                return $"{AgeCalculator(DateOfBirth.Value, DateOfDeath.Value)} Years Old";
            }

            return "Age Unknown";
        }
    }

    public string LivedFromUntilString
    {
        get
        {
            //Lived 25 Jan 1892 until 24 Mar 1918
            if (DateOfBirth.HasValue && DateOfDeath.HasValue)
            {
                return $"Lived {DateOfBirthString} until {DateOfDeathString}";
            }

            if (DateOfBirth.HasValue)
            {
                return $"Born {DateOfBirthString}";
            }

            if (DateOfDeath.HasValue)
            {
                return $"Died {DateOfDeathString}";
            }

            return string.Empty;
        }
    }

    public string? Comments { get; set; }
    public bool Deleted { get; set; }

    public string? AddressAtEnlistment { get; set; }
    public int? Cwgc { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string PlaceOfBirthString => !string.IsNullOrEmpty(PlaceOfBirth) ? PlaceOfBirth : "Unknown";
    public string? EmploymentHobbies { get; set; }
    public string EmploymentHobbiesString => !string.IsNullOrEmpty(EmploymentHobbies) ? EmploymentHobbies : "Unknown";

    public string? FamilyHistory { get; set; }
    public string? MilitaryHistory { get; set; }
    public string? ExtraInfo { get; set; }

    public int? MainPhotoId { get; private set; }

    public string Name => string.IsNullOrEmpty(FirstNames)
        ? string.IsNullOrEmpty(Initials) ? $"{LastName}" : $"{Initials} {LastName}"
        : $"{FirstNames} {LastName}";

    public List<Decoration> Decorations { get; set; } = new List<Decoration>();
    public Dictionary<int, string> Memorials { get; set; } = new Dictionary<int, string>();

    public Photo? MainPhoto
    {
        get
        {
            if (MainPhotoId.HasValue && Photos.Exists(p => p.Id == MainPhotoId))
            {
                return Photos.Single(p => p.Id == MainPhotoId);
            }

            //Photo decision
            //If main photo null, then use the first of the other images (or a random one)
            if (Photos.Any())
            {
                //Promote an image to MainPhoto
                return Photos.First();
            }

            return null;
        }
    }

    public List<Photo> Photos { get; set; } = new List<Photo>();

    private int AgeCalculator(DateTime dateOfBirth, DateTime dateOfDeath)
    {
        int age = dateOfDeath.Year - dateOfBirth.Year;
        if (dateOfBirth > dateOfDeath.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
