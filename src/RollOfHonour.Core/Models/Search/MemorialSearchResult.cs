namespace RollOfHonour.Core.Models.Search;

public class MemorialSearchResult : ISearchResult
{
    public string Description { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}