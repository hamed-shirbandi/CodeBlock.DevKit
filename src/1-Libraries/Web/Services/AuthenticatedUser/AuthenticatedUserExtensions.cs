using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Web.Services.AuthenticatedUser;

public static class AuthenticatedUserExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static IServiceCollection AddAuthenticatedUserService(this IServiceCollection services)
    {
        return services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
    }
}
