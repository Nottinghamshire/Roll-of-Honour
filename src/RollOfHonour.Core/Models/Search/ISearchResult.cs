using RollOfHonour.Core.Enums;

namespace RollOfHonour.Core.Models.Search;

public interface ISearchResult
{
    public int Id { get; set; }
    public string Name { get; set; }
}
