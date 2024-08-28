using CodeBlock.DevKit.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CodeBlock.DevKit.Web.Configuration;

internal static class BlazorExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static WebApplication ConfigureBlazorAppServices(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType
    )
    {
        builder.AddCustomSerilog();
        builder.Services.AddCodeBlockDevKitInfrastructure(validatorAssemblyMarkerType, handlerAssemblyMarkerType);
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        return builder.Build();
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplication ConfigureBlazorAppPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            // app.UseHsts();
        }

        //app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();

        return app;
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
        return builder;
    }
}
