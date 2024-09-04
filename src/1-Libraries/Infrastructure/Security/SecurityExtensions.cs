using CodeBlock.DevKit.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Security;

public static class SecurityExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddEncryptionService(this IServiceCollection services)
    {
        services.AddScoped<IEncryptionService, EncryptionService>();
    }
}
