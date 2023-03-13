namespace RollOfHonour.Core.Models.Search;
public class MemorialQuery : ISearchQuery
{
   public string SearchTerm { get; set; } = string.Empty; 
}