using Microsoft.AspNetCore.Builder;
using Serilog;

namespace CodeBlock.DevKit.Web.Configuration.Serilog;

public static class SerilogConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
        return builder;
    }
}
