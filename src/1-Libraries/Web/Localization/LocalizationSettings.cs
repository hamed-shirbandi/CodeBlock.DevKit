namespace CodeBlock.DevKit.Web.Localization;

public class LocalizationSettings
{
    public IEnumerable<SupportedLanguage> Languages { get; set; }

    public bool HasMultipleLanguages()
    {
        return Languages.Count() > 1;
    }

    public bool HasLanguageCode(string code)
    {
        return Languages.Any(l => l.Code == code);
    }

    public string GetLanguageName(string code)
    {
        var language = Languages.FirstOrDefault(l => l.Code == code);

        return language?.Name;
    }

    public string GetFirstLanguageCode()
    {
        var language = Languages.FirstOrDefault();

        return language?.Code;
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
                new SupportedLanguage { Name = "English", Code = "en-US" },
            },
        };
    }
}

public class SupportedLanguage
{
    public string Name { get; set; }
    public string Code { get; set; }
}
