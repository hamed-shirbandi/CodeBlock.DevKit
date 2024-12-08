// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Web.Observation.OpenTelemetry;
using CodeBlock.DevKit.Web.Observation.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Observation;

internal static class ObservationConfiguration
{
    internal static void AddObservation(this WebApplicationBuilder builder)
    {
        builder.AddCustomSerilog();

        builder.AddOpenTelemetry();

        builder.Services.AddHealthChecks();
    }

    internal static void UseObservation(this WebApplication app)
    {
        app.UseCustomSerilog();

        app.MapHealthChecks("/health");
    }
}
