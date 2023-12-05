using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Repositories;

namespace RollOfHonour.Web.Pages.User;

//[Authorize(Policy = AuthorizationPolicyNames.EditUser)]
[Authorize]
public class Details : PageModel
{
    public Core.Models.User? User { get; set; }
    public bool UserIsAuthenticated { get; set; }

    private IUserRepository _userRepository { get; set; } = null!;

    public Details(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> OnGet(string? reference)
    {
        if (Request.HttpContext.User.Identity != null)
            UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        if (Guid.TryParse(reference, out var userReference))
        {
            var user = await _userRepository.GetAsync(userReference);
            if (user == null)
                return NotFound();

            User = user;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return NotFound();
    }
}
