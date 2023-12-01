using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RollOfHonour.Data.Models.DB;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid? AzureCorrelationId { get; set; }

    [Required] [MinLength(2)] public string FirstName { get; set; } = default!;

    [Required] [MinLength(2)] public string Surname { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public bool IsActive { get; set; }

    public Role? Role { get; set; } = default!;
}
