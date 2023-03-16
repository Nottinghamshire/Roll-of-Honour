using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Memorial;

public class IndexModel : PageModel
{
    public List<Core.Models.Memorial> Memorials { get; set; }

    private IMemorialRepository _memorialRepository { get; set; } = null!;

    public IndexModel(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        var memorials = await _memorialRepository.GetAll();

        if (!memorials.Any())
        {
            return NotFound();
        }

        Memorials = memorials.ToList();
        return Page();
    }
}
