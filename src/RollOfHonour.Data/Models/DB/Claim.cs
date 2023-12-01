namespace RollOfHonour.Data.Models.DB;

public class Claim
{
    public int Id { get; set; }
    
    public Role Role { get; set; }

    public string Name { get; set; }
}
