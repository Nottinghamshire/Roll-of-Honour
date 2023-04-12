using Microsoft.AspNetCore.Mvc;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Shared.ViewComponents;

public class OurRecordsCounter : ViewComponent
{
    private readonly IPersonRepository _personRepo;

    public OurRecordsCounter(IPersonRepository personRepository)
    {
        _personRepo = personRepository;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var counter = new RecordCounter()
        {
            PeopleCount = 350,
            MemorialCount = 100
        };

        return View(counter);
    }
}