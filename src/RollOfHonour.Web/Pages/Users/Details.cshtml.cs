using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Models;
using RollOfHonour.Data.Repositories;

namespace RollOfHonour.Web.Pages.Users;

//[Authorize(Policy = AuthorizationPolicyNames.EditUser)]
[Authorize]
public class Details : PageModel
{
    public bool UserIsAuthenticated { get; set; }
    [BindProperty] public List<string> Usernames { get; set; } = default!;
    [BindProperty] public string SelectedUser { get; set; } = default!;
    [BindProperty] public string SelectedRole { get; set; } = default!;

    private IUserRepository _userRepository { get; set; }
    private IRoleRepository _roleRepository { get; set; }

    private IReadOnlyCollection<Core.Models.User>? _users;

    public Details(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        if (Request.HttpContext.User.Identity != null)
            UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        // will work when tested with claims etc when we store shallow copy of user to local db
        //_users = await _userRepository.GetAllAsync();
        //if(_users is not null && _users.Any())
        //    Usernames = _users.Select(_ => _.Username).ToList();

        Usernames = new List<string>
        {
            "Test username 1",
            "Test username 2",
            "Test username 3"
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userToEdit = _users?.SingleOrDefault(_ => _.Username == SelectedUser);
        if (userToEdit is null)
            return NotFound();

        var newRole = await _roleRepository.GetByNameAsync(SelectedRole);
        if (newRole is null)
            return NotFound();

        await _userRepository.UpdateRoleAsync(userToEdit, newRole);

        return Page();
    }
}
