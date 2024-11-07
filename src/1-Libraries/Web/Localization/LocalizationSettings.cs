namespace CodeBlock.DevKit.Web.Localization;

public class LocalizationSettings
{
    public IEnumerable<SupportedLanguage> Languages { get; set; }

    public bool HasLanguage(string code)
    {
        return Languages.Any(l => l.Code == code);
    }

    public string GetLanguageName(string code)
    {
        var language = Languages.FirstOrDefault(l => l.Code == code);

        return language?.Name;
    }

    public string GetLanguageDirection(string code)
    {
        var language = Languages.FirstOrDefault(l => l.Code == code);

        return language?.Direction;
    }

    public string GetFont(string code)
    {
        var language = Languages.FirstOrDefault(l => l.Code == code);

        return language?.Font;
    }

    public string GetDefaultLanguageCode()
    {
        var defaultLanguage = GetDefaultLanguage();

        return defaultLanguage.Code;
    }

    public string[] GetLanguageCodes()
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
                    Name = "English",
                    Code = "en-US",
                    IsDefault = true,
                    Direction = "ltr",
                },
            },
        };
    }

    private SupportedLanguage GetDefaultLanguage()
    {
        var defaultLanguage = Languages.FirstOrDefault(l => l.IsDefault);
        var firstLanguage = Languages.FirstOrDefault();

        return defaultLanguage ?? firstLanguage;
    }
}

public class SupportedLanguage
{
    public SupportedLanguage()
    {
        Direction = "ltr";
        Font = "RobotoMedium";
    }

    public string Name { get; set; }
    public string Code { get; set; }
    public string Direction { get; set; }
    public string Font { get; set; }
    public bool IsDefault { get; set; }
}
