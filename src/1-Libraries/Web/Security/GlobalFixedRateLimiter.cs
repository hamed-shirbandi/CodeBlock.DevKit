// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Globalization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CodeBlock.DevKit.Web.Security;

public static class GlobalFixedRateLimiter
{
    public const string GLOBAL_FIXED_RATE_LIMITER_POLICY = "global-fixed-rate-limiter";

    internal static void AddGlobalFixedRateLimiter(this WebApplicationBuilder builder, RateLimiterSettings settings)
    {
        if (!settings.Enabled)
            return;

        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter(
                GLOBAL_FIXED_RATE_LIMITER_POLICY,
                policy =>
                {
                    policy.PermitLimit = settings.PermitLimit;
                    policy.Window = TimeSpan.FromSeconds(settings.WindowSeconds);
                    policy.QueueLimit = settings.QueueLimit;
                    policy.QueueProcessingOrder = settings.QueueProcessingOrder;
                }
            );

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, token) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                Log.Logger.Warning($"OnRejected: {GetUserEndPoint(context.HttpContext)}");

                await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
            };
        });
    }

    public static void UseGlobalFixedRateLimiter(this WebApplication app)
    {
        var securitySettings = app.Configuration.GetSection("Security").Get<SecuritySettings>();
        securitySettings ??= SecuritySettings.CreateDefault();
        if (!securitySettings.RateLimiterIsEnabled())
            return;

        app.UseRateLimiter();
    }

    public static void RequireGlobalFixedRateLimiting(this IEndpointConventionBuilder builder, IConfiguration configuration)
    {
        var securitySettings = configuration.GetSection("Security").Get<SecuritySettings>();
        securitySettings ??= SecuritySettings.CreateDefault();
        if (!securitySettings.RateLimiterIsEnabled())
            return;

        builder.RequireRateLimiting(GLOBAL_FIXED_RATE_LIMITER_POLICY);
    }

    private static string GetUserEndPoint(HttpContext context) =>
        $"User {context.User.Identity?.Name ?? "Anonymous"} endpoint:{context.Request.Path}" + $" {context.Connection.RemoteIpAddress}";
}
