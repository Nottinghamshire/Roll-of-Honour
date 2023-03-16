namespace RollOfHonour.Core.Models;

internal class War 
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int StartYear { get; set; }
    public int EndYear { get; set; }

    public int? ContributorId { get; private set; }
}
