using CodeBlock.DevKit.Web.Configuration.Metric;
using CodeBlock.DevKit.Web.Configuration.Serilog;
using CodeBlock.DevKit.Web.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CodeBlock.DevKit.Web.Configuration;

/// <summary>
///
/// </summary>
public static class RazorPagesConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddRazorPagesPreConfigured(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddCustomSerilog();

        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddMetrics(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseRazorPagesPreConfigured(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        app.UseSerilogRequestLogging();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseMetrics(configuration);

        app.UseAuthorization();
    }
}
