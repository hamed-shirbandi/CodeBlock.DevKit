using BlazorServerApp.Infrastructure;
using BlazorServerApp.UserCases.GetUsers;
using BlazorServerApp.UserCases.RegisterUser;
using CodeBlock.DevKit.Web.Components.Configuration;
using CodeBlock.DevKit.Web.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddBlazorPreConfigured(
            builder.Configuration,
            validatorAssemblyMarkerType: typeof(RegisterUserValidation),
            handlerAssemblyMarkerType: typeof(GetUsersUseCase),
            mappingProfileMarkerType: typeof(MappingProfile)
        );

        builder.Services.AddUiComponents(builder.Environment);

        builder.Services.AddSingleton<Database>();

        var app = builder.Build();

        app.UseBlazorPreConfigured(builder.Configuration);

        app.Run();
    }
}
