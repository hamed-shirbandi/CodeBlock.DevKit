using CodeBlock.DevKit.Web.Api.Configuration;

namespace WebApiApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddWebApiPreConfigured(handlerAssemblyMarkerType: typeof(Program));

        var app = builder.Build();

        app.UseWebApiPreConfigured();

        app.Run();
    }
}
