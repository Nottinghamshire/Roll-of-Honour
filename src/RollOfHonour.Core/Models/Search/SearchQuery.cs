using RollOfHonour.Core.Enums;

namespace RollOfHonour.Core.Models.Search;

public class SearchQuery
{
    public string SearchTerm { get; set; } = "";
    public bool WW1 { get; set; }
    public bool WW2 { get; set; }
    public PersonType PersonType { get; set; }
    public QueryType QueryType { get; set; }
}