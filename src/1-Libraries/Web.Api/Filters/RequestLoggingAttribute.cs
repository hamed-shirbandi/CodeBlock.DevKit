// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace CodeBlock.DevKit.Web.Api.Filters;

public class RequestLoggingAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Log.Logger.Information($"Incoming model Headers for path ({GetPath(context)}) => {GetRequestHeaders(context)}");

        Log.Logger.Information($"Incoming model Body => {GetRequestBody(context)}");
    }

    private static string GetPath(AuthorizationFilterContext context)
    {
        return context.HttpContext.Request.Path;
    }

    private static string GetRequestHeaders(AuthorizationFilterContext context)
    {
        var headers = new Dictionary<string, string>();
        foreach (var (headerKey, headerValue) in context.HttpContext.Request.Headers)
            headers.Add(headerKey, headerValue);

        return JsonSerializer.Serialize(headers);
    }

    private static string GetRequestBody(AuthorizationFilterContext context)
    {
        var body = string.Empty;
        var request = context.HttpContext.Request;

        request.EnableBuffering();

        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            body = reader.ReadToEnd();
        }

        // Rewind, so the core is not lost when it looks at the body for the request
        request.Body.Position = 0;

        return body;
    }
}
