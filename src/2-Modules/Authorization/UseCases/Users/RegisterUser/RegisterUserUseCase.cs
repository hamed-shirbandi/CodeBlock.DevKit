using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly AuthorizationOptions _authorizationOptions;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        IBus bus,
        IOptions<AuthorizationOptions> authorizationOptions
    )
        : base(bus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _authorizationOptions = authorizationOptions.Value;
    }

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var passwordSalt = _encryptionService.CreateSaltKey(5);
        var passwordHash = _encryptionService.CreatePasswordHash(request.Password, passwordSalt);

        var user = User.Register(_userRepository, request.Email, passwordSalt, passwordHash);

        user.AddRole(_authorizationOptions.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
