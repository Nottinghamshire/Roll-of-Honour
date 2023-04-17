using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Memorial;

public class IndexModel : PageModel
{
    public PaginatedList<Core.Models.Memorial> Memorials { get; set; }

    private IMemorialRepository _memorialRepository { get; set; } = null!;

    public int PageNumber { get; set; } = 1;

    public IndexModel(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }

    public async Task<IActionResult> OnGet([FromQuery(Name = "PageIndex")]int? pageIndex)
    {
        if (pageIndex != null)
        {
            PageNumber = (int)pageIndex;
        }

        var memorials = await _memorialRepository.GetPageOfMemorials(PageNumber, 24);

        if (!memorials.Any())
        {
            return NotFound();
        }

        Memorials = memorials;
        return Page();
    }
}
