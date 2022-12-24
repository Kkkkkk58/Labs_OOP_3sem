using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MessageHandlingSystem.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        return collection.AddMediatR(typeof(IAssemblyMarker));
    }
}