using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Infrastructure.Services.LoggedUser;

namespace CarStore.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;


        public GetUserProfileUseCase(IMapper mapper, ILoggedUser loggedUser)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();

            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
