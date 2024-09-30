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
            if (optimizationOptions.ShouldBundleCssFiles())
                pipeline.AddCssBundle(optimizationOptions.BundledCssOutputFile, optimizationOptions.CssFilesToBundle);

            if (optimizationOptions.ShouldBundleJsFiles())
                pipeline.AddJavaScriptBundle(optimizationOptions.BundledJsOutputFile, optimizationOptions.JsFilesToBundle);
        });
    }
}
