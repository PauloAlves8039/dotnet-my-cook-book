using AutoMapper;
using MyCookBook.Application.Services.Automapper;

namespace Utils.ForTest.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Instance()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfig>();
            });

            return configuration.CreateMapper();
        }
    }
}
