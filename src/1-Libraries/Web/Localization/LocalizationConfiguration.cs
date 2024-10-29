using CodeBlock.DevKit.Web.Localization;
using Microsoft.AspNetCore.Builder;
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

        if (!localizationSettings.HasMultipleLanguages())
        {
            app.UseRequestLocalization(localizationSettings.GetFirstLanguageCode());
            return;
        }

        var localizationOptions = new RequestLocalizationOptions()
            .AddSupportedCultures(localizationSettings.GetLanguageCodes())
            .AddSupportedUICultures(localizationSettings.GetLanguageCodes());

        app.UseRequestLocalization(localizationOptions);
    }
}
