// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Web.Api.CORS;

internal class CorsSettings
{
    public CorsSettings()
    {
        AllowAnyOrigin = true;
        AllowAnyMethod = true;
        AllowAnyHeader = true;
        AllowCredentials = true;
        AllowedOrigins = [];
        AllowedMethods = [];
        AllowedHeaders = [];
    }

    public bool AllowAnyOrigin { get; set; }
    public bool AllowAnyMethod { get; set; }
    public bool AllowAnyHeader { get; set; }
    public bool AllowCredentials { get; set; }
    public string[] AllowedOrigins { get; set; }
    public string[] AllowedMethods { get; set; }
    public string[] AllowedHeaders { get; set; }

    internal static CorsSettings CreateDefault()
    {
        return new CorsSettings();
    }
}
