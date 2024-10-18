using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CodeBlock.DevKit.Web.Components.Configuration;

public static class ComponentConfiguration
{
    public static void AddUiComponents(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            var libraryPath = Path.GetFullPath(Path.Combine(environment.ContentRootPath));

            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
    }
}
