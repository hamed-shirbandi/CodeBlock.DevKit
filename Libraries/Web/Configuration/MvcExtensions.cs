using CodeBlock.DevKit.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CodeBlock.DevKit.Web.Configuration;

public static class MvcExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static WebApplication ConfigureMvcAppServices(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType
    )
    {
        builder.AddCustomSerilog();

        builder.Services.AddCodeBlockDevKitInfrastructure(validatorAssemblyMarkerType, handlerAssemblyMarkerType);

        builder.Services.AddControllersWithViews();

        return builder.Build();
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplication ConfigureMvcAppPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

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

        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}
