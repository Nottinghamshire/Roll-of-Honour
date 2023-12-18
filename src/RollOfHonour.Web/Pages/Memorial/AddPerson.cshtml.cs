using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Core.Enums;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Memorial;

//[Authorize(Policy = AuthorizationPolicyNames.EditMemorial)]

[Authorize]
public class AddPerson : PageModel
{
    [BindProperty] public Core.Models.Memorial Memorial { get; set; } = default!;

    [BindProperty] public Core.Models.Person Person { get; set; } = default!;

    [BindProperty] public bool IsMilitary { get; set; } = false;

    [BindProperty] public War War { get; set; }

    public IMemorialRepository _memorialRepository { get; set; }

    public AddPerson(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        try
        {
            VerifyAuthorizedRequest();
            
            if (id is null)
                return NotFound();

            var memorial = await _memorialRepository.GetById((int)id);
            if (memorial == null)
                return NotFound();

            Memorial = memorial;
            Person = new Core.Models.Person();

            return Page();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            VerifyAuthorizedRequest(); // do we need to do this manually when using [Authorize] ?

            Person.PersonType = IsMilitary ? PersonType.Military : PersonType.Civilian;

            Person.WarId = War != 0 ? (War == War.WW1 ? 1 : 2) : Person.WarId;
            await _memorialRepository.AddPerson(Memorial.Id, Person);
            return RedirectToAction("Details", "Memorial",
                new { id = Memorial.Id });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    private void VerifyAuthorizedRequest()
    {
        if (Request.HttpContext.User.Identity != null)
        {
            if (Request.HttpContext.User.Identity.IsAuthenticated is false)
                throw new UnauthorizedAccessException();
        }
        else throw new UnauthorizedAccessException();
    }
}
