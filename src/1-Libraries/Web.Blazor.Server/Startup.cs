// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using CodeBlock.DevKit.Web.Blazor.Server.Authentication;
using CodeBlock.DevKit.Web.Blazor.Server.Optimization;
using CodeBlock.DevKit.Web.Blazor.Server.Services;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Blazor.Server;

public static class Startup
{
    public static void AddBlazorPreConfigured(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCodeBlockDevKitWeb(handlerAssemblyMarkerType, validatorAssemblyMarkerType, mappingProfileMarkerType);

        builder.AddAuthentications();

        builder.Services.AddAuthorization();

        builder.Services.AddRazorFileProvider();

        builder.Services.AddRazorPages();

        builder.Services.AddServerSideBlazor();

        builder.AddWebOptimization();

        builder.Services.AddBlazoredToast();

        builder.Services.AddBlazoredModal();

        builder.Services.AddMessageService();
    }

    public static WebApplication UseBlazorPreConfigured(this WebApplication app)
    {
        app.UseCodeBlockDevKitWeb();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseWebOptimization();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseGlobalFixedRateLimiter();

        app.MapRazorPages().RequireGlobalFixedRateLimiting(app.Configuration);

        app.MapBlazorHub();

        app.MapFallbackToPage("/_Host");

        return app;
    }

    /// <summary>
    /// It shares all the razor views and components with consumer applications
    /// </summary>
    private static void AddRazorFileProvider(this IServiceCollection services)
    {
        string libraryPath = typeof(Startup).GetTypeInfo().Assembly.Location;

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
    }

    private static void AddMessageService(this IServiceCollection services)
    {
        services.AddScoped<MessageService>();
    }

    private static void AddAuthentications(this WebApplicationBuilder builder)
    {
        var authenticationBuilder = builder.Services.AddAuthentication();

        var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();
        if (authenticationSettings is null)
            return;

        builder.Services.AddSingleton(authenticationSettings);

        authenticationBuilder
            .AddCookie(authenticationSettings, builder.Services)
            .AddGoogle(authenticationSettings)
            .AddTwitter(authenticationSettings)
            .AddMicrosoft(authenticationSettings)
            .AddFacebook(authenticationSettings);
    }
}
