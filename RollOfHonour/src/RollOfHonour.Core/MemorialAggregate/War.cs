using RollOfHonour.SharedKernel;

namespace RollOfHonour.Core.ProjectAggregate
{
  internal class War : EntityBase
  {
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int StartYear { get; set; }
    public int EndYear { get; set; }

    public int? ContributorId { get; private set; }
  }
}
