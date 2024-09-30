using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordUseCase : BaseCommandHandler, IRequestHandler<RegisterUserWithRandomPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public RegisterUserWithRandomPasswordUseCase(IUserRepository userRepository, IEncryptionService encryptionService, IInMemoryBus inMemoryBus)
        : base(inMemoryBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<CommandResult> Handle(RegisterUserWithRandomPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = User.RegisterWithoutPassword(_userRepository, request.Mobile, request.Email);

        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);

        user.ChangePassword(_userRepository, _encryptionService, randomPassword);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
