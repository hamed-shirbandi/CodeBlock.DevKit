using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Resources;
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
        var users = await _userRepository.GetByIdAsync(request.Id);
        if (users is null)
            throw new ApplicationException(string.Format(CoreResource.Not_Found, AuthorizationResource.User));

        return _mapper.Map<GetUserDto>(users);
    }
}
