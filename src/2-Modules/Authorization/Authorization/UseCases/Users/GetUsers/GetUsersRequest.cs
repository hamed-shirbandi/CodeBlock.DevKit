using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.GetUsers;

public class GetUsersRequest : BaseQuery<IEnumerable<GetUserDto>>
{
    public GetUsersRequest() { }
}
