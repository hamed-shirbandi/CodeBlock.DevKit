using System.Net;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Core.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CodeBlock.DevKit.Web.Api.Exceptions;

public class HttpGlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<HttpGlobalExceptionHandler> _logger;
    private readonly IStringLocalizer<CoreResource> _localizer;

    public HttpGlobalExceptionHandler(ILogger<HttpGlobalExceptionHandler> logger, IStringLocalizer<CoreResource> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            await HandleExceptionAsync(context);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var result = Result.Failure(message: _localizer[CoreResource.Unknown_Exception_Error]);
        await httpContext.Response.WriteAsJsonAsync(result);
    }
}
