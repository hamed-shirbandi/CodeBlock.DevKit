using AutoMapper;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<Role, GetRoleDto>();
    }
}
