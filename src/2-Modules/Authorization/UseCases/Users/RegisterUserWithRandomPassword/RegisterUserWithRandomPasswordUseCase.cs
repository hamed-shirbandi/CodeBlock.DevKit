using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordUseCase : BaseCommandHandler, IRequestHandler<RegisterUserWithRandomPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly AuthorizationSettings _options;

    public RegisterUserWithRandomPasswordUseCase(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        IBus bus,
        IOptions<AuthorizationSettings> options
    )
        : base(bus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _options = options.Value;
    }

    public async Task<CommandResult> Handle(RegisterUserWithRandomPasswordRequest request, CancellationToken cancellationToken)
    {
        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);
        var passwordSalt = _encryptionService.CreateSaltKey(5);
        var passwordHash = _encryptionService.CreatePasswordHash(randomPassword, passwordSalt);

        var user = User.Register(_userRepository, request.Email, passwordSalt, passwordHash);

        user.AddRole(_options.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
