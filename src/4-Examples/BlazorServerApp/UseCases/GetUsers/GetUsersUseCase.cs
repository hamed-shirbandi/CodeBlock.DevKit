using AutoMapper;
using BlazorServerApp.Infrastructure;
using BlazorServerApp.Models;
using CodeBlock.DevKit.Application.Queries;
using MediatR;

namespace BlazorServerApp.UseCases.GetUsers;

public class GetUsersUseCase : BaseQueryHandler, IRequestHandler<GetUsersRequest, IEnumerable<UserDto>>
{
    private readonly Database _database;

    public GetUsersUseCase(IMapper mapper, Database database)
        : base(mapper)
    {
        _database = database;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _database.Users.ToList();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
