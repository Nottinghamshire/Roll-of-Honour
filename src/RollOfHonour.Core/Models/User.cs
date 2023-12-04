namespace RollOfHonour.Core.Models;

public class User
{
    public int Id { get; set; }

    public Guid? AzureCorrelationId { get; set; }

    public string FirstName { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public bool IsActive { get; set; }

    public Role? Role { get; set; } = default!;
}
