using AutoMapper;
using CarStore.Application.Services.AutoMapper;

namespace CommonTestUtilies.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            return new AutoMapper.MapperConfiguration(options =>
             {
                 options.AddProfile(new AutoMapping());
             }).CreateMapper();
        }
    }
}
