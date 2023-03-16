namespace RollOfHonour.Core.Models.Search;

public class PersonSearchResult : ISearchResult
{
    public int Id { get;  set; }
    public string Name { get; set; } = string.Empty;
}
