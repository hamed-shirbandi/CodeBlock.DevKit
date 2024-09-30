using AutoMapper;
using BlazorServerApp.Models;

namespace BlazorServerApp.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
