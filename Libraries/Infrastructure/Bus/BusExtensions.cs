using CodeBlock.DevKit.Application.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Bus;

public static class BusExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddInMemoryBus(this IServiceCollection services, Type handlerAssemblyMarkerType)
    {
        //Load all handlers from given assemblies
        services.AddMediatR(handlerAssemblyMarkerType);

        services.AddScoped<IInMemoryBus, InMemoryBus>();
    }
}
