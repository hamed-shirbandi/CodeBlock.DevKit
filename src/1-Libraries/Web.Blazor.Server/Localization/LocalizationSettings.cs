namespace CodeBlock.DevKit.Web.Blazor.Server.Localization;

public class LocalizationSettings
{
    public IEnumerable<SupportedLanguage> Languages { get; set; }

    public SupportedLanguage GetDefaultLanguage()
    {
        var firstLanguage = Languages.FirstOrDefault();
        var defaultLanguage = Languages.FirstOrDefault(l => l.IsDefault);

        return defaultLanguage ?? firstLanguage;
    }

    public bool HasLanguageCode(string code)
    {
        return Languages.Any(l => l.Code == code);
    }

    public string GetLanguage(string code)
    {
        var language = Languages.FirstOrDefault(l => l.Code == code);

        if (language == null)
        {
            var defaultLanguage = GetDefaultLanguage();
            return defaultLanguage.Name;
        }

        return language.Name;
    }

    public string[] GetLanguages()
    {
        return Languages.Select(l => l.Code).ToArray();
    }

    public static LocalizationSettings CreateDefault()
    {
        return new LocalizationSettings
        {
            Languages = new List<SupportedLanguage>
            {
                new SupportedLanguage
                {
                    IsDefault = true,
                    Name = "English",
                    Code = "en",
                },
            },
        };
    }
}

public class SupportedLanguage
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsDefault { get; set; }
}
