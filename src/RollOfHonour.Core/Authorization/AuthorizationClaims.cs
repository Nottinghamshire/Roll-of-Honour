namespace RollOfHonour.Core.Authorization;

public struct AuthorizationClaims
{
    public const string ModeratorPersonEdit = $"{ApplicationRoles.Moderator}.Person.Edit";
    public const string AdministratorPersonEdit = $"{ApplicationRoles.Administrator}.Person.Edit";
    
    public const string ModeratorMemorialEdit = $"{ApplicationRoles.Moderator}.Memorial.Edit";
    public const string AdministratorMemorialEdit = $"{ApplicationRoles.Administrator}.Memorial.Edit";

    public const string AdministratorUserEdit = $"{ApplicationRoles.StaffAdmin}.User.Edit";
}
