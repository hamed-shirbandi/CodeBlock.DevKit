using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordUseCase : BaseCommandHandler, IRequestHandler<RegisterUserWithRandomPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly AuthorizationOptions _options;

    public RegisterUserWithRandomPasswordUseCase(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IBus bus,
        IOptions<AuthorizationOptions> options
    )
        : base(bus)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _options = options.Value;
    }

    public async Task<CommandResult> Handle(RegisterUserWithRandomPasswordRequest request, CancellationToken cancellationToken)
    {
        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);

        var user = User.Register(_userRepository, _passwordService, request.Email, randomPassword);

        user.AddRole(_options.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
