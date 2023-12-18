using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Models;

public class Claim : IAggregateRoot
{
    public int Id { get; set; }

    public Role Role { get; set; }

    public string Name { get; set; }
}
