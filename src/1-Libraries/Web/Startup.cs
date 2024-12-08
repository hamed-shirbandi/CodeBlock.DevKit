// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Infrastructure;
using CodeBlock.DevKit.Licensing.Validator;
using CodeBlock.DevKit.Web.Localization;
using CodeBlock.DevKit.Web.Observation;
using CodeBlock.DevKit.Web.Security;
using CodeBlock.DevKit.Web.Services;
using CodeBlock.DevKit.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Web;

public static class Startup
{
    public static void AddCodeBlockDevKitWeb(
        this WebApplicationBuilder builder,
        Type handlerAssemblyMarkerType,
        Type validatorAssemblyMarkerType = null,
        Type mappingProfileMarkerType = null
    )
    {
        builder.AddObservation();

        builder.ValidateLicense();

        builder.AddSecurity();

        builder.AddApplicationSettings();

        builder.AddLocalization();

        builder.Services.AddCodeBlockDevKitInfrastructure(
            builder.Configuration,
            handlerAssemblyMarkerType,
            validatorAssemblyMarkerType,
            mappingProfileMarkerType
        );

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthenticatedUserService();

        builder.Services.AddWebServerOptions();
    }

    public static void UseCodeBlockDevKitWeb(this WebApplication app)
    {
        app.UseObservation();

        app.UseLocalization();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
    }

    private static void AddApplicationSettings(this WebApplicationBuilder builder)
    {
        var applicationSettings = builder.Configuration.GetSection("Application").Get<ApplicationSettings>();
        builder.Services.AddSingleton(applicationSettings);
    }

    private static void AddAuthenticatedUserService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
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

    private static void ValidateLicense(this WebApplicationBuilder builder)
    {
        var licensePath = Path.Combine(GetRootDirectory(), "codeblock.dev.license.lic");

        if (!File.Exists(licensePath))
        {
            throw new FileNotFoundException(
                $"License file not found at the expected path: {licensePath}. Ensure that the file exists in the root directory of your solution."
            );
        }

        var loggerFactory = LoggerFactory.Create(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        var logger = loggerFactory.CreateLogger("License");

        var licenseContent = File.ReadAllText(licensePath);

        var validationResult = LicenseValidator.ValidateLicense(licenseContent, builder.Environment.EnvironmentName);

        if (validationResult.IsValid)
        {
            builder.Services.AddSingleton(validationResult.License);

            logger.LogInformation(
                $"License validation successful. License Details: Email = {validationResult.License.Email}, Type = {validationResult.License.Type}"
            );

            return;
        }

        var LicenseErrorMessage = $"{string.Join(" - ", validationResult.Errors)}";

        if (builder.Environment.IsDevelopment())
        {
            logger.LogWarning($"{LicenseErrorMessage}");
            return;
        }

        throw new ApplicationException(LicenseErrorMessage);
    }

    private static string GetRootDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        while (currentDirectory != null)
        {
            if (Directory.GetFiles(currentDirectory, "*.sln").Length > 0)
            {
                return currentDirectory;
            }

            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        throw new FileNotFoundException("Finding License file failed! Solution (.sln) file not found in any parent directory.");
    }
}
