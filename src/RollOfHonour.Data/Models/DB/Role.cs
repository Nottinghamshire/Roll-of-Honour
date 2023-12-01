using System.ComponentModel.DataAnnotations;

namespace RollOfHonour.Data.Models.DB;

public class Role
{
    public int Id { get; set; }
    
    [Required] public string Name { get; set; } = default!;

    public string? Description { get; set; } = default!;

    public bool IsActive { get; set; }

    public List<Claim> Claims { get; set; } = default!;
}
