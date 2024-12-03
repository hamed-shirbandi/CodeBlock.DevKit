// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<Role, GetRoleDto>();
    }
}
