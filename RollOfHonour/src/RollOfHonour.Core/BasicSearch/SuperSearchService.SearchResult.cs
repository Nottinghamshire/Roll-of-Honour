namespace RollOfHonour.Core.BasicSearch;

public abstract class SearchResult
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
}

public class PersonSearchResult : SearchResult { }
public class MemorialSearchResult : SearchResult { public string Description { get; set; } = string.Empty; }
public class RecordedNameSearchResult : SearchResult { }


/*
 * e.g.
Bob (searched for)
  Person   | Bob Mortimer -> West BridgfordCemetaryMemorial
  Recorded | Bobba Fett - Rank
  Memorial | Boberton Church Memorial - SomeLocation
*/
