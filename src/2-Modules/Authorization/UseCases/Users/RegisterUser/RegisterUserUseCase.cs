using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly AuthorizationOptions _options;

    public RegisterUserUseCase(
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

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Register(_userRepository, request.Email);

        user.SetPassword(_encryptionService, request.Password);

        user.AddRole(_options.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
