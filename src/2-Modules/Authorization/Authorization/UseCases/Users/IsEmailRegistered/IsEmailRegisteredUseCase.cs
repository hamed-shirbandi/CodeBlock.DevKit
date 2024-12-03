// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain.Users;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.IsEmailRegistered;

public class IsEmailRegisteredUseCase : BaseQueryHandler, IRequestHandler<IsEmailRegisteredRequest, bool>
{
    private readonly IUserRepository _userRepository;

    public IsEmailRegisteredUseCase(IMapper mapper, IUserRepository userRepository)
        : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(IsEmailRegisteredRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        return user != null;
    }
}
