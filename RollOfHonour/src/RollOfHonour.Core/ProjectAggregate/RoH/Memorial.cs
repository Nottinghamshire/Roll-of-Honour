using RollOfHonour.SharedKernel;
using RollOfHonour.SharedKernel.Interfaces;

namespace RollOfHonour.Core.ProjectAggregate
{
    internal class Memorial : EntityBase, IAggregateRoot
  {
    //public int Id { get; set; }
    public string? UKNIWMRef { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Easting { get; set; }
    public int Northing { get; set; }
    public string? District { get; set; }
    public string? Postcode { get; set; }
    public int NamesCount { get; set; }
    public int? MainPhotoId { get; set; }
  }
}
