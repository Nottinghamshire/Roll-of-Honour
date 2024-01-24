﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

public class Details : PageModel
{
    public Core.Models.Person? Person { get; set; }
    public bool UserIsAuthenticated { get; set; }

    private IPersonRepository PersonRepository { get; set; }

    public Details(IPersonRepository personRepository)
    {
        PersonRepository = personRepository;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        if (Request.HttpContext.User.Identity != null)
            UserIsAuthenticated = Request.HttpContext.User.Identity.IsAuthenticated;

        var person = await PersonRepository.GetById(id);

        if (person == null)
        {
            return NotFound();
        }

        Person = person;
        return Page();
    }
}
