using System.Reflection;
using BizStream.Migrations.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BizStream.Migrations.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMigrationServices( this IServiceCollection services )
     => services.AddRepositories()
                .AddMigrators()
                .AddTransient<MigratorService>();

    private static IServiceCollection AddRepositories(this IServiceCollection services )
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where( t => !t.IsInterface && !t.IsAbstract && typeof( ISqlRepository ).IsAssignableFrom( t ) )
            .ToList()
            .ForEach( t => services.AddTransient( t ) );

        return services;
    }

    private static IServiceCollection AddMigrators(this IServiceCollection services)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where( t => !t.IsInterface && !t.IsAbstract && typeof( IMigrator ).IsAssignableFrom( t ) )
            .ToList()
            .ForEach( t => services.AddTransient( t ) );

        return services;
    }
}
