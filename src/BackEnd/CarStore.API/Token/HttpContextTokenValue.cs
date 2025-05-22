using CarStore.Domain.Security.Tokens;

namespace CarStore.API.Token
{
    public class HttpContextTokenValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Value()
        {
            var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            var tokenWithOutBearer = authorization.Replace("Bearer ", string.Empty);

            return tokenWithOutBearer;
        }

    }
}
