﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Web.Controllers;

public class MemorialController : Controller
{
    private readonly IMemorialRepository _memorialRepository;

    public MemorialController(IMemorialRepository memorialRepository)
    {
        _memorialRepository = memorialRepository;
    }

    [HttpPost]
    //[Authorize(Policy = AuthorizationPolicyNames.EditMemorial)]
    [Authorize]
    public async Task<IActionResult> AddPerson(int? memorialId, Person? person)
    {
        try
        {
            if (memorialId.HasValue && person is not null)
                await _memorialRepository.AddPerson((int)memorialId, person);

            return LocalRedirect($"/Memorial/Details?id={memorialId}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    //[Authorize(Policy = AuthorizationPolicyNames.EditMemorial)]
    [Authorize]
    public async Task<IActionResult> RemovePerson(int? memorialId, int? citizenId)
    {
        try
        {
            if(memorialId.HasValue && citizenId.HasValue)
                await _memorialRepository.RemovePerson((int)memorialId, (int)citizenId);

            return LocalRedirect($"/Memorial/Details?id={memorialId}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
