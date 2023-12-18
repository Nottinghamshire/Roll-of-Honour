using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Memorial;

public class Details : PageModel
{
    public Core.Models.Memorial? Memorial { get; set; }

    public IMemorialRepository _memorialRepository { get; set; }

    public bool UserIsAuthenticated { get; set; }

    public Details(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        // only needed to determine whether to show the update buttons, will need to check against roles when implemented
        if (Request.HttpContext.User.Identity != null)
            UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        var memorial = await _memorialRepository.GetById(id);
        if (memorial is null)
            return NotFound();

        Memorial = memorial;
        return Page();
    }
}
