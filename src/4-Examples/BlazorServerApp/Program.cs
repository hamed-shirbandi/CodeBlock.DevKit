using BlazorServerApp.Infrastructure;
using BlazorServerApp.UserCases.GetUsers;
using BlazorServerApp.UserCases.RegisterUser;
using CodeBlock.DevKit.Authorization;
using CodeBlock.DevKit.Authorization.UI.Configuration;
using CodeBlock.DevKit.Web.Blazor.Server.Configuration;

namespace BlazorServerApp;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddBlazorPreConfigured(
            validatorAssemblyMarkerType: typeof(RegisterUserValidation),
            handlerAssemblyMarkerType: typeof(GetUsersUseCase),
            mappingProfileMarkerType: typeof(MappingProfile)
        );

        builder.Services.AddAuthorizationModule(builder.Configuration);

        builder.AddAuthorizationUiModule();

        builder.Services.AddSingleton<Database>();

        var app = builder.Build();

        app.UseBlazorPreConfigured();

        app.Run();
    }
}
