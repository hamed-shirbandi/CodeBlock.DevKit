using System.Reflection;
using CodeBlock.DevKit.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Authorization.UI.Configuration;

public static class AuthorizationConfiguration
{
    public static void AddAuthorizationUiModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorFileProvider();

        builder.AddAdminRolePolicy();
    }

    /// <summary>
    /// It shares all the razor views and components with consumer applications
    /// </summary>
    private static void AddRazorFileProvider(this IServiceCollection services)
    {
        string libraryPath = typeof(AuthorizationConfiguration).GetTypeInfo().Assembly.Location;

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
    }

    private static void AddAdminRolePolicy(this WebApplicationBuilder builder)
    {
        var authorizationSettings = builder.Configuration.GetSection("Authorization").Get<AuthorizationSettings>();

        builder.Services.PostConfigure<AuthorizationOptions>(options =>
        {
            options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole(authorizationSettings.AdminRole));
        });
    }
}
