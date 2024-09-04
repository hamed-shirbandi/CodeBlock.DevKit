using CodeBlock.DevKit.Infrastructure.Extensions;
using CodeBlock.DevKit.Web.Metric;
using CodeBlock.DevKit.Web.Serilog;
using CodeBlock.DevKit.Web.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Configuration;

/// <summary>
///
/// </summary>
public static class RazorPagesConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddRazorPagesPreConfigured(
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

        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddMetrics(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseRazorPagesPreConfigured(this WebApplication app, IWebHostEnvironment env, IConfiguration configuration)
    {
        app.UseCustomSerilog(configuration);

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseMetrics(configuration);

        app.UseAuthorization();
    }
}
