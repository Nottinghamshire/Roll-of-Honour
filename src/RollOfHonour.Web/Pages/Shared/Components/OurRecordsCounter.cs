using Microsoft.AspNetCore.Mvc;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Shared.ViewComponents;

public class OurRecordsCounter : ViewComponent
{
    private readonly IPersonRepository _personRepo;
    private readonly IMemorialRepository _memorialRepo;

    public OurRecordsCounter(IPersonRepository personRepository, IMemorialRepository memorialRepository)
    {
        _personRepo = personRepository;
        _memorialRepo = memorialRepository;
    }
    
    public IViewComponentResult Invoke()
    {
        var peopleCount = _personRepo.Count();
        var memorialCount = _memorialRepo.Count();
        var counter = new RecordCounter()
        {
            PeopleCount = peopleCount,
            MemorialCount = memorialCount 
        };

        return View(counter);
    }
}