using CodeBlock.DevKit.Infrastructure.Extensions;
using CodeBlock.DevKit.Web.Captcha;
using CodeBlock.DevKit.Web.Metric;
using CodeBlock.DevKit.Web.Serilog;
using CodeBlock.DevKit.Web.Services.AuthenticatedUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Configuration;

public static class MvcConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddMvcPreConfigured(
        this WebApplicationBuilder builder,
        IConfiguration configuration,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType
    )
    {
        builder.AddCustomSerilog();

        builder.Services.AddCodeBlockDevKitInfrastructure(handlerAssemblyMarkerType, validatorAssemblyMarkerType, configuration);

        builder.Services.AddControllersWithViews();

        builder.Services.AddCaptcha();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddWebServerOptions();

        builder.Services.AddMetrics(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplication UseMvcPreConfigured(this WebApplication app, IConfiguration configuration)
    {
        app.UseCustomSerilog(configuration);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseMetrics(configuration);

        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }

    /// <summary>
    ///
    /// </summary>
    public static void AddWebServerOptions(this IServiceCollection services)
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
