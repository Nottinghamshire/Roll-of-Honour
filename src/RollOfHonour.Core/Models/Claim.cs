using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Models;

public class Claim : IAggregateRoot
{
    public int Id { get; set; }

    public Role Role { get; set; } = default!;

    public string Name { get; set; } = default!;
}
