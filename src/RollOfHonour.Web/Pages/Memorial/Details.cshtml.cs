using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Memorial;

public class Details : PageModel
{
    public Core.Models.Memorial? Memorial { get; set; }

    public IMemorialRepository _memorialRepository { get; set; }

    public Details(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        var memorial = await _memorialRepository.GetById(id);

        if (memorial == null)
        {
            return NotFound();
        }

        Memorial = memorial;
        return Page();
    }
}
