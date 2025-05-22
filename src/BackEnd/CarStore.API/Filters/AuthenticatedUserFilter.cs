using CarStore.Communication.Response;
using CarStore.Domain.Repositories.User;
using CarStore.Domain.Security.Tokens;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace CarStore.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {

        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;

        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {

                var token = TokenOnRequest(context);

                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);

                if (!exist)
                {
                    throw new CarStoreException(ResourceMessagesException.USER_NOT_FOUND);
                }
            }
            catch (CarStoreException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.INVALID_TOKEN)
                {
                    TokenIsExpired = true
                });
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_ACCESS_RESOURCE));
            }
        }

        public static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var token = context
                .HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(token))
            {
                throw new CarStoreException(ResourceMessagesException.INVALID_TOKEN);
            }

            var tokenWithOutBearer = token.Replace("Bearer ", string.Empty);

            return tokenWithOutBearer;
        }
    }
}
