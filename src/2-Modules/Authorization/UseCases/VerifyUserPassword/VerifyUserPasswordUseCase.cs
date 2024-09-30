using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Domain.Services;
using MediatR;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Authorization.UseCases.VerifyUserPassword;

public class VerifyUserPasswordUseCase : BaseQueryHandler, IRequestHandler<VerifyUserPasswordRequest, GetUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public VerifyUserPasswordUseCase(IUserRepository userRepository, IEncryptionService encryptionService, IMapper mapper)
        : base(mapper)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<GetUserDto> Handle(VerifyUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailOrMobileAsync(request.EmailOrMobile);
        if (user is null)
            throw new ApplicationException(AuthorizationResource.Invalid_Credentials);

        if (!user.VerifyPassword(_encryptionService, request.Password))
            throw new ApplicationException(AuthorizationResource.Invalid_Credentials);

        return _mapper.Map<GetUserDto>(user);
    }
}
