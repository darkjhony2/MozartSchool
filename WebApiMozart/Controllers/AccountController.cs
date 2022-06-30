using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Mvc;
using WebApiMozart.Services;

namespace WebApiMozart.Controllers;

public class AccountController : RestApiControllerBase
{
    private const string LocalLoginProvider = "Local";

    private IIdentityService _identityService;
    private readonly Authenticator _authenticator;
    public ISecureDataFormat<AuthenticationTicket> _accessTokenFormat { get; private set; }

    public AccountController(
        /*ISecureDataFormat<AuthenticationTicket> accessTokenFormat,*/
        IIdentityService identityService, Authenticator authenticator)
    {
        //_accessTokenFormat = accessTokenFormat;
        _identityService = identityService;
        _authenticator = authenticator;
    }


    /*[AllowAnonymous]
    [Route("Register")]
    public async Task<IActionResult> Register()
    {
       

        return Ok();
    }
    */

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> GenerateToken([FromBody] LoginModel model)
    {
        //_accessTokenFormat.
        var isValid = await _identityService.VerifyCredentialsAsync(model.username, model.password);

        if (!isValid.Item1)
        {
            return Unauthorized();
        }


        AuthenticatedUserResponse response = await _authenticator.Authenticate(isValid.Item2, model.username);

        return Ok(response);
    }

    public record LoginModel(string username, string password);

}
