using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RollOfHonour.Data.Repositories;

namespace RollOfHonour.Web.Controllers;

[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /*
       Azure AD B2C sends an HTTP request with the client credentials (username and password) in the Authorization header.

       The credentials are formatted as the base64-encoded string username:password.

       Your API then is responsible for checking these values to perform other authorization decisions.
     */

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser()
    {
        // Called via API connector in azure b2c post user creation
        try
        {
            ValidateRequest();

            // TODO - check if user already exists
            // TODO - Create the user locally
            // TODO - Assign basic user role to new account
            // TODO - Assign CorrelationId as ObjectId so we can link the local acc to the AD account


            return NotFound();
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

    [HttpPost("updateclaims")]
    public async Task<IActionResult> AttachUserClaims()
    {
        // Going to be called via API connector once the user is created and when azure starts adding claims

        try
        {
            ValidateRequest();

            // TODO - Get user by object id 
            // TODO - Attach stored user claims from role to azure user
            return NotFound();
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

    private void ValidateRequest()
    {
        // Temporary (possibly) until we have time to add certs instead
        AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authHeader);

        if (authHeader is null)
            throw new UnauthorizedAccessException();

        if (authHeader.Parameter != null && authHeader.Parameter.StartsWith("Basic ") is false)
            throw new UnauthorizedAccessException();

        var basicAuthPrefix = "Basic ";
        var base64Credentials = authHeader!.Parameter!.Substring(basicAuthPrefix.Length).Trim();
        string userPass = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));

        var basicUsername = userPass.Substring(0, userPass.IndexOf(':'));
        var basicPassword = userPass.Substring(userPass.IndexOf(':') + 1);

        if (basicUsername != "" && basicPassword != "") // update to user and pass setup in Azure
            throw new UnauthorizedAccessException();
    }
}
