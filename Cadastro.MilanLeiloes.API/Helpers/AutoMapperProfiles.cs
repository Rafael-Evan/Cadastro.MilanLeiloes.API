using AutoMapper;
using Cadastro.MilanLeiloes.API.Dtos;
using Cadastro.MilanLeiloes.Domain.Model;

namespace Cadastro.MilanLeiloes.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
