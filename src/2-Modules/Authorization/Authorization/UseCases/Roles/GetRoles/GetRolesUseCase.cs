// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.GetRoles;

public class GetRolesUseCase : BaseQueryHandler, IRequestHandler<GetRolesRequest, IEnumerable<GetRoleDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public GetRolesUseCase(IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        : base(mapper)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetRoleDto>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetListAsync();

        var rolesDto = _mapper.Map<IEnumerable<GetRoleDto>>(roles);

        foreach (var role in rolesDto)
            role.UsersCount = await _userRepository.CountByRoleAsync(role.Name);

        return rolesDto;
    }
}
