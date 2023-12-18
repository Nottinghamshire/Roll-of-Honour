using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RollOfHonour.Data.Models.DB;

public class User
{

    public static Core.Models.User ToDomainModel(User user)
    {
        return new()
        {
            Id = user.Id,
            Role = Models.DB.Role.ToDomainModel(user.Role!),
            FirstName = user.FirstName,
            Surname = user.Surname,
            IsActive = user.IsActive,
            AzureCorrelationId = user.AzureCorrelationId,
            EmailAddress = user.EmailAddress,
            Reference = user.Reference
        };
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid? AzureCorrelationId { get; set; }

    [Required] [MinLength(2)] public string FirstName { get; set; } = default!;

    [Required] [MinLength(2)] public string Surname { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public bool IsActive { get; set; }

    public Role? Role { get; set; } = default!;

    public Guid Reference { get; set; } = default!;
}
