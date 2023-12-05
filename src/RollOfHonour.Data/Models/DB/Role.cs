using System.ComponentModel.DataAnnotations;

namespace RollOfHonour.Data.Models.DB;

public class Role
{
    public static Core.Models.Role ToDomainModel(Role userRole)
    {
        return new()
        {
            Id = userRole.Id,
            Name = userRole.Name,
            Description = userRole.Description,
            IsActive = userRole.IsActive,
            Claims = userRole.Claims.Select(Claim.ToDomainModel).ToList()
        };
    }

    public int Id { get; set; }
    
    [Required] public string Name { get; set; } = default!;

    public string? Description { get; set; } = default!;

    public bool IsActive { get; set; }

    public List<Claim> Claims { get; set; } = default!;

}
