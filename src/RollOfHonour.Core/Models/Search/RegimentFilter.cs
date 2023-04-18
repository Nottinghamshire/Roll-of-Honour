namespace RollOfHonour.Core.Models.Search;

public class RegimentFilter
{
    public string Regiment { get; set; } = string.Empty;
    public int RegimentId { get; set; }

    public RegimentFilter(int regimentId, string regiment )
    {
        RegimentId = regimentId;
        Regiment = regiment;
    }
}