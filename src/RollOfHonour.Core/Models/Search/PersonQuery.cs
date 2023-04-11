using RollOfHonour.Core.Enums;

namespace RollOfHonour.Core.Models.Search;

public class PersonQuery : ISearchQuery
{
    public string SearchTerm { get; set; } = string.Empty;
    public Core.Enums.War SelectedWar { get; set; }
    public PersonType PersonType { get; set; }
}