using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Authorization;
using CodeBlock.DevKit.Authorization.Infrastructure;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Authorization.UI.Configuration;
using CodeBlock.DevKit.Web.Blazor.Server.Configuration;

namespace BlazorServerApp;

public enum TestEnum
{
    [Display(Name = nameof(AuthorizationResource.User_Email), ResourceType = typeof(AuthorizationResource))]
    First,
}

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
