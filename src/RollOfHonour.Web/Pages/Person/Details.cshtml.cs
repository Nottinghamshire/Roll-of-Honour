using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

public class Details : PageModel
{
  public Core.Models.Person Person { get; set; }

  private IPersonRepository _personRepository { get; set; } = null!;

  public Details(IPersonRepository personRepository)
  {
    _personRepository = personRepository;
  }

  public async Task<IActionResult> OnGet(int id)
  {
    var person = await _personRepository.GetById(id);

    if (person == null)
    {
      return NotFound();
    }

    Person = person;
    return Page();
  }
}
