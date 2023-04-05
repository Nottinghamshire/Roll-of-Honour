using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

public class PeopleModel : PageModel
{
    public PaginatedList<Core.Models.Person> People { get; set; }
    private IPersonRepository _personRepository { get; set; } = null!;

    public int PageNumber { get; set; } = 1;

    public PeopleModel(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IActionResult> OnGet([FromQuery(Name = "PageIndex")] int? pageIndex)
    {
        if (pageIndex != null)
        {
            PageNumber = (int)pageIndex;
        }

        var people = await _personRepository.GetPageOfPeople(PageNumber, 24);

        if (!people.Any())
        {
            return NotFound();
        }

        People = people;
        return Page();
    }
}
