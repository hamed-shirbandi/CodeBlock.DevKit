using CodeBlock.DevKit.Application.Bus;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Web.Api.Filters;

[ApiController]
[RequestLogging]
[ModelStateValidating]
public class BaseApiController : ControllerBase
{
    protected readonly IBus _bus;

    public BaseApiController(IBus bus)
    {
        _bus = bus;
    }
}
