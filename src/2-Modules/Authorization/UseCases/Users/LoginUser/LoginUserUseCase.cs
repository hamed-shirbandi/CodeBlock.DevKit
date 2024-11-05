using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain.Users;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Exceptions;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.LoginUser;

public class LoginUserUseCase : BaseQueryHandler, IRequestHandler<LoginUserRequest, GetUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public LoginUserUseCase(IUserRepository userRepository, IPasswordService passwordService, IMapper mapper)
        : base(mapper)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<GetUserDto> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw AuthorizationExceptions.UserNotFound(request.Email);

        if (!user.IsValidPassword(_passwordService, request.Password))
            throw AuthorizationExceptions.InvalidPassword();

        return _mapper.Map<GetUserDto>(user);
    }
}
