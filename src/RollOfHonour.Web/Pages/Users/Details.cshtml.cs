using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
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

    private IUserRepository _userRepository { get; set; }
    private IRoleRepository _roleRepository { get; set; }

    private Dictionary<string, int>? _users;

    public Details(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        if (Request.HttpContext.User.Identity != null)
            UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        //will work when tested with claims etc when we store shallow copy of user to local db
        _users = await _userRepository.GetAllAsUsernameIdCollectionAsync();
        if (_users is not null && _users.Any())
            Usernames = _users.Keys.ToList();
        else
            Usernames = new List<string>();

        var roles = await _roleRepository.GetAllNamesAsync();
        if (roles is not null && roles.Any())
            Roles = roles;
        else 
            Roles = new List<string>(); // better than returning notfound?

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (SelectedUser.IsNullOrEmpty())
            return BadRequest();

        var selectedRole = await _roleRepository.GetByNameAsync(UserRole);
        if (selectedRole is null)
            return BadRequest();

        var users = await _userRepository.GetAllAsUsernameIdCollectionAsync();
        if (users.Any() is false || users.TryGetValue(SelectedUser, out var userToUpdate) is false)
            return BadRequest();
            
        await _userRepository.UpdateRoleAsync(userToUpdate, selectedRole);

        return Page();
    }
}
