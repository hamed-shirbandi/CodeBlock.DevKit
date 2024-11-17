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

    public string GetCurrentLanguageDirection()
    {
        var language = Languages.FirstOrDefault(l => l.Code == GetCurrentLanguageCode());

        return language?.Direction;
    }

    public string GetFont(string langeCode)
    {
        var language = Languages.FirstOrDefault(l => l.Code == langeCode);

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

    public string GetCurrentLanguageCode()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }

    public string GetCurrentLanguageFont()
    {
        var language = Languages.FirstOrDefault(l => l.Code == GetCurrentLanguageCode());

        return language?.Font;
    }

    public static LocalizationSettings CreateDefault()
    {
        return new LocalizationSettings { Languages = new List<SupportedLanguage> { SupportedLanguage.CreateDefault() } };
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
    private SupportedLanguage()
    {
        Direction = "ltr";
        Font = "RobotoMedium";
        Name = "English";
        Code = "en-US";
        IsDefault = true;
    }

    public static SupportedLanguage CreateDefault()
    {
        return new SupportedLanguage();
    }

    public string Name { get; set; }
    public string Code { get; set; }
    public string Direction { get; set; }
    public string Font { get; set; }
    public bool IsDefault { get; set; }
}
