using System.IdentityModel.Tokens.Jwt;
using CarStore.Application.Services.Security.Tokens.Access;
using CarStore.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace CarStore.Application.Services.Security.Tokens.Access.Validator;

public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

    public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var valdiationsParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, valdiationsParameter, out _);

        var userIdentifier = principal.Claims.First(c => c.Type == "id").Value;

        return Guid.Parse(userIdentifier);
    }
}
