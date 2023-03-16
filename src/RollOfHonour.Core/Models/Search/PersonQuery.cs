using RollOfHonour.Core.Enums;

namespace RollOfHonour.Core.Models.Search;

public class PersonQuery : ISearchQuery
{
   public string SearchTerm { get; set; } = string.Empty; 
    public bool WW1 { get; set; }
    public bool WW2 { get; set; }
    public PersonType PersonType { get; set; }
}