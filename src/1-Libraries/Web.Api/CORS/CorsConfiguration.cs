// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Api.CORS;

internal static class CorsConfiguration
{
    private const string CONFIGURED_CORS_POLICY = "ConfiguredCorsPolicy";

    internal static void AddConfiguredCors(this WebApplicationBuilder builder)
    {
        var corsSettings = builder.Configuration.GetSection("Cors").Get<CorsSettings>();
        corsSettings ??= CorsSettings.CreateDefault();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                CONFIGURED_CORS_POLICY,
                builder =>
                {
                    if (corsSettings.AllowCredentials)
                        builder.DisallowCredentials();
                    else
                        builder.AllowCredentials();

                    if (corsSettings.AllowAnyOrigin)
                        builder.AllowAnyOrigin();
                    else
                        AddAllowedOrigins(builder, corsSettings);

                    if (corsSettings.AllowAnyMethod)
                        builder.AllowAnyMethod();
                    else
                        AddAllowedMethods(builder, corsSettings);

                    if (corsSettings.AllowAnyHeader)
                        builder.AllowAnyHeader();
                    else
                        AddAllowedHeaders(builder, corsSettings);
                }
            );
        });
    }

    internal static void UseConfiguredCors(this WebApplication app)
    {
        app.UseCors(CONFIGURED_CORS_POLICY);
    }

    private static void AddAllowedOrigins(CorsPolicyBuilder builder, CorsSettings corsSettings)
    {
        if (corsSettings.AllowedOrigins.Length == 0)
            return;

        builder.WithOrigins(corsSettings.AllowedOrigins);
    }

    private static void AddAllowedMethods(CorsPolicyBuilder builder, CorsSettings corsSettings)
    {
        if (corsSettings.AllowedMethods.Length == 0)
            return;

        builder.WithMethods(corsSettings.AllowedMethods);
    }

    private static void AddAllowedHeaders(CorsPolicyBuilder builder, CorsSettings corsSettings)
    {
        if (corsSettings.AllowedHeaders.Length == 0)
            return;

        builder.WithMethods(corsSettings.AllowedHeaders);
    }
}
