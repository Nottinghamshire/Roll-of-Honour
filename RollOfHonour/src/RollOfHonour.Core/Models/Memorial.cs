using Ardalis.GuardClauses;
using NetTopologySuite.Geometries;

namespace RollOfHonour.Core.Models;

public class Memorial
{
    private List<RecordedName> _recordedNames = new List<RecordedName>();

    public int Id { get; set; }
    public int? UKNIWMRef { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    //public int Easting { get; set; }
    //public int Northing { get; set; }
    public Point Location { get; set; }
    public string? District { get; set; }
    public string? Postcode { get; set; }
    public int NamesCount { get; private set; }
    public int? MainPhotoId { get; set; }
    public IEnumerable<RecordedName> RecordedNames => _recordedNames.AsReadOnly();

    public Memorial(string name, Point location)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Location = location;
    }

    public Memorial(string name, string description, Point location)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Location = location;
        Description = description;
    }

    public void RecordName(RecordedName newName)
    {
        Guard.Against.Null(newName, nameof(newName));
        _recordedNames.Add(newName);
    }
}
