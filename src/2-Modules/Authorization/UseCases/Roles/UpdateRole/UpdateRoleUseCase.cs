using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Authorization.Exceptions;
using CodeBlock.DevKit.Core.Helpers;
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

        role.Update(_roleRepository, request.Name);

        await _roleRepository.UpdateAsync(role);

        return CommandResult.Create(entityId: role.Id);
    }
}
