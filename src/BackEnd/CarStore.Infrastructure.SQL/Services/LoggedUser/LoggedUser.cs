using System.IdentityModel.Tokens.Jwt;
using CarStore.Domain.Entities;
using CarStore.Domain.Security.Tokens;
using CarStore.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly CarStoreDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CarStoreDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == "id").Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Active && user.Id == userIdentifier);
    }
}
