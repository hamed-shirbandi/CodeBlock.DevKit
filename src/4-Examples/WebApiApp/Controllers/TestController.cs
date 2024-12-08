// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebApiApp.Controllers;

[Route("test")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : BaseApiController
{
    public TestController(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    /// <summary>
    /// This is just a test!
    /// </summary>
    [HttpGet]
    [Route("t1")]
    public async Task<Result> Get()
    {
        return Result.Success();
    }

    [HttpGet]
    [Route("t2")]
    [DisableRateLimiting]
    public async Task<Result> Get2()
    {
        return Result.Success();
    }
}
