// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Globalization;
using CodeBlock.DevKit.Web.Localization;
using Microsoft.AspNetCore.Http;

namespace CodeBlock.DevKit.Web.Api.Filters;

internal class LocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LocalizationSettings _localizationSettings;

    public LocalizationMiddleware(RequestDelegate next, LocalizationSettings localizationSettings)
    {
        _next = next;
        _localizationSettings = localizationSettings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var language = GetAcceptLanguageHeader(context);

        var cultureInfo = new CultureInfo(language ?? _localizationSettings.GetDefaultLanguageCode());
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }

    private string GetAcceptLanguageHeader(HttpContext context)
    {
        string culture = null;

        if (context.Request.Headers.TryGetValue("Accept-Language", out var language))
            culture = language.ToString().Split(',').FirstOrDefault();

        return culture;
    }
}
