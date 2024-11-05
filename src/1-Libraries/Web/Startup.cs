using CodeBlock.DevKit.Infrastructure;
using CodeBlock.DevKit.Web.Localization;
using CodeBlock.DevKit.Web.Metric;
using CodeBlock.DevKit.Web.Serilog;
using CodeBlock.DevKit.Web.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web;

public static class Startup
{
    public static void AddCodeBlockDevKitWeb(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCustomSerilog();

        builder.AddLocalization();

        builder.Services.AddCodeBlockDevKitInfrastructure(
            builder.Configuration,
            handlerAssemblyMarkerType,
            validatorAssemblyMarkerType,
            mappingProfileMarkerType
        );

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddWebServerOptions();

        builder.AddMetrics();
    }

    public static void UseCodeBlockDevKitWeb(this WebApplication app)
    {
        app.UseCustomSerilog();

        app.UseLocalization();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseMetrics();
    }

    private static IServiceCollection AddAuthenticatedUserService(this IServiceCollection services)
    {
        return services.AddScoped<AuthenticatedUserService>();
    }

    private static void AddWebServerOptions(this IServiceCollection services)
    {
        // If using Kestrel:
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
        // If using IIS:
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
    }
}
