using CodeBlock.DevKit.Infrastructure.Extensions;
using CodeBlock.DevKit.Web.CookieAuthentication;
using CodeBlock.DevKit.Web.Metric;
using CodeBlock.DevKit.Web.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Configuration;

public static class BlazorConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddBlazorPreConfigured(
        this WebApplicationBuilder builder,
        IConfiguration configuration,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCustomSerilog();

        builder.Services.AddCodeBlockDevKitInfrastructure(
            configuration,
            handlerAssemblyMarkerType,
            validatorAssemblyMarkerType,
            mappingProfileMarkerType
        );

        builder.Services.AddCookieAuthentication(configuration);

        builder.Services.AddRazorPages();

        builder.Services.AddServerSideBlazor();

        builder.Services.AddMetrics(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplication UseBlazorPreConfigured(this WebApplication app, IConfiguration configuration)
    {
        app.UseCustomSerilog(configuration);

        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMetrics(configuration);

        app.MapBlazorHub();

        app.MapFallbackToPage("/_Host");

        return app;
    }
}
