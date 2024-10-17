using Microsoft.AspNetCore.Components;

namespace CodeBlock.DevKit.Web.Helpers;

public static class HtmlParser
{
    /// <summary>
    /// Parse errors list to Fragment
    /// </summary>
    public static RenderFragment ParseToFragment(this List<string> errors)
    {
        RenderFragment content = null;

        if (!errors.Any())
            return content;

        foreach (var error in errors)
            content += AddMarkupContent($"<text><strong>-</strong> {error} <br/></text>");

        return content;
    }

    private static RenderFragment AddMarkupContent(string txt) =>
        builder =>
        {
            builder.AddMarkupContent(1, txt);
        };
}
