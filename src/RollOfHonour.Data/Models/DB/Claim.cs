namespace RollOfHonour.Data.Models.DB;

public class Claim
{
    public int Id { get; set; }
    
    public Role Role { get; set; }

    public string Name { get; set; }

    public static Core.Models.Claim ToDomainModel(Claim claim)
    {
        return new() { 
            Id = claim.Id, 
            Name = claim.Name,
            //Note: mapping role at this level causes a recursive circular reference so causes a crash
            //Role = DB.Role.ToDomainModel(claim.Role) 
        };
    }
}
