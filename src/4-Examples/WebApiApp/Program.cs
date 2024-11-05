using CodeBlock.DevKit.Authorization;
using CodeBlock.DevKit.Authorization.Api;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Web.Api;

namespace WebApiApp;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddWebApiPreConfigured(handlerAssemblyMarkerType: typeof(Program));

        // builder.Services.AddAuthorizationModule(builder.Configuration);

        builder.AddAuthorizationApiModule();

        var app = builder.Build();

        app.UseWebApiPreConfigured();

        app.Services.InitialAuthorizationDb();

        app.Run();
    }
}
