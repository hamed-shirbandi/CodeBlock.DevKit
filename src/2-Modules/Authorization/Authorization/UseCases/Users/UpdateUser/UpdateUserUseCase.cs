// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Exceptions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.UpdateUser;

public class UpdateUserUseCase : BaseCommandHandler, IRequestHandler<UpdateUserRequest, CommandResult>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(IUserRepository userRepository, IRequestDispatcher requestDispatcher)
        : base(requestDispatcher)
    {
        _userRepository = userRepository;
    }

    public async Task<CommandResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
            throw AuthorizationExceptions.UserNotFound(request.Id);

        var loadedVersion = user.Version;

        user.Update(_userRepository, request.Email);

        await _userRepository.ConcurrencySafeUpdateAsync(user, loadedVersion);

        return CommandResult.Create(entityId: user.Id);
    }
}
