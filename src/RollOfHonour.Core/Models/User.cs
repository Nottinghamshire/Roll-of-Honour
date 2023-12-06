using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Models;

public class User : IAggregateRoot
{
    public int Id { get; set; }

    public Guid? AzureCorrelationId { get; set; }

    public string FirstName { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public bool IsActive { get; set; }

    public Role? Role { get; set; } = default!;

    public Guid Reference { get; set; } = default!;

    // Domain-specific info for front-end
    public string Username
    {
        get
        {
            return $"{Reference.ToString().Substring(4)} - {FirstName} {Surname}";
        }
    }


}
