using BlazorServerApp.Infrastructure;
using BlazorServerApp.UserCases.GetUsers;
using BlazorServerApp.UserCases.RegisterUser;
using CodeBlock.DevKit.Authorization.UI.Configuration;
using CodeBlock.DevKit.Web.Blazor.Server.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddBlazorPreConfigured(
            validatorAssemblyMarkerType: typeof(RegisterUserValidation),
            handlerAssemblyMarkerType: typeof(GetUsersUseCase),
            mappingProfileMarkerType: typeof(MappingProfile)
        );

        builder.Services.AddAuthorization();

        builder.AddAuthorizationUI();

        builder.Services.AddSingleton<Database>();

        var app = builder.Build();

        app.UseBlazorPreConfigured();

        app.Run();
    }
}
