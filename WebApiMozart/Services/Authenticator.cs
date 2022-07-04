using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Identity;
using WebApiMozart.Models;
using WebApiMozart.Services.TokenGenerators;

namespace WebApiMozart.Services;

public class Authenticator
{
    private readonly AccessTokenGenerator _accessTokenGenerator;
    // private readonly RefreshTokenGenerator _refreshTokenGenerator;
    // private readonly IRefreshTokenRepository _refreshTokenRepository;

    public Authenticator(AccessTokenGenerator accessTokenGenerator
       // RefreshTokenGenerator refreshTokenGenerator,
       /* IRefreshTokenRepository refreshTokenRepository*/)
    {
        _accessTokenGenerator = accessTokenGenerator;
        // _refreshTokenGenerator = refreshTokenGenerator;
        // _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthenticatedUserResponse> Authenticate(string id, string user, string role)
    {
        AccessToken accessToken = _accessTokenGenerator.GenerateToken(id, user);
        /* string refreshToken = _refreshTokenGenerator.GenerateToken();

         RefreshToken refreshTokenDTO = new RefreshToken()
         {
             Token = refreshToken,
             UserId = user.Id
         };*/
        //await _refreshTokenRepository.Create(refreshTokenDTO);

        return new AuthenticatedUserResponse()
        {
            AccessToken = accessToken.Value,
            AccessTokenExpirationTime = accessToken.ExpirationTime,
            Role = role
            //RefreshToken = refreshToken
        };
    }
}
