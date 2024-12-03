// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Blazor.Server.Optimization;

public static class WebOptimizationConfiguration
{
    public static void AddWebOptimization(this WebApplicationBuilder builder)
    {
        var optimizationOptions = builder.Configuration.GetSection("Optimization").Get<WebOptimizationOptions>();
        if (optimizationOptions == null)
            return;

        if (!optimizationOptions.Enabled)
            return;

        builder.Services.AddWebOptimizer(
            pipeline =>
            {
                foreach (var item in optimizationOptions.BundledJsFiles)
                    pipeline.AddJavaScriptBundle(item.BundledFile, item.FilesToBundle);

                foreach (var item in optimizationOptions.BundledCssFiles)
                    pipeline.AddCssBundle(item.BundledFile, item.FilesToBundle);
            },
            option =>
            {
                option.EnableCaching = optimizationOptions.EnableCaching;
                option.EnableDiskCache = optimizationOptions.EnableDiskCache;
                option.EnableMemoryCache = optimizationOptions.EnableMemoryCache;
                option.AllowEmptyBundle = optimizationOptions.AllowEmptyBundle;
            }
        );
    }

    public static void UseWebOptimization(this WebApplication app)
    {
        var optimizationOptions = app.Configuration.GetSection("Optimization").Get<WebOptimizationOptions>();
        if (optimizationOptions == null)
            return;

        if (!optimizationOptions.Enabled)
            return;

        app.UseWebOptimizer();
    }
}
