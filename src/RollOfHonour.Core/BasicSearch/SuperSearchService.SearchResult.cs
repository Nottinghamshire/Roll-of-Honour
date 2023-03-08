namespace RollOfHonour.Core.BasicSearch;

public abstract class SearchResult
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
}

public class SearchQuery
{
  public string SearchTerm { get; set; }
  public bool WW1 { get; set; }
  public bool WW2 { get; set; }
  public string PersonType { get; set; }
}

public class PersonSearchResult : SearchResult
{
}

public class MemorialSearchResult : SearchResult
{
  public string Description { get; set; } = string.Empty;
}

public class RecordedNameSearchResult : SearchResult
{
}


/*
 * e.g.
Bob (searched for)
  Person   | Bob Mortimer -> West BridgfordCemetaryMemorial
  Recorded | Bobba Fett (& Rank) - Memorial on
  Memorial | Boberton Church Memorial - Location
*/
