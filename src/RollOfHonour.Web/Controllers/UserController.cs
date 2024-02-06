using System;
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
    private readonly ILogger<UserController> _logger;
    
    private readonly IUserRepository _userRepository;
    private readonly IOptions<Whitelists> _whitelists;
    private readonly IOptions<APIBasicAuth> _authDetails;
    
    public UserController(ILogger<UserController> logger, IUserRepository userRepository, IOptions<Whitelists> whitelists, IOptions<APIBasicAuth> authDetails)
    {
        _logger = logger;
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

            _logger.LogInformation("CreateUser API Connector called during the sign up process");

            var emailWhitelist = _whitelists.Value.SignupEmails.Split(",").ToList();
            if (emailWhitelist.Contains(request.email) is false)
            {
                _logger.LogInformation($"CreateUser API Connector Email {request.email} is not whitelisted, returning BlockingResponse");

                return Ok(new BlockingResponse
                {
                    version = "1.0.0",
                    action = "ShowBlockPage",
                    userMessage = "There was a problem with your request. You are not able to sign up at this time. Please contact your system administrator"
                });
            }

            _logger.LogInformation($"CreateUser API Connector {request.email} is whitelisted, returning ContinuationResponse");

            return Ok(new ContinuationResponse
            {
                version = "1.0.0",
                action = "Continue"
            });
        }
        catch (UnauthorizedAccessException exception)
        {
            _logger.LogError($"CreateUser API Connector Unauthorized Exception thrown during Sign up Process", new { exception } );

            return Unauthorized();
        }
        catch (Exception exception) 
        {
            _logger.LogError($"CreateUser API Connector Exception thrown during Sign up Process", new { exception });

            return BadRequest();
        }
    }

    [HttpPost("updateclaims")]
    public async Task<IActionResult> AttachUserClaims()
    {
        try
        {
            ValidateRequest();

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
        _logger.LogInformation("CreateUser API Connector Trying to validate request");
        AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authHeader);

        if (authHeader is null)
        {
            _logger.LogInformation("CreateUser API Connector AuthHeader is null");
            throw new UnauthorizedAccessException();
        }

        if (authHeader.Parameter != null && authHeader.Parameter.StartsWith("Basic ") is false)
        {
            _logger.LogInformation("CreateUser API Connector AuthHeader is missing Basic auth credentials");
            throw new UnauthorizedAccessException();
        }

        var basicAuthPrefix = "Basic ";
        var base64Credentials = authHeader!.Parameter!.Substring(basicAuthPrefix.Length).Trim();
        string userPass = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));

        var basicUsername = userPass.Substring(0, userPass.IndexOf(':'));
        var basicPassword = userPass.Substring(userPass.IndexOf(':') + 1);

        if (basicUsername != _authDetails.Value.Username && basicPassword != _authDetails.Value.Password)
        {
            _logger.LogInformation("CreateUser API Connector Basic Auth details are incorrect");
            throw new UnauthorizedAccessException();
        }
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
