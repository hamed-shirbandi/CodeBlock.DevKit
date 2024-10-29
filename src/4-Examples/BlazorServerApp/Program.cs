using CodeBlock.DevKit.Authorization;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Authorization.UI.Configuration;
using CodeBlock.DevKit.Web.Blazor.Server.Configuration;

namespace BlazorServerApp;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddBlazorPreConfigured(handlerAssemblyMarkerType: typeof(Program));

        builder.Services.AddAuthorizationModule(builder.Configuration);

        builder.AddAuthorizationUiModule();

        var app = builder.Build();

        app.UseBlazorPreConfigured();

        app.Services.InitialAuthorizationDb();

        app.Run();
    }
}
