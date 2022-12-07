using NetTopologySuite.Geometries;

namespace RollOfHonour.Core.ProjectAggregate;

public class Areas
{
  public Areas(string name, Geometry border)
  {
    Name = name;
    Border = border;
  }

  public Guid Id { get; set; }

  public string Name { get; set; }

  // Database includes both Polygon and MultiPolygon values
  public Geometry Border { get; set; }
}
