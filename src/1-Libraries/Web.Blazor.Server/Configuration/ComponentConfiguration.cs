using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using CodeBlock.DevKit.Web.Blazor.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Web.Blazor.Server.Configuration;

public static class ComponentConfiguration
{
    public static void AddComponents(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorFileProvider();

        builder.Services.AddBlazoredToast();

        builder.Services.AddBlazoredModal();

        builder.Services.AddMessageService();
    }

    /// <summary>
    /// It shares all the razor views and components with consumer applications
    /// </summary>
    private static void AddRazorFileProvider(this IServiceCollection services)
    {
        string libraryPath = typeof(ComponentConfiguration).GetTypeInfo().Assembly.Location;

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
    }

    private static void AddMessageService(this IServiceCollection services)
    {
        services.AddScoped<MessageService>();
    }
}
