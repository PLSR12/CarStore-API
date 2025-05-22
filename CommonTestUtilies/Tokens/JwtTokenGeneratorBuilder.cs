using CarStore.Domain.Security.Tokens;
using CarStore.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilies.Tokens
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 15, signingKey: "fffffffffffffffffffffffffffffffffffffffffffffffffffff");
    }
}
