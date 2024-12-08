// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Licensing.Validator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace CodeBlock.DevKit.Infrastructure.Services;

public static class LicenseValidatorService
{
    public static void ValidateLicense(this IServiceCollection services, string environmentName)
    {
        var licensePath = Path.Combine(GetRootDirectory(), "codeblock.dev.license.lic");

        if (!File.Exists(licensePath))
        {
            throw new FileNotFoundException(
                $"License file not found at the expected path: {licensePath}. Ensure that the file exists in the root directory of your solution."
            );
        }

        var loggerFactory = LoggerFactory.Create(loggingBuilder => { });

        var logger = loggerFactory.CreateLogger("License");

        var licenseContent = File.ReadAllText(licensePath);

        var validationResult = LicenseValidator.ValidateLicense(licenseContent, environmentName);

        if (validationResult.IsValid)
        {
            services.AddSingleton(validationResult.License);

            logger.LogInformation(
                $"License validation successful. License Details: Email = {validationResult.License.Email}, Type = {validationResult.License.Type}"
            );

            return;
        }

        var LicenseErrorMessage = $"{string.Join(" - ", validationResult.Errors)}";

        if (environmentName == "Development")
        {
            logger.LogWarning($"{LicenseErrorMessage}");
            return;
        }

        throw new ApplicationException(LicenseErrorMessage);
    }

    public static void ValidateLicense(this IServiceCollection services, LicenseType requiredLicenseType, string moduleName)
    {
        var license = services.BuildServiceProvider().GetRequiredService<License>();

        if (license == null)
        {
            throw new ApplicationException($"License validation failed: No license has been configured!");
        }

        if (MeetsLicenseRequirements(license.Type, requiredLicenseType))
        {
            throw new ApplicationException(
                $"Invalid license detected. The '{moduleName}' module requires a '{requiredLicenseType}' license (or higher), but the current license is missing or does not meet the required type."
            );
        }
    }

    private static bool MeetsLicenseRequirements(LicenseType licenseType, LicenseType requiredLicenseType)
    {
        return licenseType >= requiredLicenseType;
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
