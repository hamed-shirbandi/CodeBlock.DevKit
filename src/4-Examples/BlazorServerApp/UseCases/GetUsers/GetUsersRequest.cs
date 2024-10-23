using BlazorServerApp.Models;
using CodeBlock.DevKit.Application.Queries;

namespace BlazorServerApp.UseCases.GetUsers;

public class GetUsersRequest : BaseQuery<IEnumerable<UserDto>>
{
    public GetUsersRequest() { }
}
