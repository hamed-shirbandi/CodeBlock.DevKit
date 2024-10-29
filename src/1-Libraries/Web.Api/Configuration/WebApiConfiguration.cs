using CodeBlock.DevKit.Web.Api.Exceptions;
using CodeBlock.DevKit.Web.Api.Filters;
using CodeBlock.DevKit.Web.Api.JwtAuthentication;
using CodeBlock.DevKit.Web.Api.Swagger;
using CodeBlock.DevKit.Web.Configuration;
using CodeBlock.DevKit.Web.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeBlock.DevKit.Web.Api.Configuration;

public static class WebApiConfiguration
{
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

        builder.AddJwtAuthentication();

        builder.Services.AddCors();

        builder.Services.AddGlobalExceptionHandler();
    }

    public static void UseWebApiPreConfigured(this WebApplication app)
    {
        app.UseCustomSerilog();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseMiddleware<LocalizationMiddleware>();

        app.UseGlobalExceptionHandler();

        app.UseSwaggerPreConfigured(app.Configuration);

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }

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
}
