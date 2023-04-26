using Microsoft.AspNetCore.Mvc;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Shared.ViewComponents;

public class MemoryDetailsList : ViewComponent
{
    private readonly IPersonRepository _personRepo;

    public MemoryDetailsList(IPersonRepository personRepository)
    {
        _personRepo = personRepository;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var today = DateTime.Now;
        var people = await _personRepo.DiedOnThisDay(today);
        return View(people);
    }
}