using CodeBlock.DevKit.Web.Configuration.Jwt;
using CodeBlock.DevKit.Web.Configuration.Metric;
using CodeBlock.DevKit.Web.Configuration.Serilog;
using CodeBlock.DevKit.Web.Configuration.Swagger;
using CodeBlock.DevKit.Web.Exceptions;
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
public static class WebApiConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddWebApiPreConfigured(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddCustomSerilog();

        builder.Services.AddControllers().WithPreventAutoValidation();

        builder.Services.AddSwaggerPreConfigured(options =>
        {
            configuration.GetSection("Swagger").Bind(options);
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddWebServerOptions();

        builder.Services.AddJwtAuthentication(configuration);

        builder.Services.AddCors();

        builder.Services.AddMetrics(configuration);

        builder.Services.AddGlobalExceptionHandler();
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseWebApiPreConfigured(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        app.UseSerilogRequestLogging();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseGlobalExceptionHandler();

        app.UseSwaggerPreConfigured();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseMetrics(configuration);

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

    /// <summary>
    ///
    /// </summary>
    private static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpGlobalExceptionHandler>();
    }

    /// <summary>
    ///
    /// </summary>
    private static void AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddTransient<HttpGlobalExceptionHandler>();
    }
}
