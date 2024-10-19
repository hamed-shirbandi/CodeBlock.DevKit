using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Web.Components.Configuration;

public static class ComponentConfiguration
{
    public static void AddUiComponents(this IServiceCollection services)
    {
        string libraryPath = typeof(ComponentConfiguration).GetTypeInfo().Assembly.Location;

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });

        services.AddBlazoredToast();

        services.AddBlazoredModal();
    }
}
