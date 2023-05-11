using AutoMapper;
using MyCookBook.Communication.Requests;

namespace MyCookBook.Application.Services.Automapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destiny => destiny.Password, config => config.Ignore());
        }
    }
}
