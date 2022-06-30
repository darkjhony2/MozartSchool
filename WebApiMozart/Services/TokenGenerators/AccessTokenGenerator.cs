namespace WebApiMozart.Services.TokenGenerators;

using ColegioMozart.Infrastructure.Identity;
using System.Security.Claims;
using WebApiMozart.Models;

public class AccessTokenGenerator
{
    private readonly AuthenticationConfiguration _configuration;
    private readonly TokenGenerator _tokenGenerator;

    public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
    {
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
    }

    public AccessToken GenerateToken(string id,string user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("id", id),
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Name, user),
        };

        DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes);
        string value = _tokenGenerator.GenerateToken(
            _configuration.AccessTokenSecret,
            _configuration.Issuer,
            _configuration.Audience,
            expirationTime,
            claims);

        return new AccessToken()
        {
            Value = value,
            ExpirationTime = expirationTime
        };
    }
}
