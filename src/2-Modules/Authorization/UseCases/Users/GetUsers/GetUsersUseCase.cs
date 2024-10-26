using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.GetUsers;

public class GetUsersUseCase : BaseQueryHandler, IRequestHandler<GetUsersRequest, IEnumerable<GetUserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersUseCase(IMapper mapper, IUserRepository userRepository)
        : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetUserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetListAsync();

        return _mapper.Map<IEnumerable<GetUserDto>>(users);
    }
}
