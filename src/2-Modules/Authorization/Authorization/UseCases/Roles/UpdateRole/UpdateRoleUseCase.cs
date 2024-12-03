// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Exceptions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.UpdateRole;

public class UpdateRoleUseCase : BaseCommandHandler, IRequestHandler<UpdateRoleRequest, CommandResult>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleUseCase(IRoleRepository roleRepository, IRequestDispatcher requestDispatcher)
        : base(requestDispatcher)
    {
        _roleRepository = roleRepository;
    }

    public async Task<CommandResult> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role is null)
            throw AuthorizationExceptions.RoleNotFound(request.Id);

        var loadedVersion = role.Version;

        role.Update(_roleRepository, request.Name);

        await _roleRepository.ConcurrencySafeUpdateAsync(role, loadedVersion);

        return CommandResult.Create(entityId: role.Id);
    }
}
