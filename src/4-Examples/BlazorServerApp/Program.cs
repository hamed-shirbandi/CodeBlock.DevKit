// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Authorization;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Authorization.UI;
using CodeBlock.DevKit.Web.Blazor.Server;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace BlazorServerApp;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("BlazorServerApp"))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("http://localhost:4317");
                    });
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("http://localhost:4317");
                    });
            });

        builder.Logging.AddOpenTelemetry(logging =>
            logging.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317");
            })
        );

        builder.AddBlazorPreConfigured(handlerAssemblyMarkerType: typeof(Program));

        builder.Services.AddAuthorizationModule(builder.Configuration);

        builder.AddAuthorizationUiModule();

        var app = builder.Build();

        app.UseBlazorPreConfigured();

        app.Services.InitialAuthorizationDb();

        app.Run();
    }
}
