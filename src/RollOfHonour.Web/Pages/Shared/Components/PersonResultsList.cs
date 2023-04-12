using Microsoft.AspNetCore.Mvc;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Shared.ViewComponents;

public class PersonResultsList : ViewComponent
{
    public PersonResultsList()
    {
    }
    
    public IViewComponentResult Invoke(List<PersonSearchResult> people)
    {
        return View(people);
    }
}