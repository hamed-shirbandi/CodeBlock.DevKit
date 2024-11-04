using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Core.Resources;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.ChangeUserPassword;

public class ChangeUserPasswordUseCase : BaseCommandHandler, IRequestHandler<ChangeUserPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public ChangeUserPasswordUseCase(IUserRepository userRepository, IEncryptionService encryptionService, IBus bus)
        : base(bus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<CommandResult> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw new ApplicationException(string.Format(CoreResource.Not_Found, AuthorizationResource.User_Email));

        user.SetPassword(_encryptionService, request.Password);

        await _userRepository.UpdateAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
