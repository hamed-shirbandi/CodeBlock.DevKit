// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace CodeBlock.DevKit.Web.Metric;

public static class MetricConfiguration
{
    public static void AddMetrics(this WebApplicationBuilder builder)
    {
        var metricOptions = builder.Configuration.GetSection("Metric").Get<MetricOptions>();
        if (metricOptions == null)
            return;

        //It starts the metrics exporter as a background service using a stand alone kestrel
        if (metricOptions.StandAloneKestrelServerEnabled)
        {
            builder.Services.AddMetricServer(options =>
            {
                options.Port = metricOptions.Port;
                options.Url = metricOptions.Url;
                options.Hostname = metricOptions.Hostname;
            });
        }

        //Inject IMetricFactory to be used in application objects instead of coupling their implementation with Metrics
        builder.Services.AddSingleton<IMetricFactory>(Metrics.DefaultFactory);
    }

    public static void UseMetrics(this WebApplication app)
    {
        var metricOptions = app.Configuration.GetSection("Metric").Get<MetricOptions>();
        if (metricOptions == null)
            return;

        //If kestrel server is not enabled then use current app server
        if (!metricOptions.StandAloneKestrelServerEnabled)
            app.UseMetricServer(metricOptions.Port, metricOptions.Url);

        if (metricOptions.HttpMetricsEnabled)
        {
            app.UseHttpMetrics(options =>
            {
                options.AddCustomLabel("host", context => context.Request.Host.Host);
            });
        }

        if (metricOptions.SuppressDefaultMetrics)
            Metrics.SuppressDefaultMetrics();
    }
}
