using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.Api.Models;

public class LoginUserResponse
{
    public string Token { get; set; }
    public GetUserDto User { get; set; }
}
