using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Infrastructure;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;

public class RegisterUserUseCase : BaseCommandHandler, IRequestHandler<RegisterUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly AuthorizationSettings _authorizationSettings;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IRequestDispatcher requestDispatcher,
        AuthorizationSettings authorizationSettings
    )
        : base(requestDispatcher)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _authorizationSettings = authorizationSettings;
    }

    public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Register(_userRepository, _passwordService, request.Email, request.Password);

        user.AddRole(_authorizationSettings.Roles.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}
