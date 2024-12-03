// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.CreateRole;

public class CreateRoleUseCase : BaseCommandHandler, IRequestHandler<CreateRoleRequest, CommandResult>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleUseCase(IRoleRepository roleRepository, IRequestDispatcher requestDispatcher)
        : base(requestDispatcher)
    {
        _roleRepository = roleRepository;
    }

    public async Task<CommandResult> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = Role.Create(_roleRepository, request.Name);

        await _roleRepository.AddAsync(role);

        return CommandResult.Create(entityId: role.Id);
    }
}
