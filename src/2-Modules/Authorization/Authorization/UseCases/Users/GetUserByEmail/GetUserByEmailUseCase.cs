// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Exceptions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.GetUserByEmail;

public class GetUserByEmailUseCase : BaseQueryHandler, IRequestHandler<GetUserByEmailRequest, GetUserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailUseCase(IMapper mapper, IUserRepository userRepository)
        : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserDto> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw AuthorizationExceptions.UserNotFound(request.Email);

        return _mapper.Map<GetUserDto>(user);
    }
}

