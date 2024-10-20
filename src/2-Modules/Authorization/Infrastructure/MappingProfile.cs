using AutoMapper;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserDto>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email ?? src.Mobile));
    }
}
