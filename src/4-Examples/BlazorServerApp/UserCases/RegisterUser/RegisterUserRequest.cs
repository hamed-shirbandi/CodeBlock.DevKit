using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;

namespace BlazorServerApp.UserCases.RegisterUser;

public class RegisterUserRequest : BaseCommand
{
    private RegisterUserRequest(string userName)
    {
        UserName = userName;
    }

    public static RegisterUserRequest Create(string userName)
    {
        return new RegisterUserRequest(userName);
    }

    [Required]
    [MaxLength(6, ErrorMessage = "UserName is limitted to 6 characters")]
    public string UserName { get; }
}
