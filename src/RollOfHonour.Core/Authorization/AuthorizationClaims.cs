namespace RollOfHonour.Core.Authorization;

public struct AuthorizationClaims
{
    private const string ModeratorPrefix = "Moderator";
    private const string AdminPrefix = "Admin";

    public const string ModeratorPersonEdit = $"{ModeratorPrefix}.Person.Edit";
    public const string AdminPersonEdit = $"{AdminPrefix}.Person.Edit";
    
    public const string ModeratorMemorialEdit = $"{ModeratorPrefix}.Memorial.Edit";
    public const string AdminMemorialEdit = $"{AdminPrefix}.Memorial.Edit";

}
