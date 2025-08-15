using AutoMapper;
using CarStore.Application.Dtos.Requests;
using CarStore.Communication.Requests;
using CarStore.Communication.Response;

namespace CarStore.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RequestVehicleJson, Domain.Entities.Vehicle>();
            CreateMap<RequestBrandJson, Domain.Entities.Brand>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

            CreateMap<Domain.Entities.User, ResponseUserBasicJson>();

            CreateMap<Domain.Entities.Brand, ResponseBrandJson>();

            CreateMap<Domain.Entities.TypesVehicle, ResponseVehicleTypeJson>();

            CreateMap<Domain.Entities.Vehicle, ResponseVehicleJson>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        }
    }
}
