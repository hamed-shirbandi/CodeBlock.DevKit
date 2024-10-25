using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Core.Resources;
using MediatR;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.UpdateUser;

public class UpdateUserUseCase : BaseCommandHandler, IRequestHandler<UpdateUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _userRepository = userRepository;
    }

    public async Task<CommandResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
            throw new ApplicationException(string.Format(CommonResource.Not_Found, AuthorizationResource.User));

        user.Update(_userRepository, request.Email);

        await _userRepository.UpdateAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
