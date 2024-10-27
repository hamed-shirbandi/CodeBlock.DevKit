using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using CodeBlock.DevKit.Web.Blazor.Server.CookieAuthentication;
using CodeBlock.DevKit.Web.Blazor.Server.Localization;
using CodeBlock.DevKit.Web.Blazor.Server.Optimization;
using CodeBlock.DevKit.Web.Blazor.Server.Services;
using CodeBlock.DevKit.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Blazor.Server.Configuration;

public static class BlazorConfiguration
{
    public static void AddBlazorPreConfigured(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddLocalization();

        builder.AddCodeBlockDevKitWeb(handlerAssemblyMarkerType, validatorAssemblyMarkerType, mappingProfileMarkerType);

        builder.AddCookieAuthentication();

        builder.Services.AddAuthorization();

        builder.Services.AddRazorFileProvider();

        builder.Services.AddRazorPages();

        builder.Services.AddServerSideBlazor();

        builder.AddWebOptimization();

        builder.Services.AddBlazoredToast();

        builder.Services.AddBlazoredModal();

        builder.Services.AddMessageService();
    }

    public static WebApplication UseBlazorPreConfigured(this WebApplication app)
    {
        app.UseLocalization();

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

    /// <summary>
    /// It shares all the razor views and components with consumer applications
    /// </summary>
    private static void AddRazorFileProvider(this IServiceCollection services)
    {
        string libraryPath = typeof(BlazorConfiguration).GetTypeInfo().Assembly.Location;

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
        });
    }

    private static void AddMessageService(this IServiceCollection services)
    {
        services.AddScoped<MessageService>();
    }
}
