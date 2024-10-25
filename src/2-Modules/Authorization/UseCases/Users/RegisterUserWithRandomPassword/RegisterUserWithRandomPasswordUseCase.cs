using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordUseCase : BaseCommandHandler, IRequestHandler<RegisterUserWithRandomPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly AuthorizationOptions _options;

    public RegisterUserWithRandomPasswordUseCase(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        IInMemoryBus inMemoryBus,
        IOptions<AuthorizationOptions> options
    )
        : base(inMemoryBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _options = options.Value;
    }

    public async Task<CommandResult> Handle(RegisterUserWithRandomPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = User.Register(_userRepository, request.Email);

        user.AddRole(_options.DefaultRole);

        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);

        user.SetPassword(_encryptionService, randomPassword);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
