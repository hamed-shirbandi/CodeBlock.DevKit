// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Web.Settings;

public class ApplicationSettings
{
    public ApplicationSettings()
    {
        Localizations = new Dictionary<string, LocalizedApplicationSettings>();
    }

    public LocalizedApplicationSettings Default { get; set; }
    public IDictionary<string, LocalizedApplicationSettings> Localizations { get; set; }

    public LocalizedApplicationSettings Localized => ResolveSettings();

    private LocalizedApplicationSettings ResolveSettings()
    {
        var currentCultureCode = Thread.CurrentThread.CurrentCulture.Name;

        if (Localizations.TryGetValue(currentCultureCode, out var localizedSettings))
        {
            return new LocalizedApplicationSettings
            {
                DisplayName = localizedSettings.DisplayName ?? Default.DisplayName,
                SafeName = localizedSettings.SafeName ?? Default.SafeName,
                SupportEmail = localizedSettings.SupportEmail ?? Default.SupportEmail,
                LogoUrl = localizedSettings.LogoUrl ?? Default.LogoUrl,
                FaviconUrl = localizedSettings.FaviconUrl ?? Default.FaviconUrl,
                PrivacyEffectiveDate = localizedSettings.PrivacyEffectiveDate ?? Default.PrivacyEffectiveDate,
                TermsEffectiveDate = localizedSettings.TermsEffectiveDate ?? Default.TermsEffectiveDate,
            };
        }

        return Default;
    }
}

public class LocalizedApplicationSettings
{
    public string DisplayName { get; init; }
    public string SafeName { get; set; }
    public string SupportEmail { get; set; }
    public string LogoUrl { get; set; }
    public string FaviconUrl { get; set; }
    public string PrivacyEffectiveDate { get; init; }
    public string TermsEffectiveDate { get; init; }
}
