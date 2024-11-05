using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Resources;
using MediatR;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

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
            throw new ApplicationException(AuthorizationResource.User_Email_Is_Wrong);

        if (!user.IsValidPassword(_passwordService, request.Password))
            throw new ApplicationException(AuthorizationResource.User_Password_Is_Wrong);

        return _mapper.Map<GetUserDto>(user);
    }
}
