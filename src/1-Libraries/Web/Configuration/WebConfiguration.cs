using CodeBlock.DevKit.Infrastructure.Extensions;
using CodeBlock.DevKit.Web.Metric;
using CodeBlock.DevKit.Web.Serilog;
using CodeBlock.DevKit.Web.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Configuration;

/// <summary>
///
/// </summary>
public static class WebConfiguration
{
    public static void AddCodeBlockDevKitWeb(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCustomSerilog();

        builder.Services.AddCodeBlockDevKitInfrastructure(
            builder.Configuration,
            handlerAssemblyMarkerType,
            validatorAssemblyMarkerType,
            mappingProfileMarkerType
        );

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.AddMetrics();
    }

    public static void UseCodeBlockDevKitWeb(this WebApplication app)
    {
        app.UseCustomSerilog();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseMetrics();
    }

    private static IServiceCollection AddAuthenticatedUserService(this IServiceCollection services)
    {
        return services.AddScoped<AuthenticatedUserService>();
    }
}
