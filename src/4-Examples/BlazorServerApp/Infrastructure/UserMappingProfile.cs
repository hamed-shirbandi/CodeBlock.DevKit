using AutoMapper;
using BlazorServerApp.Models;

namespace BlazorServerApp.Infrastructure;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
