using CodeBlock.DevKit.Application.Srvices;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlock.DevKit.Web.Api.Filters;

[ApiController]
[RequestLogging]
[ModelStateValidating]
public class BaseApiController : ControllerBase
{
    protected readonly IRequestDispatcher _requestDispatcher;

    public BaseApiController(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }
}
