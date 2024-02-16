﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Core.Enums;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Pages.Person;

//[Authorize(Policy = AuthorizationPolicyNames.EditPerson)]
[Authorize]
public class Edit : PageModel
{
    [BindProperty] public Core.Models.Person Person { get; set; } = default!;

    [BindProperty] public Core.Models.Person EditFormPerson { get; set; } = default!;

    [BindProperty] public War War { get; set; }
    
    public string ErrorMessage { get; set; }

    private IPersonRepository _personRepository;

    public Edit(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        try
        {
            VerifyAuthorizedRequest();

            if (id is null)
                return NotFound();

            var person = await _personRepository.GetById((int)id);
            if (person == null)
                return NotFound();

            Person = person;
            EditFormPerson = person;
            War = person.War;

            return Page();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            VerifyAuthorizedRequest();

            if (!ModelState.IsValid)
            {
                var modelStateErrors = ModelState.Values.Where(parameter => parameter.ValidationState == ModelValidationState.Invalid).SelectMany(state => state.Errors.Select(error => error.ErrorMessage)).ToList();
                if (modelStateErrors.Any())
                    ErrorMessage = string.Join(", ", modelStateErrors);
                return Page();
            }

            // War isn't binding properly?
            Person.WarId = War != 0 ? (War == War.WW1 ? 1 : 2) : Person.WarId;
            Person.Rank = EditFormPerson.Rank;
            Person.FirstNames = EditFormPerson.FirstNames;
            Person.LastName = EditFormPerson.LastName;
            Person.ServiceNumber = EditFormPerson.ServiceNumber;
            Person.AddressAtEnlistment = EditFormPerson.AddressAtEnlistment;
            Person.Regiment = EditFormPerson.Regiment;
            Person.PlaceOfBirth = EditFormPerson.PlaceOfBirth;
            Person.EmploymentHobbies = EditFormPerson.EmploymentHobbies;
            Person.FamilyHistory = EditFormPerson.FamilyHistory;
            Person.Unit = EditFormPerson.Unit;
            Person.MilitaryHistory = EditFormPerson.MilitaryHistory;
            Person.ExtraInfo = EditFormPerson.ExtraInfo;

            await _personRepository.Update(Person);

            return RedirectToAction("Details", "Person", new { id = Person.Id });
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
        // Needs updating to check against roles when implemented
        if (Request.HttpContext.User.Identity != null)
        {
            if (Request.HttpContext.User.Identity.IsAuthenticated is false)
                throw new UnauthorizedAccessException();
        }
        else throw new UnauthorizedAccessException();
    }
}
