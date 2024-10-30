using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.CreateRole;

public class CreateRoleUseCase : BaseCommandHandler, IRequestHandler<CreateRoleRequest, CommandResult>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleUseCase(IRoleRepository roleRepository, IBus bus)
        : base(bus)
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
