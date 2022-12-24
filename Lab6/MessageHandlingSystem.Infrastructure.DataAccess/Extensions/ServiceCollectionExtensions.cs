using MessageHandlingSystem.Application.Abstractions.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MessageHandlingSystem.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configuration)
    {
        services.AddDbContext<DatabaseContext>(configuration);
        services.AddScoped<IDataBaseContext>(x => x.GetRequiredService<DatabaseContext>());

        return services;
    }
}