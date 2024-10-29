using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace CodeBlock.DevKit.Web.Api.Filters;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if Accept-Language header is present
        if (context.Request.Headers.TryGetValue("Accept-Language", out var language))
        {
            var culture = language.ToString().Split(',').FirstOrDefault();
            if (!string.IsNullOrEmpty(culture))
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }
        }

        await _next(context);
    }
}
