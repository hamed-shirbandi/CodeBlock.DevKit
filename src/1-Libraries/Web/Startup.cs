// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure;
using CodeBlock.DevKit.Web.Localization;
using CodeBlock.DevKit.Web.Observation;
using CodeBlock.DevKit.Web.Security;
using CodeBlock.DevKit.Web.Services;
using CodeBlock.DevKit.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web;

public static class Startup
{
    public static void AddCodeBlockDevKitWeb(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddObservation();

        builder.AddSecurity();

        builder.AddApplicationSettings();

        builder.AddLocalization();

        builder.Services.AddCodeBlockDevKitInfrastructure(
            builder.Configuration,
            handlerAssemblyMarkerType,
            validatorAssemblyMarkerType,
            mappingProfileMarkerType
        );

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddWebServerOptions();
    }

    public static void UseCodeBlockDevKitWeb(this WebApplication app)
    {
        app.UseObservation();

        app.UseLocalization();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
    }

    public static void AddApplicationSettings(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection("Application").Get<ApplicationSettings>();
        builder.Services.AddSingleton(applicationSettings);
    }

    private static void AddAuthenticatedUserService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
    }

    private static void AddWebServerOptions(this IServiceCollection services)
    {
        // If using Kestrel:
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
        // If using IIS:
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
    }
}
