using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly AuthorizationOptions _authorizationOptions;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IBus bus,
        IOptions<AuthorizationOptions> authorizationOptions
    )
        : base(bus)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _authorizationOptions = authorizationOptions.Value;
    }

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Register(_userRepository, _passwordService, request.Email, request.Password);

        user.AddRole(_authorizationOptions.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
