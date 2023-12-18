using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RollOfHonour.Core;
using RollOfHonour.Data.Repositories;

namespace RollOfHonour.Web.Controllers;

[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<Whitelists> _whitelists;
    private readonly IOptions<APIBasicAuth> _authDetails;

    public UserController(IUserRepository userRepository, IOptions<Whitelists> whitelists, IOptions<APIBasicAuth> authDetails)
    {
        _userRepository = userRepository;
        _whitelists = whitelists;
        _authDetails = authDetails;
    }

    /*
       Azure AD B2C sends an HTTP request with the client credentials (username and password) in the Authorization header.

       The credentials are formatted as the base64-encoded string username:password.

       Your API then is responsible for checking these values to perform other authorization decisions.
     */

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] BeforeUserCreationInAzureRequest request)
    {
        // Called via API connector in azure b2c post user creation
        try
        {
            ValidateRequest();

            // TODO - check if user already exists
            // TODO - Create the user locally
            // TODO - Assign basic user role to new account
            // TODO - Assign CorrelationId as ObjectId so we can link the local acc to the AD account

            // TEMP - Check against config CSV emails, if the signup email isnt in there return false / invalid 
            var emailWhitelist = _whitelists.Value.SignupEmails.Split(",").ToList();
            if (emailWhitelist.Contains(request.email) is false)
                return BadRequest(new BlockingResponse
                {
                    version = "1.0.0",
                    action = "ShowBlockPage",
                    userMessage = "There was a problem with your request. You are not able to sign up at this time. Please contact your system administrator"
                });

            return Ok(new ContinuationResponse
            {
                version = "1.0.0",
                action = "Continue"
            });
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


            //string userID = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var identity = User.Identity as ClaimsIdentity;

            //identity.AddClaim(new Claim("", "")); // Type is something like Authorization? value is relevant value?

            //// TODO - Get user by object id 
            //// TODO - Attach stored user claims from role to azure user
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

        if (basicUsername != _authDetails.Value.Username && basicPassword != _authDetails.Value.Password)
            throw new UnauthorizedAccessException();
    }


    public class BlockingResponse
    {
        public string version { get; set; }
        public string action { get; set; }
        public string userMessage { get; set; }
    }


    public class ContinuationResponse
    {
        public string version { get; set; }
        public string action { get; set; }

        // example claims we can overwrite anything the user inputs if we wanted
        public string postalCode { get; set; }
        public string extension_extensionsappid_CustomAttribute { get; set; }
    }

    public class BeforeUserCreationInAzureRequest
    {
        public string email { get; set; }
        public Identity[] identities { get; set; }
        public string displayName { get; set; }

        public string? objectId { get; set; } // self-added

        public string givenName { get; set; }
        public string surname { get; set; }
        public string jobTitle { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string extension_extensionsappid_CustomAttribute1 { get; set; }
        public string extension_extensionsappid_CustomAttribute2 { get; set; }
        public string step { get; set; }
        public string client_id { get; set; }
        public string ui_locales { get; set; }
    }

    public class Identity
    {
        public string signInType { get; set; }
        public string issuer { get; set; }
        public string issuerAssignedId { get; set; }
    }

}
