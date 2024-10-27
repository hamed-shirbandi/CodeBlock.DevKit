using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Core.Resources;
using MediatR;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.UpdateRole;

public class UpdateRoleUseCase : BaseCommandHandler, IRequestHandler<UpdateRoleRequest, CommandResult>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleUseCase(IRoleRepository roleRepository, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _roleRepository = roleRepository;
    }

    public async Task<CommandResult> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role is null)
            throw new ApplicationException(string.Format(CoreResource.Not_Found, Resources.AuthorizationResource.Role));

        role.Update(_roleRepository, request.Name);

        await _roleRepository.UpdateAsync(role);

        return CommandResult.Create(entityId: role.Id);
    }
}
