using CodeBlock.DevKit.Web.Api.Exceptions;
using CodeBlock.DevKit.Web.Api.Jwt;
using CodeBlock.DevKit.Web.Api.Swagger;
using CodeBlock.DevKit.Web.Configuration;
using CodeBlock.DevKit.Web.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Api.Configuration;

/// <summary>
///
/// </summary>
public static class WebApiConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddWebApiPreConfigured(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddCodeBlockDevKitWeb(handlerAssemblyMarkerType, validatorAssemblyMarkerType, mappingProfileMarkerType);

        builder.Services.AddControllers().WithPreventAutoValidation();

        builder.Services.AddSwaggerPreConfigured(builder.Configuration);

        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.Services.AddCors();

        builder.Services.AddWebServerOptions();

        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.Services.AddCors();

        builder.Services.AddGlobalExceptionHandler();
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseWebApiPreConfigured(this WebApplication app)
    {
        app.UseCustomSerilog();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseGlobalExceptionHandler();

        app.UseSwaggerPreConfigured(app.Configuration);

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseAuthentication();

        app.UseAuthorization();
    }

    /// <summary>
    /// Prevent auto validate on model binding
    /// </summary>
    private static void WithPreventAutoValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    private static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpGlobalExceptionHandler>();
    }

    private static void AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient<HttpGlobalExceptionHandler>();
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
