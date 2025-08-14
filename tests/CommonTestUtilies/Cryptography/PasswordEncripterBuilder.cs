using CarStore.Application.Services.Cryptography;
using CarStore.Domain.Security.Cryptography;

namespace CommonTestUtilies.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("ABC123");
    }
}
