using AutoMapper;
using CarStore.Communication.Response;
using CarStore.Domain.Repositories.User;
using CarStore.Exceptions;
using CarStore.Exceptions.ExceptionsBase;

namespace CarStore.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserReadOnlyRepository _repository;

        public GetUserProfileUseCase(IMapper mapper, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _mapper = mapper;
            _repository = userReadOnlyRepository;
        }

        public async Task<ResponseUserProfileJson> Execute(Guid id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.USER_NOT_FOUND]);
            }

            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
