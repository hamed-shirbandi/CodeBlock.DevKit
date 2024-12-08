// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Web.Api.CORS;
using CodeBlock.DevKit.Web.Api.Exceptions;
using CodeBlock.DevKit.Web.Api.Filters;
using CodeBlock.DevKit.Web.Api.JwtAuthentication;
using CodeBlock.DevKit.Web.Api.Swagger;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Api;

public static class Startup
{
    public static void AddWebApiPreConfigured(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCodeBlockDevKitWeb(handlerAssemblyMarkerType, validatorAssemblyMarkerType, mappingProfileMarkerType);

        builder.Services.AddControllers().WithPreventAutoValidation();

        builder.Services.AddSwaggerPreConfigured(builder.Configuration);

        builder.AddJwtAuthentication();

        builder.AddConfiguredCors();

        builder.Services.AddGlobalExceptionHandler();
    }

    public static void UseWebApiPreConfigured(this WebApplication app)
    {
        app.UseCodeBlockDevKitWeb();

        app.UseMiddleware<LocalizationMiddleware>();

        app.UseGlobalExceptionHandler();

        app.UseSwaggerPreConfigured(app.Configuration);

        app.UseConfiguredCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseGlobalFixedRateLimiter();

        app.MapControllers().RequireGlobalFixedRateLimiting(app.Configuration);
    }

    private static void WithPreventAutoValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    private static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpGlobalExceptionHandler>();
    }

    private static void AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient<HttpGlobalExceptionHandler>();
    }
}
