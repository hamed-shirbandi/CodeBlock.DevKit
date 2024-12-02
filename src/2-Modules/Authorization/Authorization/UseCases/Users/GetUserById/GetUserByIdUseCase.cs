// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Exceptions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.GetUserById;

public class GetUserByIdUseCase : BaseQueryHandler, IRequestHandler<GetUserByIdRequest, GetUserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IMapper mapper, IUserRepository userRepository)
        : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
            throw AuthorizationExceptions.UserNotFound(request.Id);

        return _mapper.Map<GetUserDto>(user);
    }
}

