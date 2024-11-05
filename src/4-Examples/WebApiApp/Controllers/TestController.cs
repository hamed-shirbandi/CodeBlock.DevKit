using CodeBlock.DevKit.Application.Bus;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers;

[Route("test")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : BaseApiController
{
    public TestController(IBus bus)
        : base(bus) { }

    /// <summary>
    /// This is just a test!
    /// </summary>
    [HttpGet]
    public async Task<Result> Get()
    {
        return Result.Success();
    }
}
