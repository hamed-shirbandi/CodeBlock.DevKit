// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Core.Extensions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordUseCase : BaseCommandHandler, IRequestHandler<RegisterUserWithRandomPasswordRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly AuthorizationSettings _authorizationSettings;

    public RegisterUserWithRandomPasswordUseCase(
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

    public async Task<CommandResult> Handle(RegisterUserWithRandomPasswordRequest request, CancellationToken cancellationToken)
    {
        var randomPassword = RandomDataGenerator.GetRandomNumber(length: 4);

        var user = User.Register(_userRepository, _passwordService, request.Email, randomPassword);

        user.AddRole(_authorizationSettings.Roles.DefaultRole);

        await _userRepository.AddAsync(user);

        return CommandResult.Create(entityId: user.Id);
    }
}

