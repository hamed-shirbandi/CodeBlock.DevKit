// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Web.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Localization;

public static class LocalizationConfiguration
{
    public static void AddLocalization(this WebApplicationBuilder builder)
    {
        var localizationSettings = builder.Configuration.GetSection("Localization").Get<LocalizationSettings>();
        localizationSettings ??= LocalizationSettings.CreateDefault();

        builder.Services.AddSingleton(localizationSettings);

        builder.Services.AddLocalization();
    }

    public static void UseLocalization(this WebApplication app)
    {
        var localizationSettings = app.Services.GetService<LocalizationSettings>();

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(localizationSettings.GetDefaultLanguageCode())
            .AddSupportedCultures(localizationSettings.GetLanguageCodes())
            .AddSupportedUICultures(localizationSettings.GetLanguageCodes());

        localizationOptions.RequestCultureProviders.Clear();

        localizationOptions.RequestCultureProviders.Add(new CookieRequestCultureProvider { CookieName = localizationSettings.CookieName });

        app.UseRequestLocalization(localizationOptions);
    }
}
