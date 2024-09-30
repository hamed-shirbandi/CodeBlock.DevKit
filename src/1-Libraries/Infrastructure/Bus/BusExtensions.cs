using CodeBlock.DevKit.Application.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Bus;

public static class BusExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddInMemoryBus(this IServiceCollection services)
    {
        services.AddScoped<IInMemoryBus, InMemoryBus>();
    }
}
