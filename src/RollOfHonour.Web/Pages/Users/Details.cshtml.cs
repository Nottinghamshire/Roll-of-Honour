using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Data.Repositories;

namespace RollOfHonour.Web.Pages.Users;

//[Authorize(Policy = AuthorizationPolicyNames.EditUser)]
[Authorize]
public class Details : PageModel
{
    public bool UserIsAuthenticated { get; set; }
    [BindProperty] public List<string> Usernames { get; set; } = default!;
    [BindProperty] public List<string> Roles { get; set; } = default!;
    [BindProperty] public string SelectedUser { get; set; } = default!;
    [BindProperty] public string UserRole { get; set; } = default!;

    private IUserRepository UserRepository { get; set; }
    private IRoleRepository RoleRepository { get; set; }

    private Dictionary<string, int>? _users;

    public Details(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        return NotFound(); // currently needed because no-one has specified permissions to edit users,
                           // once claims are setup properly remove line 33:34 and uncomment the below

        //// TODO - when roles/claims are implemented into Azure claims stop redirecting user to index
        //if (Request.HttpContext.User.Identity != null)
        //    UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        ////will work when tested with claims etc when we store shallow copy of user to local db
        //_users = await UserRepository.GetAllAsUsernameIdCollectionAsync();
        //if (_users is not null && _users.Any())
        //    Usernames = _users.Keys.ToList();
        //else
        //    Usernames = new List<string>();

        //var roles = await RoleRepository.GetAllNamesAsync();
        //if (roles is not null && roles.Any())
        //    Roles = roles;
        //else 
        //    Roles = new List<string>(); // better than returning notfound?

        //return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return NotFound(); // TODO - Same as above

        //if (SelectedUser.IsNullOrEmpty())
        //    return BadRequest();

        //var selectedRole = await RoleRepository.GetByNameAsync(UserRole);
        //if (selectedRole is null)
        //    return BadRequest();

        //var users = await UserRepository.GetAllAsUsernameIdCollectionAsync();
        //if (users.Any() is false || users.TryGetValue(SelectedUser, out var userToUpdate) is false)
        //    return BadRequest();
            
        //await UserRepository.UpdateRoleAsync(userToUpdate, selectedRole);

        //return Page();
    }
}
