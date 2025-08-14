
using CarStore.Application.Services.AutoMapper;
using CarStore.Application.UseCases.Brand.GetAll;
using CarStore.Application.UseCases.Login.DoLogin;
using CarStore.Application.UseCases.User.ChangePassword;
using CarStore.Application.UseCases.User.Profile;
using CarStore.Application.UseCases.User.Register;
using CarStore.Application.UseCases.User.Update;
using CarStore.Application.UseCases.Vehicle;
using CarStore.Application.UseCases.Vehicle.Delete;
using CarStore.Application.UseCases.Vehicle.Register;
using CarStore.Application.UseCases.Vehicle.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarStore.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplications(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();


            services.AddScoped(options => autoMapper);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IGetByIdVehicleUseCase, GetByIdVehicleUseCase>();
            services.AddScoped<IRegisterVehicleUseCase, RegisterVehicleUseCase>();
            services.AddScoped<IDeleteVehicleUseCase, DeleteVehicleUseCase>();
            services.AddScoped<IUpdateVehicleUseCase, UpdateVehicleUseCase>();
            services.AddScoped<IGetBrandUseCase, GetBrandUseCase>();
        }
    }
}
