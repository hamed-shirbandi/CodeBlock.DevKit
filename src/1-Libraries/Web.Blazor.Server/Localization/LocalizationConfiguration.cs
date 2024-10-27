using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Localization;

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
            .SetDefaultCulture(localizationSettings.GetDefaultLanguage().Code)
            .AddSupportedCultures(localizationSettings.GetLanguages())
            .AddSupportedUICultures(localizationSettings.GetLanguages());

        app.UseRequestLocalization(localizationOptions);
    }
}
