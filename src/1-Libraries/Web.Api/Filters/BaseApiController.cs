using CodeBlock.DevKit.Application.Bus;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Web.Api.Filters;

[ApiController]
[RequestLogging]
[ModelStateValidating]
public class BaseApiController : ControllerBase
{
    protected readonly IInMemoryBus _inMemoryBus;

    public BaseApiController(IInMemoryBus inMemoryBus)
    {
        _inMemoryBus = inMemoryBus;
    }
}
