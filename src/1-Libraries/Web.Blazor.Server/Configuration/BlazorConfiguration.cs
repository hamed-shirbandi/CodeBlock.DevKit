using CodeBlock.DevKit.Web.Blazor.Server.Optimization;
using CodeBlock.DevKit.Web.Configuration;
using CodeBlock.DevKit.Web.CookieAuthentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Blazor.Server.Configuration;

public static class BlazorConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddBlazorPreConfigured(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddComponents();

        builder.AddCodeBlockDevKitWeb(handlerAssemblyMarkerType, validatorAssemblyMarkerType, mappingProfileMarkerType);

        builder.Services.AddCookieAuthentication(builder.Configuration);

        builder.Services.AddRazorPages();

        builder.Services.AddServerSideBlazor();

        builder.Services.AddWebOptimization(builder.Configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static WebApplication UseBlazorPreConfigured(this WebApplication app)
    {
        app.UseCodeBlockDevKitWeb();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseWebOptimization();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapBlazorHub();

        app.MapFallbackToPage("/_Host");

        return app;
    }
}
