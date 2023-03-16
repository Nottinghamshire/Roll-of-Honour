using Ardalis.GuardClauses;
using NetTopologySuite.Geometries;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Models;

public class Memorial : IAggregateRoot
{
  private readonly List<RecordedName> _recordedNames = new();

  public int Id { get; set; }
  public string? UKNIWMRef { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; } = string.Empty;

  //public int Easting { get; set; }
  //public int Northing { get; set; }
  public Point Location { get; set; }
  public string? District { get; set; }
  public string? Postcode { get; set; }
  public int NamesCount { get; set; } //TODO: Might be a better way to do this based on Count of recorded names
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

  public Memorial(string name)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public Memorial(string name, List<RecordedName> recordedNames)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    _recordedNames.AddRange(recordedNames);
  }

  public void RecordName(RecordedName newName)
  {
    Guard.Against.Null(newName, nameof(newName));
    _recordedNames.Add(newName);
  }
}
