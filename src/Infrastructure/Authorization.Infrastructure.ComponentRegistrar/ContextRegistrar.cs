using Authorization.Infrastructure.DataAccess.DbContexts;
using Authorization.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистратор компонентов.
/// </summary>
public static class ContextRegistrar
{
    /// <summary>
    /// Добавляет механизм подключения к БД в DI.
    /// </summary>
    /// <param name="serviceCollection">IoC.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>IoC.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator<AuthorizationDbContext>, AuthorizationDbContextConfiguration>();
        serviceCollection.AddDbContext<AuthorizationDbContext>((sp, options) =>
            sp.GetRequiredService<IDbContextOptionsConfigurator<AuthorizationDbContext>>()
                .Configure((DbContextOptionsBuilder<AuthorizationDbContext>)options)
        );
        serviceCollection.AddScoped<DbContext>(sp => sp.GetRequiredService<AuthorizationDbContext>());
        return serviceCollection;
    }
}