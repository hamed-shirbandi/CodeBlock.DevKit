using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Optimization;

public static class WebOptimizerConfiguration
{
    public static void AddWebOptimizer(this IServiceCollection services, IConfiguration configuration)
    {
        var optimizationOptions = configuration.GetSection("Optimization").Get<WebOptimizerOptions>();
        if (optimizationOptions == null)
            return;

        if (!optimizationOptions.Enabled)
            return;

        services.AddWebOptimizer(pipeline =>
        {
            foreach (var item in optimizationOptions.BundledJsFiles)
                pipeline.AddJavaScriptBundle(item.BundledFile, item.FilesToBundle);

            foreach (var item in optimizationOptions.BundledCssFiles)
                pipeline.AddJavaScriptBundle(item.BundledFile, item.FilesToBundle);
        });
    }
}
