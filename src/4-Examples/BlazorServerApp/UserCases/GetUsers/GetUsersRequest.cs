using BlazorServerApp.Models;
using CodeBlock.DevKit.Application.Queries;

namespace BlazorServerApp.UserCases.GetUsers;

public class GetUsersRequest : BaseQuery<IEnumerable<UserDto>>
{
    public GetUsersRequest() { }
}
