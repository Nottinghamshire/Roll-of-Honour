using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

public class PeopleModel : PageModel
{
  public List<Core.Models.Person>? People { get; set; }
  private IPersonRepository _personRepository { get; set; } = null!;

  public PeopleModel(IPersonRepository personRepository)
  {
    _personRepository = personRepository;
  }

  public async Task<IActionResult> OnGet()
  {
    var people = await _personRepository.GetAll();
    
    if (!people.Any())
    {
      return NotFound();
    }

    People = people.ToList();
    return Page();
  }
}
