using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.SetRandomUserPassword;

public class ChangeUserPasswordUseCase : BaseCommandHandler, IRequestHandler<ChangeUserPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public ChangeUserPasswordUseCase(IUserRepository userRepository, IEncryptionService encryptionService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<CommandResult> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailOrMobileAsync(request.EmailOrMobile);
        if (user is null)
            throw new ApplicationException(AuthorizationResource.Invalid_UserName);

        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);

        user.SetPassword(_encryptionService, randomPassword);

        await _userRepository.UpdateAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
