using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Web.Components.Configuration;

public static class ComponentConfiguration
{
    public static void AddUiComponents(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddSharedAppSettingsFile();

        services.AddRazorFileProvider();

        services.AddBlazoredToast();

        services.AddBlazoredModal();
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

    private static void AddSharedAppSettingsFile(this ConfigurationManager configuration)
    {
        string libraryPath = typeof(ComponentConfiguration).GetTypeInfo().Assembly.Location;
        var libraryFolder = Path.GetDirectoryName(libraryPath);

        configuration.AddJsonFile(Path.Combine(libraryFolder, "shared-appsettings.json"));

        configuration.AddJsonFile(Path.Combine(libraryFolder, "shared-appsettings.Development.json"));
    }
}
