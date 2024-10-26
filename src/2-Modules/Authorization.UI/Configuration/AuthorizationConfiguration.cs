using System.Reflection;
using CodeBlock.DevKit.Authorization.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Authorization.UI.Configuration;

public static class AuthorizationConfiguration
{
    public static void AddAuthorizationUiModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorFileProvider();

        builder.Services.AddAuthenticationStateValidator();
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

    private static void AddAuthenticationStateValidator(this IServiceCollection services)
    {
        services.AddSingleton<AuthenticationStateService>();

        services.AddScoped<AuthenticationStateProvider, AuthenticationStateValidator>();
    }
}
