using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CodeBlock.DevKit.Web.Configuration.Serilog;

public static class SerilogConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var serilogOptions = builder.Configuration.GetSection("Serilog");
        if (serilogOptions == null)
            return;

        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    }

    public static void UseCustomSerilog(this WebApplication app, IConfiguration configuration)
    {
        var serilogOptions = configuration.GetSection("Serilog");
        if (serilogOptions == null)
            return;

        app.UseSerilogRequestLogging();
    }
}
