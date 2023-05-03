using RollOfHonour.Core.Enums;
namespace RollOfHonour.Core.Models.Search;

public class Filters
{
    public DateTime DiedBefore { get; }
    public DateTime BornAfter { get; }
    public PersonType SelectedPersonType { get; }
    public HashSet<int> Regiments { get; }

    public bool DateRangeUsed => DiedBefore.Year != 1900 || BornAfter.Year != 1900;
    public bool HasRegiments => Regiments.Count() != 0;

    public bool IsFiltered => HasRegiments || DateRangeUsed ;

    public Filters(int diedBefore, int bornAfter, PersonType personType, HashSet<int> regiments)
    {
        DiedBefore = new DateTime(diedBefore, 1, 1);
        BornAfter = new DateTime(bornAfter, 1, 1);
        SelectedPersonType = personType; 
        Regiments = regiments;
    }
}
