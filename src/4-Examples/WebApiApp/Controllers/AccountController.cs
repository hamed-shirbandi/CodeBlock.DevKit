using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUser;
using CodeBlock.DevKit.Authorization.UseCases.Users.VerifyUserPassword;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers;

[Route("[controller]")]
public class AccountController : BaseApiController
{
    public AccountController(IInMemoryBus inMemoryBus)
        : base(inMemoryBus) { }

    [HttpPost(Name = "login")]
    public async Task<Result<GetUserDto>> Login(VerifyUserPasswordRequest verifyUserPasswordRequest)
    {
        return await _inMemoryBus.SendQuery(verifyUserPasswordRequest);
    }

    [HttpPost(Name = "register")]
    public async Task<Result<CommandResult>> Register(RegisterUserRequest registerUserRequest)
    {
        return await _inMemoryBus.SendCommand(registerUserRequest);
    }
}
